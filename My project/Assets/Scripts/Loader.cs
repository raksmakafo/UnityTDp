using UnityEngine;

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
                    Debug.LogWarning($"[Loader] ������ ���� {typeof(T)} �� ������ �� �����.");
                }
                else
                {
                    // ���������, ��� ������ � ��������
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