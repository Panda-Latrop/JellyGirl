using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private DynamicEffect effectPrefab;
    [SerializeField] private Timer delay = new Timer(2f);
    [SerializeField] private bool push = true;
    private bool inPush;
    private void Awake()
    {
        character.OnDeath += OnCharacterDeath;
    }
    private void OnDestroy()
    {
        character.OnDeath -= OnCharacterDeath;
    }
    private void OnCharacterDeath(HurtStruct obj)
    {

            inPush = true;
            delay.Run();
        
        
    }
    private void LateUpdate()
    {
        if (inPush && delay.Check())
        {
            character.Animation.ShowFacil("Def");
            GameInstance.Instance.Pool.Pop(effectPrefab,character.Center,Quaternion.identity);
            if(push)
            GameInstance.Instance.Pool.Push(character);
            else
                character.Animation.gameObject.SetActive(false);
            inPush = false;
        }
    }
}
