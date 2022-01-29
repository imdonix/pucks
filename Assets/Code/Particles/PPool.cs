using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PPool : MonoSingleton<PPool>
{
    private Dictionary<Type, Particle> parts;
    private Dictionary<Type, Queue<Particle>> pool;

    public override void Awake()
    {
        base.Awake();

        parts = new Dictionary<Type, Particle>();
        pool = new Dictionary<Type, Queue<Particle>>();
        foreach (var item in Resources.LoadAll<Particle>("Particles"))
        {
            parts.Add(item.GetType(), item);
            Queue<Particle> queue = new Queue<Particle>();
            pool.Add(item.GetType(), queue);
            for (int i = 0; i < 25; i++)
            {
                Particle part = GameObject.Instantiate(item);
                part.gameObject.SetActive(false);
                queue.Enqueue(part);
            }
        } 
    }

    public T Spawn<T>() where T : Particle
    {
        Queue<Particle> queue = pool[typeof(T)];
        if (queue.Count > 0)
        {
            Particle particle = (T)queue.Dequeue();
            particle.gameObject.SetActive(true);
            particle.Start();
            return (T) particle;
        }
        else
        {
            return (T) Instantiate(parts[typeof(T)]);
        }
    }

    public void Unload<T>(T obj) where T : Particle
    {
        pool[obj.GetType()].Enqueue((T) obj);
    }
}
