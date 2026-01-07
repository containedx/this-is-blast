using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private Block blockPrefab;
    [SerializeField] private float cellSize = 0.5f;

    [Header("Shooters")]
    [SerializeField] private ShooterManager shooterManager;

    public List<ColumnBlocks> InitLevel(LevelData levelData)
    {
        shooterManager.SpawnShooters(levelData);

        List<ColumnBlocks> levelBlocks = new();

        for (int col = 0; col < levelData.columns.Count; col++)
        {
            var column = levelData.columns[col];
            List<Block> columnBlocks = new();

            for (int row = 0; row < column.blocks.Count; row++)
            {
                BlockColor color = column.blocks[row];

                Block block = Instantiate(blockPrefab, transform);

                block.transform.localPosition = new Vector3(
                    col * cellSize,
                    0,
                    -row * cellSize
                );

                block.SetColor(color);
                block.gameObject.name = "block" + row;
                columnBlocks.Add(block);
            }

            levelBlocks.Add(new ColumnBlocks(columnBlocks));
        }

        return levelBlocks;
    }
}
