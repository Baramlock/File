internal class Weapon
{
    private readonly int _damage;
    private int _bullets;
    private readonly int _bulletsPerShot;

    public Weapon(int damage, int bullets, int bulletsToFire)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));
        if (bullets < 0)
            throw new ArgumentOutOfRangeException(nameof(bullets));
        if (bulletsToFire <= 0)
            throw new ArgumentOutOfRangeException(nameof(bulletsToFire));

        _damage = damage;
        _bullets = bullets;
        _bulletsPerShot = bulletsToFire;
    }

    public bool CanFire() => _bullets >= _bulletsPerShot;

    public void Fire(Player player)
    {
        if (CanFire() == false)
            throw new InvalidOperationException("Not enough bullets per shot");

        if (player == null)
            throw new ArgumentNullException();

        player.TakeDamage(_damage);
        _bullets -= _bulletsPerShot;
    }
}

internal class Player
{
    private int _health;
    public bool IsLife => _health > 0;

    public Player(int health)
    {
        if (health < 0)
            throw new ArgumentOutOfRangeException(nameof(health));
        _health = health;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        if (damage < _health)
            _health -= damage;
        else
            _health -= 0;
    }
}

internal class Bot
{
    private readonly Weapon _weapon;

    public Bot(Weapon weapon)
    {
        if (weapon == null)
            throw new ArgumentNullException(nameof(weapon));

        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        if (player.IsLife && _weapon.CanFire())
            _weapon.Fire(player);
    }
}