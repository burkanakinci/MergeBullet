using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergingPanel : UIPanel
{
    [SerializeField] private List<UIBaseButton<MergingPanel>> m_MergingButtons;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_MergingButtons.ForEach(_button =>
        {
            _button.gameObject.SetActive(true);
            _button.Initialize(this);
        });
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        ShowAllArea();
    }
}
