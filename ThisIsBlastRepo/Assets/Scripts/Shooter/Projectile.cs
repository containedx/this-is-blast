using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody rb;

    public Block target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void Shoot(Block block)
    {
        target = block;
        Vector3 dir = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;
        transform.forward = dir;
    }
}
