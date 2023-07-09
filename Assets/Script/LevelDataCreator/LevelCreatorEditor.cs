
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelDataCreator))]
public class LevelCreatorEditor : Editor
{
    private int m_TempRoadLength;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelDataCreator m_LevelDataCreator = (LevelDataCreator)target;

        EditorGUI.BeginChangeCheck();

        #region Set LevelNumber
        GUILayout.Label("Level Number");
        m_LevelDataCreator.LevelNumber = EditorGUILayout.IntField(m_LevelDataCreator.LevelNumber);
        #endregion

        #region Set Grid
        EditorGUI.BeginChangeCheck();
        GUILayout.Space(25f);
        GUILayout.Label("Grid Cell Width Count");
        m_LevelDataCreator.GridWeight = EditorGUILayout.IntSlider(m_LevelDataCreator.GridWeight, 1, 5);
        GUILayout.Label("Grid Cell Height Count");
        m_LevelDataCreator.GridHeight = EditorGUILayout.IntSlider(m_LevelDataCreator.GridHeight, 1, 6);
        GUILayout.Space(25f);
        if (EditorGUI.EndChangeCheck())
        {
            m_LevelDataCreator.SpawnNodesLevelData();
        }
        #endregion
        #region Missile Obstacle
        GUILayout.Space(25f);
        GUILayout.Label("Use Missile Obstacle");
        m_LevelDataCreator.UseMissile = EditorGUILayout.Toggle("Use Missile", m_LevelDataCreator.UseMissile);
        GUILayout.Label("Grid Cell Height Count");
        m_LevelDataCreator.MissileSpawnRate = EditorGUILayout.IntSlider(m_LevelDataCreator.MissileSpawnRate, 3, 6);
        #endregion

        GUILayout.Space(25);
        if (GUILayout.Button("Create Level"))
        {
            m_LevelDataCreator.CreateLevel();
        }
    }
}
#endif

