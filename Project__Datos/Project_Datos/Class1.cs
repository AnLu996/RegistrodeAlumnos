using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Windows.Input;

namespace ProjectDatos
{
    class DBconnection
    {
        public static void Main(string[] args)
        {
            DBconnection dBconnection = new DBconnection();
            dBconnection.query();

            Console.ReadKey();
        }

        private void query()
        {
            Ciudad ciudad = new Ciudad();
            IList<string> ciudades = ciudad.getCiudades();

            if (ciudades == null)
            {
                Console.WriteLine("No data");
                return;
            }

            for (int i = 0; i < ciudades.Count; i++)
            {
                Console.WriteLine(ciudades[i]);
            }
            DataAlumnos dataAlumnos = new DataAlumnos();
            Console.WriteLine(dataAlumnos.ExisteRegistro("Giomar", "Muñoz Curi"));
        }
    }
    public class Ciudad
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RegistroAlumnos;Integrated Security=True";

        public IList<string> getCiudades()
        {
            List<string> ciudades = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select Ciudad from DataCiudad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string ciudad = reader.GetString(0);
                        ciudades.Add(ciudad);
                    }

                    reader.Close();
                }
            }
            return ciudades;
        }

    }
    public class DataAlumnos
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RegistroAlumnos;Integrated Security=True";

        public void Ingregar_Alumnos(string nombre, string apellido, string sexo, string email, string direccion, int ciudad, string requerimiento)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO DataAlumnos VALUES(@Nombre, @Apellidos, @Sexo, @Email, @Direccion, @Codeciudad, @Requerimiento)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellidos", apellido);
                    command.Parameters.AddWithValue("@Sexo", sexo);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Codeciudad", ciudad);
                    command.Parameters.AddWithValue("@Requerimiento", requerimiento);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public bool ExisteRegistro(string nombre, string apellidos)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Code FROM DataAlumnos WHERE Nombre = @Nombre AND Apellidos = @Apellidos";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellidos", apellidos);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dato = reader["Code"];
                                count = int.Parse(dato.ToString());
                            }
                        }
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en la consulta: " + ex.Message);
                    return false;
                }
            }
        }
    }
}