using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PrologueTimeline : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private float end = 8f;
    private bool inEnd;
    private void Awake()
    {
        director.Play();
    }
    private void LateUpdate()
    {
        if(director.time >= end && !inEnd)
        {
            inEnd = true;
            GameInstance.Instance.UI.StartTransaction(NextLevel);
            enabled = false;
        }

    }
    private void NextLevel()
    {
        GameInstance.Instance.ChangeScene("Level0");
    }
}
