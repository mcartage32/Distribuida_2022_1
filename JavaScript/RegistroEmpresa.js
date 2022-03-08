//Función para garantizar que el código se ejecute cuando se termine de cargar la página
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnIngresar").click(function () {
        //Limpiar el mensaje de error
        ProcesarEmpresa("Insertar");
    });
});
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnActualizar").click(function () {
        ProcesarEmpresa("Actualizar");
    });
});
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnEliminar").click(function () {
        ProcesarEmpresa("Eliminar");
        Limpiar();
    });
});
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnConsultar").click(function () {
        ProcesarEmpresa("Consultar");
    });
});
function ProcesarEmpresa(Comando) {
    //Limpiar el mensaje de error
    $("#dvMensaje").html("");

    //Capturar datos de entrada
    var IdEmpresa = $("#txtIdEmpresa").val();
    var NIT = $("#txtNIT").val();
    var Nombre = $("#txtNombre").val();
    var Email = $("#txtEmail").val();
    var SitioWEB = $("#txtSitioWEB").val();
    var Telefono = $("#txtTelefono").val();
    var Accion = Comando;

    //Construimos el json
    var DatosEmpresa = {
        IdEmpresa: IdEmpresa,
        NIT: NIT,
        Nombre: Nombre,
        Email: Email,
        SitioWEB: SitioWEB,
        Telefono: Telefono,
        accion: Accion
    };
    var TipoRespuesta;
    if (Comando === "Consultar") {
        TipoRespuesta = "json";
    }
    else {
        TipoRespuesta = "html";
    }
    //Enviar la información al servidor vía ajax con el objeto $.ajax()
    $.ajax({
        type: "POST",
        url: "../WEB/ControladorEmpresa.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosEmpresa),
        dataType: TipoRespuesta,
        success: function (RepuestaEmpresa) {
            if (Comando === "Consultar") {
                $("#txtIdEmpresa").val(RepuestaEmpresa["IdEmpresa"]);
                $("#txtNIT").val(RepuestaEmpresa["NIT"]);
                $("#txtNombre").val(RepuestaEmpresa["Nombre"]);
                $("#txtEmail").val(RepuestaEmpresa["Email"]);
                $("#txtSitioWEB").val(RepuestaEmpresa["SitioWEB"]);
                $("#txtTelefono").val(RepuestaEmpresa["Telefono"]);
            }
            else {
                $("#dvMensaje").html("Respuesta: " + RepuestaEmpresa);
            }
        },
        error: function (RepuestaEmpresa) {
            $("#dvMensaje").html("Error: " + RepuestaEmpresa);
        }
    });
}