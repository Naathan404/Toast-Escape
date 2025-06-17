using UnityEngine;

public class BatMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = LevelGenerator.instance.getMoveSpeed() * 2;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
        if (rb.transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 5f)
        {
            //Destroy(gameObject);
        }
    }
}
