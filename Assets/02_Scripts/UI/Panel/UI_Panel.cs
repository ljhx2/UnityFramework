using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Panel : UI_Base
{
    protected CanvasGroup _canvasGroup;

    public override void Init()
    {
        base.Init();

        _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    public virtual void Show(float duration = 0f, Action<UI_Panel> completed = null)
    {
        gameObject.SetActive(true);
        StartCoroutine(Co_FadeShow(duration, completed));
    }

    public virtual void Hide(float duration = 0f, Action<UI_Panel> completed = null)
    {
        StartCoroutine(Co_FadeHide(duration, completed));
    }

    IEnumerator Co_FadeShow(float duration, Action<UI_Panel> completed = null)
    {
        yield return null;

        if (duration > 0f)
        {
            _canvasGroup.alpha = 0f;

            RatioTimer rt = new RatioTimer(duration);
            while (rt.Ratio < 1f)
            {
                rt.Update(Time.deltaTime);
                float alpha = rt.Ratio;
                _canvasGroup.alpha = alpha;
                yield return null;
            }
        }
        _canvasGroup.alpha = 1f;
        completed?.Invoke(this);
    }
    IEnumerator Co_FadeHide(float duration, Action<UI_Panel> completed = null)
    {
        yield return null;

        if (duration > 0f)
        {
            RatioTimer rt = new RatioTimer(duration);
            while (rt.Ratio < 1f)
            {
                rt.Update(Time.deltaTime);
                float alpha = 1f - rt.Ratio;
                _canvasGroup.alpha = alpha;
                yield return null;
            }
        }
        _canvasGroup.alpha = 0f;
        completed?.Invoke(this);
        gameObject.SetActive(false);
    }
}
