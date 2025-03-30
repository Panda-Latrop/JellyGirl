using UnityEngine;
using UnityEngine.EventSystems;
using UI;
using UnityEngine.UI;

namespace UnityEditor
{
    public class SceneSetupMenuItem : MonoBehaviour
    {
        [MenuItem("GameObject/Scene Setup", false, 0)]
        private static void SceneSetup()
        {

            if (FindObjectOfType<GameInstance>() == null)
            {
                GameObject giGO = new GameObject("[GameInstance]");
                giGO.AddComponent<GameInstance>();
            }
            GameMode gm;
            GameObject gmGO;
            if ((gm = FindObjectOfType<GameMode>()) == null)
            {
                gmGO = new GameObject("[GameMode]");
                gm = gmGO.AddComponent<GameMode>();
            }
            else
            {
                gmGO = gm.gameObject;
                gmGO.name = "[GameMode]";
            }
            gmGO.tag = "GameController";


            SerializedObject sO = new SerializedObject(gm);
            SerializedProperty sPGS = sO.FindProperty("gameState");
            SerializedProperty sPUC = sO.FindProperty("uiController");

            Camera cam = Camera.main;
            GameObject camGO;
            if (cam == null)
            {
                camGO = new GameObject("Main Camera");
                camGO.tag = "MainCamera";
                camGO.AddComponent<Camera>();
                camGO.AddComponent<AudioListener>();
                camGO.transform.position = Vector3.forward * -10;
            }
            else
            {
                camGO = cam.gameObject;
                camGO.name = "Main Camera";
            }
            camGO.transform.parent = gmGO.transform;


            GameState gs;
            GameObject gsGO;
            if ((gs = FindObjectOfType<GameState>()) == null)
            {
                gsGO = new GameObject("GameState");
                gs = gsGO.AddComponent<GameState>();
            }
            else
            {
                gsGO = gs.gameObject;
                gsGO.name = "GameState";
            }
            gsGO.transform.parent = gmGO.transform;
            sPGS.objectReferenceValue = gs;

            GameObject uiGO;
            Canvas c;
            CanvasGroup cg;
            CanvasScaler cs;
            UIController uiC;

            if ((c = FindObjectOfType<Canvas>()) == null)
            {
                uiGO = new GameObject("[UI]");
                uiGO.AddComponent<EventSystem>();
                uiGO.AddComponent<StandaloneInputModule>();
                c = uiGO.AddComponent<Canvas>();
            }
            else
            {
                uiGO = c.gameObject;
            }
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            if ((cs = FindObjectOfType<CanvasScaler>()) == null)
                cs = uiGO.AddComponent<CanvasScaler>();
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(1600f, 900f);
            cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            cs.matchWidthOrHeight = 0.5f;

            if ((cg = FindObjectOfType<CanvasGroup>()) == null)
                cg = uiGO.AddComponent<CanvasGroup>();

            if ((uiC = FindObjectOfType<UIController>()) == null)
                uiC = uiGO.AddComponent<UIController>();

            SerializedObject uiCSO = new SerializedObject(uiC);
            SerializedProperty sPC = uiCSO.FindProperty("canvas");
            SerializedProperty sPCG = uiCSO.FindProperty("group");

            sPC.objectReferenceValue = c;
            sPCG.objectReferenceValue = cg;
            uiCSO.ApplyModifiedProperties();

            sPUC.objectReferenceValue = uiC;
            sO.ApplyModifiedProperties();
        }
    }
}
