using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Endgame : MonoSingleton<Endgame>
{

    [SerializeField] private Image puck;

    #region UNITY

    public override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    #endregion

    public void Show(int type)
    {
        puck.color = Manager.Instance.GetMap().GetColor(type);
        gameObject.SetActive(true);
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
