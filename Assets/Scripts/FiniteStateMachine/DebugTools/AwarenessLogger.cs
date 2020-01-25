using UnityEngine.UI;

namespace Assets.Scripts.DebugTools
{
    public class AwarenessLogger
    {
        private readonly Text hasTarget;
        private readonly Text targetDistance;
        private readonly Text targetHorizontalDistance;

        public AwarenessLogger(Text hasTarget, Text targetDistance, Text targetHorizontalDistance)
        {
            this.hasTarget = hasTarget;
            this.targetDistance = targetDistance;
            this.targetHorizontalDistance = targetHorizontalDistance;
        }

        public void SetDistance(float distance, float horizontalDistance)
        {
            targetDistance.text = "Dist:" + distance;
            targetHorizontalDistance.text = "hDist:" + horizontalDistance;
        }

        public void SetHasTarget(bool hasTarget)
        {
            this.hasTarget.text = hasTarget ? "Has Target" : "!Has Target";
        }
    }
}
