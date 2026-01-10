using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelData data = (LevelData)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Level Analysis", EditorStyles.boldLabel);

        DrawColorAnalysis(data);
    }

    private void DrawColorAnalysis(LevelData data)
    {
        Dictionary<BlockColor, int> blockCounts = new();
        Dictionary<BlockColor, int> shooterCounts = new();

        // blocks count
        foreach (var column in data.columns)
        {
            foreach (var block in column.blocks)
            {
                if (!blockCounts.ContainsKey(block))
                    blockCounts[block] = 0;

                blockCounts[block]++;
                if(data.doubleDeck) blockCounts[block]++;
            }
        }

        // shooters count
        foreach (var shooter in data.shooters)
        {
            if (!shooterCounts.ContainsKey(shooter.blockColor))
                shooterCounts[shooter.blockColor] = 0;

            shooterCounts[shooter.blockColor] += shooter.projectiles;
        }

        HashSet<BlockColor> allColors = new();
        allColors.UnionWith(blockCounts.Keys);
        allColors.UnionWith(shooterCounts.Keys);

        foreach (var color in allColors)
        {
            int blocks = blockCounts.TryGetValue(color, out int b) ? b : 0;
            int shooters = shooterCounts.TryGetValue(color, out int s) ? s : 0;

            EditorGUILayout.LabelField(
                $"{color}: blocks {blocks}, shooters {shooters}"
            );
        }
    }
}
