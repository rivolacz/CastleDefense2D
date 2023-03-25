using Project.StateMachines;
using Project.StateMachines.States;
using Project.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class PlacementAroundTarget : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> positions;
        [SerializeField]
        private LayerMask unitLayerMask;

        private List<Transform> takenPositions = new List<Transform>();

        private List<Unit> unitsWaitingForPosition = new();

        public Transform GetAvailablePosition(Vector3 currentPosition)
        {
            List<Transform> closestTransforms = positions.Except(takenPositions)
                .OrderBy(t => Vector3.Distance(t.position, currentPosition)).ToList();
            Transform closestTransform = closestTransforms.First();
            return closestTransform;
        }

        public void AddToTaken(Unit unit)
        {
            var closestPosition = GetAvailablePosition(unit.transform.position);
            PathFinding.AddObstacle(closestPosition.position);
            takenPositions.Add(closestPosition);
            unit.AssignPlacementToTarget(this, closestPosition);
        }

        public void RemoveFromTaken(Transform position)
        {
            takenPositions.Remove(position);
            PathFinding.RemoveObstacle(position.position);
            if(unitsWaitingForPosition.Count > 0)
            {
                List<Unit> closestUnits = unitsWaitingForPosition
                .OrderBy(unit => Vector3.Distance(unit.transform.position, position.position)).ToList();
                Unit unit = closestUnits.First();
                unit.ChangeState(new MoveState(transform.position, unit.StateMachine, transform));
                unitsWaitingForPosition.Remove(unit);
                takenPositions.Add(position);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision == null) return;
            if(!IsLayerInLayerMask(collision.gameObject.layer)) return;
            if(!collision.TryGetComponent(out Unit unit)) return;

            if (CanAssignPosition())
            {
                AddToTaken(unit);
            }
            else
            {
                AddUnitToQueue(unit);
            }
        }

        private bool CanAssignPosition()
        {
            bool takenPositionsFull = takenPositions.Count == positions.Count;
            return !takenPositionsFull;
        }

        private void AddUnitToQueue(Unit unit)
        {
            if(unitsWaitingForPosition.Contains(unit)) return;
            unitsWaitingForPosition.Add(unit);
            unit.ChangeState(new IdleState(unit.StateMachine));
        }

        private bool IsLayerInLayerMask(int layer)
        {
            bool isInLayerMask = (unitLayerMask.value & (1 << layer)) > 0;
            return isInLayerMask;
        }
    }
}
