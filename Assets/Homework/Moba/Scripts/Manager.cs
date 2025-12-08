using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Manager : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private Slider _slider;

    [SerializeField] List<MovePointCharacter> _randomPoints;

    private Controller _playerAgentController;
    private Controller _playerAgentRandomPointsController;

    private void Awake()
    {
        _playerAgentController = new CompositeController(
            new AgentMouseMovableController(_agentCharacter),
            new AgentRotatableController(_agentCharacter, _agentCharacter)
            );

        _playerAgentRandomPointsController = new CompositeController(
            new AgentRandomPointsMovableController(_agentCharacter, _randomPoints),
            new AgentRotatableController(_agentCharacter, _agentCharacter)
            );

        _playerAgentController.Enable();
        _playerAgentRandomPointsController.Disable();
    }

    private void Update()
    {
        SwitchControllersIfAgentDeadOrLongIdle();

        _playerAgentController.Update(Time.deltaTime);
        _playerAgentRandomPointsController.Update(Time.deltaTime);
    }

    private void SwitchControllersIfAgentDeadOrLongIdle()
    {
        if (_agentCharacter.IsDead())
        {
            _playerAgentController.Disable();
            _playerAgentRandomPointsController.Disable();

            return;
        }

        if (_agentCharacter.IsLongIdle() && IsRandomPointNearPlayer())
        {
            _playerAgentRandomPointsController.Enable();
            _playerAgentController.Disable();
        }
        else
        {
            _playerAgentRandomPointsController.Disable();
            _playerAgentController.Enable();
        }

    }

    bool IsRandomPointNearPlayer()
    {
        foreach (var point in _randomPoints)
            if ((point.Position - _agentCharacter.Position).magnitude <= point.Radius)
                return true;

        return false;
    }
}
