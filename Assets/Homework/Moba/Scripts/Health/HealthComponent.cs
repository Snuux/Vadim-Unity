using UnityEngine;

public class HealthComponent
{
    public const float InjuredMoveSpeedModifier = .3f;

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

        //запоминаем кол-во хп, наносим дамаг, если хп было >0, то умираем.
        //Если нет, то уже умерли, и не нужно вызывать Die()

        //float healthBeforeDamage = _healthStat.CurrentValue;
        //
        //_healthStat.TakeDamage(damage);
        //
        //if (IsDead() && healthBeforeDamage > 0)
        //    Die();
    }

    public bool IsInjured() => CurrentValue < MaxValue * InjuredMoveSpeedModifier;

    public bool IsDead() => CurrentValue <= 0;

    public void Revive() => CurrentValue = MaxValue;
}
