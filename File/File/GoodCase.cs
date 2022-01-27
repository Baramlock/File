internal class GoodCase
{
    private readonly Dictionary<Good, int> _goods = new();

    protected void AddTo(Good good, int count)
    {
        if (_goods.ContainsKey(good))
            _goods[good] += count;
        else
            _goods[good] = count;
    }

    public bool CheckAvailability(Good good, int count)
    {
        return _goods.ContainsKey(good) && (_goods[good] >= count);
    }

    public void Remove(Good good, int count)
    {
        if (CheckAvailability(good, count) == false)
        {
            throw new InvalidOperationException();
        }
        else
        {
            _goods[good] -= count;
        }
    }

    public void ShowGoods()
    {
        foreach (var keyValuePair in _goods)
        {
            Console.WriteLine($"{keyValuePair.Key.Lable} - {keyValuePair.Value} штук");
        }
    }
}
