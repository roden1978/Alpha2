using GameObjectsScripts;
using UnityEngine;

public class Crosshair : MonoBehaviour, IShowable
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
