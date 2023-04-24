using System;
using System.Collections.Generic;

namespace PlanProjects
{
    public class Program
    {
        
        static void Main()
        {
            

            #region menu
            string menu = @"
               -----------------------------------------------------------------------------
                                                
                                                 MENÚ                   

               -----------------------------------------------------------------------------
               [Ventana Proyecto] 
                                        OPCIONES DE PROYECTO

                                    [1] Para ver lista de Proyectos
                                    [2] Para Agregar nuevo Proyecto
                                    [3] Para Editar un Proyecto
                                    [4] Para Borrar un Proyecto
                                    [5] Para Regresar al menú
                                    [6] Para ver los Managers de Proyectos
                                    [7] Para Buscar un proyecto especifico
                                    [8] Para ver Lideres de Proyecto
                                    [9] Para Salir de la Aplicación

               -----------------------------------------------------------------------------";
            #endregion 

            int opcion = 0;
            bool running = true;



            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(menu);
            Console.ResetColor();


            while (running)
            {
                

                Console.Write("\n\nIntroduce la opción a realizar: ");
                string respuesta = Console.ReadLine();

                if (int.TryParse(respuesta, out opcion))
                {
                    #region LISTA DE PROYECTOS

                    if (opcion == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Proyectos Almacenados\n\n");
                        LogicaProject.MostrarProyectos();

                    }

                    #endregion


                    #region BUSCAR PROYECTO INDIVIDUALMENTE

                    if (opcion == 7)
                    {
                        Console.Clear();
                        Console.WriteLine("5 para regresar al menú\n\n");

                        
                        Console.Write($"Introduce la clave del producto: ");
                        int id = int.Parse(Console.ReadLine());

                        Project project = LogicaProject.BuscarSoloUnProyecto(id);

                        if (project != null)
                        {


                            Console.WriteLine($"\nClave: {project.ProjectID}");
                            Console.WriteLine($"Nombre del Proyecto: {project.ProjectName}");
                            Console.WriteLine($"Descripción: {project.ProjectDescription}");
                            Console.WriteLine($"Fecha de Inicio: {project.StartDate.ToShortDateString()}");
                            Console.WriteLine($"Fecha de finalización: {project.EndDate.ToShortDateString()}");
                            Console.WriteLine($"Presupesto: {project.Budget}");
                            Console.WriteLine($"Costo actual: {project.ActualCost}");
                            Console.WriteLine($"Status del Proyecto: {project.StatusProject}");
                            

                        }

                        
                        
                    }

                    #endregion


                    #region AGREGAR PROYECTO NUEVO

                    if (opcion == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("5 Para regresar al menú\n");
                        Console.Write("AGREGAR NUEVO PROYECTO\n");
                        

                        var project = new Project();
                        project = LogicaProject.SolicitarDatosParaProyecto(project);
                        bool resultado = LogicaProject.AgregarProyecto(project);

                        if (resultado)
                        {
                            Console.WriteLine($"Projecto {project.ProjectName} agregado!!");
                        }

                    }

                    #endregion


                    #region HACER CAMBIOS EN PROYECTO

                    if (opcion == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("QUE PROYECTO NECESITAS EDITAR\n\n");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        LogicaProject.MostrarProyectos();
                        Console.ResetColor();

                        Console.Write("Introduce la clave del proyecto a editar: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = LogicaProject.EditarProyecto(id);

                        if (result)
                        {
                            Console.WriteLine("Producto Editado Correctamente");
                        }

                        
                        
                    }

                    #endregion


                    #region BORRAR PROYECTO
                    
                    if (opcion == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("5 para regresar al menú");
                        Console.WriteLine("QUE PROYECTO DESEAS ELIMINAR");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.ResetColor();

                        Console.Write("Ingresa la clave del proyecto a eliminar: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = LogicaProject.BorrarProyecto(id);

                        if (result)
                        {

                            Console.WriteLine("Proyecto borrado con exito!!!");

                        }

                    }

                    #endregion


                    #region VOLVEL AL MENÚ

                    if(opcion == 5)
                    {
                        Console.Clear();
                        Console.WriteLine(menu);
                    }

                    #endregion


                    #region MOSTRAR VENTANA MANAGERS

                    if (opcion == 6)
                    {
                        Console.Clear();
                        VentanaManagerMain();

                    }

                    #endregion


                    #region MOSTRAR VENTANA LEADS TEAM

                    if (opcion == 8)
                    {
                        Console.Clear();
                        VentanaLiderMain();

                    }

                    #endregion


                    #region SALIR
                    if (opcion == 9)
                    {
                        running = false;
                    }
                    #endregion

                }
                else
                {
                    Console.WriteLine("OPCIÓN NO VALIDA :/");
                }

                
            }
        }


        #region VENTANA MANAGER

        static void VentanaManagerMain()
        {
            #region menu
            string menu = @"
               ----------------------------------------------------------------------------
                                                
                                                 MENÚ                   

               ----------------------------------------------------------------------------
               [Ventana Managers]                                                       
                                               OPCIONES

                                    [1] Para ver lista de Managers
                                    [2] Para agregar un nuevo manager
                                    [3] Para editar un manager
                                    [4] Para borrar un manager
                                    [5] Para regresar al menú
                                    [6] Para Buscar un manager
                                    [7] Para regresar al menú
                                    [8] Para salir
                                    
               ----------------------------------------------------------------------------";
            #endregion menu

            bool run = true;
            int opcion = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(menu);

            while (run)
            {

                
                Console.Write("\nQue opcion deseas ? ");
                string respuesta = Console.ReadLine();

                if (int.TryParse(respuesta, out opcion))
                {

                    #region MOSTRAR MANAGERS

                    if (opcion == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("LISTA DE MANAGERS\n\n");
                        LogicaProjectManager.MostrarManagers();
                    }

                    #endregion


                    #region BUSCAR MANAGER
                    if (opcion == 6)
                    {
                        Console.Clear();
                        Console.WriteLine("BUSQUEDA DE MANAGER\n");

                        Console.Write("\n\nIntroduce la clave del manager que buscas: ");
                        int id = int.Parse(Console.ReadLine());

                        ProjectManager projectManager =  LogicaProjectManager.BuscarManager(id);

                        Console.WriteLine($"Clave: {projectManager.ProjectManagerID}");
                        Console.WriteLine($"Nombre Manager: {projectManager.FirstName}");
                        Console.WriteLine($"Apellido {projectManager.LastName}");
                        Console.WriteLine($"Correo: {projectManager.Email}");
                        Console.WriteLine($"Telefono: {projectManager.Phone}");
                        Console.WriteLine("\n5 para regresar al menú");

                    }

                    #endregion


                    #region AGREGAR MANAGER

                    if (opcion == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("AGREGAR MANAGER\n");
                        Console.WriteLine("7 para regresar al menú");

                        var projectManager = new ProjectManager();

                        projectManager=LogicaProjectManager.SolcitarDatos(projectManager);
                        bool result = LogicaProjectManager.AgregarManager(projectManager);

                        if (result)
                        {
                            Console.WriteLine("Manager agregado correctamente");
                        }

                    }

                    #endregion


                    #region METODO EDITAR MANAGER
                    if(opcion == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("EDITANDO UN MANAGER");

                        LogicaProjectManager.MostrarManagers();

                        Console.Write("Escribe la clave del manager a editar: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = LogicaProjectManager.EditarManager(id);

                        if (result)
                        {
                            Console.WriteLine("Manager editado con exito!!");
                        }
                    }

                    #endregion


                    #region METODO BORRAR MANAGER

                    if (opcion == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("BORRANDO UN MANAGER\n");

                        Console.ForegroundColor = ConsoleColor.Red;
                        LogicaProjectManager.MostrarManagers();

                        Console.Write("Escribe la clave del manager a eliminar: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = LogicaProjectManager.BorrarManger(id);

                        if (result)
                        {
                            Console.WriteLine("Manager eliminado con exito!!");
                        }
                    }

                    #endregion


                    #region REGRESAR AL MENU PRINCIPAL

                    if (opcion == 7)
                    {
                        Console.Clear();
                        Console.Write(menu);
                    }

                    #endregion

                }
                else
                {
                    Console.WriteLine("Opcion no valida :/");
                }

                #region REGRESAR AL MENÚ PRINCIPAL

                if (opcion == 8)
                {
                    run = false;
                    Console.Clear();
                    Console.WriteLine("Presiona 5 para aparecer el menú");
                    
                }

                #endregion

            }

        }

        #endregion


        #region VENTANA LIDER DE EQUIPO

        static void VentanaLiderMain()
        {
            #region menu
            string menu = @"
               ----------------------------------------------------------------------------
                                                
                                                 MENÚ                   

               ----------------------------------------------------------------------------
               [Ventana LeadMain]                                                       
                                               OPCIONES

                                    [1] Para ver lista de Lideres de equipo
                                    [2] Para agregar un nuevo Lead
                                    [3] Para editar un Lead Team
                                    [4] Para borrar un Lead Team
                                    [6] Para Buscar un Team Lead
                                    [7] Para regresar al menú
                                    [8] Para salir
                                    
               ----------------------------------------------------------------------------";
            #endregion menu

            bool ran = true;
            int opcion = 0;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(menu);

            while (ran)
            {
                

                Console.Write("\nQue opcion deseas relizar? ");
                string respuesta = Console.ReadLine();

                if (int.TryParse(respuesta, out opcion))
                {

                    #region MOSTRAR Team Leads

                    if (opcion == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("LIDEREZ DE EQUIPO\n\n");

                        Console.ForegroundColor = ConsoleColor.Gray;
                        LogicaTeamLead.MostrarTeamLeads();
                        Console.ResetColor();
                    }

                    #endregion


                    #region BUSCAR Team Leads
                    if (opcion == 6)
                    {
                        Console.Clear();
                        Console.WriteLine("BUSQUEDA DE Lider de Equipo\n");

                        Console.Write("\n\nIntroduce la clave del manager que buscas: ");
                        int id = int.Parse(Console.ReadLine());

                        TeamLead teamLead = LogicaTeamLead.BuscarTeamLeader(id);

                        Console.WriteLine($"Clave: {teamLead.TeamLeadID}");
                        Console.WriteLine($"Nombre Manager: {teamLead.FirstName}");
                        Console.WriteLine($"Apellido {teamLead.LastName}");
                        Console.WriteLine($"Correo: {teamLead.Email}");
                        Console.WriteLine($"Telefono: {teamLead.Phone}");
                        Console.WriteLine($"Tipo de especialidad: {teamLead.TeamLeadType}");
                        Console.WriteLine("\n5 para regresar al menú");

                    }

                    #endregion


                    #region AGREGAR Team Leads

                    if (opcion == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("AGREGAR LIDER DE EQUIPO\n");
                        Console.WriteLine("7 para regresar al menú");

                        var teamLead = new TeamLead();

                        teamLead = LogicaTeamLead.SolicitarDatos(teamLead);
                        bool result = LogicaTeamLead.AgregarTeamLead(teamLead);

                        if (result)
                        {
                            Console.WriteLine("Lider de equipo agregado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("Algo salio mal");
                        }
                    }

                    #endregion


                    #region METODO EDITAR LIDER EQUIPO

                    if (opcion == 3)    
                    {
                        Console.Clear();
                        Console.WriteLine("EDITANDO LIDER DE EQUIPO");

                        LogicaTeamLead.MostrarTeamLeads();

                        Console.Write("Escribe la clave del lider de equipo a editar: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = LogicaTeamLead.EditarTeamLead(id);

                        if (result)
                        {
                            Console.WriteLine("Lider de equipo editado con exito!!");
                        }
                    }

                    #endregion


                    #region METODO BORRAR Lider de Equipo

                    if (opcion == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("BORRANDO UN LIDER DE EQUIPO\n");

                        Console.ForegroundColor = ConsoleColor.Red;
                        LogicaTeamLead.MostrarTeamLeads();

                        Console.Write("Escribe la clave del lider de equipo a eliminar: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = LogicaTeamLead.BorrarTeamLead(id);

                        if (result)
                        {
                            Console.WriteLine("Lider de equipo eliminado con exito!!");
                        }
                    }

                    #endregion


                    #region REGRESAR AL MENU PRINCIPAL

                    if (opcion == 7)
                    {
                        Console.Clear();
                        Console.Write(menu);
                    }

                    #endregion

                }
                else
                {
                    Console.WriteLine("Opcion no valida :/");
                }

                #region REGRESAR AL MENÚ PRINCIPAL

                if (opcion == 8)
                {
                    ran = false;
                    Console.Clear();
                    Console.WriteLine("Presiona 5 para aparecer el menú");
                }

                #endregion

            }

        }

        #endregion
    }
}
