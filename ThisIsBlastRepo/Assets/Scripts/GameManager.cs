using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelInitializer LevelInitializer;
    [SerializeField] private LevelData levelData;

    public List<ColumnBlocks> currentLevelBlocks;
    private int currentLevelAllBlocksCount = 0;

    private void Awake()
    {
        #region Instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion

        currentLevelBlocks = LevelInitializer.InitBlocks(levelData);
        currentLevelAllBlocksCount = GetBlocksCount();
    }

    public int GetBlocksCount()
    {
        int count = 0;
        foreach (var column in currentLevelBlocks)
        {
            count += column.GetBlocksCount();
        }
        return count;
    }

    public int GetInitialBlocksCount()
    {
        return currentLevelAllBlocksCount;
    }
}
