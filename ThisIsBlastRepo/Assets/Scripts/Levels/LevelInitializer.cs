using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private Block blockPrefab;
    [SerializeField] private float cellSize = 0.5f;

    [Header("Shooters")]
    [SerializeField] private Shooter shooterPrefab;
    [SerializeField] private Transform shooterManager;

    public List<ColumnBlocks> InitLevel(LevelData levelData)
    {
        float space = 0f;
        foreach(var shooterData in levelData.shooters)
        {
            Shooter shooter = Instantiate(shooterPrefab, shooterManager);
            var shooterPos = shooter.transform.position;
            shooterPos.x += space;
            shooter.transform.position = shooterPos;
            space += 3f;
            shooter.Setup(shooterData);
        }


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
