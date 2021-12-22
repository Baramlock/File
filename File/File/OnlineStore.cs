Good iPhone12 = new Good("IPhone 12");

Good iPhone11 = new Good("IPhone 11");

Warehouse warehouse = new Warehouse();

Shop shop = new Shop(warehouse);


warehouse.Delive(iPhone12, 10);
warehouse.Delive(iPhone11, 1);

shop.ShowGoods();

Cart cart = shop.Cart();
cart.Add(iPhone12, 4);
cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

cart.ShowGoods();

Console.WriteLine(cart.Order());

cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары

internal class Cart
{
    private Dictionary<Good,int> _goods;
    private Shop _shop;

    public Cart(Shop shop)
    {
        _goods = new Dictionary<Good, int>();
        _shop = shop;
    }

    internal void Add(Good good, int count)
    {
        if (count < 1)
        {
            throw new InvalidOperationException("Count > 0");
        }

        if(_shop.TrySell(good, count))
            _goods.Add(good, count);
        else
            Console.WriteLine("Товара нет на складе");
    }

    internal string Order()
    {
        Random random = new Random();
        return random.Next(1000000, 10000000).ToString();
    }

    internal void ShowGoods()
    {
        foreach (var good in _goods)
            Console.WriteLine(good.Key.Lable + " " + good.Value);
    }
}

internal class Shop
{
    private Warehouse _warehouse;
    private Cart _cart;

    public Shop(Warehouse warehouse)
    {
        _cart = new Cart(this);
        _warehouse = warehouse;
    }

    public Cart Cart()
    {
        return _cart;
    }

    internal void ShowGoods() => _warehouse.ShowGoods();

    internal bool TrySell(Good good, int count)
    {
        if (_warehouse.IsAvailability(good,count) == false)
        {
            return false;
        }
        else
        {
            _warehouse.Sell();
            return true;
        }
    }
}


internal class Warehouse
{
    private readonly Dictionary<Good, int>? _goods;
    private Good _goodToSell;

    public Warehouse()
    {
        _goods = new Dictionary<Good, int>();
    }

    internal void Delive(Good good, int count)
    {
        if (count < 1)
            throw new InvalidOperationException("Count > 0");
        if (_goods.ContainsKey(good))
            _goods[good] += count;
        else
            _goods.Add(good, count);
    }

    internal bool IsAvailability(Good good,int count)
    {
        var IsAvailability = _goods.ContainsKey(good) && _goods[good] >= count;
        if (IsAvailability)
            _goodToSell = good;
        return IsAvailability;
    }

    internal void Sell()
    {
        _goods.Remove(_goodToSell);
    }

    internal void ShowGoods()
    {
        foreach (var good in _goods)
            Console.WriteLine(good.Key.Lable + " " + good.Value);
    }
}

internal struct Good
{
    public string Lable { get; private set; }

    public Good(string lable)
    {
        Lable = lable;
    }
}

