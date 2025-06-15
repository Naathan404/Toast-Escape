using UnityEngine;

public class PlaformMovement : MonoBehaviour
{
    [SerializeField] private float halfWidth;
    [SerializeField] private float rightPos;
    [SerializeField] private float leftScreenPos;


    void Start()
    {
        // De day vi chua biet lam gi
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {   
        // Lay toc do di chuyen theo thoi gian chay
        float moveSpeed = LevelGenerator.instance.getMoveSpeed();
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Khi chunk ra khoi meo trai cua camera thi se destroy vat the
        halfWidth = GetComponent<Renderer>().bounds.extents.x;
        rightPos = transform.position.x + halfWidth;
        leftScreenPos = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 120;
        if (rightPos < leftScreenPos)
        {
            Destroy(gameObject);
        }
    }
}
