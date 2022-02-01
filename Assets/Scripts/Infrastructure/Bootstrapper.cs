using Infrastructure;
using UnityEngine;

namespace Infrastracture
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            DontDestroyOnLoad(this);
        }
    }
}
