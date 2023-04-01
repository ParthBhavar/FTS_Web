(function($) {
  'use strict';
  $(function() {
    if ($("#order-chart").length) {
      var areaData = {
        labels: ["5","","","10","","","15","","","20","","", "25","","", "30","","","35"],
        datasets: [
          {
            data: [20, 48, 70, 60, 62, 35, 38, 35, 85, "60", "65", "35", "59", "35", "62", "50", "99", "78", "65"],
            borderColor: [
              '#4747A1'
            ],
            borderWidth: 2,
            fill: false,
            label: "Approved"
          },
          {
            data: [40, 45, 41, 50, 48, 60, 45, 55, 46, "56", "45", "70", "45", "64", "55", "65", "40", "8", "8"],
            borderColor: [
              '#F09397'
            ],
            borderWidth: 2,
            fill: false,
            label: "Rejected"
          }
        ]
      };
      var areaOptions = {
        responsive: true,
        maintainAspectRatio: true,
        plugins: {
          filler: {
            propagate: false
          }
        },
        scales: {
          xAxes: [{
            display: true,
            ticks: {
              display: true,
              padding: 10,
              fontColor:"#6C7383"
            },
            gridLines: {
              display: false,
              drawBorder: false,
              color: 'transparent',
              zeroLineColor: '#eeeeee'
            }
          }],
          yAxes: [{
            display: true,
            ticks: {
              display: true,
              autoSkip: false,
              maxRotation: 0,
              stepSize: 30,
              min: 0,
              max: 120,
              padding: 18,
              fontColor:"#6C7383"
            },
            gridLines: {
              display: true,
              color:"#f2f2f2",
              drawBorder: false
            }
          }]
        },
        legend: {
          display: false
        },
        tooltips: {
          enabled: true
        },
        elements: {
          line: {
            tension: .35
          },
          point: {
            radius: 0
          }
        }
      }
      var revenueChartCanvas = $("#order-chart").get(0).getContext("2d");
      var revenueChart = new Chart(revenueChartCanvas, {
        type: 'line',
        data: areaData,
        options: areaOptions
      });
    }
    if ($("#order-chart-dark").length) {
      var areaData = {
        labels: ["10","","","20","","","30","","","40","","", "50","","", "60","","","70"],
        datasets: [
          {
            data: [200, 480, 700, 600, 620, 350, 380, 350, 850, "600", "650", "350", "590", "350", "620", "500", "990", "780", "650"],
            borderColor: [
              '#4747A1'
            ],
            borderWidth: 2,
            fill: false,
            label: "Orders"
          },
          {
            data: [400, 450, 410, 500, 480, 600, 450, 550, 460, "560", "450", "700", "450", "640", "550", "650", "400", "850", "800"],
            borderColor: [
              '#F09397'
            ],
            borderWidth: 2,
            fill: false,
            label: "Downloads"
          }
        ]
      };
      var areaOptions = {
        responsive: true,
        maintainAspectRatio: true,
        plugins: {
          filler: {
            propagate: false
          }
        },
        scales: {
          xAxes: [{
            display: true,
            ticks: {
              display: true,
              padding: 10,
              fontColor:"#fff"
            },
            gridLines: {
              display: false,
              drawBorder: false,
              color: 'transparent',
              zeroLineColor: '#575757'
            }
          }],
          yAxes: [{
            display: true,
            ticks: {
              display: true,
              autoSkip: false,
              maxRotation: 0,
              stepSize: 200,
              min: 200,
              max: 1200,
              padding: 18,
              fontColor:"#fff"
            },
            gridLines: {
              display: true,
              color:"#575757",
              drawBorder: false
            }
          }]
        },
        legend: {
          display: false
        },
        tooltips: {
          enabled: true
        },
        elements: {
          line: {
            tension: .35
          },
          point: {
            radius: 0
          }
        }
      }
      var revenueChartCanvas = $("#order-chart-dark").get(0).getContext("2d");
      var revenueChart = new Chart(revenueChartCanvas, {
        type: 'line',
        data: areaData,
        options: areaOptions
      });
    }
    if ($("#sales-chart").length) {
      var SalesChartCanvas = $("#sales-chart").get(0).getContext("2d");
      var SalesChart = new Chart(SalesChartCanvas, {
        type: 'bar',
        data: {
          labels: ["Aug", "Sep", "Oct", "Nov", "Dec"],
          datasets: [{
              label: 'Approved',
              data: [20, 60, 30, 10, 30],
              backgroundColor: '#98BDFF'
            },
            {
              label: 'Rejected',
              data: [5, 10, 7, 8, 5],
              backgroundColor: '#4B49AC'
            }
          ]
        },
        options: {
          cornerRadius: 5,
          responsive: true,
          maintainAspectRatio: true,
          layout: {
            padding: {
              left: 0,
              right: 0,
              top: 20,
              bottom: 0
            }
          },
          scales: {
            yAxes: [{
              display: true,
              gridLines: {
                display: true,
                drawBorder: false,
                color: "#F2F2F2"
              },
              ticks: {
                display: true,
                min: 0,
                max: 60,
                callback: function(value, index, values) {
                  return  value + '' ;
                },
                autoSkip: true,
                maxTicksLimit: 5,
                fontColor:"#6C7383"
              }
            }],
            xAxes: [{
              stacked: false,
              ticks: {
                beginAtZero: true,
                fontColor: "#6C7383"
              },
              gridLines: {
                color: "rgba(0, 0, 0, 0)",
                display: false
              },
              barPercentage: 1
            }]
          },
          legend: {
            display: false
          },
          elements: {
            point: {
              radius: 0
            }
          }
        },
      });
      document.getElementById('sales-legend').innerHTML = SalesChart.generateLegend();
    }
    if ($("#sales-chart-dark").length) {
      var SalesChartCanvas = $("#sales-chart-dark").get(0).getContext("2d");
      var SalesChart = new Chart(SalesChartCanvas, {
        type: 'bar',
        data: {
          labels: ["Jan", "Feb", "Mar", "Apr", "May"],
          datasets: [{
              label: 'Offline Sales',
              data: [480, 230, 470, 210, 330],
              backgroundColor: '#98BDFF'
            },
            {
              label: 'Online Sales',
              data: [400, 340, 550, 480, 170],
              backgroundColor: '#4B49AC'
            }
          ]
        },
        options: {
          cornerRadius: 5,
          responsive: true,
          maintainAspectRatio: true,
          layout: {
            padding: {
              left: 0,
              right: 0,
              top: 20,
              bottom: 0
            }
          },
          scales: {
            yAxes: [{
              display: true,
              gridLines: {
                display: true,
                drawBorder: false,
                color: "#575757"
              },
              ticks: {
                display: true,
                min: 0,
                max: 500,
                callback: function(value, index, values) {
                  return  value + '$' ;
                },
                autoSkip: true,
                maxTicksLimit: 10,
                fontColor:"#F0F0F0"
              }
            }],
            xAxes: [{
              stacked: false,
              ticks: {
                beginAtZero: true,
                fontColor: "#F0F0F0"
              },
              gridLines: {
                color: "#575757",
                display: false
              },
              barPercentage: 1
            }]
          },
          legend: {
            display: false
          },
          elements: {
            point: {
              radius: 0
            }
          }
        },
      });
      document.getElementById('sales-legend').innerHTML = SalesChart.generateLegend();
    }
    if ($("#north-america-chart").length) {
      var areaData = {
          labels: ["Ahmedabad", "Amreli", "Anand", "Sabarkantha", "Bhavnagar", "Valsad"],
        datasets: [{
            data: [55, 25, 40, 15, 10, 15],
            backgroundColor: [
               "#4B49AC", "#FFC100", "#248AFD", "#9baecc", "#1e5429", "#966128"
            ],
            borderColor: "rgba(0,0,0,0)"
          }
        ]
      };
      var areaOptions = {
        responsive: true,
        maintainAspectRatio: true,
        segmentShowStroke: false,
        cutoutPercentage: 78,
        elements: {
          arc: {
              borderWidth: 4
          }
        },      
        legend: {
          display: false
        },
        tooltips: {
          enabled: true
        },
        legendCallback: function(chart) { 
          var text = [];
          text.push('<div class="report-chart">');
          text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[0] + '"></div><p class="mb-0">Ahmedabad</p></div>');
            text.push('<p class="mb-0">55</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[1] + '"></div><p class="mb-0">Amreli</p></div>');
            text.push('<p class="mb-0">25</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[2] + '"></div><p class="mb-0">Anand</p></div>');
            text.push('<p class="mb-0">40</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[3] + '"></div><p class="mb-0">Sabarkantha</p></div>');
            text.push('<p class="mb-0">15</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[4] + '"></div><p class="mb-0">Bhavnagar</p></div>');
            text.push('<p class="mb-0">10</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[5] + '"></div><p class="mb-0">Valsad</p></div>');
            text.push('<p class="mb-0">15</p>');
            text.push('</div>');
          text.push('</div>');
          return text.join("");
        },
      }
      var northAmericaChartPlugins = {
        beforeDraw: function(chart) {
          var width = chart.chart.width,
              height = chart.chart.height,
              ctx = chart.chart.ctx;
      
          ctx.restore();
          var fontSize = 3.125;
          ctx.font = "500 " + fontSize + "em sans-serif";
          ctx.textBaseline = "middle";
          ctx.fillStyle = "#13381B";
      
          var text = "160",
              textX = Math.round((width - ctx.measureText(text).width) / 2),
              textY = height / 2;
      
          ctx.fillText(text, textX, textY);
          ctx.save();
        }
      }
      var northAmericaChartCanvas = $("#north-america-chart").get(0).getContext("2d");
      var northAmericaChart = new Chart(northAmericaChartCanvas, {
        type: 'doughnut',
        data: areaData,
        options: areaOptions,
        plugins: northAmericaChartPlugins
      });
      document.getElementById('north-america-legend').innerHTML = northAmericaChart.generateLegend();
    }
    if ($("#north-america-chart-dark").length) {
      var areaData = {
        labels: ["Jan", "Feb", "Mar"],
        datasets: [{
            data: [100, 50, 50],
            backgroundColor: [
               "#4B49AC","#FFC100", "#248AFD",
            ],
            borderColor: "rgba(0,0,0,0)"
          }
        ]
      };
      var areaOptions = {
        responsive: true,
        maintainAspectRatio: true,
        segmentShowStroke: false,
        cutoutPercentage: 78,
        elements: {
          arc: {
              borderWidth: 4
          }
        },      
        legend: {
          display: false
        },
        tooltips: {
          enabled: true
        },
        legendCallback: function(chart) { 
          var text = [];
          text.push('<div class="report-chart">');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[0] + '"></div><p class="mb-0">Offline sales</p></div>');
            text.push('<p class="mb-0">88333</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[1] + '"></div><p class="mb-0">Online sales</p></div>');
            text.push('<p class="mb-0">66093</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[2] + '"></div><p class="mb-0">Returns</p></div>');
            text.push('<p class="mb-0">39836</p>');
            text.push('</div>');
          text.push('</div>');
          return text.join("");
        },
      }
      var northAmericaChartPlugins = {
        beforeDraw: function(chart) {
          var width = chart.chart.width,
              height = chart.chart.height,
              ctx = chart.chart.ctx;
      
          ctx.restore();
          var fontSize = 3.125;
          ctx.font = "500 " + fontSize + "em sans-serif";
          ctx.textBaseline = "middle";
          ctx.fillStyle = "#fff";
      
          var text = "90",
              textX = Math.round((width - ctx.measureText(text).width) / 2),
              textY = height / 2;
      
          ctx.fillText(text, textX, textY);
          ctx.save();
        }
      }
      var northAmericaChartCanvas = $("#north-america-chart-dark").get(0).getContext("2d");
      var northAmericaChart = new Chart(northAmericaChartCanvas, {
        type: 'doughnut',
        data: areaData,
        options: areaOptions,
        plugins: northAmericaChartPlugins
      });
      document.getElementById('north-america-legend').innerHTML = northAmericaChart.generateLegend();
    }

    if ($("#south-america-chart").length) {
      var areaData = {
          labels: ["Banaskantha", "Bharuch", "Bhavnagar", "Vadodara", "Mehsana", "Rajkot"],
        datasets: [{
            data: [25, 15, 20, 15, 18, 12],
            backgroundColor: [
              "#4B49AC", "#FFC100", "#248AFD", "#9baecc", "#1e5429", "#966128"
            ],
            borderColor: "rgba(0,0,0,0)"
          }
        ]
      };
      var areaOptions = {
        responsive: true,
        maintainAspectRatio: true,
        segmentShowStroke: false,
        cutoutPercentage: 78,
        elements: {
          arc: {
              borderWidth: 4
          }
        },      
        legend: {
          display: false
        },
        tooltips: {
          enabled: true
        },
        legendCallback: function(chart) { 
          var text = [];
          text.push('<div class="report-chart">');
          text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[0] + '"></div><p class="mb-0">Banaskantha</p></div>');
            text.push('<p class="mb-0">25</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[1] + '"></div><p class="mb-0">Bharuch</p></div>');
            text.push('<p class="mb-0">15</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[2] + '"></div><p class="mb-0">Bhavnagar</p></div>');
            text.push('<p class="mb-0">20</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[3] + '"></div><p class="mb-0">Vadodara</p></div>');
            text.push('<p class="mb-0">15</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[4] + '"></div><p class="mb-0">Mehsana</p></div>');
            text.push('<p class="mb-0">18</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[5] + '"></div><p class="mb-0">Rajkot</p></div>');
            text.push('<p class="mb-0">12</p>');
            text.push('</div>');
          text.push('</div>');
          return text.join("");
        },
      }
      var southAmericaChartPlugins = {
        beforeDraw: function(chart) {
          var width = chart.chart.width,
              height = chart.chart.height,
              ctx = chart.chart.ctx;
      
          ctx.restore();
          var fontSize = 3.125;
          ctx.font = "600 " + fontSize + "em sans-serif";
          ctx.textBaseline = "middle";
          ctx.fillStyle = "#000";
      
          var text = "100",
              textX = Math.round((width - ctx.measureText(text).width) / 2),
              textY = height / 2;
      
          ctx.fillText(text, textX, textY);
          ctx.save();
        }
      }
      var southAmericaChartCanvas = $("#south-america-chart").get(0).getContext("2d");
      var southAmericaChart = new Chart(southAmericaChartCanvas, {
        type: 'doughnut',
        data: areaData,
        options: areaOptions,
        plugins: southAmericaChartPlugins
      });
      document.getElementById('south-america-legend').innerHTML = southAmericaChart.generateLegend();
    }
    if ($("#south-america-chart-dark").length) {
      var areaData = {
        labels: ["Jan", "Feb", "Mar"],
        datasets: [{
            data: [260, 150, 35],
            backgroundColor: [
              "#4B49AC","#FFC100", "#248AFD",
            ],
            borderColor: "rgba(0,0,0,0)"
          }
        ]
      };
      var areaOptions = {
        responsive: true,
        maintainAspectRatio: true,
        segmentShowStroke: false,
        cutoutPercentage: 78,
        elements: {
          arc: {
              borderWidth: 4
          }
        },      
        legend: {
          display: false
        },
        tooltips: {
          enabled: true
        },
        legendCallback: function(chart) { 
          var text = [];
          text.push('<div class="report-chart">');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[0] + '"></div><p class="mb-0">Submitted</p></div>');
            text.push('<p class="mb-0">260</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[1] + '"></div><p class="mb-0">Approved</p></div>');
            text.push('<p class="mb-0">150</p>');
            text.push('</div>');
            text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[2] + '"></div><p class="mb-0">Rejected</p></div>');
            text.push('<p class="mb-0">35</p>');
            text.push('</div>');
          text.push('</div>');
          return text.join("");
        },
      }
      var southAmericaChartPlugins = {
        beforeDraw: function(chart) {
          var width = chart.chart.width,
              height = chart.chart.height,
              ctx = chart.chart.ctx;
      
          ctx.restore();
          var fontSize = 3.125;
          ctx.font = "600 " + fontSize + "em sans-serif";
          ctx.textBaseline = "middle";
          ctx.fillStyle = "#fff";
      
          var text = "76",
              textX = Math.round((width - ctx.measureText(text).width) / 2),
              textY = height / 2;
      
          ctx.fillText(text, textX, textY);
          ctx.save();
        }
      }
      var southAmericaChartCanvas = $("#south-america-chart-dark").get(0).getContext("2d");
      var southAmericaChart = new Chart(southAmericaChartCanvas, {
        type: 'doughnut',
        data: areaData,
        options: areaOptions,
        plugins: southAmericaChartPlugins
      });
      document.getElementById('south-america-legend').innerHTML = southAmericaChart.generateLegend();
    }

    function format ( d ) {
      // `d` is the original data object for the row
      return '<table cellpadding="5" cellspacing="0" border="0" style="width:100%;">'+
          '<tr class="expanded-row">'+
              '<td colspan="8" class="row-bg"><div><div class="d-flex justify-content-between"><div class="cell-hilighted"><div class="d-flex mb-2"><div class="mr-2 min-width-cell"><p>Policy start date</p><h6>25/04/2020</h6></div><div class="min-width-cell"><p>Policy end date</p><h6>24/04/2021</h6></div></div><div class="d-flex"><div class="mr-2 min-width-cell"><p>Sum insured</p><h5>$26,000</h5></div><div class="min-width-cell"><p>Premium</p><h5>$1200</h5></div></div></div><div class="expanded-table-normal-cell"><div class="mr-2 mb-4"><p>Quote no.</p><h6>Incs234</h6></div><div class="mr-2"><p>Vehicle Reg. No.</p><h6>KL-65-A-7004</h6></div></div><div class="expanded-table-normal-cell"><div class="mr-2 mb-4"><p>Policy number</p><h6>Incsq123456</h6></div><div class="mr-2"><p>Policy number</p><h6>Incsq123456</h6></div></div><div class="expanded-table-normal-cell"><div class="mr-2 mb-3 d-flex"><div class="highlighted-alpha"> A</div><div><p>Agent / Broker</p><h6>Abcd Enterprices</h6></div></div><div class="mr-2 d-flex"> <img src="../../images/faces/face5.jpg" alt="profile"/><div><p>Policy holder Name & ID Number</p><h6>Phillip Harris / 1234567</h6></div></div></div><div class="expanded-table-normal-cell"><div class="mr-2 mb-4"><p>Branch</p><h6>Koramangala, Bangalore</h6></div></div><div class="expanded-table-normal-cell"><div class="mr-2 mb-4"><p>Channel</p><h6>Online</h6></div></div></div></div></td>'
          '</tr>'+
      '</table>';
  }
  var table = $('#example').DataTable( {
    "ajax": "js/data.txt",
    "columns": [
        { "data": "Quote" },
        { "data": "Product" },
        { "data": "Business" },
        { "data": "Policy" }, 
        { "data": "Premium" }, 
        { "data": "Status" }, 
        { "data": "Updated" }, 
        {
          "className":      'details-control',
          "orderable":      false,
          "data":           null,
          "defaultContent": ''
        }
    ],
    "order": [[1, 'asc']],
    "paging":   false,
    "ordering": true,
    "info":     false,
    "filter": false,
    columnDefs: [{
      orderable: false,
      className: 'select-checkbox',
      targets: 0
    }],
    select: {
      style: 'os',
      selector: 'td:first-child'
    }
  } );
$('#example tbody').on('click', 'td.details-control', function () {
  var tr = $(this).closest('tr');
  var row = table.row( tr );

  if ( row.child.isShown() ) {
      // This row is already open - close it
      row.child.hide();
      tr.removeClass('shown');
  }
  else {
      // Open this row
      row.child( format(row.data()) ).show();
      tr.addClass('shown');
  }
} );
  
  });
})(jQuery);