using UnityEngine;

public enum BlockColor
{
    Red,
    Green,
    Blue,
    Pink,
    Yellow,
    Orange,
    Purple,
    White,
    Black
}


[CreateAssetMenu(fileName = "BlockMaterials", menuName = "Game/BlockMaterials")]
public class BlockMaterials : ScriptableObject
{
    public Material red;
    public Material green;
    public Material blue;
    public Material pink;
    public Material yellow;
    public Material orange;
    public Material purple;
    public Material white;
    public Material black;

    public Material GetMaterial(BlockColor color)
    {
        return color switch
        {
            BlockColor.Red => red,
            BlockColor.Green => green,
            BlockColor.Blue => blue,
            BlockColor.Pink => pink,
            BlockColor.Yellow => yellow,
            BlockColor.Purple => purple,
            BlockColor.Orange => orange,
            BlockColor.White => white,  
            BlockColor.Black => black,
            _ => red
        };
    }
}