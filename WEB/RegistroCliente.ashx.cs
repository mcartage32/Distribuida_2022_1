using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO; //Librería para utilizar el system.streamreader, que permite capturar la información que viene del cliente
using Newtonsoft.Json;//Permite manipular listas tipo json y convertirlas hacia y desde una clase
using pProgramacionDistribuida.Clases;

namespace pProgramacionDistribuida.WEB
{
    /// <summary>
    /// Descripción breve de RegistroCliente
    /// </summary>
    public class RegistroCliente : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String DatosCliente;
            StreamReader oReader = new StreamReader(context.Request.InputStream);
            DatosCliente = oReader.ReadToEnd();

            //Se convierte el texto de datos del cliente a la clase cliente
            clsCliente oCliente = JsonConvert.DeserializeObject<clsCliente>(DatosCliente);

            string Respuesta;
            //Se hace un switch de la propiedad acción del cliente
            switch (oCliente.Accion.ToUpper())
            {
                case "INSERTAR":
                    Respuesta = InsertarCliente(oCliente);
                    context.Response.Write(Respuesta);
                    break;
                case "CONSULTAR":
                    //Serializamos la respuesta como un objeto json
                    context.Response.Write(JsonConvert.SerializeObject(Consultar(oCliente)));
                    break;
                case "ACTUALIZAR":
                    Respuesta = ActualizarCliente(oCliente);
                    context.Response.Write(Respuesta);
                    break;
                case "ELIMINAR":
                    Respuesta = EliminarCliente(oCliente);
                    context.Response.Write(Respuesta);
                    break;
                default:
                    Respuesta = "Acción sin definir, consulte con el administrador";
                    break;
            }
        }
        private clsCliente Consultar(clsCliente oCliente)
        {
            oCliente.Consultar();
            return oCliente;
        }
        private string InsertarCliente(clsCliente oCliente)
        {
            if (oCliente.Insertar())
            {
                return "Registro ingresado con éxito";
            }
            else
            {
                return oCliente.Error;
            }
        }
        private string ActualizarCliente(clsCliente oCliente)
        {
            if (oCliente.Actualizar())
            {
                return "Registro actualizado con éxito";
            }
            else
            {
                return oCliente.Error;
            }
        }
        private string EliminarCliente(clsCliente oCliente)
        {
            if (oCliente.Eliminar())
            {
                return "Registro eliminado con éxito";
            }
            else
            {
                return oCliente.Error;
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


//Responde al cliente la información
/*context.Response.Write("Documento: " + oCliente.Documento + ", " + "Nombre: " + oCliente.Nombre + " " +
    oCliente.PrimerApellido + " " + oCliente.SegundoApellido + ", " + "Correo: " + oCliente.Email + 
    ", Dirección: " + oCliente.Direccion + ", Teléfono: " + oCliente.Telefono + ", Fecha de nacimiento: " + 
    oCliente.FechaNacimiento.ToString("yyyy-MMM-dd") + ", Clave: " + oCliente.Clave);*/
/*if (oCliente.Insertar())
{
    context.Response.Write("Registro ingresado con éxito");
}
else
{
    context.Response.Write(oCliente.Error);
}*/
