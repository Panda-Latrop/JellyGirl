using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBot : MonoBehaviour
{
    [SerializeField] private Character prefab;
    [SerializeField] private Timer delay = new Timer(3f);
    private Character target;
    private bool hasSpawn;
    public event System.Action OnSpawn;
    private void Spawn()
    {
        target = GameInstance.Instance.Pool.Pop(prefab, transform.position, Quaternion.identity) as Character;
        hasSpawn = true;
        OnSpawn?.Invoke();
    }

    private void LateUpdate()
    {

        if(hasSpawn && !target.IsAlive)
        {
            hasSpawn = false;
            delay.Run();
        }

        if (!hasSpawn && delay.Check())
        {
            Spawn();

        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        bool has = prefab != null;
        Gizmos.color = has ? Color.magenta : Color.red;
        if (has)
            Gizmos.DrawCube(transform.position, Vector3.one);
        else
            Gizmos.DrawWireCube(transform.position, Vector3.one);

    }
#endif
}
