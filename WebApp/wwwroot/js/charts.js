charts = {
	newLineChart: (element, title, labels, values) => {
		new Chart(element,
			{
				"type": "line",
				"data": {
					"labels": labels,
					"datasets": [{
						"label": '',
						"data": values,
						"fill": false,
						"borderColor": "rgb(86, 192, 216)",
						"lineTension": 0.1
					}]
				}, "options": {
					title: {
						display: true,
						text: title
					},
					legend: {
						display: false
					}
				}
			});
	},
	newBarChart: (element, title, labels, values) => {
		new Chart(element,
			{
				"type": "bar",
				"data": {
					"labels": labels,
					"datasets": [
						{
							"data": values,
							"fill": false,
							"backgroundColor": ["rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)", "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(54, 162, 235, 0.2)", "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"],
							"borderColor": ["rgb(239, 83, 80)", "rgb(255, 159, 64)", "rgb(255, 178, 43)", "rgb(86, 192, 216)", "rgb(57, 139, 247)", "rgb(153, 102, 255)", "rgb(201, 203, 207)"],
							"borderWidth": 1
						}
					]
				},
				"options": {
					"scales": { "yAxes": [{ "ticks": { "beginAtZero": true } }] },
					title: {
						display: true,
						text: title
					},
					legend: {
						display: false
					}
				}
			});
	},
	newPieChart: (element, title, labels, values) => {
		new Chart(element,
			{
				"type": "pie",
				"data": {
					"labels": labels,
					"datasets": [
						{
							"data": values,
							"backgroundColor": ["rgb(239, 83, 80)", "rgb(57, 139, 247)", "rgb(255, 178, 43)"]
						}
					]
				},
				"options": {
					title: {
						display: true,
						text: title
					}
				}
			});
	},
	newDoughnutChart: (element, title, labels, values) => {
		new Chart(element,
			{
				"type": "doughnut",
				"data": {
					"labels": labels,
					"datasets": [{
						"label": title,
						"data": values,
						"backgroundColor": ["rgb(239, 83, 80)", "rgb(57, 139, 247)", "rgb(255, 178, 43)"]
					}
					]
				},
				"options": {
					title: {
						display: true,
						text: title
					}
				}
			});
	},
	newPolarAreaChart: (element, title, labels, values) => {
		new Chart(element,
			{
				"type": "polarArea",
				"data": {
					"labels": labels,
					"datasets": [
						{
							"label": title,
							"data": values,
							"backgroundColor": ["rgb(239, 83, 80)", "rgb(86, 192, 216)", "rgb(255, 178, 43)", "rgb(201, 203, 207)", "rgb(57, 139, 247)"
							]
						}
					]
				},
				"options": {
					title: {
						display: true,
						text: title
					}
				}
			});
	},
	newRadarChart: (element, title, labels, datasets) => {
		let data = []
		for (var dataset of datasets) {
			data.push({
				"label": dataset.title,
				"data": dataset.values,
				"fill": true,
				"backgroundColor": dataset.color
			})
		}
		new Chart(element,
			{
				"type": "radar",
				"data": {
					"labels": labels,
					"datasets": data
				},
				"options": {
					"elements": {
						"line": {
							"tension": 0,
							"borderWidth": 3
						}
					},
					title: {
						display: true,
						text: title
					}
				}
			});
	}
};