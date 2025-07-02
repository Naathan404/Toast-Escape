using Unity.Mathematics;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    private float halfWidth;
    float rightPos;
    void Start()
    {
        halfWidth = GetComponent<Renderer>().bounds.extents.x;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float speed = LevelGenerator.instance.getMoveSpeed() * 2.5f;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        rightPos = transform.position.x + halfWidth;
        float leftPosCam = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 10f;
        if (rightPos < leftPosCam)
            gameObject.SetActive(false);
    }
}
