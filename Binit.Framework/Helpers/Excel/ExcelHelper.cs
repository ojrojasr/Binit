
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers.Configuration;
using Binit.Framework.Interfaces.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.Helpers.ExcelHelper;

namespace Binit.Framework.Helpers.Excel
{
    public class ExcelHelper<TEntity> where TEntity : class, IEntity, new()
    {
        #region Private Properties

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly LocaleConfiguration locale;

        /// <summary>
        /// List of TEntity properties to consider.
        /// </summary>
        private readonly PropertyInfo[] entityProperties;

        #endregion

        #region Constructor

        /// <summary>
        /// ExcelHelper constructor. Can be injected.
        /// The string localizer is required to obtain localized error messages.
        /// </summary>
        public ExcelHelper(IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            this.localizer = localizer;
            this.locale = new LocaleConfiguration(configuration);
            this.entityProperties = typeof(TEntity).GetProperties();
        }

        #endregion


        #region Import methods

        /// <summary>
        /// Import a file using the default mapper.
        /// </summary>
        public async Task<List<TEntity>> Import(IFormFile formFile)
        {
            // Map excel values to entity.
            Func<List<string>, List<string>, TEntity> mapper = (headers, cells) =>
            {
                TEntity entity = new TEntity();

                for (int i = 0; i < headers.Count; i++)
                {
                    PropertyInfo prop = entity.GetType().GetProperty(headers[i], BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.CanWrite)
                    {
                        try
                        {
                            prop.SetValue(entity, Convert.ChangeType(cells[i], prop.PropertyType), null);
                        }
                        catch
                        {
                            prop.SetValue(entity, this.Parse(prop.PropertyType, cells[i]));
                        }
                    }
                }

                return entity;
            };

            return await Import(formFile, mapper);
        }

        /// <summary>
        /// Import a file using a custom mapper.
        /// </summary>
        public async Task<List<TEntity>> Import(IFormFile formFile, Func<List<string>, List<string>, TEntity> mapper)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                throw new NotFoundException(this.localizer[Lang.ImportFileNotProvidedEx]);
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidationException(this.localizer[Lang.ImportWrongFileExtensionEx]);
            }

            var entityList = new List<TEntity>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    var headers = new List<string>();
                    string header = string.Empty;

                    for (int cellIndex = 1; cellIndex <= worksheet.Dimension.Columns; cellIndex++)
                    {
                        header = worksheet.Cells[1, cellIndex].Value.ToString();

                        headers.Add(header.ToString());
                    }

                    List<string> cells = new List<string>();

                    // Starts in row 2 to avoid headers.
                    for (int row = 2; row <= rowCount; row++)
                    {
                        for (int cell = 1; cell <= headers.Count; cell++)
                        {
                            cells.Add(worksheet.Cells[row, cell].Value.ToString());
                        }

                        entityList.Add(mapper(headers, cells));

                        cells.Clear();
                    }
                }
            }

            return entityList;
        }

        #endregion

        #region Export methods

        /// <summary>
        /// Export a query using the default mapper.
        /// </summary>
        public async Task<ExportResult> Export(IQueryable<TEntity> query, string filename = null)
        {
            // Function that parses entities to List<string>.
            Func<TEntity, List<string>> mapper = (entity) =>
            {
                List<string> propertiesValues = new List<string>();

                foreach (var property in this.entityProperties)
                {
                    propertiesValues.Add(this.Parse(property, entity));
                }

                return propertiesValues;
            };

            // Add a header for each entity property.
            var propertiesNames = this.entityProperties.Select(p => p.Name);

            var headers = new List<string>();
            foreach (var propertyName in propertiesNames)
                headers.Add(propertyName);

            // Export.
            return await this.Export(query, mapper, headers, filename);
        }

        /// <summary>
        /// Export a query using a custom mapper.
        /// </summary>
        public async Task<ExportResult> Export(IQueryable<TEntity> query, Func<TEntity, List<string>> mapper, List<string> headers, string filename = null)
        {
            var dataTable = new DataTable();

            foreach (var header in headers)
                dataTable.Columns.Add(header);

            // Execute the query and use the mapper to parse every entity to List<string>
            var listToExport = (await query.ToListAsync())
                    .Select(e => mapper(e));

            // Parse to DataTable because of this issue: https://github.com/JanKallman/EPPlus/issues/8
            // Tracking code: epplus-issue1
            foreach (var itemToExport in listToExport)
            {
                if (itemToExport.Count != dataTable.Columns.Count)
                    throw new ValidationException(localizer[Lang.ExportHeadersDontMatchCells]);

                var dataRow = dataTable.NewRow();

                for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    dataRow[columnIndex] = itemToExport[columnIndex];
                }

                dataTable.Rows.Add(dataRow);
            }

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromDataTable(dataTable, true);
                package.Save();
            }
            stream.Position = 0;

            return new ExportResult(stream, this.BuildFilename(filename));
        }


        #endregion

        #region Private methods
        /// <summary>
        /// Parses common complex types like Guid and DateTime from string.
        /// </summary>
        private object Parse(Type type, string value)
        {
            // If type is nullable and string is null or empty, return null.
            if (string.IsNullOrEmpty(value) && Nullable.GetUnderlyingType(type) != null)
                return null;

            // Parse to Guid.
            if (!string.IsNullOrEmpty(value) && (type == typeof(Guid) || type == typeof(Guid?)))
                return new Guid(value);

            // Parse to DateTime.
            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                var date = new DateTime();

                if (DateTime.TryParseExact(value, locale.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    return date;
                else
                    throw new ValidationException(string.Format(localizer[Lang.ImportInvalidDateFormat], locale.DateTimeFormat));
            }

            throw new ValidationException(string.Format(localizer[Lang.ImportCouldntCastValue], value, type.FullName));
        }

        /// <summary>
        /// Parses common complex types like Guid, DateTime & Lists to string.
        /// Note: For Lists it returns the amount of items.
        /// </summary>
        private string Parse(PropertyInfo property, object entity)
        {
            if (property.GetValue(entity) == null)
            {
                return string.Empty;
            }
            else if (property.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
            {
                return property.PropertyType.GetProperty("Count").GetValue(property.GetValue(entity)).ToString();
            }
            else if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
            {
                return (property.GetValue(entity) as DateTime?).Value.ToString(this.locale.DateTimeFormat);
            }
            else
            {
                return property.GetValue(entity).ToString();
            }
        }

        /// <summary>
        /// Generates a proper filename for the exported excel.
        /// </summary>
        private string BuildFilename(string filename = null)
        {
            if (string.IsNullOrEmpty(filename))
                return $"{Guid.NewGuid().ToString()}.xlsx";

            if (!Path.HasExtension(filename))
                return $"{filename}.xlsx";

            return filename;
        }

        #endregion
    }
}