using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelInitializer LevelInitializer;
    [SerializeField] private LevelData levelData;

    public List<ColumnBlocks> currentLevelBlocks;

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
    }
}
