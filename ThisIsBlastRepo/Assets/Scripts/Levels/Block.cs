using System;
using UnityEngine;
using UnityEngine.Events;

public enum BlockColor
{
    Red,
    Green,
    Blue,
    Pink
}

public class Block : MonoBehaviour
{
    public UnityEvent<Block> onBlockShot;

    public BlockColor blockColor = BlockColor.Red;
    [SerializeField] private float blockSize = 0.6f;

    [Header("Materials for colors")]
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material pinkMaterial;
    private MeshRenderer mr;

    private bool moveDown = false;
    private bool moveScale = false;
    private Vector3 targetPosition;
    private Vector3 targetScale;
    private float moveSpeed = 5f;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnDestroy()
    {
        onBlockShot.RemoveAllListeners();
    }

    private void Update()
    {
        if(moveDown)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                moveDown = false;
            }
        }
    }

    public void MoveDown()
    {
        moveDown = true;
        targetPosition = transform.position;
        targetPosition.z -= blockSize;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile == null) return;

        if (projectile.target == this)
        {
            Destroy(collision.gameObject);
            onBlockShot?.Invoke(this);
            BeginRemoveBlock();
        }
    }

    public void SetColor(BlockColor color)
    {
        blockColor = color;

        switch (color)
        {
            case BlockColor.Red:
                mr.material = redMaterial;
                break;
            case BlockColor.Blue:
                mr.material = blueMaterial;
                break;
            case BlockColor.Green:
                mr.material = greenMaterial;
                break;
            case BlockColor.Pink:
                mr.material = pinkMaterial;
                break;

        }
    }

    private void BeginRemoveBlock()
    {
        Destroy(gameObject, 0.1f);
    }
}
