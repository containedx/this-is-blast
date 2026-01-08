using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    protected virtual void Awake()
    {
        views.Add(GetType(), this);
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
