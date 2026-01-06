using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        // it is enough to listen to one block finishing moving down
        blocks[0].onMoveDownFinished.AddListener(FinishProcessing);
    }

    public bool IsEmpty()
    {
        return blocks.Count == 0;
    }

    public Block TryToFindTarget(BlockColor color)
    {
        if (isProcessing || IsEmpty()) return null;

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
        MoveColumnDown();
    }

    private void MoveColumnDown()
    {
        foreach(var block in blocks)
        {
            block.MoveDown();
        }
    }

    private void FinishProcessing()
    {
        isProcessing = false;
    }
}
