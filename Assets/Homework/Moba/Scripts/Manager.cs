using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Manager : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private Slider _slider;

    [SerializeField] List<MovePointCharacter> _randomPoints;

    private Controller _playerAgentMoveableController;
    private Controller _playerRotationController;
    private Controller _playerRandomPointsMovableController;

    private void Awake()
    {
        _playerAgentMoveableController = new AgentMouseMovableController(_agentCharacter);
        _playerAgentMoveableController.Enable();

        _playerRotationController = new AgentRotatableController(_agentCharacter, _agentCharacter);
        _playerRotationController.Enable();

        _playerRandomPointsMovableController = new AgentRandomPointsMovableController(
            _agentCharacter, _randomPoints);

        _playerRandomPointsMovableController.Enable();
    }

    private void Update()
    {
        if (_agentCharacter.IsDead())
        {
            _playerAgentMoveableController.Disable();
            _playerRotationController.Disable();
            _playerRandomPointsMovableController.Disable();
        }
        else
        {
            _playerAgentMoveableController.Enable();
            _playerRotationController.Enable();
            _playerRandomPointsMovableController.Enable();
        }

        _playerAgentMoveableController.Update(Time.deltaTime);
        _playerRotationController.Update(Time.deltaTime);
        _playerRandomPointsMovableController.Update(Time.deltaTime);
    }
}
