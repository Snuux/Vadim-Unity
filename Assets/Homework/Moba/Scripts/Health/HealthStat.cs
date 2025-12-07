using UnityEngine;

public class HealthStat : IStat
{
    public const float InjuredMoveSpeedModifier = .3f;

    private float _currentHealth;
    private float _maxHealth;

    public float CurrentValue
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    public float MaxValue => _maxHealth;

    public HealthStat(float maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentValue = maxHealth;
    }

    public void Update(float deltaTime)
    {

    }

    public void ChangeValue(float value) => CurrentValue -= value;

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.Log("Damage is negative!");
            return;
        }

        ChangeValue(damage);
    }

    public bool IsInjured() => CurrentValue < MaxValue * InjuredMoveSpeedModifier;

    public bool IsDead() => CurrentValue <= 0;

    public void Revive() => CurrentValue = MaxValue;
}
