using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BodyCapsuleCollider : BodyCollider
{
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    public override Collider2D Collider => capsuleCollider;
    public override Vector3 Size 
    {
        get => capsuleCollider.size; 
        set 
        {
            if (value.x > value.y)
                capsuleCollider.direction = CapsuleDirection2D.Horizontal;
            else
                capsuleCollider.direction = CapsuleDirection2D.Vertical;
            capsuleCollider.size = value;
        } 
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            if (capsuleCollider != null)
                EditorUtility.SetDirty(this);
        }
    }
#endif
}
