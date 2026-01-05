using UnityEngine;

public enum BlockColor
{
    Red,
    Green,
    Blue,
    Pink
}

public class Block : MonoBehaviour
{
    private MeshRenderer mr;

    [Header("Materials for colors")]
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material pinkMaterial;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public void SetColor(BlockColor color)
    {
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
}
