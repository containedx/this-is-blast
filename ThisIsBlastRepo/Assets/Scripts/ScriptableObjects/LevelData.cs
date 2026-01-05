using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ColumnData
{
    public List<BlockColor> blocks = new();
}

[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public List<ColumnData> columns; 
}
