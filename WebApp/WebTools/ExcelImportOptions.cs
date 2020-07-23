using Binit.Framework;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.WebTools.ExcelImportOptions;

namespace WebApp.WebTools
{
    public class ExcelImportOptions : ViewDataDictionary
    {
        private string actionUrl;
        public string ActionUrl
        {
            get
            {
                return actionUrl;
            }
            set
            {
                actionUrl = value;
                this.Remove("ActionUrl");
                this.Add("ActionUrl", actionUrl);
            }
        }

        private int maxFileSize;
        public int MaxFileSize
        {
            get
            {
                return maxFileSize;
            }
            set
            {
                maxFileSize = value;
                this.Remove("MaxFileSize");
                this.Add("MaxFileSize", maxFileSize);
            }
        }

        private Dictionary<string, string> messages;
        public Dictionary<string, string> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                this.Remove("Messages");
                this.Add("Messages", messages);
            }
        }

        private string acceptedMimeTypes;
        public string AcceptedMimeTypes
        {
            get
            {
                return acceptedMimeTypes;
            }
            set
            {
                acceptedMimeTypes = value;
                this.Remove("AcceptedMimeTypes");
                this.Add("AcceptedMimeTypes", acceptedMimeTypes);
            }
        }


        public ExcelImportOptions(ViewDataDictionary source, IStringLocalizer<SharedResources> localizer) : base(source)
        {
            actionUrl = source["ActionUrl"] != null ? source["ActionUrl"].ToString() : "";
            maxFileSize = source["MaxFileSize"] != null ? (int)source["MaxFileSize"] : 1;
            acceptedMimeTypes = source["AcceptedMimeTypes"] != null ? source["AcceptedMimeTypes"].ToString() : "application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Dropzone Localization
            messages = new Dictionary<string, string>();
            messages.Add("DictDefaultMessage", source["DictDefaultMessage"] != null ? localizer[source["DictDefaultMessage"].ToString()] : localizer[Lang.DictDefaultMessage]);
            messages.Add("DictCancelUpload", source["DictCancelUpload"] != null ? localizer[source["DictCancelUpload"].ToString()] : localizer[Lang.DictCancelUpload]);
            messages.Add("DictUploadCanceled", source["DictUploadCanceled"] != null ? localizer[source["DictUploadCanceled"].ToString()] : localizer[Lang.DictUploadCanceled]);
            messages.Add("DictCancelUploadConfirmation", source["DictCancelUploadConfirmation"] != null ? localizer[source["DictCancelUploadConfirmation"].ToString()] : localizer[Lang.DictCancelUploadConfirmation]);
            messages.Add("DictRemoveFile", source["DictRemoveFile"] != null ? localizer[source["DictRemoveFile"].ToString()] : localizer[Lang.DictRemoveFile]);
            messages.Add("DictMaxFilesExceeded", source["DictMaxFilesExceeded"] != null ? localizer[source["DictMaxFilesExceeded"].ToString()] : localizer[Lang.DictMaxFilesExceeded]);
            messages.Add("DictInvalidFileType", source["DictInvalidFileType"] != null ? localizer[source["DictInvalidFileType"].ToString()] : localizer[Lang.DictInvalidFileType]);
            messages.Add("DictFileTooBig", source["DictFileTooBig"] != null ? localizer[source["DictFileTooBig"].ToString()] : localizer[Lang.DictFileTooBig]);
            messages.Add("DictImportSucceededTitle", source["DictImportSucceededTitle"] != null ? localizer[source["DictImportSucceededTitle"].ToString()] : localizer[Lang.DictImportSucceededTitle]);
            messages.Add("DictImportSucceededMessage", source["DictImportSucceededMessage"] != null ? localizer[source["DictImportSucceededMessage"].ToString()] : localizer[Lang.DictImportSucceededMessage]);
            messages.Add("DictImportFailedTitle", source["DictImportFailedTitle"] != null ? localizer[source["DictImportFailedTitle"].ToString()] : localizer[Lang.DictImportFailedTitle]);
        }

        public ExcelImportOptions(IModelMetadataProvider metadataProvider, ModelStateDictionary modelState) : base(metadataProvider, modelState)
        {
        }

        protected ExcelImportOptions(IModelMetadataProvider metadataProvider, Type declaredModelType) : base(metadataProvider, declaredModelType)
        {
        }

        protected ExcelImportOptions(ViewDataDictionary source, Type declaredModelType) : base(source, declaredModelType)
        {
        }

        protected ExcelImportOptions(IModelMetadataProvider metadataProvider, ModelStateDictionary modelState, Type declaredModelType) : base(metadataProvider, modelState, declaredModelType)
        {
        }

        protected ExcelImportOptions(ViewDataDictionary source, object model, Type declaredModelType) : base(source, model, declaredModelType)
        {
        }
    }

}