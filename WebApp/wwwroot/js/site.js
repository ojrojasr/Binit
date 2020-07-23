// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
	// Autoresize textarea based on content
	autosize($('textarea'));

	// Initialize date pickers
	$('.date-picker').each(function () {
		$(this).datepicker({
			format: 'dd/mm/yyyy'
		});
	});

	// Initialize datatables
	$('.ignite-datatable').each(function () {
		dataTableInitiator($(this))
	})

	// Initialize select2
	$('.select2').each(function () {
		if ($(this).data('search-url')) {
			$(this).select2({
				minimumInputLength: 3,
				ajax: {
					url: $(this).data('searchUrl')
				}
			});
		} else {
			$(this).select2()
		}
	})

	// Initialize Google Maps Autocomplete for addresses
	$('.ignite-address').each(function () {
		const mapsAutocomplete = new MapsAutocomplete()
		mapsAutocomplete.init($(this))
	})

	// Initialize common charts
	$(".ignite-chart").each(function () {
		let title = $(this).data("title")
		let labels = $(this).data("labels")
		let values = $(this).data("values")

		if($(this).hasClass("line-chart")) {
			charts.newLineChart($(this), title, labels, values)
		}

		if($(this).hasClass("bar-chart")) {
			charts.newBarChart($(this), title, labels, values)
		}

		if($(this).hasClass("pie-chart")) {
			charts.newPieChart($(this), title, labels, values)
		}

		if($(this).hasClass("doughnut-chart")) {
			charts.newDoughnutChart($(this), title, labels, values)
		}

		if($(this).hasClass("polar-chart")) {
			charts.newPolarAreaChart($(this), title, labels, values)
		}

		if($(this).hasClass("radar-chart")) {
			charts.newRadarChart($(this), title, labels, values)
		}
	})
})


// Autohide sidebar for mobile
$(document).click(function () {
	if (window.innerWidth < 1170) {
		$('body').trigger('resize');
		$('body').addClass('mini-sidebar');
		$('.navbar-brand span').hide();

		$('body').removeClass('show-sidebar');
		$('.nav-toggler i').addClass('ti-menu');
		$('.nav-toggler i').removeClass('ti-close');
	}
});

$('.left-sidebar, .sidebartoggler, .nav-toggler').click(function (e) {
	if (window.innerWidth < 1170) {
		e.stopPropagation();
	}
});
