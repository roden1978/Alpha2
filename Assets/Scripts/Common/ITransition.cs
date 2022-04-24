namespace Common
{
    public interface ITransition
    {
        ICondition Condition { get; }
        IState To { get; }
    }
}
