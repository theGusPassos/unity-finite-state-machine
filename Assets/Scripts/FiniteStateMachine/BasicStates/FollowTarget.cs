namespace Assets.Scripts.FiniteStateMachine.BasicStates
{
    public class FollowTarget : State
    {
        private int lastDirection;
        private bool stopped = false;

        public FollowTarget(Fsm fsm) : base(fsm)
        {
        }

        protected override void Execute()
        {
            int targetDirection = fsm.awareness.GetTargetDirection();

            if (targetDirection != lastDirection || stopped)
            {
                stopped = false;
                fsm.controller.SetXInput(targetDirection);
            }

            lastDirection = targetDirection;
        }

        public override void OnEnter()
        {
            stopped = true;
            lastDirection = 0;
        }

        public override void OnExit()
        {
            fsm.controller.SetXInput(0);
        }

        public override string ToString()
        {
            return "Follow Target";
        }
    }
}
