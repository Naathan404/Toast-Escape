using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatGroupManager : MonoBehaviour
{

    [Header("Cat Group Settings")]
    public List<Cat> catList = new List<Cat>();
    public GameObject catPrefab;
    //[SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float formatSpeed = 30f;
    [SerializeField] private float jumpDelay = 0.1f;
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
        HandleJump();
        FormatGroup();
        CheckCatsOffscreen();
    }

    // Xu ly nhay voi delay
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //isJumping = true;
            if (catList.Count > 0)
                catList[0].StartJump();
            for (int i = 1; i < catList.Count; i++)
            {
                float delay = (catList[0].transform.position.x - catList[i].transform.position.x) / 15f;
                delay = delay < 0 ? delay * -1 : delay;
                StartCoroutine(JumpCatWithDelay(catList[i], delay));
            }
        }

        // if (Input.GetKey(KeyCode.Mouse0))
        // {
        //     foreach (var cat in catList)
        //     {
        //         cat.HoldJump(); 
        //     }
        // }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //isJumping = false;
            foreach (var cat in catList)
            {
                cat.ExitJump();
            }
        }
    }

    /// Cach nhay cu: dieu khien ca nhom cung nhay voi 1 khoang jumpdelay chung, meo o index cang lon thi cang delay 
    // private IEnumerator StartJumpWithDelay()
    // {
    //     for (int i = 0; i < catList.Count; i++)
    //     {
    //         catList[i].StartJump();
    //         yield return new WaitForSeconds(jumpDelay);
    //     }
    // }

    /// Dieu khien tung nhan vat nhay rieng biet voi delay dua vao khoang cach cua no toi leader
    private IEnumerator JumpCatWithDelay(Cat cat, float delay)
    {
        yield return new WaitForSeconds(delay);
        cat.StartJump();
        Debug.Log("Meo nhay!");
    }

    // On dinh doi hinh
    public void FormatGroup()
    {
        if (catList.Count == 0) return;
        // Cập nhật sorting order của index 0 để nó luôn nằm phía trên các index khác
        UpdateLeaderSortingOrder();

        // Di chuyển bọn mèo di chuyển vị trí 1/3 phía trái màn hình
        float targetX = Camera.main.ViewportToWorldPoint(new Vector3(0.3f, 0, 0)).x;

        if (!isBlocked)
        {
            if (catList[0].IsGrounded())
            {
                Rigidbody2D leaderRb = catList[0].GetComponent<Rigidbody2D>();
                Vector2 leaderTargetPos = new Vector2(targetX, catList[0].transform.position.y);
                Vector2 newLeaderPos = Vector2.MoveTowards(leaderRb.position, leaderTargetPos, Time.deltaTime * formatSpeed);
                leaderRb.MovePosition(newLeaderPos);
            }

            float normalizedGap = Mathf.Lerp(minGap, maxGap, Mathf.Clamp01((catList.Count - 1) / 10f));
            float leftmostX = targetX - normalizedGap * (catList.Count - 1);
            float screenLeftX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + 2f;
            if (leftmostX < screenLeftX)
            {
                float maxLength = targetX - screenLeftX;
                normalizedGap = maxLength / Mathf.Max(1, catList.Count - 1);
            }

            // Gian cach doi hinh va di chuyen cac con meo den vi tri cat leader
            for (int i = 1; i < catList.Count; i++)
            {
                if (!catList[i].IsGrounded()) continue;

                Vector2 targetPos = new Vector2(catList[0].transform.position.x - i * normalizedGap, catList[i].transform.position.y);
                Rigidbody2D rb = catList[i].GetComponent<Rigidbody2D>();
                Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, Time.deltaTime * formatSpeed);
                rb.MovePosition(newPos);
            }
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

    // Gan isGrounded của tất cả mèo 
    public void SetIsGrounded(bool isTrue)
    {
        for (int i = 0; i < catList.Count; i++)
        {
            if (catList[i].IsGrounded() == true) continue;
            catList[i].setIsGrounded(isTrue);
            catList[i].setIsGroundFalseWhenCatFalling();
        }
    }

    // Thêm mèo
    public void AddNewCat()
    {
        Vector3 spawnPosition = Vector3.zero;
        if (catList.Count > 0)
        {
            spawnPosition = catList[0].transform.position;
        }
        else
        {
            spawnPosition = transform.position;
        }
        GameObject newCat = Instantiate(catPrefab, spawnPosition, Quaternion.identity);
        if (newCat.GetComponent<CatCollisionHandler>() == null)
            newCat.AddComponent<CatCollisionHandler>();
        Cat cat = newCat.GetComponent<Cat>();
        catList.Add(cat);
        // Set parent là GameObject đang giữ script này
        cat.transform.SetParent(transform);
        // Set sorting order cho mèo mới được thêm vào
        SpriteRenderer sr = cat.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 50 + Random.Range(-50, 99);
        float colorScale = Mathf.Clamp(sr.sortingOrder / 89f, 0.6f, 1f);
        sr.color = new Color(colorScale, colorScale, colorScale, 1f);
        cat.transform.position = new Vector3(cat.transform.position.x, cat.transform.position.y, Random.Range(-1f, 0f));
        // Tang diem 
        GameManager.instance.IncreaseScore();
    }

    private void UpdateLeaderSortingOrder()
    {
        catList[0].GetComponent<SpriteRenderer>().sortingOrder = 150;
        catList[0].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        catList[0].transform.position = new Vector3(catList[0].transform.position.x, catList[0].transform.position.y, 1f);
    }

    // Xóa mèo
    public void RemoveCat(Cat cat)
    {
        if (catList.Count == 0) return;
        bool isLeader = false;
        if (catList.Contains(cat))
        {
            if (cat == catList[0]) isLeader = true;
            catList.Remove(cat);
            Destroy(cat.gameObject);
            if (isLeader)
                UpdateLeaderSortingOrder();
        }

        GameManager.instance.DecreaseScore();
        if (catList.Count <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    // Kiểm tra nếu con mèo ra khỏi màn hình thì xóa nó
    private void CheckCatsOffscreen()
    {
        if (catList.Count == 0) return;

        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));

        float leftBound = bottomLeft.x;
        float bottomBound = bottomLeft.y;

        for (int i = catList.Count - 1; i >= 0; i--)
        {
            if (catList[i] == null) continue;

            Vector3 catPos = catList[i].transform.position;

            // Nếu ra khỏi màn hình bên trái hoặc rớt xuống dưới
            if (catPos.x < leftBound - 2f || catPos.y < bottomBound - 5f)
            {
                Cat catToRemove = catList[i];
                catList.RemoveAt(i);
                GameManager.instance.DecreaseScore();
                Destroy(catToRemove.gameObject);
            }
        }
        if (catList.Count <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    // Get con mèo ở index 0
    public GameObject getLeaderCat()
    {
        if (catList.Count <= 0) return null;
        return catList[0].gameObject;
    }
    
}
