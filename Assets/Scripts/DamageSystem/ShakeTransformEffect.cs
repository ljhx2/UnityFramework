using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class ShakeTransformEffect : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationCurve _shakeCurve;
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _ShakeIntensity = 0.2f;

    private Vector3 _originalPosition;
    //private Coroutine _shakeCoroutine;
    private bool _isShaking;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    public void Shake()
    {
        //if (_shakeCoroutine != null)
        //{
        //    StopCoroutine(_shakeCoroutine);
        //}
        //_shakeCoroutine = StartCoroutine(ShakeCoroutine());
        
        if (!_isShaking)
        {
            ShakeForget(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }
        
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _shakeCurve.Evaluate(elapsedTime / _shakeDuration);
            transform.position = _originalPosition + Random.insideUnitSphere * strength * _ShakeIntensity;
            transform.position = new Vector3(transform.position.x, _originalPosition.y, transform.position.z);
            yield return null;
        }

        transform.position = _originalPosition;
    }

    private async UniTaskVoid ShakeForget(CancellationToken cancellationToken)
    {
        _isShaking = true;
        float elapsedTime = 0f;

        try
        {
            while (elapsedTime < _shakeDuration)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

                elapsedTime += Time.deltaTime;
                float strength = _shakeCurve.Evaluate(elapsedTime / _shakeDuration);
                Vector3 targetPos = _originalPosition + Random.insideUnitSphere * strength * _ShakeIntensity;
                transform.position = new Vector3(targetPos.x, _originalPosition.y, targetPos.z);
            }
        }
        catch (System.OperationCanceledException)
        {
            return;
        }
        finally
        {
            transform.position = _originalPosition;
            _isShaking = false;
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        Shake();
    }

}
