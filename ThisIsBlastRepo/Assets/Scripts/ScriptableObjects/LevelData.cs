using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public List<ShooterData> shooters;
    public List<ColumnData> columns; 
}

[System.Serializable]
public class ShooterData
{
    public int projectiles;
    public BlockColor blockColor;
}

[System.Serializable]
public class ColumnData
{
    public List<BlockColor> blocks = new();
}
