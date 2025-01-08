using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;

        public EFStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public IQueryable<Category> Categories => _context.Categories;

        public void CreateProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                }
            }
            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            var products = Products;
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(p => p.Categories).Where(p => p.Categories.Any(c => c.CategoryName == category));
            }




            return products.Skip((page - 1) * pageSize).Take(pageSize);


        }

        public int GetProductsCount(string category)
        {
            return category == null
            ? Products.Count() :
             Products.Include(p => p.Categories)
             .Where(p => p.Categories.Any(c => c.CategoryUrl == category)).Count();
        }
    }

}