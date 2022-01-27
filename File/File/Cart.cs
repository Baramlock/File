internal class Cart : GoodCase
{
    private Order _order;
    private Warehouse _warehouse;

    public Cart(Warehouse warehouse)
    {
        _order = new Order();
        _warehouse = warehouse;
    }

    public void Add(Good good, int count)
    {
        if (_warehouse.CheckAvailability(good, count))
        {
            AddTo(good, count);
            _warehouse.Remove(good, count);
        }
        else
        {
            Console.WriteLine($"Товара {good.Lable} в количестве {count} штук, нет на складе");
        }
    }

    internal Order Order()
    {
        return new Order();
    }
}

internal class Order
{
    public string Paylink { get; private set; }

    public Order()
    {
        Random random = new Random();
        var orderlink = new char[10];

        for (int i = 0; i < orderlink.Length; i++)
        {
            orderlink[i] = (char)random.Next(0x0410, 0x44F);
        }

        Paylink = new string(orderlink);
    }
}