using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EFOrderRepository : IOrderRepository
{
    private StoreDbContext _context;

    public EFOrderRepository(StoreDbContext context)
    {
        _context = context;
    }

    public IQueryable<Order> Orders => _context.Orders;

    public void CreateOrder(Order order)
    {
        order.OrderDate = DateTime.Now;
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
}
