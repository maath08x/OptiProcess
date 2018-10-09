function pesquisarMensal() {

   
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            for (var i = 0; i < response.length; i++) {

                var pedidoId = response[i]["pedidoId"];
                var nome = response[i]["nome"];
                var leadTime = response[i]["leadTime"];
                var qntEstoque = response[i]["qntEstoque"];
                var qntEstoqueReservado = response[i]["qntEstoqueReservado"];
                var QntPedidoTotal = response[i]["QntPedidoTotal"];
                var dtPedido = response[i]["dtPedido"];
                var dtPrevisao = response[i]["dtPrevisao"];
            }
        }
    }


    var sURL = "http://" + location.host + "/Home/DashboardMensal?";

    xhttp.open("GET", sURL, true);
    xhttp.send();


}


function pesquisarProduto() {



    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            for (var i = 0; i < response.length; i++) {

                var pedidoId = response[i]["pedidoId"];
                var nome = response[i]["nome"];
                var leadTime = response[i]["leadTime"];
                var qntEstoque = response[i]["qntEstoque"];
                var qntEstoqueReservado = response[i]["qntEstoqueReservado"];
                var QntPedidoTotal = response[i]["QntPedidoTotal"];
                var dtPedido = response[i]["dtPedido"];
                var dtPrevisao = response[i]["dtPrevisao"];
            }
        }
    }


    var sURL = "http://" + location.host + "/Home/DashboardProduto?";

    xhttp.open("GET", sURL, true);
    xhttp.send();


}





function pesquisarDiario() {

    //var usuario = window.localStorage.getItem("usuario");

    //var sRequest = "Usuario=" + usuario;


    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            for (var i = 0; i < response.length; i++) {

                var pedidoId = response[i]["pedidoId"];
                var nome = response[i]["nome"];
                var leadTime = response[i]["leadTime"];
                var qntEstoque = response[i]["qntEstoque"];
                var qntEstoqueReservado = response[i]["qntEstoqueReservado"];
                var QntPedidoTotal = response[i]["QntPedidoTotal"];
                var dtPedido = response[i]["dtPedido"];
                var dtPrevisao = response[i]["dtPrevisao"];
            }
        }
    }


    var sURL = "http://" + location.host + "/Home/DashboardDiario?";

    xhttp.open("GET", sURL, true);
    xhttp.send();


}






