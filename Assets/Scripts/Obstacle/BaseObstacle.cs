using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField] protected int requiredCatCount;
    [SerializeField] private int currentCat = 0;
    [SerializeField] protected int bonusCat;

    [SerializeField] private TextMeshProUGUI counterText;
    protected bool isDestroyed = false;

    protected virtual void Start()
    {
        UpdateCounterText();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (isDestroyed) return;

        // Kiem tra va cham voi meo
        if (other.gameObject.CompareTag("Cat"))
        {
            ContactPoint2D contact = other.contacts[0];
            Vector2 contactDirection = contact.point - (Vector2)transform.position;

            // Goi ham BlockGroup de nhom meo bi don lai
            CatGroupManager.instance.BlockGroup();

            if (contactDirection.x < -0.1f)
            {
                currentCat++;
                UpdateCounterText();
                if (requiredCatCount <= currentCat)
                {
                    isDestroyed = true;
                    DestroyObstacle();
                }
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            CatGroupManager.instance.UnblockGroup();
            currentCat = Mathf.Max(currentCat - 1, 0);
            UpdateCounterText();
        }
    }

    protected virtual void DestroyObstacle()
    {
        EffectManager.instance.CallDestroyObstacleEffect(this.transform.position);
        Destroy(this.gameObject);

        // Dat isGrounded = true cho toan bo
        CatGroupManager.instance.SetIsGrounded(true);

        // Goi ham Unblock de nhom meo on dinh doi hinh
        CatGroupManager.instance.UnblockGroup();

        // Goi ham them meo moi
        ExecuteWhenDetroyed(); 
    }

    protected virtual void ExecuteWhenDetroyed()
    {
        Debug.Log("Vat can bi pha huy.");
        AudioManager.instance.PlaySFX(AudioManager.instance.destroyObstacle);
    }

    // Xu li so meo dang va cham / so meo can de pha huy 
    private void UpdateCounterText()
    {
        if (counterText != null)
            counterText.text = currentCat + "/" + requiredCatCount;
    }
}