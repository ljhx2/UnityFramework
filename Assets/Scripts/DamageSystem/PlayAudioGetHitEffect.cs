using UnityEngine;

public class PlayAudioGetHitEffect : MonoBehaviour, IDamageable
{
    [SerializeField] private AudioSource _audioSource;

    public void TakeDamage(DamageData damageData)
    {
        _audioSource.Play();
    }
}
