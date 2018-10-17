var usuario = window.localStorage.getItem("usuario");
var sRequest = "Usuario=" + usuario;


var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        // Set new default font family and font color to mimic Bootstrap's default styling

        dayName = new Array("Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado")
        monName = new Array("Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro")

        //var mes = monName[now.getMonth()]
        //var dia = now.getDate()
        Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
        Chart.defaults.global.defaultFontColor = '#292b2c';

        var hoje = new Date();
        var dia = hoje.getDate();

        var mes = hoje.getMonth();

        mes = monName[mes - 1].substring(0, 3);
        var response = JSON.parse(this.response);

        // Area Chart Example
        var ctx = document.getElementById("myAreaChartt");
        var myLineChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: [
                        mes + " " + (dia - 14),
                        mes + " " + (dia - 13),
                        mes + " " + (dia - 12),
                        mes + " " + (dia - 11),
                        mes + " " + (dia - 10),
                        mes + " " + (dia - 9),
                        mes + " " + (dia - 8),
                        mes + " " + (dia - 7),
                        mes + " " + (dia - 6),
                        mes + " " + (dia - 5),
                        mes + " " + (dia - 4),
                        mes + " " + (dia - 3),
                        mes + " " + (dia - 2),
                        mes + " " + (dia - 1),
                    mes + " " + dia
                ],
                datasets: [{
                    label: "Pedidos",
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
                    data: [
                        response[0]["Primeiro"]
                        response[0]["Segundo"],
                        response[0]["Terceiro"],
                        response[0]["Quarto"],
                        response[0]["Quinto"],
                        response[0]["Sexto"],
                        response[0]["Setimo"],
                        response[0]["Oitavo"],
                        response[0]["Nono"],
                        response[0]["Decimo"],
                        response[0]["DecimoPrimeiro"],
                        response[0]["DecimoSegundo"],
                        response[0]["DecimoTerceiro"],
                        response[0]["DecimoQuarto"],
                        response[0]["DecimoQuinto"],
                    ]
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
                            max: 10,
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
    }
}

var sURL = "http://" + location.host + "/Home/PrimeiroGrafico?";

xhttp.open("GET", sURL, true);
xhttp.send();



segundoGrafico();

function segundoGrafico(){
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            // Set new default font family and font color to mimic Bootstrap's default styling
            Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
            Chart.defaults.global.defaultFontColor = '#292b2c';
            dayName = new Array("Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado")
            monName = new Array("Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro")

            var hoje = new Date();
            var mes_ = hoje.getMonth();
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

    terceiroGrafico();
}

function terceiroGrafico() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.response);

            // Set new default font family and font color to mimic Bootstrap's default styling
            Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
            Chart.defaults.global.defaultFontColor = '#292b2c';

            var outros = 0;
            for (var i = 5; i < response.length; i++) {
                outros = outros + response[i]["qntEstoque"];
            }

            // Pie Chart Example
            var ctx = document.getElementById("myPieChartt");
            var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: [response[0]["nome"], response[1]["nome"], response[2]["nome"], response[3]["nome"], response[4]["nome"], "Outros"],
                    datasets: [{
                        data: [response[0]["qntEstoque"], response[1]["qntEstoque"], response[2]["qntEstoque"], response[3]["qntEstoque"], response[4]["qntEstoque"], outros],
                        backgroundColor: ['#007bff', '#dc3545', '#ffc107', '#28a745', '#0cc7f5', '#541300'],
                    }],
                },
            });

        }
    }

    var sURL = "http://" + location.host + "/Home/TerceiroGrafico?";

    xhttp.open("GET", sURL, true);
    xhttp.send();
}






