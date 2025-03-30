using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyCollider : MonoBehaviour
{
    public System.Action<Collider2D> OnTriggerEnter;
    public System.Action<Collider2D> OnTriggerExit;
    public System.Action<Collision2D> OnCollisionEnter;
    public System.Action<Collision2D> OnCollisionExit;
    public abstract Collider2D Collider { get; }
    public virtual Vector3 Center {get => Offset + transform.position; }
    public virtual Vector3 Offset { get => Collider.offset; set => Collider.offset = value; }
    public abstract Vector3 Size { get; set; }
    public virtual Vector3 Extents { get => Size*0.5f; set => Size = value*2f; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter?.Invoke(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit?.Invoke(other);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {     
        OnCollisionEnter?.Invoke(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(collision);
    }
}