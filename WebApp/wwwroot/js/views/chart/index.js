chart = {
	index: {
		init: (basicLabels, basicValues, radarLabels, firstDataSet, secondDataSet) => {
			// Init Line Chart
			charts.newLineChart($("#chart-1"), "Price", basicLabels, basicValues)

			// Init Bar Chart
			charts.newBarChart($("#chart-2"), "Bar Chart", basicLabels, basicValues)

			// Init Pie Chart
			charts.newPieChart($("#chart-3"), "Pie Chart", basicLabels, basicValues)

			// Init Doughnut Chart
			charts.newDoughnutChart($("#chart-4"), "Doughnut Chart", basicLabels, basicValues)

			// Init Polar Area Chart
			charts.newPolarAreaChart($("#chart-5"), "Polar Area Chart", basicLabels, basicValues)

			// Init Radar Chart
			charts.newRadarChart($("#chart-6"), "Radar chart", radarLabels, firstDataSet, secondDataSet)
		}
	}
}