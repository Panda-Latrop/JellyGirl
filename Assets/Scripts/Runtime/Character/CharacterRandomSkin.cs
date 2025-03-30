using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomSkin : MonoBehaviour
{
    [SerializeField] private CharacterSwapClass[] swaps;
    private void Start()
    {
        Execute();
    }
    [ContextMenu("Random")]
    private void Execute()
    {
        int r = Random.Range(0, 16);
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].Show(r);
        }
    }
}
