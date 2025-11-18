namespace SistemaAnalisisVentas.Domain.Entities.DB
{
    public class OrderDetailDb
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
