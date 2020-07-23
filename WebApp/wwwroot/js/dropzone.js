// Disable auto discover for all elements:
Dropzone.autoDiscover = false;

DropzoneInitiator = (element, options) => {

	$(element).dropzone({
		url: options.uploadUrl,
		method: "post",
		paramName: options.propertyName, // The name that will be used to transfer the file
		maxFilesize: options.maxFileSize, // MB
		uploadMultiple: false,
		autoQueue: true,
		maxFiles: options.maxFiles,
		addRemoveLinks: true,
		autoProcessQueue: true,
		acceptedMimeTypes: options.acceptedMimeTypes,
		init: options.init,
		maxfilesexceeded: function (file) {
			swal("Ups", options.dictMaxFilesExceeded, "warning");
			this.removeFile(file);
		},

		// Localization
		dictDefaultMessage: options.dictDefaultMessage,
		dictCancelUpload: options.dictCancelUpload,
		dictUploadCanceled: options.dictUploadCanceled,
		dictCancelUploadConfirmation: options.dictCancelUploadConfirmation,
		dictRemoveFile: options.dictRemoveFile,
		dictMaxFilesExceeded: options.dictMaxFilesExceeded,
		dictInvalidFileType: options.dictInvalidFileType,
		dictFileTooBig: options.dictFileTooBig
	});
}

DropzoneFileManagerInit = () => {
	$(".file-manager-dropzone").each(function () {

		const options = {};

		options.uploadUrl = $(this).data("uploadUrl");
		options.maxFileSize = $(this).data("maxFileSize");
		options.maxFiles = $(this).data("maxFiles");
		options.parentFormId = $(this).data("parentFormId");
		options.propertyName = $(this).data("propertyName");
		options.acceptedMimeTypes = $(this).data("acceptedMimeTypes");

		// Localization
		options.dictDefaultMessage = $(this).data("dict-default-message");
		options.dictCancelUpload = $(this).data("dict-cancel-upload");
		options.dictUploadCanceled = $(this).data("dict-upload-canceled");
		options.dictCancelUploadConfirmation = $(this).data("dict-cancel-upload-confirmation");
		options.dictRemoveFile = $(this).data("dict-remove-file");
		options.dictMaxFilesExceeded = $(this).data("dict-max-files-exceeded");
		options.dictInvalidFileType = $(this).data("dict-invalid-file-type");
		options.dictFileTooBig = $(this).data("dict-file-too-big");

		options.init = function () {
			let self = this;
			let existingFiles = $(".existing-file")
			// Add hidden inputs within the form
			addHiddenInputs(options.parentFormId, options.propertyName, existingFiles)

			existingFiles.each(function (index, element) {
				// Get file id
				let fileId = $(element).attr("id")

				// Add event listener in remove button
				let deleteBtn = $(element).find(".remove-file-btn").first()
				if (deleteBtn) {
					$(deleteBtn).on("click", function () {
						swal({
							text: "Esta seguro que desea eliminar el archivo?",
							type: "warning",
							showCancelButton: true,
							confirmButtonColor: "#DD6B55",
							confirmButtonText: "Confirmar",
							cancelButtonText: "Cancelar"
						}).then(function (selection) {
							if (selection.value) {
								$.ajax({
									dataType: 'json',
									type: "delete",
									url: `/File/Delete/${fileId}`,
									success: function (result) {
										swal("Archivo eliminado con éxito", result.message, "success").then(function () {

											$(`#${fileId}`).remove()
											$(`#${options.parentFormId} input#${fileId}`).remove()
											orderFilesForBinding(options.propertyName)
										});
									},
									error: function (error) {
										if (error != undefined)
											swal("Error", error.responseJSON.message);
									}
								});
							}
						})
					})
				}
			})

			this.on("success", function (file, res) {
				file.referenceId = res.id;
				refreshFiles(options.parentFormId, options.propertyName, options.maxFiles, this.files);
			});

			this.on("error", function (file, res) {
				$(file.previewElement).find('.dz-error-message').text(res.message);
			});

			this.on("addedfiles", function (files) {
				let filesAlreadyAdded = $("input[type='hidden'].file").length
				for (file of files) {
					if (filesAlreadyAdded + 1 > options.maxFiles) {
						swal("Ups", dictMaxFilesExceeded, "warning")
						self.removeFile(file)
						return
					}
					filesAlreadyAdded++
				}
			})

			this.on("removedfile", function (file, res) {
				refreshFiles(options.parentFormId, options.propertyName, options.maxFiles, this.files);
			});
		};

		DropzoneInitiator(this, options);
	});
}

DropzoneExcelImportInit = () => {
	$(".excel-import-dropzone").each(function () {

		const options = {};

		options.uploadUrl = $(this).data("uploadUrl");
		options.maxFileSize = $(this).data("maxFileSize");
		options.maxFiles = 1;
		options.propertyName = 'excel-file';
		options.acceptedMimeTypes = $(this).data("acceptedMimeTypes");

		// Localization
		options.dictDefaultMessage = $(this).data("dict-default-message");
		options.dictCancelUpload = $(this).data("dict-cancel-upload");
		options.dictUploadCanceled = $(this).data("dict-upload-canceled");
		options.dictCancelUploadConfirmation = $(this).data("dict-cancel-upload-confirmation");
		options.dictRemoveFile = $(this).data("dict-remove-file");
		options.dictMaxFilesExceeded = $(this).data("dict-max-files-exceeded");
		options.dictInvalidFileType = $(this).data("dict-invalid-file-type");
		options.dictFileTooBig = $(this).data("dict-file-too-big");
		options.dictImportSucceededTitle = $(this).data("dict-import-succeeded-title");
		options.dictImportSucceededMessage = $(this).data("dict-import-succeeded-message");
		options.dictImportFailedTitle = $(this).data("dict-import-failed-title");


		options.init = function () {
			this.on("success", function (file, res) {
				swal(options.dictImportSucceededTitle, options.dictImportSucceededMessage, "success")
					.then(function () {
						// Refresh to see the results.
						window.location.reload();
					});
				this.removeFile(file);
			});

			this.on("error", function (file, res) {
				swal(options.dictImportFailedTitle, res.message || res || '', "error");
				this.removeFile(file);
			});
		};

		DropzoneInitiator(this, options);
	});
}

function refreshFiles(parentId, propertyName, maxFiles, files) {

	// Remove files.
	$(".file-manager-file").remove();

	// Add files.
	if (maxFiles == 1) {
		// replace the existing hidden field with the new one
		$(".file-manager-existing-file").remove();
		const file = files[0];
		$(`#${parentId}`).append(`<input type='hidden' class="file-manager-file file" id='${file.referenceId}' name='${propertyName}' value='${file.referenceId}' />`);
	}
	else {
		for (const key in files) {
			const file = files[key];
			$(`#${parentId}`).append(`<input type='hidden' class="file-manager-file file" id='${file.referenceId}' name='${propertyName}[${key}]' value='${file.referenceId}' />`);
		}
		// Update file indexes for data binding
		orderFilesForBinding(propertyName)
	}

}

// This function will be called when our entity has existing files already asociated.
function addHiddenInputs(parentId, propertyName, existingFiles) {
	if (existingFiles.length == 1) {
		let fileId = $(existingFiles).first().attr("id")
		$(`#${parentId}`).append(`<input class='file-manager-existing-file file' type='hidden' id='${fileId}' name='${propertyName}' value='${fileId}' />`);
	}
	else {
		$(existingFiles).each(function (index, file) {
			let fileId = $(file).attr("id")
			$(`#${parentId}`).append(`<input class='file-manager-existing-file file' type='hidden' id='${fileId}' name='${propertyName}[${index}]' value='${fileId}' />`);
		})
	}
}

// This function will be called every time an existing file is deleted or after add/remove a file from the dropzone
// In this way, we keep the file names ordered from 0 to N to be able to bind the data properly.
function orderFilesForBinding(propertyName) {
	let files = $("input[type='hidden'].file")
	files.each(function (index, file) {
		$(file).attr("name", `${propertyName}[${index}]`)
	})
}