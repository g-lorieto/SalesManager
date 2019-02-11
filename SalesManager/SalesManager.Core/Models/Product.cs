namespace SalesManager.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal CostPerKilo { get; set; }
        public decimal PricePerKilo { get; set; }
        public decimal Over10KilosPricePerKilo { get; set; }
        public decimal FriendsPricePerKilo { get; set; }
        public string Description { get; set; }
    }
}