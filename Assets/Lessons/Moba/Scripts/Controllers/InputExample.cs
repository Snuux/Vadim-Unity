using MobaLesson;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace MobaLesson
{
    public class InputExample : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Character _enemy;
        [SerializeField] private Tower _tower;
        [SerializeField] private AgentCharacter _agentEnemy;
        [SerializeField] private AgentPlayerCharacter _playerAgentCharacter;

        private Controller _characterController;
        private Controller _enemyController;
        private Controller _towerController;
        private Controller _agentEnemyController;

        private NavMeshPath _navMeshPath;

        private void Awake()
        {
            _navMeshPath = new NavMeshPath();

            //_characterController = new CompositeController(
            //    new PlayerDirectionMovableController(_character),
            //    new AlongMovableVelocityRotatableController(_character, _character)
            //    );

            //_characterController = new CompositeController(
            //    new PlayerMouseAgentController(_playerAgentCharacter.Mover)
            //    //new AlongMovableVelocityRotatableController(_character, _character)
            //    );


            NavMeshQueryFilter navMeshQueryFilter = new NavMeshQueryFilter();
            navMeshQueryFilter.agentTypeID = 0;
            navMeshQueryFilter.areaMask = NavMesh.AllAreas;

            _enemyController = new CompositeController(
                new DirectinalMoverAgroController(_enemy, _character.transform, 30, 2, navMeshQueryFilter, 1),
                new AlongMovableVelocityRotatableController(_enemy, _enemy)
                );

            _towerController = new AlongMovableVelocityRotatableController(_character, _tower);

            _agentEnemyController = new AgentCharacterAgroController(_agentEnemy, _character.transform, 30, 2, 1);

            _characterController.Enable();
            _enemyController.Enable();
            _towerController.Enable();
            _agentEnemyController.Enable();
        }

        private void Start()
        {
            _enemy.gameObject.SetActive(false);
            _tower.gameObject.SetActive(false);
            _agentEnemy.gameObject.SetActive(false);
        }

        private void Update()
        {
            _characterController.Update(Time.deltaTime);
            //_enemyController.Update(Time.deltaTime);
            //_towerController.Update(Time.deltaTime);
            //_agentEnemyController.Update(Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            /*
            NavMeshQueryFilter navMeshQueryFilter = new NavMeshQueryFilter();
            navMeshQueryFilter.agentTypeID = 0;
            navMeshQueryFilter.areaMask = NavMesh.AllAreas;

            NavMesh.CalculatePath(_enemy.transform.position, _character.transform.position, navMeshQueryFilter, _navMeshPath);

            Gizmos.color = Color.yellow;

            if (_navMeshPath.status != NavMeshPathStatus.PathInvalid)
                foreach (Vector3 corner in _navMeshPath.corners)
                    Gizmos.DrawSphere(corner, .3f);

            */
        }
    }
}