using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BodyCircleCollider : BodyCollider
{
    [SerializeField] private CircleCollider2D circleCollider;
    public override Collider2D Collider => circleCollider;
    public override Vector3 Size { get => new Vector3(circleCollider.radius, circleCollider.radius, circleCollider.radius); set => circleCollider.radius = value.Max(); }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (circleCollider == null)
        {
            circleCollider = GetComponent<CircleCollider2D>();
            if (circleCollider != null)
                EditorUtility.SetDirty(this);
        }
    }
#endif
}