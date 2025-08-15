using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public event Action<Collider2D> onEnter;
    public event Action<Collider2D> onStay;
    public event Action<Collider2D> onExit;
    public HashSet<Collider2D> col = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        col.Add(other);
        onEnter?.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        onStay?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        col.Remove(other);
        onExit?.Invoke(other);
    }
}