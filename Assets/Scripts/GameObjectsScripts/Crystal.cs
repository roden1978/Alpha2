namespace GameObjectsScripts
{
    public class Crystal : PickableObject
    {
        private const int CrystalValue = 1;
        protected override void Start()
        {
            base.Start();
            Value = CrystalValue;
        }

        private void Update()
        {
            FloatingMove();
        }
    }
}