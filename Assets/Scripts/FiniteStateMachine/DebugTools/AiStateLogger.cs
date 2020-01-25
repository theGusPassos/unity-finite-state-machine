using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FiniteStateMachine.DebugTools
{
    public class AiStateLogger : MonoBehaviour
    {
        [SerializeField] private Text stateLabel;

        public void SetCurrentState(string newState)
        {
            stateLabel.text = newState;
        }
    }
}
