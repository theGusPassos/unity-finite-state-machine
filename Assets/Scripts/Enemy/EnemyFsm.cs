using Assets.Scripts.FiniteStateMachine;
using Assets.Scripts.FiniteStateMachine.Awareness;
using Assets.Scripts.FiniteStateMachine.BasicStates;
using Assets.Scripts.FiniteStateMachine.BasicTransitions;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyFsm : Fsm
    {
        [SerializeField] private AreaAwareness areaAwareness;

        [SerializeField] private PatrolData patrolData;
        [SerializeField] private float distanceToStopFollowing;

        protected override void SetupStates()
        {
            SetAwareness(areaAwareness);

            State patrol = new Patrol(this, patrolData);
            State followTarget = new FollowTarget(this);

            Transition patrolToFollow = new HasTarget(followTarget, areaAwareness);
            Transition followToPatrol = new TargetIsFar(patrol, distanceToStopFollowing, areaAwareness);

            patrol.SetTransitions(patrolToFollow);
            followTarget.SetTransitions(followToPatrol);

            SetupFirstState(patrol);
        }
    }
}
