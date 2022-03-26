using UnityEngine;

public class LivesPanel : MonoBehaviour
{
    [SerializeField] private BonusLifeUI _life;
    public GameObject BonusLifeUI => _life.gameObject;
}
