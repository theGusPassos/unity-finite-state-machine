namespace Assets.Scripts.FiniteStateMachine
{
    public abstract class State
    {
        protected Fsm fsm;
        private Transition[] transitions;

        public State(Fsm fsm)
        {
            this.fsm = fsm;
        }

        public void SetTransitions(params Transition[] transitions)
            => this.transitions = transitions;

        public void Update()
        {
            Execute();

            if (CanStateBeChanged())
            {
                CheckTransitions();
            }
        }

        public abstract void OnEnter();
        protected abstract void Execute();
        public abstract void OnExit();

        private void CheckTransitions()
        {
            if (transitions == null) return;
            foreach (var transition in transitions)
            {
                if (transition.IsValid())
                {
                    transition.OnTransition();
                    fsm.ChangeState(transition.GetNextState());
                }
            }
        }

        public virtual bool CanStateBeChanged() => true;
    }
}
