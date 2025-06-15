using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private GameObject[] backgrounds; // Kéo 3 nền vào đây
    private float backgroundWidth;

    private void Start()
    {
        if (backgrounds.Length == 0) return;

        SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
        backgroundWidth = sr.bounds.size.x;
    }

    private void Update()
    {
        foreach (GameObject bg in backgrounds)
        {
            bg.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
        foreach (GameObject bg in backgrounds)
        {
            if (bg.transform.position.x <= -backgroundWidth * 1.5f)
            {
                MoveToRightEnd(bg);
            }
        }
    }

    private void MoveToRightEnd(GameObject bg)
    {
        GameObject rightMost = backgrounds[0];
        foreach (GameObject b in backgrounds)
        {
            if (b.transform.position.x > rightMost.transform.position.x)
                rightMost = b;
        }

        bg.transform.position = rightMost.transform.position + Vector3.right * backgroundWidth;
    }

}
