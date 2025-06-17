using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class BasePowerUp : MonoBehaviour
{
    [SerializeField] protected float duration;
    //[SerializeField] protected bool isActive = false;
    protected abstract bool isActive { get; }
    public virtual void Activate()
    {
        if (isActive)
        {
            CancelInvoke(nameof(Deactivate));
            Invoke(nameof(Deactivate), duration);
            return;
        }
        OnActivate();
        Invoke(nameof(Deactivate), duration);
    }
    public virtual void Deactivate()
    {
        if (!isActive) return;
        CancelInvoke(nameof(Deactivate));
        OnDeactivate();
    }

    public virtual void InstanceActivate()
    {
        OnActivate();
    }

    public virtual void InstanceDeactivate()
    {
        OnDeactivate();
    }

    protected abstract void OnActivate();
    protected abstract void OnDeactivate();
}
