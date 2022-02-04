internal class Warehouse : GoodCase
{
    internal void Delive(Good good, int count) => AddTo(good, count);
}