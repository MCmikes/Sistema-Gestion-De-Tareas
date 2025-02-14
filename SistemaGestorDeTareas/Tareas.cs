namespace SistemaGestorDeTareas;

[Serializable]
public class Tareas
{
    public string Id { get; set; }
    public string Descripcion { get; set; }
    public bool Estado { get; set; }

    public Tareas()
    {
        Id = Guid.NewGuid().ToString();
        Descripcion = string.Empty;
        Estado = false;
    }

    public Tareas(string descripcion)
    {
        Id = Guid.NewGuid().ToString();
        Descripcion = descripcion;
        Estado = false;
    }

    public void completarTarea() => Estado = true;
}