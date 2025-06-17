using UnityEngine;

public class DoubleCoin : BasePowerUp
{
    private bool isActiveDoubleCoin = false;
    protected override bool isActive => isActiveDoubleCoin;

    protected override void OnActivate()
    {
        Debug.Log("Activate double coin");
        isActiveDoubleCoin = true;
        GameManager.instance.DoubleCoinReward(true);
    }

    protected override void OnDeactivate()
    {
        Debug.Log("Deactivate double coin");
        isActiveDoubleCoin = false;
        GameManager.instance.DoubleCoinReward(false);
        EffectManager.instance.CallMagnetDeactivateEffect(this.transform.position);
        AudioManager.instance.PlaySFX(AudioManager.instance.powerTimeOut);
    }
}
