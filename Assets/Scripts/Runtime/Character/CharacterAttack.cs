using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float power = 10f;
    [SerializeField] private float radius = 2.5f;
    [SerializeField] private DamageType damageType;
    [SerializeField] private Timer attactCooldown = new Timer(1f);
    public bool Attack(DestructibleObject target)
    {
        float magnitude;
        Vector3 normal;
        (target.Center - owner.Center).Extract(out magnitude, out normal);

        if (magnitude < radius)
        {
            if(attactCooldown.Check())
            {
                target.Hurt(new DamageStruct(damage, power, damageType, owner.Team, owner.Center, normal), owner.Center, -normal);
                attactCooldown.Run();
                return true;
            }
        }
        return false;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (owner != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(owner.Center, radius);
        }
    }
#endif
}
