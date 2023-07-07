using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyBulletButton : UIBaseButton<MergingPanel>
{
    [SerializeField] private Image m_ButtonBG;
    private Color m_BGColor;
    [SerializeField] private Image m_BulletIcon;
    private Color m_IconColor;
    [SerializeField] private TextMeshProUGUI m_BuyPriceText;
    private Color m_TextColor;
    public override void Initialize(MergingPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_BGColor = m_ButtonBG.color;
        m_IconColor = m_BulletIcon.color;
        m_TextColor = m_BuyPriceText.color;
        SetButtonTransparency();
        GameManager.Instance.PlayerManager.Player.OnChangedCoinValue += SetButtonTransparency;
    }
    private Node m_TempEmptyNode;
    protected override void OnClickAction()
    {
        if (GameManager.Instance.Entities.MergingPlatform.MergingGrid.GetEmptyNode() == null ||
        GameManager.Instance.PlayerManager.Player.PlayerCoin < 100)
        {
            return;
        }
        GameManager.Instance.PlayerManager.Player.SetPlayerCoin(GameManager.Instance.PlayerManager.Player.PlayerCoin - 100);
        m_TempEmptyNode = GameManager.Instance.Entities.MergingPlatform.MergingGrid.GetEmptyNode();
        m_TempEmptyNode.SetBulletOnNode(GameManager.Instance.ObjectPool.SpawnFromPool(
            PooledObjectTags.CONST_MERGING_BULLET + "1",
            m_TempEmptyNode.transform.position,
            Quaternion.identity,
            GameManager.Instance.Entities.GetActiveParent(ActiveParents.MergingBulletParent)
        ).GetGameObject().GetComponent<MergingBullet>());
        GameManager.Instance.PlayerManager.Player.AddPlayerFilledNodes(m_TempEmptyNode, 1);
    }
    private float m_ButtonAlpha;
    private void SetButtonTransparency()
    {
        m_ButtonAlpha = GameManager.Instance.PlayerManager.Player.PlayerCoin < 100 ? 0.5f : 1.0f;
        m_BGColor.a = m_ButtonAlpha;
        m_IconColor.a = m_ButtonAlpha;
        m_TextColor.a = m_ButtonAlpha;
        m_ButtonBG.color = m_BGColor;
        m_BulletIcon.color = m_IconColor;
        m_BuyPriceText.color = m_TextColor;
    }
    #region Event
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.Instance.PlayerManager.Player.OnChangedCoinValue -= SetButtonTransparency;
    }
    #endregion
}
