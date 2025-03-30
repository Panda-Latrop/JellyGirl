using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Trigger : MonoBehaviour
{
    [SerializeField] private Collider2D triggetCollider;
    [SerializeField] private bool singleExecute;
    [SerializeField] private bool onlyForPlayer;
    private int executionCount = 0;

    private List<Character> targets = new List<Character>();
    public event System.Action<Character> OnTriggerEnter;
    public event System.Action<Character> OnTriggerStay;
    public event System.Action<Character> OnTriggerExit;

    public int ExecutionCount => executionCount;
    public bool IsSingleExecute { get => singleExecute; set => singleExecute = value; }
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (triggetCollider == null)
        {
            triggetCollider = GetComponent<Collider2D>();
            if (triggetCollider != null)
                EditorUtility.SetDirty(this);
        }
        if (triggetCollider != null &&!triggetCollider.isTrigger)
        {
            triggetCollider.isTrigger = true;

            EditorUtility.SetDirty(triggetCollider);

        }
    }
#endif
    protected virtual void HandleEnter(Character destructible)
    {
        executionCount++;
        OnTriggerEnter?.Invoke(destructible);
        if (singleExecute)
        {
            triggetCollider.enabled = false;
            enabled = false;
        }
        else
        {
            targets.Add(destructible);
        }
    }
    protected virtual void HandleStay(Character destructible)
    {
        OnTriggerStay?.Invoke(destructible);
    }
    protected virtual void HandleExit(Character destructible)
    {
        OnTriggerExit?.Invoke(destructible);
        targets.Remove(destructible);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character destructible;
        bool player = (onlyForPlayer && string.Equals(collision.gameObject.tag, "Player")) || !onlyForPlayer;
        if (player && collision.TryGetComponent(out destructible))
        {
            HandleEnter(destructible);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Character destructible;
        if (collision.TryGetComponent(out destructible))
        {
            HandleExit(destructible);
        }
    }
    private void LateUpdate()
    {
        if (triggetCollider.enabled)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                HandleStay(targets[i]);
            }
        }else enabled = false;
    }
}
