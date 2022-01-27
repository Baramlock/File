internal class Cart : GoodCase
{
    private readonly Order _order;
    private readonly Warehouse _warehouse;

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
        return _order;
    }
}
