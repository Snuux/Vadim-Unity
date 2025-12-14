using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");
    private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
    private readonly int RecieveDamageKey = Animator.StringToHash("RecieveDamage");
    private const int InjuredLayer = 1;

    private const double MinimumVelocitySpeed = 0.1;
    private const int InjuredLayerWeight = 1;
    private const int InjuredLayerWeightZero = 0;

    private const float DiePointParticleSystemOffsetY = .5f;

    [SerializeField] Animator _animator;
    [SerializeField] AgentCharacter _character;

    [SerializeField] private Spawner _diePointPrefab;

    private float _previusUpdateCycleHealth;

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > MinimumVelocitySpeed)
            _animator.SetBool(IsRunningKey, true);
        else
            _animator.SetBool(IsRunningKey, false);

        if (_character.IsInjured())
            _animator.SetLayerWeight(InjuredLayer, InjuredLayerWeight);
        else
            _animator.SetLayerWeight(InjuredLayer, InjuredLayerWeightZero);

        if (_character.IsDead())
            _animator.SetBool(IsDeadKey, true);
        else
            _animator.SetBool(IsDeadKey, false);

        if (IsDamageRecieved())
            _animator.SetTrigger(RecieveDamageKey);

        if (_character.IsDeadTrigger)
            _diePointPrefab.Spawn(transform.position + DiePointParticleSystemOffsetY * Vector3.up);

        if (_character.InJumpProcess)
            _animator.SetBool(IsJumpingKey, true);
        else
            _animator.SetBool(IsJumpingKey, false);
    }

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
