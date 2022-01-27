internal class Weapon
{
    private readonly int _damage;

    private int _bullets;
    private readonly int _bulletsToFire;

    public Weapon(int damage, int bullets, int bulletsToFire)
    {
        if ((damage < 0) && (bullets < 0) && (bulletsToFire <= 0))
            throw new InvalidOperationException();
        _damage = damage;
        _bullets = bullets;
        _bulletsToFire = bulletsToFire;
    }

    public bool CanFire() => _bullets >= _bulletsToFire;

    public void Fire(Player player)
    {
        if (CanFire() == false)
            throw new InvalidOperationException();

        player.TakeDamage(_damage);
        _bullets -= _bulletsToFire;
    }
}

internal class Player
{
    private int _health;
    public bool IsLife => _health >= 0;

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}

internal class Bot
{
    private readonly Weapon _Weapon;

    public Bot(Weapon weapon)
    {
        _Weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        if (_Weapon.CanFire() && player.IsLife)
            _Weapon.Fire(player);
    }
}