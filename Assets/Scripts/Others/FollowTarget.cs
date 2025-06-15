using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float followSpeed;
    [SerializeField] private float offsetY;

    private void Update()
    {
        if (!CatGroupManager.instance.getLeaderCat()) return;
        target = CatGroupManager.instance.getLeaderCat().transform;
        Follow(target);
    }

    private void Follow(Transform target)
    {
        if (!target) return;
        Vector2 targetPos = new Vector2(target.position.x, target.position.y + offsetY);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
