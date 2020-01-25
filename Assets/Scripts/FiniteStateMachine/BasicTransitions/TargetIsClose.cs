using Assets.Scripts.FiniteStateMachine.Awareness;

namespace Assets.Scripts.FiniteStateMachine.BasicTransitions
{
    public class TargetIsClose : Transition
    {
        private readonly float distance;
        private readonly IAwareness awareness;

        public TargetIsClose(State state, float distance, IAwareness awareness) : base(state)
        {
            this.distance = distance;
            this.awareness = awareness;
        }

        public override bool IsValid() => awareness.GetTargetDistance() < distance;

        public override void OnTransition() { }

        public override string ToString()
        {
            return "Target is Close";
        }
    }
}
