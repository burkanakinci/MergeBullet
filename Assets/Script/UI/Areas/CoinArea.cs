using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinArea : UIArea
{
    [SerializeField] private TextMeshProUGUI m_CoinText;
    public override void Initialize(UIPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        GameManager.Instance.PlayerManager.Player.OnChangedCoinValue += SetCoinText;
    }
    public override void ShowArea()
    {
        base.ShowArea();
        SetCoinText();
    }
    private void SetCoinText()
    {
        m_CoinText.text = GameManager.Instance.PlayerManager.Player.PlayerCoin.ToString();
    }
    #region Event
    private void OnDestroy()
    {
        GameManager.Instance.PlayerManager.Player.OnChangedCoinValue -= SetCoinText;
    }
    #endregion
}
