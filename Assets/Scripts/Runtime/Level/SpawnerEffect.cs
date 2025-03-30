using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEffect : MonoBehaviour
{
    [SerializeField] private SpawnerBot spawner;
    [SerializeField] private DynamicEffect effectPrefab;
    private void Awake()
    {
        spawner.OnSpawn += OnSpawnerSpawn;
    }
    private void OnDestroy()
    {
        spawner.OnSpawn -= OnSpawnerSpawn;
    }
    private void OnSpawnerSpawn()
    {
        var effect = GameInstance.Instance.Pool.Pop(effectPrefab, spawner.transform.position, Quaternion.identity);

    }
}
