function graphic(data) {
    Highcharts.chart('container', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: data.title
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        series: [{
            name: 'Percentage',
            colorByPoint: true,
            data: data.series,
            colors: data.colors
        }]
    });
}

function graphicBar(data) {
    var chart = Highcharts.chart('containerBar', {

        title: {
            text: data.title
        },

        subtitle: {
            text: data.subtitle
        },

        xAxis: {
            categories: data.categories
        },

        series: [{
            type: 'column',
            colorByPoint: true,
            data: data.series,
            colors: data.colors,
            showInLegend: false
        }]

    });
}

function graphicInverted(data) {
    var chart = Highcharts.chart('containerInverted', {



        title: {
            text: data.title
        },

        subtitle: {
            text: data.subtitle
        },

        xAxis: {
            categories: data.categories
        },

        series: [{
            type: 'column',
            colorByPoint: true,
            data: data.series,
            colors: data.colors,
            showInLegend: false
        }],

        chart: {
            inverted: true,
            polar: false
        }
    });
}

function ajaxBar() {
    $.ajax({
        type: "GET",
        url: "/Statistics/GraphicBar",
        success: function (data) {
            graphicBar(data);
            ajaxInverted();
        },
        error: function (data) {
            console.log(data);
        },
    });
}

function ajaxInverted() {
    $.ajax({
        type: "GET",
        url: "/Statistics/GraphicInverted",
        success: function (data) {
            graphicInverted(data);

        },
        error: function (data) {
            console.log(data);
        },
    });
}

$.ajax({
    type: "GET",
    url: "/Statistics/GraphicPie",
    success: function (data) {
        graphic(data);
        ajaxBar();
    },
    error: function (data) {
        console.log(data);
    },
});
