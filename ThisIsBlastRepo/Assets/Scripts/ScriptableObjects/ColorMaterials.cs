using UnityEngine;

[CreateAssetMenu(fileName = "BlockMaterials", menuName = "Game/BlockMaterials")]
public class BlockMaterials : ScriptableObject
{
    public Material red;
    public Material green;
    public Material blue;
    public Material pink;

    public Material GetMaterial(BlockColor color)
    {
        return color switch
        {
            BlockColor.Red => red,
            BlockColor.Green => green,
            BlockColor.Blue => blue,
            BlockColor.Pink => pink,
            _ => red
        };
    }
}