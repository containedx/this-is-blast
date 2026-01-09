using System;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    public UnityEvent<Block> onBlockShot = new UnityEvent<Block>();

    public BlockColor blockColor = BlockColor.Red;
    [SerializeField] private float cellSize = 0.6f;

    [Header("Block Color Materials")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private BlockMaterials materials;

    private bool moveDown = false;
    //private bool moveScale = false;
    private Vector3 targetPosition;
    //private Vector3 targetScale;
    private float moveSpeed = 10f;

    private void Awake()
    {
        alreadyShot = false;
    }

    private void Start()
    {
        targetPosition = transform.localPosition;
    }

    private void OnDestroy()
    {
        onBlockShot.RemoveAllListeners();
    }

    private void Update()
    {
        if(moveDown)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.localPosition = targetPosition;
                moveDown = false;
            }
        }
    }

    public void MoveDown()
    {
        moveDown = true;
        targetPosition.z -= cellSize;
    }

    private bool alreadyShot = false;
    private void OnTriggerEnter(Collider collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile == null) return;

        if (projectile.target == this)
        {
            Shot(collision.gameObject);
        }
    }

    public void Shot(GameObject projectile)
    {
        if (alreadyShot)
        {
            return;
        }

        alreadyShot = true;
        ObjectPooler.Instance.SpawnFromPool(PoolObjectType.ShotParticle, transform.position, Quaternion.identity, 0.5f);
        onBlockShot?.Invoke(this);
        BeginRemoveBlock();
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
