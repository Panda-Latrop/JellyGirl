using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    private Transform target;
    private bool hasTarget;
    [SerializeField] private float speed = 10f;
    private Vector3 position;
    public Transform Target { get => target; set { target = value; hasTarget = value != null; if (hasTarget) Teleport();   } }
    public void Init(Camera cam)
    {
        this.cam = cam;
        position = cam.transform.position;
        position.z = 0f;

    }
    public Vector3 Execute()
    {
        if (hasTarget)
        {
            Vector3 position = this.position;
            Vector3 target = this.target.position;
            position.z = target.z = 0f;
            position = Vector3.Lerp(position, target, speed * Time.deltaTime);
            //float scale = Mathf.Max((position - target).sqrMagnitude, 1f);

            // position = Vector3.MoveTowards(position, target, (speed * Time.deltaTime) * scale);
            return this.position = position;
        }
        return Vector3.zero;
    }
    public void Teleport()
    {
        Vector3 target = this.target.position;
        cam.transform.position = this.position = target;
    }
}
