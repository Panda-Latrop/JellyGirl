using UnityEngine;
using UnityEngine.SceneManagement;
using Management.Singleton;
using Management.Pool;
using UI;
using UnityEngine.MusicSystem;

public class GameInstance : Singleton<GameInstance>
{
    public enum LoadStateEnum
    {
        none,
        load,
        restart,
    }

    private GameMode gameMode;
    private MusicSystem musicSystem;
    private PoolManager poolManager;
    //private SaveSystem saveSystem;
    private LoadStateEnum loadState;
    private Scene currentScene;

    protected override void OnAwake()
    {
        base.OnAwake();
        if (isOrigin)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            musicSystem = SetupMusicSystem();
            //saveSystem = SetupSaveSystem();
        }
        Instance.SetupSceneObjects(SceneManager.GetActiveScene());
        
    }
    private void OnDestroy()
    {
        if (isOrigin)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
    public LoadStateEnum LoadState => loadState;
    public GameMode GameMode => gameMode;
    public GameState GameState => gameMode.GameState;
    public UIController UI => gameMode.UIController;
    public MusicSystem Music => musicSystem;
    public PoolManager Pool => poolManager;


    
    public bool GetGameMode<T>(out T gameMode) where T : GameMode
    {
        gameMode = null;
        if (this.gameMode is T)
        {
            gameMode = this.gameMode as T;
            return true;
        }
        else
            return false;
    }
    public bool GetGameState<T>(out T gameState) where T : GameState
    {
        gameState = null;
        if (gameMode.GameState is T)
        {
            gameState = gameMode.GameState as T;
            return true;
        }
        else
            return false;
    }
    //public JSONObject GetSystemData()
    //{
    //    return saveSystem.GetSystemData();
    //}
    //public void SetSystemData(JSONObject jObject)
    //{
    //    saveSystem.SetSystemData(jObject);
    //}
    //public void SaveScene(string file)
    //{
    //    saveSystem.InitSave(currentScene,file);
    //}
    //public void LoadScene(string file)
    //{
    //    string scene = saveSystem.InitLoad(file);
    //    if (!string.IsNullOrEmpty(scene))
    //        SceneManager.LoadScene(scene);
    //}
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        //saveSystem.PrepareTransition(currentScene);
        loadState = LoadStateEnum.restart;
        SceneManager.LoadScene(sceneName);
    }
    [ContextMenu("Restart")]
    public void RestartScene()
    {
        Time.timeScale = 1f;

        //saveSystem.InitRestart();
        loadState = LoadStateEnum.restart;
        SceneManager.LoadScene(currentScene.name);        
    }
    //public bool GetLoadedSavedObject(string name, out ISaveableObject reference)
    //{
    //    reference = null;
    //    return false;
    //    //return saveSystem.GetLoadedSavedObject(name, out reference);
    //}
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //currentScene = scene;
        //saveSystem.OnSceneLoaded(scene, mode);
    }
    private void OnSceneUnloaded(Scene scene)
    {
        
    }
    private void SetupSceneObjects(Scene scene)
    {
        currentScene = scene;
        if (Utility.FindComponentInRootObjectsByTag(currentScene, "GameController", out gameMode))
        {
            gameMode.GameState.Init();
            poolManager = SetupPoolManager();
        }
        gameMode.UIController.Init();
    }
    private PoolManager SetupPoolManager()
    {
        GameObject pGO = new GameObject("Pool");
        pGO.transform.parent = gameMode.transform;
        return new PoolManager(pGO.transform);
    }
    private MusicSystem SetupMusicSystem()
    {
        GameObject msGO = new GameObject("MusicSystem");
        msGO.transform.parent = transform;
        MusicSystem musicSystem = msGO.AddComponent<MusicSystem>();
        musicSystem.Initialization();
        return musicSystem;
    }
    //private SaveSystem SetupSaveSystem()
    //{
    //    return new SaveSystem();
    //}
}
