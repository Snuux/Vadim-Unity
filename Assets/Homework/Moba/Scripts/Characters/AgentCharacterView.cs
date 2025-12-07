using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");
    private readonly int RecieveDamageKey = Animator.StringToHash("RecieveDamage");
    private const int InjuredLayer = 1;

    private const double MinimumVelocitySpeed = 0.1;
    private const int InjuredLayerWeight = 1;
    private const int InjuredLayerWeightZero = 0;

    [SerializeField] Animator _animator;
    [SerializeField] AgentCharacter _character;

    private float _previusUpdateCycleHealth;

    private void Awake()
    {
        _previusUpdateCycleHealth = _character.CurrentHealth;
    }

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > MinimumVelocitySpeed)
            StartRunning();
        else
            StopRunning();

        if (_character.IsInjured())
            _animator.SetLayerWeight(InjuredLayer, InjuredLayerWeight);
        else
            _animator.SetLayerWeight(InjuredLayer, InjuredLayerWeightZero);

        if (_character.IsDead())
            StartDead();
        else
            StopDead();

        if (IsDamageRecieved())
            RecieveDamageTrigger();
    }

    private void StartRunning() => _animator.SetBool(IsRunningKey, true);

    private void StopRunning() => _animator.SetBool(IsRunningKey, false);

    private void StartDead() => _animator.SetBool(IsDeadKey, true);

    private void StopDead() => _animator.SetBool(IsDeadKey, false);

    private void RecieveDamageTrigger() => _animator.SetTrigger(RecieveDamageKey);

    private bool IsDamageRecieved()
    {
        //если в кадре был нанесён урон

        bool isDamageRecieved = false;
        if (_previusUpdateCycleHealth != _character.CurrentHealth)
            isDamageRecieved = true;

        _previusUpdateCycleHealth = _character.CurrentHealth;
        return isDamageRecieved;
    }

}
