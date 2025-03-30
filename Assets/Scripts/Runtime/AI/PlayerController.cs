using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool canInput = true;
    private CameraObject cameraObject;
    [SerializeField] private Character character;
    private Vector3 target;
    public Character Player => character;
    public bool CanInput { get => canInput; set { if (canInput != value) { canInput = value; character.Stop(); } } }
    private void Awake()
    {
        character.OnHurt += OnHurt;
        character.OnDeath += OnDeath;
    }
    private void Start()
    {
        cameraObject = GameInstance.Instance.GameState.CameraObject;
        cameraObject.SetTarget(character.transform);


    }
    private void OnDestroy()
    {
        character.OnHurt -= OnHurt;
        character.OnDeath -= OnDeath;
    }
    private void OnHurt(HurtStruct hurt)
    {
        if (hurt.damage.type != DamageType.none)
            cameraObject.StartShake(0.25f, 0.01f * hurt.damage.amount);

    }
    private void OnDeath(HurtStruct hurt)
    {
        OnHurt(hurt);
        character.Movement.Momentum = Vector3.zero;
        character.Movement.Stop();
    }
    private void Process()
    {
        if (GameInstance.Instance.GameState.InPause)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                GameInstance.Instance.GameState.Resume();
                //GameInstance.Instance.UI.Change("process");
            }
        }
        else
        {

            if (canInput && character.IsAlive)
            {
                target = cameraObject.Main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0f;

                Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                character.Movement.Move(dir);

                if (Input.GetKeyDown(KeyCode.Space))
                    character.Sprint.Sprint();
                else if (Input.GetKeyUp(KeyCode.Space))
                    character.Sprint.CancelSprint();

                if (Input.GetMouseButtonDown(1))
                {
                    character.Dash.Dash((target - character.Center).normalized);
                }
            }
            if (Input.GetButtonDown("Cancel"))
            {
                GameInstance.Instance.GameState.Pause();
                // GameInstance.Instance.UI.Change("pause");
            }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    cameraObject.StartShake(0.25f,0.25f);
            //}
        }
    }
    private void LateUpdate()
    {
        if (GameInstance.Instance.GameState.State == GameStateEnum.start)
        {
            Process();
        }
    }
}
