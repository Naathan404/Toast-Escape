using UnityEngine;
using UnityEngine.EventSystems;

public class Cat : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float baseJumpForce;
    [SerializeField] private float extraJumpForce;
    [SerializeField] private float maxHoldTime;
    //[SerializeField] private float jumpTimer = 0f;
    [SerializeField] private bool isGrounded = false;
    //[SerializeField] private bool isHoldingJump = false;
    [SerializeField] private GameObject checkGround;

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ApplySkinSelected();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    public void StartJump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            //isHoldingJump = true;
            //jumpTimer = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, baseJumpForce);
        }
    }

    // public void HoldJump()
    // {
    //     if (isHoldingJump && jumpTimer < maxHoldTime)
    //     {
    //         rb.AddForce(Vector2.up * extraJumpForce * Time.deltaTime, ForceMode2D.Force);
    //         jumpTimer += Time.deltaTime;
    //     }
    // }

    public void ExitJump()
    {
        //isHoldingJump = false;
        //jumpTimer = 0f;
    }

    public void MoveForward(float speed)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public bool IsGrounded() { return isGrounded; }

    public void setIsGrounded(bool isTrue) { isGrounded = isTrue; }

    public void setIsGroundFalseWhenCatFalling()
    {
        if (isGrounded && rb.linearVelocity.y < -0.5f)
        {
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Kiểm tra mặt tiếp xúc có hướng lên (đất bên dưới mèo)
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    break; // Chỉ cần 1 mặt hợp lệ là đủ
                }
            }
        }
        // if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        // {
        //     isGrounded = true;
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
        // if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        // {
        //     isGrounded = true;
        // }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = false;
        }
    }

    // Animation
    private void UpdateAnimation()
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    // Cap nhat skin da duoc nguoi dung chon animator
    private void ApplySkinSelected()
    {
        int skinID = PlayerPrefs.GetInt("SelectedSkinID", 0);
        RuntimeAnimatorController runtimeAnimatorController = SkinManager.instance.GetSkinByID(skinID);

        if (!runtimeAnimatorController) Debug.Log("Khong tim thay skin nao ca");
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }

}

internal class CheckGround
{
}