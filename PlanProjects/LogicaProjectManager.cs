using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace PlanProjects
{
    class LogicaProjectManager
    {

        #region CONEXION DATABASE

        private static string connectionString = "Data Source=DESKTOP-H2RJN7F;Initial Catalog=ProyectsManage;User=sa;Password=cero41";
        
        #endregion


        #region LISTAR OBJETO

        public static List<ProjectManager> Get()
        {
             
            var projectManagers = new List<ProjectManager>();

            string query = "SELECT * FROM ProjectManager";
            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        var projectManager = new ProjectManager();
                        projectManager.ProjectManagerID = reader.GetInt32(0);
                        projectManager.FirstName = reader.GetString(1);
                        projectManager.LastName = reader.GetString(2);
                        projectManager.Email = reader.GetString(3);
                        projectManager.Phone = reader.GetString(4);

                        projectManagers.Add(projectManager);
                    }

                    return projectManagers;
                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR " + ex.Message);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
                
            }
        }

        #endregion


        #region MOSTRAR LISTA MANAGERS

        public static void MostrarManagers()
        {
            var projectManagers = LogicaProjectManager.Get();

            var table = new ConsoleTable("Clave","Nombre","Apellido","Email","Teléfono");

            foreach(ProjectManager projectManager in projectManagers)
            {
                table.AddRow(projectManager.ProjectManagerID,projectManager.FirstName,projectManager.LastName,projectManager.Email,projectManager.Phone);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            table.Write(Format.Alternative);

            Console.WriteLine("7 regresar al menú");
        }

        #endregion


        #region METODO BUSCAR MANAGER

        public static ProjectManager BuscarManager(int id)
        {
            string query = "SELECT * FROM ProjectManager WHERE ProjectManagerID = @id";

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@id",id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var projectManager = new ProjectManager();

                        projectManager.ProjectManagerID = reader.GetInt32(0);
                        projectManager.FirstName = reader.GetString(1);
                        projectManager.LastName = reader.GetString(2);
                        projectManager.Email = reader.GetString(3);
                        projectManager.Phone = reader.GetString(4);

                        return projectManager;
                    }
                    else
                    {
                        Console.WriteLine("El manager no existe");
                        return null;
                    }
                }
                catch (Exception ex)
                {


                    Console.WriteLine("ERROR : " + ex.Message);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region METODO SOLICITAR DATOS

        public static ProjectManager SolcitarDatos(ProjectManager projectManager)
        {
            Console.WriteLine("Deseas Continuar? (s/n)");
            string respuest = Console.ReadLine();

            if(respuest.ToLower() != "s")
            {
                return null;
            }

            try
            {

                Console.Write("Escribir el nombre del manager: ");
                string nombre = Console.ReadLine();

                Console.Write("Escribir apellido del maneger: ");
                string apellido = Console.ReadLine();

                Console.Write("Escribe el correo: ");
                string correo = Console.ReadLine();

                Console.Write("Numero de teléfono: ");
                string telefono = Console.ReadLine();

                return new ProjectManager
                {
                    FirstName = nombre,
                    LastName = apellido,
                    Email = correo,
                    Phone = telefono,
                };

            }
            catch (Exception  ex)
            {

                Console.WriteLine("ERROR : " + ex.Message);
                return null;
            }
        }

        #endregion


        #region METODO AGREGAR MANAGER

        public static bool AgregarManager(ProjectManager projectManager)
        {
            if(projectManager == null)
            {
                return false;
            }

            using(var connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ProjectManager(FirstName,LastName,Email,Phone)VALUES(@FisrtName,@LastName,@Email,@Phone)";
                try
                {

                    connection.Open();
                    var command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@FirstName", projectManager.FirstName);
                    command.Parameters.AddWithValue("@LastName", projectManager.LastName);
                    command.Parameters.AddWithValue("@Email", projectManager.Email);
                    command.Parameters.AddWithValue("@Phone", projectManager.Phone);

                    int result = command.ExecuteNonQuery();

                    return result > 0;

                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region METODO EDITAR MANAGER

        public static bool EditarManager(int id)
        {
            var projectManager = LogicaProjectManager.BuscarManager(id);
            if (projectManager == null)
            {
                return false;
            }

            projectManager = LogicaProjectManager.SolcitarDatos(projectManager);
            using(var connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE ProjectManager SET FirstName=@FirstName,LastName=@LastName,Email=@Email,Phone=@Phone WHERE ProjectManagerID=@id";
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@FirstName",projectManager.FirstName);
                    command.Parameters.AddWithValue("@LastName", projectManager.LastName);
                    command.Parameters.AddWithValue("@Email", projectManager.Email);
                    command.Parameters.AddWithValue("@Phone", projectManager.Phone);
                    command.Parameters.AddWithValue("@id", id);


                    Console.WriteLine($"Seguro que quieres guardar los cambios en {projectManager.LastName} (s/n)");
                    string respuesta = Console.ReadLine();

                    if (respuesta.ToLower() == "s")
                    {
                        int result = command.ExecuteNonQuery();

                        return result > 0;
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada :/");
                        return false;
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        #endregion


        #region METODO BORRAR MANAGER

        public static bool BorrarManger(int id)
        {
            
            using(var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ProductManager WHERE ProjectManagerID=@id";

                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    Console.WriteLine($"Seguro que deseas borrar este elemento? (s/n)");
                    string respuesta = Console.ReadLine();

                    if (respuesta.ToLower() == "s")
                    {
                        int result = command.ExecuteNonQuery();

                        return result > 0;
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada :O");
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
               
            }
        }

        #endregion              

    }
}
