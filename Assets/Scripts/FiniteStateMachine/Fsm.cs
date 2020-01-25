using Assets.Scripts.FiniteStateMachine.Awareness;
using Assets.Scripts.FiniteStateMachine.DebugTools;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    public abstract class Fsm : MonoBehaviour
    {
        [SerializeField] private AiStateLogger logger;

        public Controller.CharacterController controller;
        public IAwareness awareness;
        private State currentState;
        private Transition[] mutualTransitions;

        private void Awake()
        {
            logger = GetComponent<AiStateLogger>();
            controller = GetComponent<Controller.CharacterController>();
            if (controller == null)
                throw new System.Exception("IAiController not found in " + gameObject.name);

            SetupStates();
        }

        protected abstract void SetupStates();

        protected void SetAwareness(IAwareness awareness) => this.awareness = awareness;

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

            if (logger != null) logger.SetCurrentState(currentState.ToString());
        }

        public void ChangeState(State newState)
        {
            if (!currentState.CanStateBeChanged()) return;

            currentState.OnExit();
            newState.OnEnter();
            currentState = newState;
           
            if (logger != null) logger.SetCurrentState(currentState.ToString());
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
