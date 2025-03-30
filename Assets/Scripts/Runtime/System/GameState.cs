using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateEnum
{
    none,
    start,
    over,
    win,
}
public class GameState : MonoBehaviour
{
    
    private bool inPause;
    private GameStateEnum state = GameStateEnum.none;
    [SerializeField] private CameraObject cameraObject;
    public GameStateEnum State => state;
    public bool InPause => inPause;
    public CameraObject CameraObject => cameraObject;
    public System.Action OnGameStart;
    public System.Action OnGameWin;
    public System.Action OnGameOver;

    public virtual void Init() { }
    public virtual void StartGame()
    {

        state = GameStateEnum.start;
        OnGameStart?.Invoke();
    }
    public virtual void WinGame()
    {
        state = GameStateEnum.win;
        OnGameWin?.Invoke();
    }
    public virtual void OverGame()
    {
        state = GameStateEnum.over;
        OnGameOver?.Invoke();
    }
    public virtual void Pause()
    {
        Time.timeScale = 0.0f;
        inPause = true;
    }
    public virtual void Resume()
    {
        Time.timeScale = 1.0f;
        inPause = false;
    }
}
