using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action OnLevelStarted;
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelInitializer LevelInitializer;
    public ShooterManager ShooterManager;
    [SerializeField] private List<LevelData> levelData;

    public List<ColumnBlocks> currentLevelBlocks;
    private int currentLevelAllBlocksCount = 0;
    [SerializeField]
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

    public int GetLevelIndex()
    {
        return levelIndex;
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
        if(levelIndex >= levelData.Count)
        {
            Debug.Log("NO MORE LEVELS");
            return;
        }

        ChangeState(new LoadingState());
        currentLevelBlocks = LevelInitializer.InitLevel(levelData[levelIndex]);
        currentLevelAllBlocksCount = GetBlocksCount();

        OnLevelStarted?.Invoke();
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
