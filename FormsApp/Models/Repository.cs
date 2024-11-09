namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new List<Product>();
        private static readonly List<Category> _categories = new List<Category>();

        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });
            _products.Add(new Product { ProductId = 1, Name = "iPhone 8", Price = 1000, imageFile = "1.jpg", IsAvailable = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "MacBook", Price = 100000, imageFile = "2.jpg", IsAvailable = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 3, Name = "Monster Abra", Price = 2, imageFile = "3.jpg", IsAvailable = true, CategoryId = 2 });
        }

        public static List<Product> Products 
        {
            get
            {
                return _products;
            }

        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == deletedProduct.ProductId);
            if (entity != null)
            {
                _products.Remove(entity);
            }
        }
        

        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);

            if (entity != null)
            {
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.imageFile = updatedProduct.imageFile;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsAvailable = updatedProduct.IsAvailable;
            }

        }


        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        public static void AddProduct(Product entity)
        {
            _products.Add(entity); 

        }


    }

}