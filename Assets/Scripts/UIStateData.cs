using System;
using UnityEngine;
using UI;

[CreateAssetMenu(fileName = "UIStateData", menuName = "UI State Data", order = 51)]
public class UIStateData : ScriptableObject
{
    public event Action<UIScreens> UIScreenChanged;

    [SerializeField]
    private UIScreens m_UIScreenState;

    public UIScreens ScreenState
    {
        get { return m_UIScreenState; }
        set
        {
            m_UIScreenState = value;
            UIScreenChanged?.Invoke(m_UIScreenState);
        }
    }
}
