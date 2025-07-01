using UnityEngine;

public class MysticJam : MonoBehaviour
{
    private int transformID = 0;

    public int getTransformID() => transformID;
    private void OggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            transformID = Random.Range(1, CatGroupManager.instance.GetTransformVariety());
            //CatGroupManager.instance.TransformCats(transformID, 10f);
        }
    }
}
