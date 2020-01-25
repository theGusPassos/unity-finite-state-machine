using Assets.Scripts.FiniteStateMachine.Awareness;

namespace Assets.Scripts.FiniteStateMachine.BasicTransitions
{
    public class HasTarget : Transition
    {
        private readonly IAwareness awareness;

        public HasTarget(State state, IAwareness awareness) : base(state)
        {
            this.awareness = awareness;
        }

        public override bool IsValid() => awareness.HasTarget();

        public override void OnTransition() { }

        public override string ToString()
        {
            return "Has Target";
        }
    }
}
