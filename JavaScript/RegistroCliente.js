//Función para garantizar que el código se ejecute cuando se termine de cargar la página
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnIngresar").click(function () {
        //Limpiar el mensaje de error
        ProcesarCliente("Insertar");
    });
});

function ProcesarCliente(Comando) {
    //Limpiar el mensaje de error
    $("#dvMensaje").html("");

    //Capturar datos de entrada
    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombres").val();
    var PrimerApellido = $("#txtPrimerApellido").val();
    var SegundoApellido = $("#txtSegundoApellido").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();
    var FechaNacimiento = $("#txtFechaNacimiento").val();
    var Email = $("#txtEmail").val();
    var Clave = $("#txtClave").val();
    var ConfirmaClave = $("#txtConfirmarClave").val();
    var Accion = Comando;
    //Verificamos si la clave y la confirmación son iguales
    if (Clave !== ConfirmaClave) {
        $("#dvMensaje").html("Las claves no coinciden, por favor verifique la información");
        return;
    }
    //Despues de capturar los datos, el tipo de dato fecha, no acepta null
    FechaNacimiento === '' ? FechaNacimiento = "1900/01/01" : FechaNacimiento = FechaNacimiento;
    //alert("Doc: " + Documento + ", Nombre: " + Nombre);

    //Construimos el json
    var DatosCliente = {
        documento: Documento,
        nombre: Nombre,
        primerapellido: PrimerApellido,
        segundoapellido: SegundoApellido,
        direccion: Direccion,
        telefono: Telefono,
        fechanacimiento: FechaNacimiento,
        email: Email,
        clave: Clave,
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
        url: "../WEB/RegistroCliente.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosCliente),
        dataType: TipoRespuesta,
        success: function (RepuestaCliente) {
            if (Comando === "Consultar") {
                $("#txtNombres").val(RepuestaCliente["Nombre"]);
                $("#txtPrimerApellido").val(RepuestaCliente["PrimerApellido"]);
                $("#txtSegundoApellido").val(RepuestaCliente["SegundoApellido"]);
                $("#txtDireccion").val(RepuestaCliente["Direccion"]);
                $("#txtTelefono").val(RepuestaCliente["Telefono"]);
                $("#txtEmail").val(RepuestaCliente["Email"]);
                //Para capturar solo la fecha, se hace un split de la respuesta y se captura la primera parte
                //donde está la fecha, en la segunda parte queda el tiempo
                var FechaNcto = RepuestaCliente["FechaNacimiento"].split('T')[0];
                $("#txtFechaNacimiento").val(FechaNcto);
            }
            else {
                $("#dvMensaje").html("Respuesta: " + RepuestaCliente);
            }
        },
        error: function (RepuestaCliente) {
            $("#dvMensaje").html("Error: " + RepuestaCliente);
        }
    });
}
function Limpiar() {
    $("#txtDocumento").val("");
    $("#txtNombres").val("");
    $("#txtPrimerApellido").val("");
    $("#txtSegundoApellido").val("");
    $("#txtDireccion").val("");
    $("#txtTelefono").val("");
}
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnActualizar").click(function () {
        ProcesarCliente("Actualizar");
    });
});
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnEliminar").click(function () {
        ProcesarCliente("Eliminar");
        Limpiar();
    });
});
$(document).ready(function () {
    //Función para capturar el click del botón
    $("#btnConsultar").click(function () {
        ProcesarCliente("Consultar");
    });
});
