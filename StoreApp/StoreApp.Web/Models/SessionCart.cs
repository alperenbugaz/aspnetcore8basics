using System.Text.Json.Serialization;
using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models;

public class SessionCart : Cart
{   
    public static Cart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext?.Session;

        SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
        cart.Session = session;

        return cart;
    }

    [JsonIgnore]
    public ISession? Session { get; set; }
    override public void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.SetJson("Cart", this);
    }

    override public void RemoveItem(Product product)
    {
        base.RemoveItem(product);
        Session?.SetJson("Cart", this);
    }

    override public void Clear()
    {
        base.Clear();
        Session?.SetJson("Cart", this);
    }

}