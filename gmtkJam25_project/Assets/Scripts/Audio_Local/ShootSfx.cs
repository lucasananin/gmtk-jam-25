using UnityEngine;

public class ShootSfx : AudioPlayer
{
    [SerializeField] WeaponBehaviour _weapon = null;

    private void OnEnable()
    {
        _weapon.OnShoot += PlayShootSFX;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= PlayShootSFX;
    }

    private void PlayShootSFX()
    {
        Play();
    }
}
