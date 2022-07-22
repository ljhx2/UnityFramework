using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public IEnumerator Co_InitAsync()
    {

        yield return null;
    }

    public abstract void Clear();

    public virtual IEnumerator Co_FadeOut(Color fadeColor, float duration = 0.5f)
    {
        yield return null;
    }
    public virtual IEnumerator Co_FadeIn(Color fadeColor, float duration = 0.5f)
    {
        yield return null;
    }
}
