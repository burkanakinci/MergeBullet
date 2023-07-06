using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct PlayerData
{
    public int PlayerLevel;
    public int PlayerScore;
    public int PlayerCoin;
}
public struct Constant
{
    public const string PLAYER_DATA = "PlayerSavedData";
}
public enum PooledObjectType
{
    Road = 0,
}
public enum PlayerStates
{
    MergingState = 0,
    RunState = 1,
    FinishState = 2,
    FailState = 3,
    GeneralState,
}
public enum ObjectsLayer
{
    Default = 0,
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
}
public enum DeactiveParents
{
    RoadDeactiveParent = 0,
}
public enum ListOperations
{
    Adding,
    Substraction,
}

