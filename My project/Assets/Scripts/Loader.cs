﻿using UnityEngine;

public class Loader<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();

                if (instance == null)
                {
                    Debug.LogWarning($"[Loader] Объект типа {typeof(T)} не найден на сцене.");
                }
                else
                {
                    // Убедиться, что объект — корневой
                    if (instance.transform.parent != null)
                    {
                        instance.transform.SetParent(null);
                    }

                    DontDestroyOnLoad(instance.gameObject);
                }
            }

            return instance;
        }
    }
}
