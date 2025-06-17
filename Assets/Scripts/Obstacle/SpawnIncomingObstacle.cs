using UnityEngine;

public class SpawnIncomingObstacle : MonoBehaviour
{
    public GameObject incomingObstaclePrefab;
    public float offsetY = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            GameObject incomingObstacle = Instantiate(incomingObstaclePrefab, this.transform.position + new Vector3(0, offsetY), Quaternion.identity);
            Destroy(incomingObstacle, 7f);
            Destroy(this.gameObject);
        }
    }
}
