namespace SistemaAnalisisVentas.Domain.Entities.DB
{
    public class OrderDb
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
    }
}
