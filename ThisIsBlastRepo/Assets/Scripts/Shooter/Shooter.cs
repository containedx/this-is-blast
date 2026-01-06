using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Shooter Settings")]
    public int projectilesCount = 20;
    public BlockColor blockColor = BlockColor.Red;
    private float cooldownTime = 0.2f;

    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;

    [Header("UI")]
    [SerializeField] private TMP_Text countText;

    private List<ColumnBlocks> levelBlocks;

    private void Awake()
    {
        countText.text = projectilesCount.ToString();
    }

    public void Activate(List<ColumnBlocks> levelBlocks)
    {
        this.levelBlocks = levelBlocks;
        StartCoroutine(ShootRoutine());
    }

    public void Activate()
    {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);

        while(projectilesCount > 0)
        {
            foreach (var column in levelBlocks)
            {
                var potentialTarget = column.TryToFindTarget(blockColor);
                //Debug.Log(potentialTarget);

                if (potentialTarget != null)
                {
                    Shoot(potentialTarget);
                    yield return new WaitForSeconds(cooldownTime);
                }
            }
        }
    }

    private void Shoot(Block target)
    {
        //Debug.Log("shooting " + target.name);

        projectilesCount -= 1;
        countText.text = projectilesCount.ToString();

        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Shoot(target);
    }
}
