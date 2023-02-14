using System;
using System.Collections;
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

    public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
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
                if (m_BelongsTo == ItemBelongsTo.Equipped) return;
                m_Selected = value;
                m_ItemInfo.SelectHighlight.SetActive(m_Selected);
            }
        }

        #region Lifecycle
        private void Awake()
        {
            if (m_ItemInfo.ItemButton != null)
            {
                m_ItemInfo.ItemButton.onClick.AddListener(delegate { Selected = !Selected; });
            }
        }

        private void OnDestroy()
        {
            if (m_ItemInfo.ItemButton != null)
            {
                m_ItemInfo.ItemButton.onClick.RemoveAllListeners();
            }
        }
        #endregion

        #region Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_BelongsTo == ItemBelongsTo.Equipped) return;
            m_CursorHandler.HandCursor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_BelongsTo == ItemBelongsTo.Equipped) return;
            m_CursorHandler.PointerCursor();
        }
        #endregion
    }
}
