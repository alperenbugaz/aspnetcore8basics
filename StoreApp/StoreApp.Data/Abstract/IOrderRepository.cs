using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface IOrderRepository
{   
    IQueryable<Order> Orders { get; }
    void CreateOrder(Order order);



}
