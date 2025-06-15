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

    // Xử lý nhảy
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //isJumping = true;
            StartCoroutine(StartJumpWithDelay());
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

    private IEnumerator StartJumpWithDelay()
    {
        for (int i = 0; i < catList.Count; i++)
        {
            catList[i].StartJump();
            yield return new WaitForSeconds(jumpDelay);
        }
    }

    // On dinh doi hinh
    public void FormatGroup()
    {
        if (catList.Count == 0) return;



        // Di chuyen bon meo den 1/3 ben trai man hinh
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
        cat.transform.SetParent(transform);

        // Cap nhat layer
        UpdateSortingOrder();
        // Tang diem 
        GameManager.instance.IncreaseScore();
    }

    private void UpdateSortingOrder()
    {
        catList[0].GetComponent<SpriteRenderer>().sortingOrder = 200;
        for (int i = 1; i < catList.Count; i++)
        {
            SpriteRenderer sr = catList[i].GetComponent<SpriteRenderer>();
            int temp = Random.Range(1, 3);
            if (temp < 2) sr.sortingOrder = 100 - i;
            if (temp >= 2) sr.sortingOrder = 100 + i;
        }
    }

    // Xóa mèo
    public void RemoveCat(Cat cat)
    {
        if (catList.Count == 0) return;
        if (catList.Contains(cat))
        {
            catList.Remove(cat);
            Destroy(cat.gameObject);
        }
        
        GameManager.instance.DecreaseScore();
        if (catList.Count <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

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

    // GetLeaderCat
    public GameObject getLeaderCat()
    {
        if (catList.Count <= 0) return null;
        return catList[0].gameObject;
    }
    
}
