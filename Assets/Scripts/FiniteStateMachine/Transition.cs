namespace Assets.Scripts.FiniteStateMachine
{
    public abstract class Transition
    {
        private readonly State state;

        public Transition(State state)
        {
            this.state = state;
        }

        public State GetNextState() => state;
        public abstract bool IsValid();
        public abstract void OnTransition();
    }
}
