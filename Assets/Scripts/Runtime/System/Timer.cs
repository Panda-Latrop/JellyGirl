using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    public enum TimerType
    {
        scaled,
        unscaled,
        fixedScaled,
        fixedUnscaled
    }

    [SerializeField] protected float time;
    [SerializeField] protected TimerType type;
    protected float nextTime;

    public float Duration => time;
    public float Elapsed => 1f - (nextTime - Time);
    public Timer()
    {
        time = 0f;
        type = TimerType.scaled;
    }
    public Timer(float time)
    {
        this.time = time;
        type = TimerType.scaled;
    }
    public Timer(float time, TimerType type)
    {
        this.time = time;
        this.type = type;
    }
    public void Set(float time)
    {
        this.time = time;
    }
    public void Set(TimerType type)
    {
        this.type = type;
    }
    public virtual bool Check()
    {
        return Time >= nextTime;
    }
    public virtual void Run()
    {
        nextTime = Time + time;
    }
    public virtual void Run(float randomOffsetPercent)
    {
        nextTime = Time + time * Random.Range(1.0f - randomOffsetPercent, 1.0f + randomOffsetPercent);
    }
    private float Time { get
        {
            {
                switch (type)
                {
                    case TimerType.scaled:
                        return UnityEngine.Time.time;
                    case TimerType.unscaled:
                        return UnityEngine.Time.unscaledTime;                       
                    case TimerType.fixedScaled:
                        return UnityEngine.Time.fixedTime;
                    case TimerType.fixedUnscaled:
                        return UnityEngine.Time.fixedUnscaledTime;
                    default:
                        return 0f;
                }
            }
        }
    }
    public void Reset()
    {
        nextTime = 0f;
    }
}
