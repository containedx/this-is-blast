using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    protected virtual void Awake()
    {
        views.Add(GetType(), this);
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        Hide();
    }

    protected virtual void OnDestroy()
    {
        views.Remove(GetType());
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    private Coroutine showRoutine;
    public void ShowDelayed(float delay)
    {
        if (showRoutine != null)
            StopCoroutine(showRoutine);

        Show();
        canvasGroup.alpha = 0f;
        showRoutine = StartCoroutine(ShowDelayedRoutine(delay));
    }

    private IEnumerator ShowDelayedRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;
        const float duration = 0.8f;

        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }


    // - - - - - - - Menus access - - - - - - - - -

    private static Dictionary<Type, View> views = new();
    public static T Get<T>() where T : View
    {
        if (views.TryGetValue(typeof(T), out View view))
        {
            return view as T;
        }
        throw new TypeAccessException($"No view in scene {typeof(T)}");
    }
}
