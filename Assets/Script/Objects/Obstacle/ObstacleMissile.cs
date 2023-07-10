using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMissile : Obstacle
{
    [SerializeField] private float m_MissileSpeed;
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.WinState).OnEnterEvent += OnObjectDeactive;
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.FailState).OnEnterEvent += OnObjectDeactive;
    }
    public override void OnObjectDeactive()
    {
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.WinState).OnEnterEvent -= OnObjectDeactive;
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.FailState).OnEnterEvent -= OnObjectDeactive;
        base.OnObjectDeactive();
    }
    private void Update()
    {
        transform.position -= Vector3.forward * Time.deltaTime * m_MissileSpeed;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.WinState).OnEnterEvent -= OnObjectDeactive;
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.FailState).OnEnterEvent -= OnObjectDeactive;
    }
}
