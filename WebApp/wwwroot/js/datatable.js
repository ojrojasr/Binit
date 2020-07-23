// Setup event handlers for action buttons
setupEventHandlers = (row, table) => {
	let datatable = table.DataTable();
	row.find(".datatable-action-button").each(function (index, button) {
		$(button).on("click", function () {
			let actionData = $(this).data("action")

			// Redirect buttons
			if (actionData.typeOfButton == "redirect") {
				window.open(actionData.url, actionData.target)
			}

			// Decision buttons
			if (actionData.typeOfButton == "decision") {
				swal({
					text: actionData.modal.message,
					type: actionData.modal.typeOfAlert,
					showCancelButton: true,
					confirmButtonColor: "#DD6B55",
					confirmButtonText: actionData.modal.confirmLabel,
					cancelButtonText: actionData.modal.cancelLabel
				}).then(function (selection) {
					if (selection.value) {
						$.ajax({
							dataType: 'json',
							type: actionData.type,
							url: actionData.url,
							success: function (result) {
								swal(actionData.successTitle, result.message, "success").then(function () {
									datatable.ajax.reload();
								});
							},
							error: function (error) {
								swal("Error", error.responseJSON.message);
							}
						});
					}
				})

			}

			// Action buttons
			if (actionData.typeOfButton == "action") {
				// Disable button to prevent multiple clicks
				let button = $(this)
				button.attr("disabled", true)
				$.ajax({
					dataType: 'json',
					type: actionData.type,
					url: actionData.url,
					success: function (result) {
						swal(actionData.successTitle, result.message, "success").then(function () {
							datatable.ajax.reload();
						});
					},
					error: function (error) {
						swal("Error", error.responseJSON.message);
					},
					complete: function () {
						button.attr("disabled", false)
					}
				});
			}
		})
	})
}

createButtons = (actions, container) => {
	actions.forEach(function (action) {
		let button = `
			<button type="button" class="btn waves-effect waves-light ${action.class} ml-2 btn-sm ${action.internalName}-action datatable-action-button">
				<i class="fa fa-${action.icon}"></i>
				${action.displayName}
			</button>
			`
		container.append(button)

		// Set data for each action button
		container.find(`.${action.internalName}-action`).first().data({
			"action": action
		})
	})
}

dataTableInitiator = (table) => {

	let columnsToDisplay = []
	let columnsToExport = []

	let getUrl = table.data("get-url")

	table.find("th[data-column-name]").each(function (index, column) {
		columnsToDisplay.push({
			data: $(column).data("columnName")
		})
		columnsToExport.push(index)
	})

	let hideActionButtons = table.data("hide-action-buttons") != undefined;
	if (!hideActionButtons) {
		// Additional column for actions
		columnsToDisplay.push({
			render: function () {
				let html = `
				<div class="d-flex datatable-row-actions">
				</div>
				`
				return html;
			},
			orderable: false
		})
	}

	table.DataTable({
		columns: columnsToDisplay,
		columnDefs: [
			{
				render: function (data, type, full, meta) {
					if (data) {
						if (isObject(data)) {
							let output = '<div class="list d-flex">'
							let limit = data.limit > 0 && data.data.length > data.limit ? data.limit : data.data.length
							for (let i = 0; i < limit; i++) {
								const specificTagStyle = data.stylePerValue ? data.stylePerValue.find(s => s.value == data.data[i]) : null
								if (specificTagStyle) {
									output += `<p class='list-item ${getDatatableTagStyle(specificTagStyle.style)}'>${data.data[i]}</p>`								
								} else {
									output += `<p class='list-item ${getDatatableTagStyle(data.defaultStyle)}'>${data.data[i]}</p>`
								}
							}
							if (data.limit > 0 && data.data.length > data.limit) {
								output += `<p class='list-item ${data.style} additional'><i class='fa fa-plus'></i> ${data.data.length - data.limit}</p>`
							}
							return `${output}</div>`
						} else if (typeof data === 'string') {
							return `<div class="wrap">${(data)}</div>`;
						}
					}
					return `<div class="wrap">${(data || datatableResources["WebApp.Js.Datatables.NoData"])}</div>`;
				},
				targets: "_all"
			}
		],
		ordering: true,
		serverSide: true,
		lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, datatableResources["WebApp.Js.Datatables.LengthMenuAll"]]],
		buttons: {
			buttons: [
				{ extend: 'csv', text: 'CSV', exportOptions: { columns: columnsToExport } },
				{ extend: 'excel', text: 'Excel', exportOptions: { columns: columnsToExport } },
				{ extend: 'pdf', text: 'PDF', exportOptions: { columns: columnsToExport } },
				{ extend: 'print', text: datatableResources["WebApp.Js.Datatables.ButtonsPrint"], exportOptions: { columns: columnsToExport } }
			],
			dom: {
				button: {
					className: "btn btn-outline-secondary mr-2"
				}
			}
		},
		ajax: {
			url: getUrl,
			type: 'POST'
		},
		// order: orderBy,
		responsive: {
			details: {
				renderer: function (api, rowIdx, columns) {
					let actions = api.rows().data()[rowIdx].actions
					let data = $.map(columns, function (col, i) {
						if (col.hidden) {
							if (col.data.indexOf("datatable-row-actions") > -1 && actions != null) {
								let childRow = $(
									`<div class="row-child-container d-flex" data-dt-row="${col.rowIndex}" data-dt-column="${col.columnIndex}">` +
									`<p>${col.title}:</p>` +
									'<div class="datatable-row-actions"/>' +
									'</div>')

								return childRow.html()

							} else {
								return `<div class="row-child-container" data-dt-row="${col.rowIndex}" data-dt-column="${col.columnIndex}">` +
									`<p>${col.title}:</p> ${col.data}` +
									'</div>'
							}
						}
						return ''
					}).join('');

					if (data) {
						let result = $('<div/>').append(data)

						if (!hideActionButtons) {
							// Create buttons
							createButtons(actions, result.find('.datatable-row-actions'))

							// Setup event handlers
							setupEventHandlers(result, table)
						}
						return result
					}
					return false
				}
			}
		},
		language: {
			"processing": datatableResources["WebApp.Js.Datatables.LanguageProcessing"],
			"lengthMenu": datatableResources["WebApp.Js.Datatables.LanguageLengthMenu"],
			"zeroRecords": datatableResources["WebApp.Js.Datatables.LanguageZeroRecords"],
			"emptyTable": datatableResources["WebApp.Js.Datatables.LanguageEmptyTable"],
			"info": datatableResources["WebApp.Js.Datatables.LanguageInfo"],
			"infoEmpty": datatableResources["WebApp.Js.Datatables.LanguageInfoEmpty"],
			"infoFiltered": datatableResources["WebApp.Js.Datatables.LanguageInfoFiltered"],
			"search": datatableResources["WebApp.Js.Datatables.LanguageSearch"],
			"thousands": datatableResources[""],
			"loadingRecords": datatableResources["WebApp.Js.Datatables.LanguageLoadingRecords"],
			"paginate": {
				"first": datatableResources["WebApp.Js.Datatables.LanguagePaginateFirst"],
				"last": datatableResources["WebApp.Js.Datatables.LanguagePaginateLast"],
				"next": datatableResources["WebApp.Js.Datatables.LanguagePaginateNext"],
				"previous": datatableResources["WebApp.Js.Datatables.LanguagePaginatePrevious"]
			},
			"aria": {
				"sortAscending": datatableResources["WebApp.Js.Datatables.LanguageAriaSortAscending"],
				"sortDescending": datatableResources["WebApp.Js.Datatables.LanguageAriaSortDescending"]
			}
		},
		dom: '<"mr-2"l>Bfrt<"dataTables_footer"ip>',
		drawCallback: function () {
			let datatable = table.DataTable();
			let rows = table.find("tbody tr")
			let data = datatable.rows().data()

			data.each(function (row, index) {

				// Add expand / collapse buttons for responsive
				let firstCol = $(rows[index]).find("td:first-child");
				firstCol.addClass("flex")
				let expandIcon = $("<i class='fa fa-plus'></i>");
				let collapseIcon = $("<i class='fa fa-minus'></i>");
				expandIcon.insertBefore(firstCol.children().first());
				collapseIcon.insertBefore(firstCol.children().first());

				if (!hideActionButtons) {
					let actionsContainer = $(rows[index]).find(".datatable-row-actions").first()
					// Create buttons
					createButtons(row.actions, actionsContainer)

					// Setup event handlers
					setupEventHandlers(actionsContainer, table)
				}
			})
		}
	});
}

isObject = (obj) => {
	return obj != null && obj.constructor.name === "Object"
}

getDatatableTagStyle = (key) => {
	switch (key) {
		case 0: return "Gray"
		case 1: return "Red"
		case 2: return "Orange"
		case 3: return "Yellow"
		case 4: return "Green"
		case 5: return "Teal"
		case 6: return "Blue"
		case 7: return "Indigo"
		case 8: return "Purple"
		case 9: return "Pink"
	}
}