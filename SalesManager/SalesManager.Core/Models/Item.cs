namespace SalesManager.Core.Models
{
    public class Item : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
    }
}