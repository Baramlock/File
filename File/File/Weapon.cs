internal class Weapon
{
    public int Damage { get; private set; }
    public int Bullets { get; private set; }

    public void Fire(Player player)
    {
        player.TakeDamage(Damage);
        Bullets -= 1;
    }
}

internal class Player
{
    public int Health { get; private set; }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

internal class Bot
{
    private Weapon _Weapon;

    public Bot(Weapon weapon)
    {
        _Weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _Weapon.Fire(player);
    }
}