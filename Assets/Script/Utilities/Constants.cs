using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct PlayerData
{
    public int PlayerLevel;
    public List<int> FilledNodesX;
    public List<int> FilledNodesY;
    public List<int> FilledNodesLevel;
    public int PlayerCoin;
}
public struct Constant
{
    public const string PLAYER_DATA = "PlayerSavedData";
}
public struct PooledObjectTags
{
    public const string ROAD = "Road";
    public const string MERGING_NODE = "MergingNode";
    public const string CONST_MERGING_BULLET = "MergingBulletLevel";
}
public enum PlayerStates
{
    MergingState = 0,
    RunState = 1,
    WinState = 2,
    FailState = 3,
    GeneralState = 4,
}
public enum ObjectsLayer
{
    Default = 0,
    MergingBullet=6,
    Road=7,
    Node=8,
}
public enum UIPanelType
{
    MergingPanel = 0,
    RunPanel = 1,
    FinishPanel = 2,
}
public enum FinishAreaType
{
    WinArea = 0,
    FailArea = 1,
}
public enum ActiveParents
{
    RoadActiveParent = 0,
    MergingBulletParent = 1,
}
public enum DeactiveParents
{
    RoadDeactiveParent = 0,
    NodeDeactiveParent = 1,
    MergingBulletDeactiveParent = 2,
}
public enum ListOperations
{
    Adding,
    Substraction,
}

