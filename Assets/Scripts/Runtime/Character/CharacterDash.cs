using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterDash : MonoBehaviour
{
    private bool canDash = true, inDash;
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private float power = 20f;
    [SerializeField] private Timer duration = new Timer(0.5f);
    [SerializeField] private Timer cooldown = new Timer(1f);
    public event System.Action OnDash,OnCooldown;
    public bool CanSprint { get => canDash; set { canDash = value; if (!value) inDash = false; } }
    public bool InDash { get => inDash && movement.Momentum.sqrMagnitude >= 0.01f; }
    public bool CanDash => canDash;
    public float Precent => cooldown.Elapsed / cooldown.Duration;
    public void Dash(Vector3 direction)
    {
        if (canDash)
        {
            if (!inDash && direction.sqrMagnitude >= 0.1f)
            {
                inDash = true;
                duration.Run();
                movement.Momentum = direction * power;
                OnDash?.Invoke();
            }
        }else
            inDash = false;
    }
    public void CancelDash()
    {
        inDash = false;

    }
    private void Update()
    {
        if (InDash && duration.Check() && canDash)
        {
            canDash = false;
            cooldown.Run();
            movement.Momentum = movement.Momentum.normalized * movement.Speed;
            CancelDash();
        }
        else if (!canDash && cooldown.Check())
        {
            canDash = true;
            OnCooldown?.Invoke();
        }
    }
    public void Stop()
    {
        CancelDash();
    }
}

