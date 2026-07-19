using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 플레이어가 피해를 입을 때 화면에 붉은색 오버레이를 생성
/// </summary>
public class PlayerTakeDamageEffect : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    private Vignette _vignette;

    [SerializeField] private float _damageDuration = 0.5f;
    private float _currentTime = 0f;

    [SerializeField] private AnimationCurve _curve;

    private void Start()
    {
        _volume.profile.TryGet<Vignette>(out _vignette);
    }

    public void TriggerHitEffect()
    {
        _currentTime = 0f;
        _vignette.intensity.Override(0f);
        //StartCoroutine(HitCoroutine());
        HitForget().Forget();
    }

    private async UniTaskVoid HitForget()
    {
        while (_currentTime <= _damageDuration)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);

            _currentTime += Time.deltaTime;
            float currentValue = Mathf.Clamp01(_currentTime / _damageDuration);
            _vignette.intensity.Override(_curve.Evaluate(currentValue));
        }
        _vignette.intensity.Override(0f);
    }
    private IEnumerator HitCoroutine()
    {
        while (_currentTime <= _damageDuration)
        {
            _currentTime += Time.deltaTime;
            float currentValue = Mathf.Clamp01(_currentTime / _damageDuration);
            _vignette.intensity.Override(_curve.Evaluate(currentValue));
            yield return null;
        }
        _vignette.intensity.Override(0f);
    }
}
