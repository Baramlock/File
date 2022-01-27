internal class Order
{
    public string Paylink { get; private set; }

    public Order()
    {
        Random random = new();
        var orderlink = new char[10];

        for (int i = 0; i < orderlink.Length; i++)
        {
            orderlink[i] = (char)random.Next(0x0410, 0x44F);
        }

        Paylink = new string(orderlink);
    }
}