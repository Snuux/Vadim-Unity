using UnityEngine;

public class HealthComponent
{
    public const float InjuredHealthMultiplier = .3f;

    private float _currentHealth;
    private float _maxHealth;

    private bool _isDeadTrigger;

    public bool IsDeadTrigger => _isDeadTrigger;

    public float CurrentValue
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    public float MaxValue => _maxHealth;

    public HealthComponent(float maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentValue = maxHealth;
    }

    public void Update(float deltaTime)
    {
    }

    public void LateUpdate(float deltaTime)
    {
        _isDeadTrigger = false;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.Log("Damage is negative!");
            return;
        }

        float healthBeforeDamageApply = CurrentValue;

        CurrentValue -= damage;

        if (IsDead() && healthBeforeDamageApply > 0)
            _isDeadTrigger = true;
    }

    public bool IsInjured() => CurrentValue < MaxValue * InjuredHealthMultiplier;

    public bool IsDead() => CurrentValue <= 0;

    public void Revive() => CurrentValue = MaxValue;
}
