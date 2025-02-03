namespace SistemaGestorDeTareas;

class Program
{

    public static LinkedList<Tareas> guardarTareas = new LinkedList<Tareas>();

    public static void CrearTareas()
    {
        string ingresarDescripcion = Console.ReadLine();


        Tareas nuevaTarea = new Tareas(ingresarDescripcion);
        guardarTareas.AddLast(nuevaTarea);
        
        Console.WriteLine($"ID: {nuevaTarea.getId()} ");
        Console.WriteLine($"Descripción: {nuevaTarea.getDescripcion()}");
        Console.WriteLine($"Estado: {(nuevaTarea.getEstado() ? "Completada" : "Pendiente")}");



    }

    public static void MostrarTareas()
    {
        if (guardarTareas.Count == 0)
        {
            Console.WriteLine("No hay tareas en la lista para mostrar.");
            return;
        }

        
        Console.WriteLine("******* Lista de Tareas ************");

        foreach (var tarea in guardarTareas)
        {
            Console.WriteLine($"- ID {tarea.getId()}");
            Console.WriteLine($"\tDescripcion: {tarea.getDescripcion()}");
            Console.WriteLine($"\tEstado: {(tarea.getEstado() ? "Completada" : "Pendiente")}");
            Console.WriteLine();

        }

    }

    public static void MarcarTareaCompleta()
    {

        Console.WriteLine("Ingresa el ID de la tarea que deseas marcar como completada: ");
        string id = Console.ReadLine();

        var tarea = guardarTareas.FirstOrDefault(t => t.getId() == id);
        if (tarea != null)
        {
            tarea.completarTarea();
            
            Console.WriteLine($"Tarea '{tarea.getDescripcion()}' marcada como completada.");
        }
        else
        {
            
            Console.WriteLine($"No se encontro la tarea con el {id}");
        }

    }

    public static void EliminarTarea()
    {
        
        Console.WriteLine("Ingresa el ID de la tarea que deseas eliminar");
        string id = Console.ReadLine();
        var tarea = guardarTareas.FirstOrDefault(t => t.getId() == id);
        if (tarea != null)
        {
            guardarTareas.Remove(tarea);
            Console.WriteLine($"Tarea '{tarea.getDescripcion()}' eliminada con éxito.");
        }
        else
        {
           
            Console.WriteLine($"No se encontro la tarea con el {id} para poder eliminar.");
        }

    }


    static void Main(string[] args)
    {
        int optionMenu;

        do
        {

            Console.WriteLine("============ SISTEMA GESTIÓN DE TAREAS ==================");
            Console.WriteLine("*\t\t1. Agregar Tarea.                                  *");
            Console.WriteLine("*\t\t2. Mostrar Tarea.                                  *");
            Console.WriteLine("*\t\t3. Marcar Tarea Como Completada.                   *");
            Console.WriteLine("*\t\t4. Eliminar Tarea.                                 *");
            Console.WriteLine("*\t\t5. Salir.                                          *");
            Console.WriteLine("=========================================================");
            Console.WriteLine("Seleccione una opción: ");
            optionMenu = Convert.ToInt32(Console.ReadLine());

            switch (optionMenu)
            {
                case 1:
                    Console.WriteLine("Ingresa una descripción de la tarea");
                    CrearTareas();
                    break;
                case 2:
                    MostrarTareas();
                    break;
                case 3:
                    MarcarTareaCompleta();
                    break;
                case 4:
                    EliminarTarea();
                    break;
                case 5:
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    break;
            }
            Console.WriteLine();
        } while (optionMenu != 5);
    }
}