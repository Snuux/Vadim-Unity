using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Manager : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private Slider _slider;

    [SerializeField] List<MovePointCharacter> _randomPoints;

    private Controller _agentCompositeController;
    private Controller _agentLongIdleCompositeConroller;
    private Controller _mouseMovableController;
    private Controller _jumpableController;

    private void Awake()
    {
        _mouseMovableController = new AgentMouseMovableController(_agentCharacter);

        Controller randomPointsAiController = new AgentRandomPointsMovableController(
            _agentCharacter, _randomPoints);

        Controller rotatableController = new DirectionalRotatableController(
            _agentCharacter, _agentCharacter);

        Controller playerRandomPointsController = new CompositeController(
            randomPointsAiController, rotatableController);

        Controller longIdleStateController = new LongIdleStateController(
            _agentCharacter, _agentCharacter.RequiredTimeToLongIdle, playerRandomPointsController);

        _jumpableController = new AgentJumpableController(_agentCharacter, _agentCharacter);

        _agentCompositeController =
            new CompositeController(_mouseMovableController, rotatableController, _jumpableController);

        _agentLongIdleCompositeConroller = new CompositeController(longIdleStateController, _jumpableController);

        _agentCompositeController.Enable();
        _agentLongIdleCompositeConroller.Disable();
    }

    private void Update()
    {
        SwitchControllersIfAgentDeadOrLongIdle();

        _agentCompositeController.Update(Time.deltaTime);
        _agentLongIdleCompositeConroller.Update(Time.deltaTime);
    }

    private void SwitchControllersIfAgentDeadOrLongIdle()
    {
        if (_agentCharacter.IsDead())
        {
            _agentCompositeController.Disable();
            _agentLongIdleCompositeConroller.Disable();

            return;
        }

        _agentCompositeController.Enable();
    }
}
