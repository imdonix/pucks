using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private Text player;
    [SerializeField] private Text left;
    [SerializeField] private Text center;
    [SerializeField] private Text right;

    #region UNITY

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    #endregion

    public void Set(bool isplayer, string player, Color color, KeyCode left, KeyCode center, KeyCode right)
    {
        this.player.text = player;
        this.player.color = color;

        this.left.text = K(left);
        this.center.text = K(center);
        this.right.text = K(right);

        this.left.transform.parent.gameObject.SetActive(isplayer);
        this.center.transform.parent.gameObject.SetActive(isplayer);
        this.right.transform.parent.gameObject.SetActive(isplayer);

        gameObject.SetActive(true);
    }

    public string K(KeyCode code)
    {
        if (code == KeyCode.LeftArrow)
        {
            return "<";
        }
        else if (code == KeyCode.RightArrow)
        {
            return "^";
        }
        else if (code == KeyCode.UpArrow)
        {
            return ">";
        }

        return code.ToString();
    }

}
