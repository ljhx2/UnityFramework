using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class BaseScene : MonoBehaviour
{
    [SerializeField] protected Camera _mainCamera;
    [SerializeField] protected CanvasGroup _fadeGroup;
    [SerializeField] protected Image _fadeImage;
    [SerializeField] protected Transform _spawnPosition;

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
        Application.targetFrameRate = 60;
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("Init") == false)
        {
            try
            {
                Managers.Scene.NextSceneType = (Define.Scene)System.Enum.Parse(typeof(Define.Scene), sceneName, true);
                SceneManager.LoadScene("Init");
            }
            catch
            {
                Managers.Scene.NextSceneType = Define.Scene.Unknown;
            }
        }
    }
#endif
    protected virtual void Reset()
    {
        if (_fadeGroup == null)
            _fadeGroup = transform.GetComponentInChildren<CanvasGroup>();
        if (_fadeImage == null)
            _fadeImage = _fadeGroup.GetComponentInChildren<Image>();
        gameObject.tag = "Scene";

        if (_fadeGroup != null)
        {
            _fadeGroup.alpha = 0f;
            _fadeGroup.blocksRaycasts = false;
        }
        _mainCamera = Camera.main;
        _spawnPosition = transform.GetComponentByName<Transform>("spawnPosition");
    }
    private void Awake()
    {
        if (_fadeGroup != null)
        {
            _fadeGroup.alpha = 1f;
            _fadeGroup.blocksRaycasts = true;
        }

        if (_fadeImage != null)
        {
            _fadeImage.color = Managers.Scene.FadeColor;
        }
    }

 
    public virtual IEnumerator Co_InitAsync()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

        yield break;
    }

    public virtual void Clear() { }

    public virtual IEnumerator Co_FadeIn(Color fadeColor, float duration = 0.5f)
    {
        if (duration > 0f)
        {
            RatioTimer rt = new RatioTimer(duration);
            _fadeImage.color = fadeColor;
            while (rt.Ratio < 1f)
            {
                rt.Update(Time.deltaTime);
                _fadeGroup.alpha = 1f - rt.Ratio;
                yield return null;
            }
        }
        _fadeGroup.alpha = 0f;
        _fadeGroup.blocksRaycasts = false;

        yield break; ;
    }
    public virtual IEnumerator Co_FadeOut(Color fadeColor, float duration = 0.5f)
    {
        _fadeGroup.blocksRaycasts = true;
        if (duration > 0f)
        {
            _fadeImage.color = fadeColor;
            RatioTimer rt = new RatioTimer(duration);
            while (rt.Ratio < 1f)
            {
                rt.Update(Time.deltaTime);
                _fadeGroup.alpha = rt.Ratio;
                yield return null;
            }
        }
        _fadeGroup.alpha = 1f;

        yield break;
    }
}
