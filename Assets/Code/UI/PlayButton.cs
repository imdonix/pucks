using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void PlayPVP()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayPVC()
    {
        SceneManager.LoadScene("Main");
    }
}
