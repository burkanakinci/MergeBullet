using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelArea : UIArea
{
    [SerializeField] private TextMeshProUGUI m_LevelText;
    public override void Initialize(UIPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }
    public override void ShowArea()
    {
        m_LevelText.text = "LEVEL : " + GameManager.Instance.PlayerManager.Player.PlayerLevel.ToString();
        base.ShowArea();
    }
}
