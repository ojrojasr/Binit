using System.IO;

namespace Binit.Framework.Helpers.Excel
{
    /// <summary>
    /// Class that contains the results of the excel export.
    /// </summary>
    public class ExportResult
    {
        #region Properties

        public readonly string ExportMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public MemoryStream Stream { get; set; }
        public string Filename { get; set; }

        #endregion

        #region Constructor

        public ExportResult(MemoryStream stream, string filename)
        {
            this.Stream = stream;
            this.Filename = filename;
        }

        #endregion
    }
}