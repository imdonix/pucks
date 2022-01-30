using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void PlayPVP()
    {
        Settings.PVP = true;
        Play();
    }

    public void PlayPVC()
    {
        Settings.PVP = false;
        Play();
    }


    private void Play()
    {
        SceneManager.LoadScene("Main");
    }
}
