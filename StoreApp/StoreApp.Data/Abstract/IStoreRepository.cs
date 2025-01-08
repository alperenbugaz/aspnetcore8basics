using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        void CreateProduct(Product product);
        Product DeleteProduct(int productId);

        IEnumerable<Product> GetProductsByCategory(string category,int page,int pageSize);

        int GetProductsCount(string category);
    }
}