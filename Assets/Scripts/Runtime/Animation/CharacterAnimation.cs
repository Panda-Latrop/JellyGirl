
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSwapClass
{
    public CharacterSwapStruct[] swaps = new CharacterSwapStruct[0];

    public void Show(string key)
    {
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].SetActive(string.Equals(swaps[i].key, key));
        }
    }
    public void Show(int index)
    {
        index = index % swaps.Length;
        for (int i = 0; i < swaps.Length; i++)
        {
            
            swaps[i].SetActive(i == index);
        }
    }
    public void Random()
    {

        var r = UnityEngine.Random.Range(0, swaps.Length);
        Debug.Log(r);
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].SetActive(r == i);
        }
    }
}

[System.Serializable]
public struct CharacterSwapStruct
{
    public string key;
    public GameObject[] targets;

    public void SetActive(bool active)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].SetActive(active);
        }
    }

}

public class CharacterAnimation : MonoBehaviour
{
    


    [SerializeField] protected Animator anim;
    [SerializeField] protected Character character;
    [SerializeField] private bool canFlip;
    [SerializeField] private CharacterSwapClass facilSwap = new CharacterSwapClass();
    private float direction;

    private readonly int
        moveHash = Animator.StringToHash("Move"),
        directionHash = Animator.StringToHash("Direction"),
        hurtHash = Animator.StringToHash("Hurt");

    protected virtual void Awake()
    {
        character.OnHurt += OnCharacterHurt;
    }
    protected virtual void OnDestroy()
    {
        character.OnHurt -= OnCharacterHurt;

    }
    protected virtual void OnCharacterHurt(HurtStruct obj)
    {
        if(obj.damage.type != DamageType.none)
        anim.SetTrigger(hurtHash);
    }

    public void ShowFacil(string key)
    {
        facilSwap.Show(key);
    }
    protected virtual void Process()
    {
        anim.SetBool(moveHash, character.Movement.Momentum.sqrMagnitude >= 0.1f);

        var dir = character.Movement.Direction;
        float d = 0;
        if (Mathf.Abs(dir.y) <= Mathf.Abs(dir.x))
            d = Mathf.Sign(dir.x);
        anim.SetFloat(directionHash, direction = Mathf.MoveTowards(direction, d, 4f * Time.deltaTime));

        if (canFlip)
        {
            var s = Mathf.Sign(character.Movement.Direction.x);
            var scale = anim.transform.localScale;
            if (s != 0f && s != scale.x)
            {
                scale.x = s;
                anim.transform.localScale = scale;
            }
        }
    }

    private void LateUpdate()
    {
        Process();
    }
}
