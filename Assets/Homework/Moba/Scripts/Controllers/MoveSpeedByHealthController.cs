public class MoveSpeedByHealthController : Controller
{
    private AgentCharacter _agentCharacter;

    public MoveSpeedByHealthController(AgentCharacter agentCharacter)
    {
        _agentCharacter = agentCharacter;
    }

    public override void UpdateControlling(float deltaTime)
    {
        UpdateMoveSpeedByHealth();
    }

    private void UpdateMoveSpeedByHealth()
    {
        float defaultAgentMovementSpeed = _agentCharacter.GetDefaultMovementSpeed();

        if (_agentCharacter.IsInjured())
            _agentCharacter.SetMovementSpeed(defaultAgentMovementSpeed * HealthComponent.InjuredHealthMultiplier);
        else
            _agentCharacter.SetMovementSpeed(defaultAgentMovementSpeed);
    }
}