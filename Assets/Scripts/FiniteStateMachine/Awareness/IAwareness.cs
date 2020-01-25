namespace Assets.Scripts.FiniteStateMachine.Awareness
{
    public interface IAwareness
    {
        bool HasTarget();
        int GetTargetDirection();
        float GetTargetDistance();
        float GetTargetHorizontalDistance();
        T GetComponentFromTarget<T>();
        void ResetTarget();
    }
}
