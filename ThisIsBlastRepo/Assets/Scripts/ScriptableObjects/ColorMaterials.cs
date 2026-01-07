using UnityEngine;

public enum BlockColor
{
    Red,
    Green,
    Blue,
    Pink,
    Yellow
}


[CreateAssetMenu(fileName = "BlockMaterials", menuName = "Game/BlockMaterials")]
public class BlockMaterials : ScriptableObject
{
    public Material red;
    public Material green;
    public Material blue;
    public Material pink;
    public Material yellow;

    public Material GetMaterial(BlockColor color)
    {
        return color switch
        {
            BlockColor.Red => red,
            BlockColor.Green => green,
            BlockColor.Blue => blue,
            BlockColor.Pink => pink,
            BlockColor.Yellow => yellow,
            _ => red
        };
    }
}