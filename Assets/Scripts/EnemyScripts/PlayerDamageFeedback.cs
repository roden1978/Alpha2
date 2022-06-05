using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class PlayerDamageFeedback : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _feedbackColor;
        [SerializeField] private InteractableObjectsCollector _interactableObjectsCollector;
        [SerializeField] [Range(.1f, 1f)]private float _delay;
        [SerializeField] private AudioSource _damageSoundFx;
        private Color _originalColor;

        private void Awake()
        {
            _interactableObjectsCollector.DamageCollecting += OnHealthUpdate;
        }

        private void OnHealthUpdate(int damage)
        {
            StartCoroutine(ChangeColor());
            _damageSoundFx.Play();
        }

        private IEnumerator ChangeColor()
        {
            _spriteRenderer.color = _feedbackColor;
            yield return new WaitForSeconds(_delay);
            _spriteRenderer.color = _originalColor;
        }

        private void Start()
        {
            _originalColor = _spriteRenderer.color;
        }
    
    
    }
}
