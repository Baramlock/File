public void SetOn()
{
    _effects.StartEnableAnimation();
}

public void SetOff()
{
    _pool.Free(this);
}