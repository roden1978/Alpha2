using Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public Game()
        {
            RegisterInputService(); 
        }
        
        private void RegisterInputService()
        {
            if (Application.isEditor)
                InputService = new KeyboardInputService();
            else
                InputService = new UiInputService();
        }
    }
}
