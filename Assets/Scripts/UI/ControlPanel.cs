using GameObjectsScripts;
using UnityEngine;

namespace UI
{
    public class ControlPanel : MonoBehaviour, IShowable
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
