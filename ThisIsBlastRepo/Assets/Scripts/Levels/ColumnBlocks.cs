using System.Collections.Generic;


[System.Serializable]
public class ColumnBlocks
{
    public List<Block> blocks = new();
    public bool isProcessing = false;

    public ColumnBlocks(List<Block> columnBlocks)
    {
        this.blocks = columnBlocks;

        foreach(Block block in columnBlocks)
        {
            block.onBlockShot.AddListener(OnBlockShot);
        }
    }

    public Block TryToFindTarget(BlockColor color)
    {
        if (isProcessing) return null;

        var bottomBlock = blocks[blocks.Count - 1];
        if (bottomBlock.blockColor == color)
        {
            isProcessing = true;
            return bottomBlock;
        }
        else return null;
    }

    private void OnBlockShot(Block block)
    {
        blocks.Remove(block);
        isProcessing = false;

        // TODO: move column down
    }
}
