using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDash : UIElement
{
    private CharacterDash dash;
    [SerializeField] private Image indicator, icon;
    [SerializeField] private AnimationAction anim;
    public void Init()
    {
        dash = (GameInstance.Instance.GameState as RaceGameState).Player.Dash;
        dash.OnDash += OnDash;
        dash.OnCooldown += OnDashCooldown;
    }

    private void OnDestroy()
    {
        dash.OnDash -= OnDash;
        dash.OnCooldown -= OnDashCooldown;
    }
    private void OnDash()
    {
        indicator.fillAmount = 0;
        icon.color = Color.white*0.75f;
    }
    private void OnDashCooldown()
    {
        anim.Play();
        indicator.fillAmount = 1;
        icon.color = Color.white;

    }
    private void LateUpdate()
    {
        if (!dash.CanDash)
        {
            indicator.fillAmount = dash.Precent;
        }
    }
}
