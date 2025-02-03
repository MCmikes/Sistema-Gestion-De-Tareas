namespace SistemaGestorDeTareas;

class Tareas{
    private string id;
    private string description;
    private bool estado;

    public Tareas(string descripcion){
        this.id = Guid.NewGuid().ToString();
        this.description = descripcion;
        this.estado = false;
    }

    public string getId() {
        return id;
    }

    public string getDescripcion(){
        return description;
    }

    public bool getEstado() {
        return estado;
    }
    
    public void completarTarea(){
        this.estado = true;
    }
}