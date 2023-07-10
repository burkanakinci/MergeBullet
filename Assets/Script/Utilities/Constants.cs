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
    public const string GUN = "Gun";
    public const string CONST_SHOOTING_BULLET = "ShootingBulletLevel";
    public const string TRIPLE_SHOOT_GATE = "TipleShootGate";
    public const string FIRE_RATE_GATE = "FireRateGate";
    public const string RANGE_GATE = "RangeGate";
    public const string OBSTACLE_WALL = "ObstacleWall";
    public const string OBSTACLE_MISSILE = "ObstacleMissile";
    public const string COLLECTABLE_COIN = "CollectableCoin";
    public const string COLLECTABLE_SHIELD = "CollectableShield";
    public const string UI_COIN= "UIEarnedCoin";
}
public struct ObjectTags
{
    public const string START_GAME_TRIGGER = "StartTrigger";
    public const string TRIPLE_SHOOT_GATE = "TripleShootGate";
    public const string FIRE_RATE_GATE = "FireRateGate";
    public const string SHOOTING_BULLET = "ShootingBullet";
    public const string RANGE_GATE = "RangeGate";
    public const string OBSTACLE = "Obstacle";
    public const string GUN = "Gun";
    public const string ROAD = "Road";
    public const string COLLECTABLE_COIN = "CollectableCoin";
    public const string COLLECTABLE_SHIELD = "CollectableShield";
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
    MergingBullet = 6,
    Road = 7,
    Node = 8,
    StartGameTrigger = 9,
    Gun = 10,
    ShootingBullet = 11,
    Gate = 12,
    Obstacle = 13,
    Collectable=14,
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
    GunActiveParent = 2,
    ShootingBulletActiveParent = 3,
    GateActiveParent = 4,
    ObstacleActiveParent = 5,
    CollectableActiveParent=6,
    Canvas=7,
    CoinArea=8,
}
public enum DeactiveParents
{
    RoadDeactiveParent = 0,
    NodeDeactiveParent = 1,
    MergingBulletDeactiveParent = 2,
    GunDeactiveParent = 3,
    ShootingBulletDeactiveParent = 4,
    GateDeactiveParent = 5,
    ObstacleDeactiveParent = 6,
    CollectableDeactiveParent=7,
    UIObjectsDeactiveParent=8,
}
public enum ListOperations
{
    Adding,
    Removing,
}

