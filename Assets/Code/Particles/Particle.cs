using System.Collections;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public abstract class Particle : MonoBehaviour
{
    protected ParticleSystem system;
    protected float live;

    #region UNITY

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }

    public void Start()
    {
        live = 0;
    }

    private void Update()
    {
        live += Time.deltaTime;
        if (Alive() < live)
        {
            gameObject.SetActive(false);
            PPool.Instance.Unload(this);
        }
    }

    #endregion

    public abstract float Alive();
}
