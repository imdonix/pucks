using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Effect : MonoBehaviour
{
    protected Map map;
    protected TimeControl control;

    #region UNITY

    private void Awake()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Puck puck = collision.gameObject.GetComponent<Puck>();
        if (!ReferenceEquals(null, puck))
        {
            if (!ReferenceEquals(null, puck.GetPlayer()))
            {
                Play(puck.GetPlayer().Type());
            }
        }
        
        Destroy(gameObject);
    }

    #endregion

    public void Bind(Map map, TimeControl control)
    {
        this.control = control;
        this.map = map;
    }

    public void ParticleCall(HashSet<Vector2Int> effected, int type)
    {
        foreach (Vector2Int grid in effected)
        {
            if (grid.x % 5 == 0 || grid.y % 5 == 0)
            {
                Takeover over = PPool.Instance.Spawn<Takeover>();
                over.transform.position = new Vector3(
                    (grid.x - Map.SIZE / 2) / 5,
                    (grid.y - Map.SIZE / 2) / 5,
                    -1);
                over.SetColor(map.GetColor(type));
            }
        }
    }

    protected abstract void Play(int type);

}