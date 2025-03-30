using System;
using UnityEngine;

public enum HitType
{
    none,
    solid,
    rigidbody,
    destructible,
}
public enum DamageType
{
    none,
    bullet,
    projectile,
    explosion,
    fire,
    physics,
}
[Flags]
public enum HurtState
{
    none = 2,
    miss = 4,
    hurt = 8,
    friend = 16,
    kill = 32,
}
public enum HealType
{
    none,
    self,
    friend,
}
public struct DetectStruct
{
    public Collider2D collider;
    public Vector3 position, normal;

    public DetectStruct(Collider2D collider, Vector3 position, Vector3 normal)
    {
        this.collider = collider;
        this.position = position;
        this.normal = normal;
    }
    public DetectStruct(Collision2D collision)
    {
        collider = collision.collider;
        if (collision.contactCount > 0)
        {
            var contact = collision.GetContact(0);
            position = contact.point;
            normal = contact.normal;
        }
        else
        {
            position = Vector3.zero;
            normal = Vector3.zero;
        }
    }
    public DetectStruct(Collider2D self, Collider2D collider)
    {
        this.collider = collider;
        position = (self.bounds.center + collider.bounds.center)/2f;
        normal = (self.bounds.center - collider.bounds.center).normalized;
    }
    public DetectStruct(RaycastHit2D hit)
    {
        collider = hit.collider;
        position = hit.point;
        normal = hit.normal;
    }
    public override string ToString()
    {
        return $"Collider: {collider.name}, Position: {position}, Normal: {normal}";
    }
}
public struct HitStruct
{
    public HitType type;
    public Collider2D collider;
    public Rigidbody2D rigidbody;
    public IDestructible destructible;
    public Vector3 position, normal;

    public HitStruct(DetectStruct detect) : this()
    {
        collider = detect.collider;
        position = detect.position;
        normal = detect.normal;
        if (!DefineDestructible())
            if (!DefineRigidbody())
                type = HitType.solid;
    }
    private bool DefineDestructible()
    {
        destructible = collider.GetComponent<IDestructible>();
        if (!object.Equals(destructible, null) && destructible.IsAlive)
        {
            type = HitType.destructible;
            return true;
        }
        return false;
    }
    private bool DefineRigidbody()
    {
        if (!object.Equals(collider.attachedRigidbody, null))
        {
            rigidbody = collider.attachedRigidbody;
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                type = HitType.rigidbody;
                return true;
            }
        }
        return false;
    }
    public override string ToString()
    {
        return $"Collider: {collider.name}, Type: {type}, Position: {position}, Normal: {normal}";
    }
}
public struct DamageStruct
{
    public float amount;
    public float power;
    public DamageType type;
    public byte team;
    public Vector3 origin;
    public Vector3 direction;

    public DamageStruct(float amount, float power, DamageType type, byte team, Vector3 origin, Vector3 direction) : this()
    {
        this.amount = amount;
        this.power = power;
        this.type = type;
        this.team = team;
        this.origin = origin;
        this.direction = direction;
    }
    public bool TeamCompare(byte otherTeam)
    {
        return team == otherTeam && otherTeam != 0;
    }
    public override string ToString()
    {
        return $"Amount: {amount}, Power: {power}, Type: {type}, Team: {team}, Origin: {origin}, Direction: {direction}";
    }
}
public struct HurtStruct
{
    public HurtState state;
    public DamageStruct damage;
    public Vector3 position;
    public Vector3 normal;
    public float overDamage;
    public HurtStruct(HurtState state, DamageStruct damage, Vector3 position, Vector3 normal, float overDamage)
    {
        this.state = state;
        this.damage = damage;
        this.position = position;
        this.normal = normal;
        this.overDamage = overDamage;
    }
    public HurtStruct(HurtState state, DamageStruct damage, Vector3 position, Vector3 normal)
    {
        this.state = state;
        this.damage = damage;
        this.position = position;
        this.normal = normal; 
        overDamage = 0f;
    }
    public HurtStruct(DamageStruct damage, Vector3 position, Vector3 normal)
    {
        this.state = HurtState.miss;       
        this.damage = damage;
        this.position = position;
        this.normal = normal;
        overDamage = 0f;
    }
    public override string ToString()
    {
        return $"Type: {state}, Damage: {damage}, Position: {position}, Normal: {normal}, OverDamage: {overDamage}";
    }
}
public struct HealStruct
{
    public float amount;
    public HealType type;
    public byte team;
    public Vector2 origin;

    public HealStruct(float amount, HealType type, byte team, Vector2 origin)
    {
        this.amount = amount;
        this.type = type;
        this.team = team;
        this.origin = origin;
    }
}


