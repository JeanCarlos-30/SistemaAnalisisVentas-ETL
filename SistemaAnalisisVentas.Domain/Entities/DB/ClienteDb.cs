namespace SistemaAnalisisVentas.Domain.Entities.DB
{
    public class ClienteDb
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? FuenteID { get; set; }
    }
}
