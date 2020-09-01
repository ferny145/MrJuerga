namespace MrJuerga.Entity
{
    public class DetalleBoleta
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int BoletaId { get; set; }
        public Boleta Boleta { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal { get; set; }
        public int PaqueteId { get; set; }
        public Paquete Paquete { get; set; }
    }
}