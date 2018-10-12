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