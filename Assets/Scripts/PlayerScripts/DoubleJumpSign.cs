using GameObjectsScripts;
using UnityEngine;

public class DoubleJumpSign : MonoBehaviour, IShowable
{
    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
