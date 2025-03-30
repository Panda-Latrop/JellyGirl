using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JellyScale : MonoBehaviour
{
    [System.Serializable]
    public struct ScaleTarget
    {
        public Vector2 percent;
        public GameObject target;
        public float offset;
        public bool ignoreScale;
        
        public bool Check(float p)
        {
            return p<= percent.x && p> percent.y;
        }
        public void Show(Transform root,Transform trail)
        {
            target.gameObject.SetActive(true);
            var pos = root.localPosition;
            pos.y = offset;
            root.localPosition = pos;
            pos = trail.localPosition;
            pos.y = -offset;
            trail.localPosition = pos;

        }
        public void Hide()
        {
            target.gameObject.SetActive(false);
        }
        public float CorrectScale(float p)
        {
            if(ignoreScale)
                return p;
            float max = percent.x;
            if (max <= 0)
                max = 1;
            float c = p;
            return c/max;
        }
    }




    [SerializeField] private Character character;
    [SerializeField] private Transform root;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private ScaleTarget[] scales;
    private int currentScale = 0;
    private Vector3 defaultScale,defaultCollider;
    private float defaultWidth;
    private float percent;
    private void Awake()
    {
        defaultCollider = character.Collider.Size;
        defaultWidth = trail.widthMultiplier;
        defaultScale = root.localScale;



    }
    private void Start()
    {
        percent = character.HealthPercent;
        Scale(percent);
    }
    private void Update()
    {

        percent = Mathf.MoveTowards(percent, Mathf.Max(character.HealthPercent, 0.25f), 2f * Time.deltaTime);


        Scale(percent);

    }  
    private void Scale(float percent)
    {
        for (int i = 0; i < scales.Length; i++)
        {
            var scale = scales[i];
            if (i != currentScale && scale.Check(percent))
            {
                scales[currentScale].Hide();
                scale.Show(root, trail.transform);
                currentScale = i;
                break;
            }
        }
        var correct = scales[currentScale].CorrectScale(percent);

        root.localScale = defaultScale * correct;
        trail.widthMultiplier = percent;
        character.Collider.Size = defaultCollider * percent; ;
    }
}
