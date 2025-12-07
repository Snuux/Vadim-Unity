using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MobaLesson
{
    public class PlayerMouseAgentController : Controller
    {
        private const float DefaultRayDistance = 1000f;
        private const float Treshhold = 0.1f;

        private Vector3 _destinationPosition;

        private AgentMover _agent;

        public PlayerMouseAgentController(AgentMover agent)
        {
            _agent = agent;
        }

        public override void UpdateLogic(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = ScreenPointToRay();
                Vector3 mouseWorldPos = MouseWorldPosition();
                _destinationPosition = GetHitPoint(ray);
            }

            //
            //TODO
            //

            _agent.SetDestination(_destinationPosition);
        }

        private static Vector3 GetHitPoint(Ray ray)
        {
            Vector3 hitPoint = Vector3.zero;

            if (Physics.Raycast(ray, out RaycastHit hit, DefaultRayDistance))
                hitPoint = hit.point;
            return hitPoint;
        }

        private Ray ScreenPointToRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

        private Vector3 MouseWorldPosition()
            => Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, DefaultRayDistance)
            );
    }
}