using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private int projectilesCount = 20;
    [SerializeField] private Projectile projectilePrefab;


    private void Shoot(Transform target)
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Shoot(target);
    }
}
