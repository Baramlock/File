class Weapon
{
    private int _bullets;
    private const int _bulletsToShoot = 1;

    public bool CanShoot() => _bullets >= _bulletsToShoot;

    public void Shoot() => _bullets -= _bulletsToShoot;
}
