internal class Shop
{
    private readonly Warehouse _warehouse;

    public Shop(Warehouse warehouse)
    {
        _warehouse = warehouse;
    }

    internal void ShowGoods()
    {
        _warehouse.ShowGoods();
    }

    internal Cart Cart()
    {
        return new Cart(_warehouse);
    }
}