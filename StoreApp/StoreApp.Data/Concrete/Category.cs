namespace StoreApp.Data.Concrete
{
    public class Category
    {   
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string CategoryUrl { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new();

    }
}