// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
public void SetEnable(bool enable)
{
    _enable = enable;

    if (_enable)
    {
        _effects.StartEnableAnimation();
    }
    else
    {
        _pool.Free(this);
    }
}