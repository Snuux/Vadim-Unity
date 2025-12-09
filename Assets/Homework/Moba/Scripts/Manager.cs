using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Manager : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private Slider _slider;

    [SerializeField] List<MovePointCharacter> _randomPoints;

    [SerializeField] private float _requiredTimeToLongIdle;

    private Controller _playerAgentController;
    private Controller _playerAgentRandomPointsController;
    private Controller _playerAgentLongIdleConroller;

    private void Awake()
    {
        _playerAgentController = new CompositeController(
            new AgentMouseMovableController(_agentCharacter),
            new AgentRotatableController(_agentCharacter, _agentCharacter),
            new MoveSpeedByHealthController(_agentCharacter)
            );

        _playerAgentRandomPointsController = new CompositeController(
            new AgentRandomPointsMovableController(_agentCharacter, _randomPoints),
            new AgentRotatableController(_agentCharacter, _agentCharacter),
            new MoveSpeedByHealthController(_agentCharacter)
            );

        _playerAgentLongIdleConroller = 
            new LongIdleStateController(_agentCharacter, _requiredTimeToLongIdle, _playerAgentRandomPointsController);

        _playerAgentController.Enable();
        _playerAgentRandomPointsController.Disable();
        _playerAgentLongIdleConroller.Enable();
    }

    private void Update()
    {
        SwitchControllersIfAgentDeadOrLongIdle();

        _playerAgentController.Update(Time.deltaTime);
        _playerAgentRandomPointsController.Update(Time.deltaTime);
        _playerAgentLongIdleConroller.Update(Time.deltaTime);
    }

    private void SwitchControllersIfAgentDeadOrLongIdle()
    {
        if (_agentCharacter.IsDead())
        {
            _playerAgentController.Disable();
            _playerAgentRandomPointsController.Disable();

            return;
        }

        _playerAgentController.Enable();
    }
}
