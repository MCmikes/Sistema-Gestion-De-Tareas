using System.Text.Json;
using Spectre.Console;

namespace SistemaGestorDeTareas
{
    class Program
    {
        public static LinkedList<Tareas> guardarTareas = new LinkedList<Tareas>();

        public static void CrearTareas()
        {
            string ingresarDescripcion = AnsiConsole.Ask<string>("Ingresa la descripción de la tarea:");

            Tareas nuevaTarea = new Tareas(ingresarDescripcion);
            guardarTareas.AddLast(nuevaTarea);

            AnsiConsole.MarkupLine($"[green]Tarea creada con éxito![/]");
            AnsiConsole.MarkupLine($"[bold]ID:[/] {nuevaTarea.Id}");
            AnsiConsole.MarkupLine($"[bold]Descripción:[/] {nuevaTarea.Descripcion}");
            AnsiConsole.MarkupLine($"[bold]Estado:[/] {(nuevaTarea.Estado ? "[green]Completada[/]" : "[red]Pendiente[/]")}");
        }

        public static void MostrarTareas()
        {
            if (guardarTareas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay tareas en la lista para mostrar.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Descripción[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Estado[/]").Centered());

            foreach (var tarea in guardarTareas)
            {
                table.AddRow(
                    tarea.Id,
                    tarea.Descripcion,
                    tarea.Estado ? "[green]Completada[/]" : "[red]Pendiente[/]"
                );
            }

            AnsiConsole.Write(table);
        }

        public static void MarcarTareaCompleta()
        {
            string id = AnsiConsole.Ask<string>("Ingresa el ID de la tarea que deseas marcar como completada:");

            var tarea = guardarTareas.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                tarea.completarTarea();
                AnsiConsole.MarkupLine($"[green]Tarea '{tarea.Descripcion}' marcada como completada.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No se encontró la tarea con el ID {id}.[/]");
            }
        }

        public static void EliminarTarea()
        {
            string id = AnsiConsole.Ask<string>("Ingresa el ID de la tarea que deseas eliminar:");

            var tarea = guardarTareas.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                guardarTareas.Remove(tarea);
                AnsiConsole.MarkupLine($"[green]Tarea '{tarea.Descripcion}' eliminada con éxito.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No se encontró la tarea con el ID {id} para poder eliminar.[/]");
            }
        }

        public static void GuardarTarea()
        {
            // Convertir LinkedList a List para serialización
            var listaTareas = guardarTareas.ToList();
            string json = JsonSerializer.Serialize(listaTareas);
            File.WriteAllText("tarea.txt", json);
        }

        public static void CargarTarea()
        {
            if (File.Exists("tarea.txt"))
            {
                string json = File.ReadAllText("tarea.txt");
                var listaTareas = JsonSerializer.Deserialize<List<Tareas>>(json);

                if (listaTareas != null)
                {
                    guardarTareas = new LinkedList<Tareas>(listaTareas);
                }
                else
                {
                    guardarTareas = new LinkedList<Tareas>();
                }
            }
        }

        static void Main(string[] args)
        {
            AnsiConsole.Markup("[underline red]Hello[/] World!");

            CargarTarea();

            do
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("Gestor de Tareas")
                        .Centered()
                        .Color(Color.Blue)
                );

                var panel = new Panel("Selecciona una opción:")
                    .Border(BoxBorder.Rounded)
                    .Header("[yellow]Menú Principal[/]")
                    .HeaderAlignment(Justify.Center);
                AnsiConsole.Write(panel);

                var menu = new SelectionPrompt<string>()
                    .Title("[green]Opciones:[/]")
                    .PageSize(5)
                    .AddChoices(new[] {
                        "Agregar Tarea",
                        "Mostrar Tareas",
                        "Marcar Tarea Como Completada",
                        "Eliminar Tarea",
                        "Salir"
                    });

                var opcion = AnsiConsole.Prompt(menu);

                switch (opcion)
                {
                    case "Agregar Tarea":
                        CrearTareas();
                        GuardarTarea();
                        break;
                    case "Mostrar Tareas":
                        MostrarTareas();
                        break;
                    case "Marcar Tarea Como Completada":
                        MarcarTareaCompleta();
                        GuardarTarea();
                        break;
                    case "Eliminar Tarea":
                        EliminarTarea();
                        GuardarTarea();
                        break;
                    case "Salir":
                        AnsiConsole.MarkupLine("[yellow]Saliendo del sistema...[/]");
                        break;
                }

                if (opcion != "Salir")
                {
                    AnsiConsole.MarkupLine("[yellow]Presiona cualquier tecla para continuar...[/]");
                    Console.ReadKey();
                }

            } while (AnsiConsole.Confirm("¿Deseas continuar en el sistema?"));

            GuardarTarea();
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}