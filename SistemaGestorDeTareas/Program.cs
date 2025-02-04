namespace SistemaGestorDeTareas;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static LinkedList<Tareas> guardarTareas = new LinkedList<Tareas>();

    public static void CrearTareas()
    {
        string ingresarDescripcion = AnsiConsole.Ask<string>("Ingresa la descripción de la tarea:");

        Tareas nuevaTarea = new Tareas(ingresarDescripcion);
        guardarTareas.AddLast(nuevaTarea);

        AnsiConsole.MarkupLine($"[green]Tarea creada con éxito![/]");
        AnsiConsole.MarkupLine($"[bold]ID:[/] {nuevaTarea.getId()}");
        AnsiConsole.MarkupLine($"[bold]Descripción:[/] {nuevaTarea.getDescripcion()}");
        AnsiConsole.MarkupLine($"[bold]Estado:[/] {(nuevaTarea.getEstado() ? "[green]Completada[/]" : "[red]Pendiente[/]")}");
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
                tarea.getId(),
                tarea.getDescripcion(),
                tarea.getEstado() ? "[green]Completada[/]" : "[red]Pendiente[/]"
            );
        }

        AnsiConsole.Write(table);
    }

    public static void MarcarTareaCompleta()
    {
        string id = AnsiConsole.Ask<string>("Ingresa el ID de la tarea que deseas marcar como completada:");

        var tarea = guardarTareas.FirstOrDefault(t => t.getId() == id);
        if (tarea != null)
        {
            tarea.completarTarea();
            AnsiConsole.MarkupLine($"[green]Tarea '{tarea.getDescripcion()}' marcada como completada.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]No se encontró la tarea con el ID {id}.[/]");
        }
    }

    public static void EliminarTarea()
    {
        string id = AnsiConsole.Ask<string>("Ingresa el ID de la tarea que deseas eliminar:");

        var tarea = guardarTareas.FirstOrDefault(t => t.getId() == id);
        if (tarea != null)
        {
            guardarTareas.Remove(tarea);
            AnsiConsole.MarkupLine($"[green]Tarea '{tarea.getDescripcion()}' eliminada con éxito.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]No se encontró la tarea con el ID {id} para poder eliminar.[/]");
        }
    }

    static void Main(string[] args)
    {
        AnsiConsole.Markup("[underline red]Hello[/] World!");

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
                    break;
                case "Mostrar Tareas":
                    MostrarTareas();
                    break;
                case "Marcar Tarea Como Completada":
                    MarcarTareaCompleta();
                    break;
                case "Eliminar Tarea":
                    EliminarTarea();
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
    }
}