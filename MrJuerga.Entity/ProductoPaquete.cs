namespace MrJuerga.Entity
{
    public class ProductoPaquete
    {
        public int Id { get; set; }

        public int PaqueteId { get; set; }
        public Paquete Paquete { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}