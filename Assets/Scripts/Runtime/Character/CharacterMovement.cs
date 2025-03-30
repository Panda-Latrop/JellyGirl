using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private bool canMove = true;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float speed = 10f,accel = 100f,decel = 100f;
    private float speedScale = 1f;
    private Vector3 targetVelocity;
    private Vector3 momentum;
    private Vector3 direction;
    public bool CanMove { get => canMove; set => canMove = value; }
    public Rigidbody2D Rigidbody => rig;
    public Vector3 Position => rig.position;
    public Vector3 Momentum { get => momentum; set => momentum = value; }
    public Vector3 Direction => direction;
    public float Speed { get => speed* speedScale; set => speed = value; }
    public float SpeedScale { get => speedScale; set => speedScale = value; } 
    public void Move(Vector3 direction)
    {
        this.direction = direction;
        targetVelocity = this.direction * Speed;
    }
    private Vector3 HandleMomentum(Vector3 targetVelocity)
    {
        if (targetVelocity.sqrMagnitude < momentum.sqrMagnitude)
            momentum = Vector3.MoveTowards(momentum, targetVelocity, decel * Time.fixedDeltaTime);
        else
            momentum = Vector3.MoveTowards(momentum, targetVelocity, accel * Time.fixedDeltaTime);
    return momentum;
    }
    private void HandleRigidbody()
    {
        rig.velocity = HandleMomentum(targetVelocity);
    }
    private void FixedUpdate()
    {
        HandleRigidbody();
    }
    public void Stop()
    {
        targetVelocity = Vector3.zero;
    }
}
