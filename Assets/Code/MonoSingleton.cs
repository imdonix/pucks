using System.Collections;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
{
    public static T Instance { private set; get; }

    private void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            MonoSingleton<T>.Instance = gameObject.GetComponent<T>();
        }
    }
}
