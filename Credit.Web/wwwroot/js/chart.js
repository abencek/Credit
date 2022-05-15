var CreateCharts = function(){

    // Dummy data for charts
    var dataLabels=['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
    var dataSeries1=[0.5,0.4,0.7,0.2,0.5,0.5,0.6,0.6,0.4,0.3,0.6,0.5];
    var dataSeries2=[0.5737,0.8465,0.6347,0.5255,0.5394,0.6108,0.5324,0.8104,0.5153,0.6325,0.4905,0.6992];

    var charts = {};
  
    charts.chart1 =
    {
        type: 'bar',
        data: {
            labels:dataLabels,
            datasets: [
            {
                label: "Data1",
                type:'line',
                data: dataSeries1,
                fill:false,
                borderColor:"#f56954",
                backgroundColor:"#f56954",
                lineTension:0
            },
            {
                label: "Data2",
                data: dataSeries2,
                fill:false,
                backgroundColor:"#00c0ef",
            }
            ]
        },
        options: {
            maintainAspectRatio: false,
            title: {
                display: true
            },
            legend: { 
                display: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 1,
                        callback: function(value, index, values) {
                            return '' + (value*100)+'%';
                        },
                    },
                }],
                xAxes: [{
                    type:'category',
                    gridLines:{
                        drawOnChartArea: false,
                    }
                }]
            }
        }
    }

    charts.chart2=
    {
        type: 'line',
        lineTension:0,
        data: {
            labels:dataLabels,
            datasets: [
            {
                label: "Data1",
                data: dataSeries1,
                fill:false,
                borderColor:"#f56954",
                backgroundColor:"#f56954",
                lineTension:0
            },
            {
                label: "Data2",
                data: dataSeries2,
                fill:false,
                borderColor:"#00c0ef",
                backgroundColor:"#00c0ef",
                lineTension:0
            }
            ]
        },
        options: {
            maintainAspectRatio: false,
            title: {
                display: true
            },
            legend: { 
                display: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 1,
                        callback: function(value, index, values) {
                            return ''+(value*100)+'%';
                        },
                    },
                }],
                xAxes: [{
                    type:'category',
                    gridLines:{
                        drawOnChartArea: false,
                    }
                }]
            }
        }
    }

    charts.chart3 = {
        type: 'doughnut',
        data: {
        labels: dataLabels.slice(0,4),
        datasets: [
            {
                backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc'],
                data: dataSeries2.slice(0,4)
            }
        ]
        },
        options: {
            maintainAspectRatio: false,
            title: {
            display: false,
            },
            legend: {
                position:"right", 
                display: true
            },
        }
    }

    charts.chart4 = {
        type: 'pie',
        data: {
        labels: dataLabels.slice(0,4),
        datasets: [
            {
                backgroundColor : ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc'],
                data: dataSeries2.slice(0,4)
            }
        ]
        },
        options: {
            maintainAspectRatio: false,
            title: {
            display: false,
            },
            legend: { 
                position:"right", 
                display: true
            },
        }
    }

    return charts;

}



$(document).ready(function() {
    var charts = CreateCharts();

    //Set and display charts sequentially
    var animate = [];
    animate.push(function(){new Chart(document.getElementById('chart1'), charts.chart1)});
    animate.push(function(){new Chart(document.getElementById('chart2'), charts.chart2)});
    animate.push(function(){new Chart(document.getElementById('chart3'), charts.chart3)});
    animate.push(function(){new Chart(document.getElementById('chart4'), charts.chart4)});

    animate.forEach(
        function myFunction(value, index) {
            setTimeout(function () { value() }, 300 * index);
        }
    );
});
