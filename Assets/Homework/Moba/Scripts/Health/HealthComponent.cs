using UnityEngine;
using UnityEngine.UIElements;

public class HealthComponent
{
    private float _injuredHealthMultiplier;
    private float _currentHealth;
    private float _maxHealth;

    private bool _isDeadTrigger;

    public HealthComponent(float maxHealth, float injuredHealthMultiplier)
    {
        _maxHealth = maxHealth;
        _injuredHealthMultiplier = injuredHealthMultiplier;

        CurrentValue = maxHealth;
    }

    public bool IsDeadTrigger => _isDeadTrigger;

    public float CurrentValue
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    public float MaxValue => _maxHealth;


    public void Update(float deltaTime)
    {
    }

    public void LateUpdate(float deltaTime)
    {
        _isDeadTrigger = false;
    }

    public void TakeDamage(float damage)
    {
        //if (damage < 0)
        //{
        //    Debug.Log("Damage is negative!");
        //    return;
        //}

        float healthBeforeDamageApply = CurrentValue;

        CurrentValue -= damage;

        if (IsDead() && healthBeforeDamageApply > 0)
            _isDeadTrigger = true;
    }

    public bool IsInjured() => CurrentValue < MaxValue * _injuredHealthMultiplier;

    public bool IsDead() => CurrentValue <= 0;

    public void Revive() => CurrentValue = MaxValue;
}