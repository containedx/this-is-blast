using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Shooters Data")]
    public int activeShootersCount = 2;
    public List<ShooterData> shooters;

    [Header("Blocks Data")]
    public List<ColumnData> columns; 
}

[System.Serializable]
public class ShooterData
{
    public int projectiles = 20;
    public BlockColor blockColor;
}

[System.Serializable]
public class ColumnData
{
    public List<BlockColor> blocks = new();
}
