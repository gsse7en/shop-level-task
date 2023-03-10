using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinsData", menuName = "Coins Data", order = 51)]
public class CoinsData : ScriptableObject
{
    public event Action<int> CoinsChanged;

    [SerializeField]
    private int m_CoinsCount;

    public int Count
    {
        get { return m_CoinsCount; }
        set
        {
            m_CoinsCount = value;
            CoinsChanged?.Invoke(m_CoinsCount);
        }
    }
}
