using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ShopItem
{
    public enum ItemBelongsTo { Equipped, Inventory, Shop }

    [System.Serializable]
    public struct ItemInfo
    {
        public GameObject Parent;
        public GameObject SelectHighlight;
        public Button ItemButton;
        public Image Icon;
    }

    [System.Serializable]
    public struct ItemData
    {
        public string Name;
        public int Cost;
        public Sprite Icon;
        public ItemBelongsTo BelongsTo;

        public ItemData(string name, int cost, Sprite icon, ItemBelongsTo belongsTo)
        {
            Name = name;
            Cost = cost;
            Icon = icon;
            BelongsTo = belongsTo;
        }
    }

    public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action ItemSelected;
        [SerializeField]
        private ItemInfo m_ItemInfo;
        [SerializeField]
        private MouseCursor m_CursorHandler;
        private int m_Cost;
        private bool m_Selected = false;
        private string m_Name;
        private ItemBelongsTo m_BelongsTo;

        public GameObject Parent { get { return m_ItemInfo.Parent; } }
        public Sprite Icon
        {
            get { return m_ItemInfo.Icon.sprite; }
            set { m_ItemInfo.Icon.sprite = value; }
        }

        public ItemBelongsTo BelongsTo
        {
            get { return m_BelongsTo; }
            set { m_BelongsTo = value; }
        }

        public int Cost
        {
            get { return m_Cost; }
            set { m_Cost = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public bool Selected
        {
            get { return m_Selected; }
            set
            {
                m_Selected = value;
                m_ItemInfo.SelectHighlight.SetActive(m_Selected);
                ItemSelected?.Invoke();
            }
        }

        #region Lifecycle
        private void Awake()
        {
            if (m_ItemInfo.ItemButton != null) m_ItemInfo.ItemButton.onClick.AddListener(delegate { Selected = !Selected; });
        }

        private void OnDestroy()
        {
            if (m_ItemInfo.ItemButton != null) m_ItemInfo.ItemButton.onClick.RemoveAllListeners();
        }
        #endregion

        #region Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_CursorHandler.HandCursor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_CursorHandler.PointerCursor();
        }
        #endregion
    }
}
