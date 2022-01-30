using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DJImmortality : MonoBehaviour
{
    public static DJImmortality Instance;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(this);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;
    }
}
