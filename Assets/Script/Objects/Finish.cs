using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : CustomBehaviour
{
    [SerializeField] private FinishBarrel[] m_FinishBarrel;
    public override void Initialize()
    {
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.MergingState).OnEnterEvent += OnLevelStart;
        for (int _barrel = 0; _barrel < m_FinishBarrel.Length; _barrel++)
        {
            m_FinishBarrel[_barrel].Initialize(this);
        }
    }
    public FinishBarrel GetBarrel(int _index)
    {
        return m_FinishBarrel[_index];
    }
    private void SetFinishPos()
    {
        transform.position = (Vector3.forward * GameManager.Instance.LevelManager.CurrentLevelData.RoadCount * 10) + (Vector3.forward * 10);
    }
    private void SetBarrels()
    {
        for (int _barrel = 0; _barrel < m_FinishBarrel.Length; _barrel++)
        {
            m_FinishBarrel[_barrel].gameObject.SetActive(true);
            m_FinishBarrel[_barrel].SetBarrelValue(GameManager.Instance.LevelManager.CurrentLevelData.BarrelValues[_barrel]);
            m_FinishBarrel[_barrel].SetStartValue(GameManager.Instance.LevelManager.CurrentLevelData.BarrelValues[_barrel]);
        }
    }
    private void OnLevelStart()
    {
        SetFinishPos();
        SetBarrels();
    }

    #region Event
    private void OnDestroy()
    {
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.MergingState).OnEnterEvent -= OnLevelStart;
    }
    #endregion
}
