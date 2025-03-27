namespace API.Models.Respuesta
{
    public class Respuesta
    {
        public int exito { get; set; }

        public string? mensaje { get; set; }

        public object? data { get; set; }

        public Respuesta()
        {
            exito = 0;
        }
    }
}
