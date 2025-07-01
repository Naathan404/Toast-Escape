using UnityEngine;

public class SpawnIncomingObstacle : MonoBehaviour
{
    public GameObject incomingObstaclePrefab;
    private float rightPosCam;
    public float offsetY = 0f;

    private void Start()
    {
        rightPosCam = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect - 10f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            GameObject incomingObstacle = Instantiate(incomingObstaclePrefab, new Vector3(rightPosCam, transform.position.y + offsetY), Quaternion.identity);
            //Destroy(incomingObstacle);
            Destroy(this.gameObject);
        }
    }
}
