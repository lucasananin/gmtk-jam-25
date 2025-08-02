using UnityEngine;

public class TakeDamageSFX : AudioPlayer
{
    [SerializeField] HealthBehaviour _health = null;

    private void OnEnable()
    {
        _health.OnDamageTaken += _health_OnDamageTaken;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= _health_OnDamageTaken;
    }

    private void _health_OnDamageTaken()
    {
        Play();
    }
}
