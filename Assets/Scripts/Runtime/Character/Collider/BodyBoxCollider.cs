using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BodyBoxCollider : BodyCollider
{
    [SerializeField] private BoxCollider2D boxCollider;
    public override Collider2D Collider => boxCollider;
    public override Vector3 Size { get => boxCollider.size; set => boxCollider.size = value; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
            if(boxCollider != null)
                EditorUtility.SetDirty(this);
        }
    }
#endif
}
