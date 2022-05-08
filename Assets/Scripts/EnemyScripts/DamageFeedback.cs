using System.Collections;
using UnityEngine;

namespace EnemyScripts
{
    public class DamageFeedback : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _feedbackColor;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] [Range(.1f, 1f)]private float _delay;
        private Color _originalColor;

        private void Awake()
        {
            _enemyHealth.HealthUpdate += OnHealthUpdate;
        }

        private void OnHealthUpdate()
        {
            StartCoroutine(ChangeColor());
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
