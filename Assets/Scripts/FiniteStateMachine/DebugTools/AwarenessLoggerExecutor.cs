using Assets.Scripts.FiniteStateMachine.Awareness;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DebugTools
{
    public class AwarenessLoggerExecutor : MonoBehaviour
    {
        [SerializeField] private IAwareness awareness;
        [SerializeField] private Text hasTarget;
        [SerializeField] private Text targetDistance;
        [SerializeField] private Text targetHorizontalDistance;

        [SerializeField] private float timeToWaitBetweenChecks;

        private AwarenessLogger logger;
        private bool executing;

        private void Awake()
        {
            logger = new AwarenessLogger(hasTarget, targetDistance, targetHorizontalDistance);
        }

        private void Start()
        {
            executing = true;
            StartCoroutine(Execute());
        }

        private IEnumerator Execute()
        {
            while (executing)
            {
                bool hasTarget = awareness.HasTarget();
                logger.SetHasTarget(hasTarget);

                if (hasTarget)
                    logger.SetDistance(awareness.GetTargetDistance(), awareness.GetTargetHorizontalDistance());

                yield return new WaitForSeconds(timeToWaitBetweenChecks);
            }
        }
    }
}
