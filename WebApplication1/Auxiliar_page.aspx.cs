using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadSession();
            deleteSessions();
        }

        private void loadSession()
        {
            String nombre = (String)(Session["Nombre"]);
            String apellido = (String)(Session["Apellido"]);
            // Asignacion de la informacion a los campos HTML respectivos
            LabelUsuario.Text = "Enviado por Sesion: ";
            LabelNombre.Text = "Nombre: " + nombre;
            LabelApellido.Text = " Apellido: " + apellido;
        }
        private void deleteSessions()
        {
            Session.RemoveAll();
            Session.Abandon();
        }
        protected void ButtonMostrarCookie_Click(object sender, EventArgs e)
        {
            string email = Request.Cookies["Email"]?.Value;
            string requerimiento = Request.Cookies["Requerimiento"]?.Value;

            string textoGenerado = "Email: " + email + "\nRequerimientos: " + requerimiento;

            cookieinfo.Text = textoGenerado;
        }

        [WebMethod]
        public static String getInformation(String valor)
        {
            return "Desde el servidor se recibio :" + valor;
        }
    }
}