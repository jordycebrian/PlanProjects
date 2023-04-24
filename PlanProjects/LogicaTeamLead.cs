using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanProjects
{
    class LogicaTeamLead
    {

        #region CONEXION DATABASE

        private static string connectionString = "Data Source=DESKTOP-H2RJN7F;Initial Catalog=ProyectsManage;User=sa;Password=cero41";

        #endregion


        #region LISTAR OBJETO

        public static List<TeamLead> Get()
        {

            var teamLeads = new List<TeamLead>();

            string query = "SELECT * FROM TeamLead";
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        var teamLead = new TeamLead();
                        teamLead.TeamLeadID = reader.GetInt32(0);
                        teamLead.FirstName = reader.GetString(1);
                        teamLead.LastName = reader.GetString(2);
                        teamLead.Email = reader.GetString(3);
                        teamLead.Phone = reader.GetString(4);
                        teamLead.TeamLeadType = reader.GetString(5);

                        teamLeads.Add(teamLead);
                    }

                    return teamLeads;
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


        #region MOSTRAR LISTA TeamLeads

        public static void MostrarTeamLeads()
        {
            var teamLeads = LogicaTeamLead.Get();

            var table = new ConsoleTable("Clave", "Nombre", "Apellido", "Email", "Teléfono", "Tipo lider");

            foreach (TeamLead teamLead in teamLeads)
            {
                table.AddRow(teamLead.TeamLeadID, teamLead.FirstName, teamLead.LastName, teamLead.Email, teamLead.Phone, teamLead.TeamLeadType);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            table.Write(Format.Alternative);

            Console.WriteLine("7 regresar al menú");
        }

        #endregion


        #region METODO BUSCAR TEAM LEAD

        public static TeamLead BuscarTeamLeader(int id)
        {
            string query = "SELECT * FROM TeamLead WHERE TeamLeadID = @id";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var teamLead = new TeamLead();
                        teamLead.TeamLeadID = reader.GetInt32(0);
                        teamLead.FirstName = reader.GetString(1);
                        teamLead.LastName = reader.GetString(2);
                        teamLead.Email = reader.GetString(3);
                        teamLead.Phone = reader.GetString(4);
                        teamLead.TeamLeadType = reader.GetString(5);

                        return teamLead;
                    }
                    else
                    {
                        Console.WriteLine($"El Lider de equipo no existe");
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

        public static TeamLead SolicitarDatos(TeamLead teamLead)
        {
            Console.WriteLine("Deseas Continuar? (s/n)");
            string respuest = Console.ReadLine();

            if (respuest.ToLower() == "n")
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

                Console.Write("Especialidad del lider: ");
                string especialidad = Console.ReadLine();

                return new TeamLead
                {
                    FirstName = nombre,
                    LastName = apellido,
                    Email = correo,
                    Phone = telefono,
                    TeamLeadType = especialidad,
                };

            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR : " + ex.Message);
                return null;
            }
        }

        #endregion


        #region METODO AGREGAR Team Lead

        public static bool AgregarTeamLead(TeamLead teamLead)
        {
            if (teamLead == null)
            {
                return false;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TeamLead(FirstName,LastName,Email,Phone,TeamLeadType)VALUES(@FirstName,@LastName,@Email,@Phone,@TeamLeadType)";
                try
                {

                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", teamLead.FirstName);
                    command.Parameters.AddWithValue("@LastName", teamLead.LastName);
                    command.Parameters.AddWithValue("@Email", teamLead.Email);
                    command.Parameters.AddWithValue("@Phone", teamLead.Phone);
                    command.Parameters.AddWithValue("@TeamLeadType", teamLead.TeamLeadType);

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


        #region METODO EDITAR Team Lead

        public static bool EditarTeamLead(int id)
        {
            var teamLead = BuscarTeamLeader(id);
            if (teamLead == null)
            {
                return false;
            }

            teamLead = LogicaTeamLead.SolicitarDatos(teamLead);
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE TeamLead SET FirstName=@FirstName,LastName=@LastName,Email=@Email,Phone=@Phone,TeamLeadType=@TeamLeadType WHERE TeamLeadID=@id";
                try
                {
                    connection.Open(); 
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", teamLead.FirstName);
                    command.Parameters.AddWithValue("@LastName", teamLead.LastName);
                    command.Parameters.AddWithValue("@Email", teamLead.Email);
                    command.Parameters.AddWithValue("@Phone", teamLead.Phone);
                    command.Parameters.AddWithValue("@TeamLeadType", teamLead.TeamLeadType);
                    command.Parameters.AddWithValue("@id", id);


                    Console.WriteLine($"Seguro que quieres guardar los cambios (s/n)");
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


        #region METODO BORRAR Team Lead

        public static bool BorrarTeamLead(int id)
        {
            var teamLead = BuscarTeamLeader(id);
            if (teamLead == null)
            {
                return false;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TeamLead WHERE TeamLeadID=@id";

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
