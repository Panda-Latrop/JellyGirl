using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotController : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private float radius = 10f,lostRadius = 20f;
    [SerializeField] private Timer attackRestor = new Timer(0.5f);
    [SerializeField] private bool dieAfterAttack, returnToStart;
    private Vector3 start;
    private Character target;
    private bool inTarget;
    public event System.Action OnTarget;

    public bool InTarget { get => inTarget; set { if(inTarget != value) {  inTarget = value; if(value) OnTarget?.Invoke(); } } }
    private void OnEnable()
    {
        start = transform.position;
    }
    private void Awake()
    {
        owner.OnDeath += OnOwnerDeath;
        owner.OnResurrect += OnOwnerResurrect;
        
    }
    private void OnDestroy()
    {
        target.OnDeath -= OnOwnerDeath;
    }
    private void OnOwnerDeath(HurtStruct obj)
    {
        InTarget = false;
        enabled = false;
    }
    private void OnOwnerResurrect()
    {
        enabled = true;
    }
    private void Start()
    {
        var gameState = GameInstance.Instance.GameState as RaceGameState;
        if (gameState != null)
        {
            target = gameState.Player;
        }
        else
        {
            enabled = false;
        }
    }

    private bool CheckTarget(Character target)
    {
        if (target.IsAlive && attackRestor.Check())
        {
            var sqr = (target.Center - owner.Center).sqrMagnitude;
            var ra = (InTarget ? lostRadius : radius);
            if (sqr < ra * ra)
            {
                if (!InTarget)
                {
                    owner.Animation.ShowFacil("Angry");
                    InTarget = true;
                }
                return true;
            }
        }

        if (InTarget)
        {
            owner.Animation.ShowFacil("Def");
            InTarget = false;
        }
        return false;
    }
    private void ProcessAI()
    {
        if(owner.IsAlive)
        {
            if (CheckTarget(target))
            {
                owner.Movement.Move((target.Center - owner.Center).normalized);
                if (owner.Attack.Attack(target))
                {
                    owner.Animation.ShowFacil("Feed");
                    if (dieAfterAttack)
                    {
                        owner.Kill();
                    }
                    else
                    {
                        InTarget = false;
                    
                        attackRestor.Run();
                    }
                }

            }
            else  if (returnToStart && attackRestor.Check())
            {
                if ((owner.Center - start).sqrMagnitude >= 1f)
                {
                    owner.Movement.Move((start - owner.Center).normalized);
                }
                else
                {
                    owner.Movement.Stop();
                }
            }
            else
            {

                owner.Movement.Move(Vector3.zero);
            }

        }
    }
    private void Update()
    {
        if(GameInstance.Instance.GameState.State == GameStateEnum.start)
        ProcessAI();
        else
            owner.Stop();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(owner != null) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(owner.Center, InTarget? lostRadius: radius);
        }
    }
#endif
}
