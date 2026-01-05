using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelInitializer LevelInitializer;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Shooter shooter;

    public List<ColumnBlocks> currentLevelBlocks;

    private void Awake()
    {
        currentLevelBlocks = LevelInitializer.InitBlocks(levelData);
        
        shooter.Activate(currentLevelBlocks);
    }
}
