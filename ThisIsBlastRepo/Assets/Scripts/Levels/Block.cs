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

    [Header("Materials for colors")]
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material pinkMaterial;
    private MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnDestroy()
    {
        onBlockShot.RemoveAllListeners();
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
