using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelInitializer LevelInitializer;
    public ShooterManager ShooterManager;
    [SerializeField] private List<LevelData> levelData;

    public List<ColumnBlocks> currentLevelBlocks;
    private int currentLevelAllBlocksCount = 0;
    private int levelIndex = -1;

    public IGameState currentState { private set; get; }

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

        NextLevel();
    }

    private void OnDestroy()
    {
        currentState?.Exit();
        currentState = null;
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void RepeatLevel()
    {
        RunLevel();
    }

    public void NextLevel()
    {
        levelIndex++;
        RunLevel();
    }

    private void RunLevel()
    {
        currentLevelBlocks = LevelInitializer.InitLevel(levelData[levelIndex]);
        currentLevelAllBlocksCount = GetBlocksCount();

        ChangeState(new PlayingState());
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
