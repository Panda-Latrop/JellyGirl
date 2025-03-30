using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool inShake;
    private float scale;
    private Timer duration = new Timer(1f);
    [SerializeField] private float power = 1;
    public void Shake(float duration ,float scale = 1f)
    {
        this.scale = scale* power;
        inShake = true;
        this.duration.Set(duration);
        this.duration.Run();
    }
    public Vector3 Execute()
    {

        if (inShake && !duration.Check())
        {
            Vector3 position = new Vector3(Noise(), Noise(), 0f) * scale;
            return position;
        }
        else
        {
            inShake = false;
            return Vector3.zero;
        }
    }
    private float Noise()
    {
        return (0.5f - Mathf.PerlinNoise(Random.value, Random.value)) *2f;
    }
}
