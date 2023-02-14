using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.ShopItem;
using UnityEngine.UI;
using System.Linq;
using Game.Player;

namespace UI.Inventory
{
    public class InvetoryController : MonoBehaviour
    {
        //temp
        [SerializeField]
        private Sprite helmet;
        [SerializeField]
        private Sprite sword;
        /*
        [SerializeField]
        private Data m_PlayerData;
         */
        [SerializeField]
        private PlayerController m_Player;
        [SerializeField]
        private RectTransform m_EquippedContent;
        [SerializeField]
        private RectTransform m_InventoryContent;
        [SerializeField]
        private Button m_ButtonClose;
        [SerializeField]
        private Button m_ButtonEquip;
        [SerializeField]
        private Item m_ItemPrefab;
        private List<Item> m_Items = new List<Item>();

        #region Lyfecicle
        private void Awake()
        {
            PopulateIventory();
            PopulateEquipped();
            //LoadItems();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            Clean();
            UnsubscribeFromEvents();
        }
        #endregion

        #region Public
        
        #endregion

        #region Private
        private Item CreateItem(string name, int cost, Sprite icon, ItemBelongsTo belongsTo)
        {
            Item item = Instantiate(m_ItemPrefab, belongsTo == ItemBelongsTo.Inventory ? m_InventoryContent : m_EquippedContent);
            item.BelongsTo = belongsTo;
            item.Name = name;
            item.Cost = cost;
            item.Icon = icon;
            return item;
        }

        private void PopulateIventory()
        {
            m_Items.Add(CreateItem("Sword", 150, sword, ItemBelongsTo.Inventory));
        }

        private void PopulateEquipped()
        {
            m_Items.Add(CreateItem("Helmet", 100, helmet, ItemBelongsTo.Inventory));
        }

        private void LoadItems()
        {
            List<Item> items = new List<Item>();
            items.AddRange(m_Items);
            m_Items.Clear();

            foreach (Item item in items)
            {
                m_Items.Add(CreateItem(item.Name, item.Cost, item.Icon, item.BelongsTo));
            }
        }

        private void Clean()
        {
            foreach (Item item in m_Items) Destroy(item.Parent);
        }

        private void Equip()
        {
            List<Item> itemsToEquip = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Inventory && item.Selected).ToList();
            foreach (Item item in itemsToEquip) item.BelongsTo = ItemBelongsTo.Equipped;
            Clean();
            LoadItems();
            m_Player.Equip(itemsToEquip.Select(item => item.Name).ToList());
        }

        private void DeselectAll()
        {
            foreach (Item item in m_Items) item.Selected = false;
        }
        #endregion

        #region Delegates
        private void SubscribeToEvents()
        {
            if (m_ButtonClose != null) m_ButtonClose.onClick.AddListener(delegate { DeselectAll(); });
            if (m_ButtonEquip != null) m_ButtonEquip.onClick.AddListener(delegate { Equip(); });
        }

        private void UnsubscribeFromEvents()
        {
            //if (m_ButtonClose != null) m_ButtonClose.onClick.RemoveAllListeners();
            if (m_ButtonEquip != null) m_ButtonEquip.onClick.RemoveAllListeners();
        }
        #endregion
    }
}
