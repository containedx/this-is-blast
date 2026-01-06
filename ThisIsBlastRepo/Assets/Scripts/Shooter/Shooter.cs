using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

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
    [SerializeField] private Button activateButton;

    private List<ColumnBlocks> levelBlocks;
    private Transform shootTarget;
    private bool active = false;

    private void Awake()
    {
        countText.text = projectilesCount.ToString();
        activateButton.onClick.AddListener(Activate);
    }

    public void Activate()
    {
        this.levelBlocks = GameManager.Instance.currentLevelBlocks;
        active = true;
        StartCoroutine(ShootRoutine());
    }

    private void Update()
    {
        if (!active || shootTarget == null) return;

        Vector3 direction = shootTarget.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
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

        ShooterFinished();
    }

    private void Shoot(Block target)
    {
        //Debug.Log("shooting " + target.name);
        shootTarget = target.transform;

        projectilesCount -= 1;
        countText.text = projectilesCount.ToString();

        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Shoot(target);
    }

    private void ShooterFinished()
    {
        //TODO: animate, shooter move to the side
        active = false;
        gameObject.SetActive(false);
    }
}
