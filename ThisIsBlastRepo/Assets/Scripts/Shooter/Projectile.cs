using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody rb;

    public Block target;

    private float timer = 0f;
    private float maximumLifespan = 0.6f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        timer = 0f;
    }

    private void Update()
    {
        // make sure block is always shot
        timer += Time.deltaTime;
        if (timer > maximumLifespan)
        {
            timer = 0f;
            target.Shot(gameObject);
        }
    }

    public void Shoot(Block block)
    {
        timer = 0f;
        target = block;
        Vector3 dir = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;
        transform.forward = dir;
    }
}
