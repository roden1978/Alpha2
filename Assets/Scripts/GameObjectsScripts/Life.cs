namespace GameObjectsScripts
{
    public class Life : PickableObject
    {
        private const int LifeValue = 1;
        protected override void Start()
        {
            base.Start();
            Value = LifeValue;
        }

        private void Update()
        {
            FloatingMove();        
        }
    }
}
