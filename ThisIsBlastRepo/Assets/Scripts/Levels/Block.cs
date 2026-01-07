using System;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    public UnityEvent<Block> onBlockShot = new UnityEvent<Block>();
    public UnityEvent onMoveDownFinished = new UnityEvent();

    public BlockColor blockColor = BlockColor.Red;
    [SerializeField] private float cellSize = 0.65f;

    [Header("Block Color Materials")]
    private MeshRenderer meshRenderer;
    [SerializeField] private BlockMaterials materials;

    private bool moveDown = false;
    private bool moveScale = false;
    private Vector3 targetPosition;
    private Vector3 targetScale;
    private float moveSpeed = 10f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDestroy()
    {
        onBlockShot.RemoveAllListeners();
        onMoveDownFinished.RemoveAllListeners();
    }

    private void Update()
    {
        if(moveDown)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                moveDown = false;
                onMoveDownFinished?.Invoke();
            }
        }
    }

    public void MoveDown()
    {
        moveDown = true;
        targetPosition = transform.position;
        targetPosition.z -= cellSize;
    }

    private void OnTriggerEnter(Collider collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile == null) return;

        if (projectile.target == this)
        {
            ObjectPooler.Instance.ReturnToPool(PoolObjectType.Projectile, collision.gameObject);
            onBlockShot?.Invoke(this);
            BeginRemoveBlock();
        }
    }

    public void SetColor(BlockColor color)
    {
        blockColor = color;

        meshRenderer.material = materials.GetMaterial(color);
    }

    private void BeginRemoveBlock()
    {
        Destroy(gameObject, 0.1f);
    }
}
