using UnityEngine;
using UDOGames;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _bloodExploison;
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _maxHealth;


    private float _currentHealth;
    private bool _isDeath;


    private void Start()
    {
        _currentHealth = _maxHealth;
    }


    public void PlayBloodExplosion()
    {
        _bloodExploison.Play();
    }




    public void TakeDamage(float value)
    {
        if (!_isDeath)
        {
            DecreaseHealth(value);
            PlayBloodExplosion();
        }

    }



    private void DecreaseHealth(float value)
    {
        _currentHealth -= value;

        if (_currentHealth <= 0)
        {
            _isDeath = true;
            GameManager.Instance.IsGameEnd = true;
            UIManager.Instance.SetGameEndPanelMoves();
            PlayDeathAnimation();
        }
    }



    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(AnimationType.DEATH);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.MATERIAL))
        {
            if (!other.gameObject.GetComponent<MaterialController>().IsCollectable)
            {
                other.gameObject.GetComponentInParent<PlayerDataTransmitter>().SetFalseColliderActivity();
                TakeDamage(_damageAmount);
            }
        }
    }
}
