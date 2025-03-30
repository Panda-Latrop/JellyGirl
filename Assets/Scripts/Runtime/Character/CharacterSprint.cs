using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprint : MonoBehaviour
{
    private bool canSprint = true, inSprint;
    [SerializeField] private CharacterMovement movement;
    [SerializeField, Min(1f)] private float sprintScale = 2f;
    [SerializeField] private float maxStamina = 100f;
    private float stamina;
    [SerializeField] private float staminaConsume = 50f, staminaRestore = 50f;
    [SerializeField] private Timer timerToRestoreStamina = new Timer(1f);
    public bool CanSprint { get => canSprint; set { canSprint = value; if (!value) inSprint = false; } }
    public float MaxStamina { get => maxStamina; }
    public float Stamina { get => stamina; set => stamina = Mathf.Max(value, 0f); }
    public bool InSprint { get => inSprint && movement.Momentum.sqrMagnitude >= 0.01f && stamina > 0; }
    private void Awake()
    {
        stamina = maxStamina;
    }
    public void Sprint()
    {
        if (canSprint)
        {

            if (!inSprint)
            {
                inSprint = true;
                if (InSprint)
                    movement.SpeedScale = sprintScale;
                else
                    movement.SpeedScale = 1f;
            }
        }
        else
            inSprint = false;
    }
    public void CancelSprint()
    {
        inSprint = false;
    }
    private void Update()
    {
        if (InSprint)
        {
            movement.SpeedScale = sprintScale;
            stamina = Mathf.MoveTowards(stamina, 0f, staminaConsume * Time.deltaTime);
            timerToRestoreStamina.Run();
            return;
        }
        else
            movement.SpeedScale = 1f;
        if (timerToRestoreStamina.Check())
        {
            stamina = Mathf.MoveTowards(stamina, maxStamina, staminaRestore * Time.deltaTime);
        }
    }
    public void Stop()
    {
        CancelSprint();
    }
}
