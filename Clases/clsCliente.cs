using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;

namespace pProgramacionDistribuida.Clases
{
    public class clsCliente
    {
        #region Propiedades
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Error { get; private set; }
        public string Accion { get; set; }
        private string SQL;
        #endregion
        #region Metodos
        public bool Insertar()
        {
            SQL = "INSERT INTO tblCliente(Documento, Nombre, PrimerApellido, SegundoApellido, Direccion, Telefono, " +
                                          "FechaNacimiento, CorreoElectronico, Clave) " +
                  "VALUES (@prDocumento, @prNombre, @prPrimerApellido, @prSegundoApellido, @prDireccion, @prTelefono, " +
                                          "@prFechaNacimiento, @prCorreoElectronico, @prClave)";

            //Se crea el objeto de conexión, para pasar la instrucción SQL y los parámetros de la consulta
            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            //Se definen los parámetros
            oConexion.AgregarParametro("@prDocumento", Documento);
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prPrimerApellido", PrimerApellido);
            oConexion.AgregarParametro("@prSegundoApellido", SegundoApellido);
            oConexion.AgregarParametro("@prDireccion", Direccion);
            oConexion.AgregarParametro("@prTelefono", Telefono);
            oConexion.AgregarParametro("@prFechaNacimiento", FechaNacimiento);
            oConexion.AgregarParametro("@prCorreoElectronico", Email);
            oConexion.AgregarParametro("@prClave", Clave);

            //Se ejecuta la instrucción SQL
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
        public bool Consultar()
        {
            SQL = "SELECT   Nombre, PrimerApellido, SegundoApellido, Direccion," +
                           "Telefono, CorreoElectronico, Clave, FechaNacimiento " +
                  "FROM     tblCliente " +
                  "WHERE    Documento=@prDocumento";
            //Se crea el objeto de conexión, para pasar la instrucción SQL y los parámetros de la consulta
            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            //Se definen los parámetros
            oConexion.AgregarParametro("@prDocumento", Documento);
            //Consultar en la base de datos
            if (oConexion.Consultar())
            {
                //Se revisa si hay datos
                if (oConexion.Reader.HasRows)
                {
                    //Se obliga a leer los datos
                    oConexion.Reader.Read();
                    //Se capturan los datos en las propiedades / atributos de la clase
                    Nombre = oConexion.Reader.GetString(0);
                    PrimerApellido = oConexion.Reader.GetString(1);
                    SegundoApellido = oConexion.Reader.GetString(2);
                    Direccion = oConexion.Reader.GetString(3);
                    Telefono = oConexion.Reader.GetString(4);
                    Email = oConexion.Reader.GetString(5);
                    Clave = oConexion.Reader.GetString(6);
                    FechaNacimiento = oConexion.Reader.GetDateTime(7);
                    return true;
                }
                else
                {
                    //NO hay datos, es un error, se levanta el error para el usuario final
                    Error = "No hay datos de cliente con el documento: " + Documento;
                    return false;
                }
            }
            else
            {
                //Captura el error y retorna falseo
                Error = oConexion.Error;
                return false;
            }
        }
        public bool Actualizar()
        {
            SQL = "UPDATE       tblCliente " +
                  "SET          Nombre=@prNombre, " +
                               "PrimerApellido=@prPrimerApellido, " +
                               "SegundoApellido=@prSegundoApellido, " +
                               "Direccion=@prDireccion, " +
                               "Telefono=@prTelefono, " +
                               "FechaNacimiento=@prFechaNacimiento, " +
                               "CorreoElectronico=@prCorreoElectronico, " +
                               "Clave=@prClave " +
                  "WHERE        Documento=@prDocumento";

            //Se crea el objeto de conexión, para pasar la instrucción SQL y los parámetros de la consulta
            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            //Se definen los parámetros
            oConexion.AgregarParametro("@prDocumento", Documento);
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prPrimerApellido", PrimerApellido);
            oConexion.AgregarParametro("@prSegundoApellido", SegundoApellido);
            oConexion.AgregarParametro("@prDireccion", Direccion);
            oConexion.AgregarParametro("@prTelefono", Telefono);
            oConexion.AgregarParametro("@prFechaNacimiento", FechaNacimiento);
            oConexion.AgregarParametro("@prCorreoElectronico", Email);
            oConexion.AgregarParametro("@prClave", Clave);

            //Se ejecuta la instrucción SQL
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
        public bool Eliminar()
        {
            SQL = "DELETE FROM  tblCliente " +
                  "WHERE        Documento=@prDocumento";

            //Se crea el objeto de conexión, para pasar la instrucción SQL y los parámetros de la consulta
            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            //Se definen los parámetros
            oConexion.AgregarParametro("@prDocumento", Documento);

            //Se ejecuta la instrucción SQL
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
        #endregion
    }
}