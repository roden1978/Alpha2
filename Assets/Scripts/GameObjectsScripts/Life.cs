namespace GameObjectsScripts
{
    public class Life : PickableObject
    {
        private const int LifeValue = 1;
        private void Start()
        {
            Value = LifeValue;
        }
    }
}
