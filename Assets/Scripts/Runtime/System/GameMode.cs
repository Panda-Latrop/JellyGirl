using UI;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private UIController uiController;
    public GameState GameState => gameState;
    public UIController UIController => uiController;
}
