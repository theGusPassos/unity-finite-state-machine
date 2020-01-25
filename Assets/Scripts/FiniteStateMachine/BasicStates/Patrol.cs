using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine.BasicStates
{
    public class Patrol : State
    {
        private readonly PatrolData patrolData;

        private int direction;
        private float currentTimer;
        private bool walking = false;

        public Patrol(Fsm fsm, PatrolData patrolData) : base(fsm)
        {
            this.patrolData = patrolData;
        }

        protected override void Execute()
        {
            currentTimer += Time.deltaTime;

            if (walking)
            {
                if (currentTimer > patrolData.timeWalking)
                {
                    walking = false;
                    currentTimer = 0;
                    fsm.controller.SetXInput(0);

                    // changes dir for next walk
                    direction = -direction;
                }
            }
            else
            {
                if (currentTimer > patrolData.timeStopped)
                {
                    walking = true;
                    currentTimer = 0;
                    fsm.controller.SetXInput(direction);
                }
            }
        }

        public override void OnEnter()
        {
            currentTimer = 0;
            direction = patrolData.firstDirection;
        }

        public override void OnExit()
        {
            fsm.controller.SetXInput(0);
        }

        public override string ToString()
        {
            return "Patrol State";
        }
    }

    [System.Serializable]
    public struct PatrolData
    {
        public float timeStopped;
        public float timeWalking;
        public int firstDirection;
    }
}
