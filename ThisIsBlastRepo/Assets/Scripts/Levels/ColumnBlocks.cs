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
    }

    public int GetBlocksCount()
    {
        return blocks.Count;
    }

    public bool IsEmpty()
    {
        return blocks.Count == 0;
    }

    public BlockColor GetBottomColor()
    {
        return blocks[blocks.Count - 1].blockColor;
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
        if (!block.IsDoubleDeck())
        {
            blocks.Remove(block);
            MoveColumnDown();
        }
        
        FinishProcessing();
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
