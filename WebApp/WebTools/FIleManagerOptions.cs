using Binit.Framework;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.WebTools.FileManagerOptions;

namespace WebApp.WebTools
{
    public class FileManagerOptions : ViewDataDictionary
    {
        private bool uploadEnabled;
        public bool UploadEnabled
        {
            get
            {
                return uploadEnabled;
            }
            set
            {
                uploadEnabled = value;
                this.Remove("UploadEnabled");
                this.Add("UploadEnabled", uploadEnabled);
            }
        }

        private bool showDownloadButton;
        public bool ShowDownloadButton
        {
            get
            {
                return showDownloadButton;
            }
            set
            {
                showDownloadButton = value;
                this.Remove("ShowDownloadButton");
                this.Add("ShowDownloadButton", showDownloadButton);
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

        private int maxFiles;
        public int MaxFiles
        {
            get
            {
                return maxFiles;
            }
            set
            {
                maxFiles = value;
                this.Remove("MaxFiles");
                this.Add("MaxFiles", maxFiles);
            }
        }

        private string parentFormId;
        public string ParentFormId
        {
            get
            {
                return parentFormId;
            }
            set
            {
                parentFormId = value;
                this.Remove("ParentFormId");
                this.Add("ParentFormId", parentFormId);
            }
        }

        private string propertyName;
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
                this.Remove("PropertyName");
                this.Add("PropertyName", propertyName);
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

        private bool allowRemoveFiles;
        public bool AllowRemoveFiles
        {
            get
            {
                return allowRemoveFiles;
            }
            set
            {
                allowRemoveFiles = value;
                this.Remove("AllowRemoveFiles");
                this.Add("AllowRemoveFiles", allowRemoveFiles);
            }
        }

        public FileManagerOptions(ViewDataDictionary source, IStringLocalizer<SharedResources> localizer) : base(source)
        {
            uploadEnabled = source["UploadEnabled"] != null ? (bool)source["UploadEnabled"] : false;
            showDownloadButton = source["ShowDownloadButton"] != null ? (bool)source["ShowDownloadButton"] : false;
            maxFileSize = source["MaxFileSize"] != null ? (int)source["MaxFileSize"] : 1;
            maxFiles = source["MaxFiles"] != null ? (int)source["MaxFiles"] : 0;
            parentFormId = source["ParentFormId"] != null ? source["ParentFormId"].ToString() : "";
            propertyName = source["PropertyName"] != null ? source["PropertyName"].ToString() : "file";
            acceptedMimeTypes = source["AcceptedMimeTypes"] != null ? source["AcceptedMimeTypes"].ToString() : "";
            allowRemoveFiles = source["AllowRemoveFiles"] != null ? (bool)source["AllowRemoveFiles"] : true;

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
        }

        public FileManagerOptions(IModelMetadataProvider metadataProvider, ModelStateDictionary modelState) : base(metadataProvider, modelState)
        {
        }

        protected FileManagerOptions(IModelMetadataProvider metadataProvider, Type declaredModelType) : base(metadataProvider, declaredModelType)
        {
        }

        protected FileManagerOptions(ViewDataDictionary source, Type declaredModelType) : base(source, declaredModelType)
        {
        }

        protected FileManagerOptions(IModelMetadataProvider metadataProvider, ModelStateDictionary modelState, Type declaredModelType) : base(metadataProvider, modelState, declaredModelType)
        {
        }

        protected FileManagerOptions(ViewDataDictionary source, object model, Type declaredModelType) : base(source, model, declaredModelType)
        {
        }
    }

}