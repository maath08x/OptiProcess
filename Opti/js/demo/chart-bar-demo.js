var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {

        // Set new default font family and font color to mimic Bootstrap's default styling
        Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
        Chart.defaults.global.defaultFontColor = '#292b2c';
        dayName = new Array("Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado")
        monName = new Array("Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro")

        var hoje = new Date()
        var mes_ = hoje.getMonth()
        var response = JSON.parse(this.response);

        // Bar Chart Example
        var ctx = document.getElementById("myBarChartt");
        var myLineChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: [monName[mes_ - 4], monName[mes_ - 3], monName[mes_ - 2], monName[mes_ - 1], monName[mes_]],
                datasets: [{
                    label: "Revenue",
                    backgroundColor: "rgba(2,117,216,1)",
                    borderColor: "rgba(2,117,216,1)",
                    data: [response[0]["QuartoMes"], response[0]["TerceiroMes"], response[0]["SegundoMes"], response[0]["PrimeiroMes"], response[0]["MesAtual"]],
                }],
            },
            options: {
                scales: {
                    xAxes: [{
                        time: {
                            unit: 'month'
                        },
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            maxTicksLimit: 6
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: 7,
                            maxTicksLimit: 5
                        },
                        gridLines: {
                            display: true
                        }
                    }],
                },
                legend: {
                    display: false
                }
            }
        });


    }
}

var sURL = "http://" + location.host + "/Home/SegundoGrafico?";

xhttp.open("GET", sURL, true);
xhttp.send();