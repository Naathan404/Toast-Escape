using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatGroupManager : MonoBehaviour
{

    [Header("Toast Group Settings")]
    public List<Toast> toastList = new List<Toast>();
    public List<GameObject> toastPrefab = new List<GameObject>();
    [SerializeField] private float formatSpeed = 30f;
    [SerializeField] private float delayFactor;
    // private bool isJumping = false;
    [Header("Format Group")]
    [SerializeField] private float posX = -3.5f;
    [SerializeField] private float posY = -2.5f;
    [SerializeField] private bool isBlocked = false;
    [SerializeField] private float minGap;
    [SerializeField] private float maxGap;

    Vector2 targetPos;

    // Singleton Pattern
    public static CatGroupManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
        targetPos = new Vector2(posX, posY);
    }

    private void Update()
    {
        if (toastList.Count <= 0)
        {
            GameManager.instance.GameOver();
        }
        HandleJump();
        FormatGroup();
        CheckToastOffscreen();
    }

    // Xu ly nhay voi delay
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (int i = 0; i < toastList.Count; i++)
            {
                float delay = (toastList[0].transform.position.x - toastList[i].transform.position.x) * delayFactor;
                delay = delay < 0 ? delay * -1 : delay;
                StartCoroutine(JumpWithDelay(toastList[i], delay));
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            for (int i = 0; i < toastList.Count; i++)
            {
                float delay = (toastList[0].transform.position.x - toastList[i].transform.position.x) * delayFactor;
                delay = delay < 0 ? delay * -1 : delay;
                StartCoroutine(SetGravityWithDelay(toastList[i], delay));
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            for (int i = 0; i < toastList.Count; i++)
            {
                float delay = (toastList[0].transform.position.x - toastList[i].transform.position.x) * delayFactor;
                delay = delay < 0 ? delay * -1 : delay;
                StartCoroutine(ExitJumpWithDelay(toastList[i], delay));
            }
        }
    }

    // On dinh doi hinh
    public void FormatGroup()
    {
        if (toastList.Count == 0) return;
        // Cập nhật sorting order của index 0 để nó luôn nằm phía trên các index khác
        UpdateLeaderSortingOrder();

        // Di chuyển bọn mèo di chuyển vị trí 1/3 phía trái màn hình
        float targetX = Camera.main.ViewportToWorldPoint(new Vector3(0.3f, 0, 0)).x;

        if (!isBlocked)
        {
            if (toastList[0].IsGrounded())
            {
                Rigidbody2D leaderRb = toastList[0].GetComponent<Rigidbody2D>();
                Vector2 leaderTargetPos = new Vector2(targetX, toastList[0].transform.position.y);
                Vector2 newLeaderPos = Vector2.MoveTowards(leaderRb.position, leaderTargetPos, Time.deltaTime * formatSpeed);
                leaderRb.MovePosition(newLeaderPos);
            }

            float normalizedGap = Mathf.Lerp(minGap, maxGap, Mathf.Clamp01((toastList.Count - 1) / 10f));
            float leftmostX = targetX - normalizedGap * (toastList.Count - 1);
            float screenLeftX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + 2f;
            if (leftmostX < screenLeftX)
            {
                float maxLength = targetX - screenLeftX;
                normalizedGap = maxLength / Mathf.Max(1, toastList.Count - 1);
            }

            // Gian cach doi hinh va di chuyen cac con meo den vi tri cat leader
            for (int i = 1; i < toastList.Count; i++)
            {
                if (!toastList[i].IsGrounded()) continue;

                Vector2 targetPos = new Vector2(toastList[0].transform.position.x - i * normalizedGap, toastList[i].transform.position.y);
                Rigidbody2D rb = toastList[i].GetComponent<Rigidbody2D>();
                Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, Time.deltaTime * formatSpeed);
                rb.MovePosition(newPos);
            }
        }
    }

    // Thêm mèo
    public void AddNewCat()
    {
        Vector3 spawnPosition = Vector3.zero;
        if (toastList.Count > 0)
        {
            spawnPosition = toastList[0].transform.position;
        }
        else
        {
            spawnPosition = transform.position;
        }
        GameObject newToast = ToastPool.instance.GetToast();
        newToast.transform.position = spawnPosition;
        newToast.SetActive(true);
        Toast toast = newToast.GetComponent<Toast>();
        toastList.Add(toast);

        // Set sorting order cho mèo mới được thêm vào
        SpriteRenderer sr = toast.GetComponent<SpriteRenderer>();
        if (toastList.Count % 3 != 0 || toastList.Count == 2)
        {
            sr.sortingOrder = Random.Range(0, 99);
            float colorScale = Mathf.Clamp(sr.sortingOrder / 90f, 0.6f, 1f);
            sr.color = new Color(colorScale, colorScale, colorScale, 1f);
        }
        else
        {
            sr.sortingOrder = Random.Range(100, 149);
        }
        toast.transform.position = new Vector3(toast.transform.position.x, toast.transform.position.y, Random.Range(-1f, 0f));

        // Tang diem 
        GameManager.instance.IncreaseScore();
    }

    // Xóa mèo
    public void RemoveCat(Toast toast)
    {
        if (toastList.Count == 0) return;
        bool isLeader = false;
        if (toastList.Contains(toast))
        {
            if (toast == toastList[0]) isLeader = true;
            toastList.Remove(toast);
            // Tra ve pool
            toast.gameObject.SetActive(false);
            if (isLeader)
                UpdateLeaderSortingOrder();
        }

        GameManager.instance.DecreaseScore();
    }

    // Kiểm tra nếu con mèo ra khỏi màn hình thì xóa nó
    private void CheckToastOffscreen()
    {
        if (toastList.Count == 0) return;

        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));

        float leftBound = bottomLeft.x;
        float bottomBound = bottomLeft.y;

        for (int i = toastList.Count - 1; i >= 0; i--)
        {
            if (toastList[i] == null) continue;

            Vector3 toastPos = toastList[i].transform.position;

            // Nếu ra khỏi màn hình bên trái hoặc rớt xuống dưới
            if (toastPos.x < leftBound - 2f || toastPos.y < bottomBound - 5f)
            {
                // Tra ve pool
                toastList[i].gameObject.SetActive(false);
                // Xoa khoi list
                toastList.RemoveAt(i);
                GameManager.instance.DecreaseScore();
            }
        }
    }

    /// Dieu khien tung nhan vat nhay rieng biet voi delay dua vao khoang cach cua no toi leader
    private IEnumerator JumpWithDelay(Toast toast, float delay)
    {
        yield return new WaitForSeconds(delay);
        toast.StartJump();
        //Debug.Log("Meo nhay!");
    }

    private IEnumerator ExitJumpWithDelay(Toast toast, float delay)
    {
        yield return new WaitForSeconds(delay);
        toast.ExitJump();
        toast.SetBaseGravity();
        //ebug.Log("Meo roi!");
    }

    private IEnumerator SetGravityWithDelay(Toast toast, float delay)
    {
        yield return new WaitForSeconds(delay);
        toast.SetGlideGravity();
    }

    private void UpdateLeaderSortingOrder()
    {
        if (toastList.Count > 0)
        {
            toastList[0].GetComponent<SpriteRenderer>().sortingOrder = 150;
            toastList[0].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            toastList[0].transform.position = new Vector3(toastList[0].transform.position.x, toastList[0].transform.position.y, 1f);
        }
    }

    // Gan isGrounded của tất cả mèo 
    public void SetIsGrounded(bool isTrue)
    {
        for (int i = 0; i < toastList.Count; i++)
        {
            if (toastList[i].IsGrounded() == true) continue;
            toastList[i].setIsGrounded(isTrue);
            toastList[i].setIsHoldingJump(isTrue);
            toastList[i].setIsGroundFalseWhenFalling();
        }
    }

    public void BlockGroup()
    {
        isBlocked = true;
    }
    public void UnblockGroup()
    {
        isBlocked = false;
    }

    // Get con mèo ở index 0
    public GameObject getLeader()
    {
        if (toastList.Count <= 0) return null;
        return toastList[0].gameObject;
    }

    public int GetTransformVariety() => toastPrefab.Count - 1;
    
}