var nID;
var nIDSearch;
var nIDEdit;
var sNome;
var sNomeSearch;
var sNomeEdit;
var nTipo;
var sDescricao;

function onload() {
    nID = document.getElementById("ID");
    nIDSearch = document.getElementById("ID_Search");
    nIDEdit = document.getElementById("ID_Edit");
    sNome = document.getElementById("Nome");
    sNomeSearch = document.getElementById("Nome_Search");
    sNomeEdit = document.getElementById("Nome_Edit");
    nTipo = document.getElementById("Tipo");
    sDescricao = document.getElementById("Descrição");

    var ctxx = document.getElementById("mmm");
    var myLineChartt = new Chart(ctxx, {
        type: 'line',
        data: {
            labels: ["Mar", "Jun", "Jul", "Ago"],
            datasets: [{
                label: "Sessions",
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
                data: [10055, 30055, 26245, 38666],
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
                        max: 40000,
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

    var ctx = document.getElementById("mm");
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ["Mar 1", "Mar 2", "Mar 3", "Mar 4", "Mar 5", "Mar 6", "Mar 7", "Mar 8", "Mar 9", "Mar 10", "Mar 11", "Mar 12", "Mar 13"],
            datasets: [{
                label: "Sessions",
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
                data: [10, 30, 26, 18, 18, 28, 31, 33, 25, 24, 32, 31, 38],
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
                        max: 40,
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

function Adicionar() {
    var sRequest = "Tipo=" + escape(nTipo.value) + "&Nome=" + escape(sNome.value) + "&Descricao=" + escape(sDescricao.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText)
        }
    };

    var sURL = "http://" + location.host + "/Maquinarios/Adicionar?Status=0" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function Pesquisar() {
    var sRequest = "ID=" + escape(nIDSearch.value) + "&Nome=" + escape(sNomeSearch.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            var dt;
            for (var i = 0; i < response.length; i++) {
                sInner += "<tr>";

                sInner += "<td id=maquinarioID" + i + ">" + response[i]["maquinarioID"] + "</td>";
                sInner += "<td id=nome" + i + ">" + response[i]["nome"] + "</td>";
                sInner += "<td id=tipoMaquinario" + i + ">" + response[i]["tipoMaquinario"] + "</td>";
                sInner += "<td id=descricao" + i + ">" + response[i]["descricao"] + "</td>";
                sInner += "<td id=statusMaquinario" + i + ">" + (response[i]["statusMaquinario"] == null ? "0" : response[i]["statusMaquinario"]) + "</td>";
                if (response[i]["dtOcupacao"] != null) {
                    dt = new Date(parseInt(response[i]["dtOcupacao"].replace('/Date(', '')));
                    sInner += "<td id=dtOcupacao" + i + ">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=dtOcupacao" + i + "></td>";
                }
                if (response[i]["dtDesocupacao"] != null) {
                    dt = new Date(parseInt(response[i]["dtDesocupacao"].replace('/Date(', '')));
                    sInner += "<td id=dtDesocupacao" + i + ">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=dtDesocupacao" + i + "></td>";
                }
                sInner += "<td id=btn" + i + "><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editModal\"><div class=\"fas fa-fw fa-pen\"></div></button></td>";

                sInner += "</tr>";
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/Maquinarios/Pesquisar?Tipo=0&" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Alterar() {
    var teste = 1;
}

