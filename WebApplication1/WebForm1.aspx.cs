using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ServiceReference1;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Departamentos()
        {
            String[] cities = serviceCall();
            Array.Sort(cities);
            ciudades.Items.Add("Selecciona una opcion");
            for (int i = 0; i < cities.Length; i++)
            {
                string c = cities[i];
                ciudades.Items.Add(c);
            }
            /*string filepath = "D:\\Departamentos.txt";
            if (File.Exists(filepath))
            {
                List<string> lines = new List<string>();
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                lines.Sort();
                lines.Insert(0, "Selecciona una opcion");
                ciudades.DataSource = lines;
                ciudades.DataBind();
            }*/
        }
        private String[] serviceCall()
        {
            Service1Client client = new Service1Client();
            String[] ciudades = client.getCiudades();

            return ciudades;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Departamentos();
            }
        }
        protected void Limpiar_contenido()
        {
            TextboxNombre.Text = "";
            TextboxlApellido.Text = "";
            RadioButtonM.Checked = false;
            RadioButtonF.Checked = false;
            TextboxEmail.Text = "";
            TextboxDireccion.Text = "";
            ciudades.SelectedIndex = 0;
            TextboxRequerimiento.Text = "";
        }
        protected void Button_Enviar_click(object sender, EventArgs e)
        {
            string nombre = TextboxNombre.Text;
            string apellido = TextboxlApellido.Text;
            string sexo = RadioButtonM.Checked ? "Masculino" : "Femenino";
            string email = TextboxEmail.Text;
            string direccion = TextboxDireccion.Text;
            int ciudad = ciudades.SelectedIndex;
            string requerimiento = TextboxRequerimiento.Text;
            string respuesta = "Nombre: " + nombre + "\nApellido: " + apellido + "\nSexo: " + sexo + "\nEmail: " + email + "\nDireccion: " + direccion
                + "\nCiudad: " + ciudad + "\nRequerimiento: " + requerimiento;
            TextBoxRpta.Visible = true;
            TextBoxRpta.Text = respuesta;
            Service1Client client = new Service1Client();
            client.Information(nombre, apellido, sexo, email, direccion, ciudad, requerimiento);
            createSession(nombre, apellido);
            createCookie(email, sexo, direccion, requerimiento);
            Limpiar_contenido();
            Response.Redirect("Auxiliar_page.aspx");
        }
        private void createSession(String nombre, String apellido)
        {
            Session["Nombre"] = nombre;
            Session["Apellido"] = apellido;
        }
        private void createCookie(String email, String sexo, String direccion, String requerimiento)
        {
            HttpCookie cookie1 = new HttpCookie("Email", email);
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie("Sexo", sexo);
            Response.Cookies.Add(cookie2);
            HttpCookie cookie3 = new HttpCookie("Direccion", direccion);
            Response.Cookies.Add(cookie3);
            HttpCookie cookie4 = new HttpCookie("Requerimiento", requerimiento);
            Response.Cookies.Add(cookie4);
        }
    }
}