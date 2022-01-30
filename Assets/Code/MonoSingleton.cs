using System.Collections;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
{
    public static T Instance { private set; get; }

    public virtual void Awake()
    {
        MonoSingleton<T>.Instance = gameObject.GetComponent<T>();
    }
}
