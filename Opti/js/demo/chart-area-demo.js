// Set new default font family and font color to mimic Bootstrap's default styling

dayName = new Array("Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado")
monName = new Array("Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Agosto", "Outubro", "Novembro", "Dezembro")

//var mes = monName[now.getMonth()]
//var dia = now.getDate()



Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

var hoje = new Date()
var dia = hoje.getDate()

var mes = hoje.getMonth()

mes = monName[mes-1].substring(0, 3)

// Area Chart Example
var ctx = document.getElementById("myAreaChart");
var myLineChart = new Chart(ctx, {
  type: 'line',
  data: {
      labels: [mes + " " + (dia - 14), mes + " " + (dia - 13), mes + " " + (dia - 12), mes + " " + (dia - 11), mes + " " + (dia - 10), mes + " " + (dia - 9), mes + " " + (dia - 8), mes + " " + (dia - 7), mes + " " + (dia - 6), mes + " " + (dia - 5), mes + " " + (dia - 4), mes + " " + (dia - 3), mes + " " + (dia - 2), mes + " " + (dia - 1), mes + " " + dia ],
      datasets: [{
      label: "Produtos",
      lineTension: 0.3,
      backgroundColor: "rgba(2,117,216,0.2)",
      borderColor: "rgba(2,117,216,1)",
      pointRadius: 5,
      pointBackgroundColor: "rgba(2,117,216,1)",
      pointBorderColor: "rgba(255,255,255,0.8)",
      pointHoverRadius: 5,
      pointHoverBackgroundColor: "rgba(2,117,216,1)",
      pointHitRadius: 50,
      pointBorderWidth: 2,
      data: [1000, 3000, 2406, 108, 1802, 208, 3001, 3322, 2500, 2844, 3172, 371, 348, 500, 4888],
    }],
  },
  options: {
    scales: {
      xAxes: [{
        time: {
          unit: 'date'
        },
        gridLines: {
          display: false
        },
        ticks: {
          maxTicksLimit: 7
        }
      }],
      yAxes: [{
        ticks: {
          min: 0,
          max: 10000,
          maxTicksLimit: 5
        },
        gridLines: {
          color: "rgba(0, 0, 0, .125)",
        }
      }],
    },
    legend: {
      display: false
    }
  }
});
