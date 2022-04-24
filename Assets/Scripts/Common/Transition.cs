
namespace Common
{
    public class Transition : ITransition
    {
        public ICondition Condition { get; }
        public IState To { get; }

        public Transition(IState to, ICondition condition)
        {
            To = to;
            Condition = condition;
        }
    }
}
