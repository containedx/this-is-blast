using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    [SerializeField] private LevelData levelData;
    [SerializeField] private float cellSize = 0.5f;

    private void Awake()
    {
        InitBlocks();
    }

    private void InitBlocks()
    {
        for (int col = 0; col < levelData.columns.Count; col++)
        {
            var column = levelData.columns[col];

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
            }
        }
    }
}
