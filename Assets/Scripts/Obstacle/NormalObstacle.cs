using UnityEngine;

public class NormalObstacle : BaseObstacle
{
    protected override void ExecuteWhenDetroyed()
    {
        base.ExecuteWhenDetroyed();
        for (int i = 0; i < bonusCat; i++)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.hitScoreSFX);
            ToastGroupManager.instance.AddNewCat();
        }
    }
}
