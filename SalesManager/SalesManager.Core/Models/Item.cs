namespace SalesManager.Core.Models
{
    public class Item : BaseEntity
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }

        public Product Product { get; set; }
        public Sale Sale { get; set; }
    }
}