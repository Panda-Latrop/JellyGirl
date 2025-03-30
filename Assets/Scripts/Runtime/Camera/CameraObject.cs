using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private CameraFollow follow;
    private bool hasFollow;
    [SerializeField] private CameraShake shaker;
    private bool hasShaker;

    private Vector3 offset;
    public Camera Main => cam;
    public void SetTarget(Transform target)
    {
        if (hasFollow)
        {
            follow.Target = target;
        }
    }
    public void StartShake(float duration, float scale = 1f)
    {
        if (hasShaker)
        {
            shaker.Shake(duration,scale);
        }
    }
    private void Awake()
    {
        hasFollow = follow != null;
        hasShaker = shaker != null;
        offset = cam.transform.position;
        offset.x = offset.y = 0f;
        follow.Init(cam);

    }
    private void LateUpdate()
    {
        Vector3 position = offset;
        if (hasFollow)
            position += follow.Execute();
        if (hasShaker)
        {
            var h = shaker.Execute();
            position += h;
        }
 
        cam.transform.position = position;
    }
}
