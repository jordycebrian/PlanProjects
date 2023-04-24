using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace PlanProjects
{
    public class LogicaProject
    {

        #region CONEXIÓN DATABASE

        private static string connectionString = "Data Source=DESKTOP-H2RJN7F;Initial Catalog=ProyectsManage;User=sa;Password=cero41";

        #endregion


        #region LISTAR OBJETO

        public static List<Project>Get()
        {
            List<Project> projects = new List<Project>();

            using(var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Projects";
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        var project = new Project();
                        project.ProjectID = reader.GetInt32(0);
                        project.ProjectName = reader.GetString(1);
                        project.ProjectDescription = reader.GetString(2);
                        project.StartDate = reader.GetDateTime(3);
                        project.EndDate = reader.GetDateTime(4);
                        project.ProjectManagerID = reader.GetInt32(5);
                        project.TemaLeadID = reader.GetInt32(6);
                        project.Budget = reader.GetDecimal(7);
                        project.ActualCost = reader.GetDecimal(8);
                        project.StatusProject = reader.GetString(9);

                        projects.Add(project);
                    }
                    return projects;
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


        #region METODO BUSCAR PROYECTO POR CLAVE

        public static Project BuscarSoloUnProyecto(int id)
        {
            string query = "SELECT * FROM Projects WHERE ProjectID=@id";
            using var connection = new SqlConnection(connectionString);
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var project = new Project();
                        project.ProjectID = reader.GetInt32(0);
                        project.ProjectName = reader.GetString(1);
                        project.ProjectDescription = reader.GetString(2);
                        project.StartDate = reader.GetDateTime(3);
                        project.EndDate = reader.GetDateTime(4);
                        project.ProjectManagerID = reader.GetInt32(5);
                        project.TemaLeadID = reader.GetInt32(6);
                        project.Budget = reader.GetDecimal(7);
                        project.ActualCost = reader.GetDecimal(8);
                        project.StatusProject = reader.GetString(9);

                        return project;
                    }
                    else
                    {
                        Console.WriteLine("El proyecto no existe :/");
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


        #region METODO MOSTRAR LISTA PROYECTOS

        public static void MostrarProyectos()
        {
            List<Project> projects = LogicaProject.Get();

            
            var table = new ConsoleTable("Codigo","Nombre Proyecto","Descripción","Fecha de Inicio","Finaliza","Presupuesto","Costo Actual","Estado");

            foreach (Project project in projects)
            {
              
                table.AddRow(
                    project.ProjectID,
                    project.ProjectName,
                    project.ProjectDescription.Substring(0,5) + "...",
                    project.StartDate.ToShortDateString(),
                    project.EndDate.ToShortDateString(),
                    project.Budget,
                    project.ActualCost,
                    project.StatusProject
                    );
            
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            table.Write(Format.Alternative);

            Console.WriteLine("\nPresiona 5 para regresar al menú");
        }

        #endregion


        #region METODO PEDIR DATOS PROYECTO

        public static Project SolicitarDatosParaProyecto(Project project)
        { 

            Console.WriteLine("\nDeseas continuar? (s/n): " );
            string respuesta = Console.ReadLine();

            if (respuesta.ToLower() != "s")
            {
                return null;
            }
            try
            {
                Console.Write("Ingresa el nombre del proyecto: ");
                string nombre = Console.ReadLine();

                Console.Write("Ingrese la Descripción del proyecto: ");
                string descripcion = Console.ReadLine();

                Console.Write("Ingresar fecha de inicio del proyecto (yyyy-mm-dd): ");
                DateTime fechaInicio;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaInicio))
                {
                    Console.WriteLine("Fecha inválida. Por favor ingrese una fecha válida en formato yyyy-mm-dd.");
                }

                Console.Write("Ingresar fecha de entrega final del proyecto (yyyy-mm-dd): ");
                DateTime fechaFinal;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaFinal))
                {
                    Console.WriteLine("Fecha inválida. Por favor ingrese una fecha válida en formato yyyy-mm-dd.");
                }

                Console.Write("Clave de Gerente de proyecto: ");
                int projectManagerID = int.Parse(Console.ReadLine());

                Console.Write("Clave de lider de proyecto: ");
                int TeamLeadID = int.Parse(Console.ReadLine());

                Console.Write("Presupuesto del proyecto: ");
                decimal budget = decimal.Parse(Console.ReadLine());

                Console.Write("Costo actual del proyecto: ");
                decimal actualCost = decimal.Parse(Console.ReadLine());

                Console.Write("Estatus del proyecto: ");
                string estatus = Console.ReadLine();

                if(fechaInicio >= fechaFinal)
                {
                    Console.WriteLine("ERROR fecha inicio debe ser menor a la fecha de entrega");
                    return null;
                }

                return new Project()
                {
                    ProjectName = nombre,
                    ProjectDescription = descripcion,
                    StartDate = fechaInicio,
                    EndDate = fechaFinal,
                    ProjectManagerID = projectManagerID,
                    TemaLeadID = TeamLeadID,
                    Budget = budget,
                    ActualCost = actualCost,
                    StatusProject = estatus,
                };
            }
            catch (Exception ex)
            {

                Console.WriteLine("Ha ocurrido un error al ingresar los datos.Por favor intentelo de nuevo\n" + ex.Message);
                return null;

            }
            

        }

        #endregion


        #region METODO AGREGAR PROYECTO

        public static bool AgregarProyecto(Project project)
        {
            if (project == null)
            {
                return false;
            }

            

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO Projects(ProjectName,ProjectDescription,StartDate,EndDate,ProjectManagerID,TeamLeadID,Budget,ActualCost,StatusProject)VALUES(@ProjectName,@ProjectDescription,@StartDate,@EndDate,@ProjectManagerID,@TeamLeadID,@Budget,@ActualCost,@StatusProject)";

                    connection.Open();
                    var command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@ProjectName",project.ProjectName);
                    command.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);
                    command.Parameters.AddWithValue("@StartDate",project.StartDate);
                    command.Parameters.AddWithValue("@EndDate", project.EndDate);
                    command.Parameters.AddWithValue("@ProjectManagerID", project.ProjectManagerID);
                    command.Parameters.AddWithValue("@TeamLeadID", project.TemaLeadID);
                    command.Parameters.AddWithValue("@Budget", project.Budget);
                    command.Parameters.AddWithValue("@ActualCost", project.ActualCost);
                    command.Parameters.AddWithValue("@StatusProject", project.StatusProject);

                    Console.WriteLine($"Seguro que quieres agregar el proyecto {project.ProjectName} (s/n)");
                    string respuesta = Console.ReadLine();

                    if(respuesta.ToLower() == "s")
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

                    Console.WriteLine("ERROR " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region METODO EDITAR PROYECTO

        public static bool EditarProyecto(int id)
        {
            Project project = BuscarSoloUnProyecto(id);
            if (project == null)
            {

                return false;

            }

            project = SolicitarDatosParaProyecto(project);
            if (project == null)
            {
                return false;
            }
            
            string query = "UPDATE Products SET " +
                  "ProjectName=@ProjectName,ProjetcDescription=@ProjectDescription,StartDate=@StartDate,EndDate=@EndDate,ProjectManagerID=@ProjectManagerID,TeamLeadID=@TeamLeadID,Budget=@Budget,ActualCost=@ActualCost,StatusProject=@StatusProject WHERE ProjectID=@id";
            using var connection = new SqlConnection(connectionString);
            {
                try
                {

                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    command.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);
                    command.Parameters.AddWithValue("@StartDate", project.StartDate);
                    command.Parameters.AddWithValue("@EndDate", project.EndDate);
                    command.Parameters.AddWithValue("@Budget", project.Budget);
                    command.Parameters.AddWithValue("@PorjectManagerID", project.ProjectManagerID);
                    command.Parameters.AddWithValue("@TeamLeadID", project.TemaLeadID);
                    command.Parameters.AddWithValue("@ActualCost", project.ActualCost);
                    command.Parameters.AddWithValue("@StatusProject", project.StatusProject);
                    command.Parameters.AddWithValue("@id", id);

                    Console.WriteLine("Deseas guardar los cambios editados? (s/n)");
                    string respuesta = Console.ReadLine();

                    if (respuesta.ToLower() != "n")
                    {
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada");
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


        #region METODO BORRAR PROYECTO

        public static bool BorrarProyecto(int id)
        {
            var project = BuscarSoloUnProyecto(id);
            if (project == null)
            {
                return false;
            }
            using var connection = new SqlConnection(connectionString);
            {
                string query = "DELETE FROM Projects WHERE ProjectID=@id";
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id",id);

                    Console.WriteLine($"Seguro que quieres borrar el proyecto {project.ProjectName}? (s/n)");
                    string respuesta = Console.ReadLine();

                    if(respuesta.ToLower() != "n")
                    {
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada");
                        return false;
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR :" + ex.Message);
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
