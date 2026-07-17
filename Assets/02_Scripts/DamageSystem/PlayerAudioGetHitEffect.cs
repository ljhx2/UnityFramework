using UnityEngine;

public class PlayerAudioGetHitEffect : MonoBehaviour, IDamageable
{
    [SerializeField] private AudioSource _audioSource;

    public void TakeDamage(DamageData damageData)
    {
        _audioSource.Play();
    }
}
