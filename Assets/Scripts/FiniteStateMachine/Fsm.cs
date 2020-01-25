using Assets.Scripts.FiniteStateMachine.Awareness;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    public abstract class Fsm : MonoBehaviour
    {
        public Controller.CharacterController controller;
        public IAwareness playerAwareness;
        private State currentState;
        private Transition[] mutualTransitions;

        private void Awake()
        {
            controller = GetComponent<Controller.CharacterController>();
            if (controller == null)
                throw new System.Exception("IAiController not found in " + gameObject.name);

            SetupStates();
        }

        protected abstract void SetupStates();

        private void Update()
        {
            currentState.Update();

            if (currentState.CanStateBeChanged())
            {
                TestMutualTransitions();
            }
        }

        protected void AddMutualTransitions(params Transition[] transitions) => this.mutualTransitions = transitions;

        protected void SetupFirstState(State state)
        {
            currentState = state;
            currentState.OnEnter();
        }

        public void ChangeState(State newState)
        {
            if (!currentState.CanStateBeChanged()) return;

            currentState.OnExit();
            newState.OnEnter();
            currentState = newState;
        }

        private void TestMutualTransitions()
        {
            if (mutualTransitions == null) return;
            foreach (var transition in mutualTransitions)
            {
                if (transition.IsValid())
                {
                    transition.OnTransition();
                    ChangeState(transition.GetNextState());
                }
            }
        }
    }
}
