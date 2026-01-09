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

    public int rowIndex = 0;

    private bool doubleDeck = false;
    private GameObject secondDeck;

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

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.05f)
            {
                transform.localPosition = targetPosition;
                moveDown = false;
            }
        }
    }

    public void InitDoubleDeck()
    {
        doubleDeck = true;
        secondDeck = Instantiate(this.gameObject, transform);
        secondDeck.transform.localPosition = new Vector3(
            0,
            cellSize * 1.5f,
            0
        );

        secondDeck.transform.localScale = new Vector3(1f,1f,1f);
    }

    public bool IsDoubleDeck()
    {
        return doubleDeck;
    }

    public void MoveDown()
    {
        moveDown = true;
        rowIndex--;
        targetPosition.z = rowIndex * cellSize;
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

        if(doubleDeck)
        {
            Destroy(secondDeck);
            onBlockShot?.Invoke(this);
            ObjectPooler.Instance.ReturnToPool(PoolObjectType.Projectile, projectile);
            doubleDeck = false;
            return;
        }

        alreadyShot = true;
        ObjectPooler.Instance.SpawnFromPool(PoolObjectType.ShotParticle, transform.position, Quaternion.identity, 0.5f);
        ObjectPooler.Instance.ReturnToPool(PoolObjectType.Projectile, projectile);
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
