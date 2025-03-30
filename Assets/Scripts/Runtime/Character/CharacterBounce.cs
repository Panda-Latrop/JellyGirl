using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterBounce : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private float bounce = 0.5f;
    private void Awake()
    {
        character.Collider.OnCollisionEnter += HandleBounce;
    }
    private void OnDestroy()
    {
        character.Collider.OnCollisionEnter -= HandleBounce;

    }
    private void HandleBounce(Collision2D collision)
    {
        var momentum = Utility.ExtractDotVector(character.Movement.Momentum, -collision.contacts[0].normal);
        var keep = character.Movement.Momentum - momentum;

        character.Movement.Momentum = -momentum * bounce + keep;
    }
}
