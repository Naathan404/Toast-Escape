using UnityEngine;

public class NewMonoBehaviourScript : BaseObstacle
{
    public GameObject coinRewardsPattern;

    protected override void Start()
    {
        base.Start();
        coinRewardsPattern.SetActive(false);
    }

    protected override void ExecuteWhenDetroyed()
    {       
        // base.ExecuteWhenDetroyed();
        Debug.Log("Ruong kho bau da bi pha huy");
        AudioManager.instance.PlaySFX(AudioManager.instance.powerUp);

        coinRewardsPattern.SetActive(true);    
    }
}
