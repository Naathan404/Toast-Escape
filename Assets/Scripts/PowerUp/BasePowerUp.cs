using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class BasePowerUp : MonoBehaviour
{
    [SerializeField] protected float duration;
    [SerializeField] protected bool isActive = false;
    public virtual void Activate()
    {
        if (isActive)
        {
            CancelInvoke(nameof(Deactivate));
            Invoke(nameof(Deactivate), duration);
            return;
        }
        isActive = true;
        OnActivate();
        Invoke(nameof(Deactivate), duration);
    }
    public virtual void Deactivate()
    {
        if (!isActive) return;
        isActive = false;
        CancelInvoke(nameof(Deactivate));
        OnDeactivate();
    }

    protected abstract void OnActivate();
    protected abstract void OnDeactivate();
}
