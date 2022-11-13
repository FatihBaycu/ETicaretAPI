namespace ETicaretAPI.Application.Features.Products.Models
{
    public class UpdateProductCommandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

}