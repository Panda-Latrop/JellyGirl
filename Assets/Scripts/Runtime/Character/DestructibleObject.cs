using Management.Pool;
using System;
using UnityEngine;

public interface IDestructible
{
    public bool IsAlive { get; }
    public float MaxHealth { get; }
    public float Health { get; }
    public float HealthPercent { get; }
    public byte Team { get; set; }
    public HurtStruct Hurt(DamageStruct damage, Vector3 position, Vector3 normal);
    public bool Heal(HealStruct heal);
    public void Kill();
    public void Resurrect();
    public Vector3 Center { get;}

    public event Action<HurtStruct> OnHurt;
    public event Action<HurtStruct> OnDeath;
    public event Action<HealStruct> OnHeal;
    public event Action OnResurrect;

}
public class DestructibleObject : PoolObject, IDestructible
{
    private bool isAlive = true;
    [SerializeField] private float overHealth = 50f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;
    private float percent = 1f;
    [SerializeField] private byte team;

    public event Action<HurtStruct> OnHurt;
    public event Action<HealStruct> OnHeal;
    public event Action<HurtStruct> OnDeath;
    public event Action OnResurrect;

    public bool IsAlive => isAlive;
    public float MaxHealth => maxHealth;
    public float Health => health;
    public float HealthPercent => percent;
    public byte Team { get => team; set => team = value; }
    public virtual Vector3 Center => transform.position;
    private void Awake()
    {
        percent = health / maxHealth;

    }
    public override void OnPop() { Resurrect(); }

    public override void OnPush() {  }
    public HurtStruct Hurt(DamageStruct damage, Vector3 position, Vector3 normal)
    {
        if (isAlive) 
        {
            if (!damage.TeamCompare(team))
            {
                health -= damage.amount;
                percent = health / maxHealth;
                if (isAlive = health > 0f)
                {
                    HurtStruct hurt = new HurtStruct(HurtState.hurt, damage, position, normal);
                    HurtHandler(hurt, Vector3.zero);
                    return hurt;
                }
                else
                {
                    HurtStruct hurt = new HurtStruct(HurtState.kill, damage, position, normal, -health);
                    DeathHandler(hurt,Vector3.zero);
                        
                    return hurt;
                }
            }
            else
                return new HurtStruct(HurtState.friend, damage, position, normal);
        }
        else
            return new HurtStruct(HurtState.miss, damage, position, normal);
    }
    public bool Heal(HealStruct heal)
    {
        if (isAlive && health != maxHealth)
        {
            health = Mathf.Min(maxHealth+ overHealth, health + heal.amount);
            percent = health / maxHealth;
            OnHeal?.Invoke(heal);
            return health == maxHealth;
        }
        return false;
    }
    public void Kill()
    {
        Vector2 position = transform.position;
        Hurt(new DamageStruct(
            health,
            0f,
            DamageType.bullet,
            0,
            position,
            Vector3.forward),
            position,
            Vector3.back);
    }
    public void Resurrect()
    {
        isAlive = true;
        health = maxHealth;
        ResurrectHandler();
    }
    protected virtual void HurtHandler(HurtStruct hurt, Vector3 velocity = default)
    {
        OnHurt?.Invoke(hurt);
        //for (int i = 0; i < hurts.Length; i++)
        //    hurts[i].Handle(hurt, velocity);
    }
    protected virtual void DeathHandler(HurtStruct hurt, Vector3 velocity = default)
    {
        OnDeath?.Invoke(hurt);
        //for (int i = 0; i < deaths.Length; i++)
        //    deaths[i].Handle(hurt, velocity);
    }
    protected virtual void ResurrectHandler()
    {
        OnResurrect?.Invoke();
    }
}