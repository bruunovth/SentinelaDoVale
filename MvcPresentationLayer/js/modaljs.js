
$(document).ready(function () {


    $('#modalServe').click(function abrirModalParaQueServe() {
        $("#myModalComoFunciona").modal('hide');
        $("#myModalComoVirarModerador").modal('hide');
        $("#myModalOcorrencia").modal('hide');
        $("#myModalParaQueServe").modal('show');
    });
    $('#modalComoFunciona').click(function abrirModalComoFunciona() {
        $("#myModalParaQueServe").modal('hide');
        $("#myModalComoVirarModerador").modal('hide');
        $("#myModalOcorrencia").modal('hide');
        $("#myModalComoFunciona").modal('show');
    });
    $('#modalComoPontuar').click(function abrirModalComoVirarModerador() {
        $("#myModalComoFunciona").modal('hide');
        $("#myModalParaQueServe").modal('hide');
        $("#myModalOcorrencia").modal('hide');
        $("#myModalComoVirarModerador").modal('show');
    });
    $('#modalOcorrencia').click(function abrirModalOcorrencia() {
        $("#myModalComoFunciona").modal('hide');
        $("#myModalParaQueServe").modal('hide');
        $("#myModalComoVirarModerador").modal('hide');
        $("#myModalOcorrencia").modal('show');
    });


   
});

function abrirModalParaQueServe() {
    $("#myModalComoFunciona").modal('hide');
    $("#myModalComoVirarModerador").modal('hide');
    $("#myModalOcorrencia").modal('hide');
    $("#myModalParaQueServe").modal('show');
}

function abrirModalComoFunciona() {
    $("#myModalParaQueServe").modal('hide');
    $("#myModalComoVirarModerador").modal('hide');
    $("#myModalOcorrencia").modal('hide');
    $("#myModalComoFunciona").modal('show');
}

function abrirModalComoVirarModerador() {
    $("#myModalComoFunciona").modal('hide');
    $("#myModalParaQueServe").modal('hide');
    $("#myModalOcorrencia").modal('hide');
    $("#myModalComoVirarModerador").modal('show');
}

function abrirModalOcorrencia() {
    $("#myModalComoFunciona").modal('hide');
    $("#myModalParaQueServe").modal('hide');
    $("#myModalComoVirarModerador").modal('hide');
    $("#myModalOcorrencia").modal('show');
}