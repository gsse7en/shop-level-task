//using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
//using Game.Player;
using UI.ShopItem;

namespace UI.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField]
        private ItemsData m_ItemsData;
        //[SerializeField]
        //private PlayerController m_Player;
        [SerializeField]
        private RectTransform m_ShopContent;
        [SerializeField]
        private RectTransform m_InventoryContent;
        [SerializeField]
        private Button m_ButtonSell;
        [SerializeField]
        private Button m_ButtonBuy;
        [SerializeField]
        private Item m_ItemPrefab;
        private List<Item> m_Items = new List<Item>();

        #region Lyfecicle
        private void Awake()
        {
            LoadItems();
            SubcribeToEvents();
        }

        private void OnDisable()
        {
            DeselectAll();
        }

        private void OnDestroy()
        {
            ClearItems();
            UnsubscribeFromEvents();
        }
        #endregion

        #region Private
        private Item CreateItem(string name, int cost, Sprite icon, ItemBelongsTo belongsTo)
        {
            Item item = Instantiate(m_ItemPrefab, belongsTo == ItemBelongsTo.Inventory ? m_InventoryContent : m_ShopContent);
            item.BelongsTo = belongsTo;
            item.Name = name;
            item.Cost = cost;
            item.Icon = icon;
            return item;
        }

        private void SaveItems()
        {
            List<ItemData> items = new List<ItemData>();

            foreach (Item item in m_Items)
            {
                items.Add(new ItemData(item.Name, item.Cost, item.Icon, item.BelongsTo));
            }

            m_ItemsData.ItemList.Clear();
            m_ItemsData.ItemList.AddRange(items);
        }

        private void LoadItems()
        {
            ClearItems();

            foreach (ItemData item in m_ItemsData.ItemList)
            {
                m_Items.Add(CreateItem(item.Name, item.Cost, item.Icon, item.BelongsTo));
            }
        }

        private void ClearItems()
        {
            foreach (Item item in m_Items) Destroy(item.Parent);
            m_Items.Clear();
        }

        private void Sell()
        {
            SaveItems();
            LoadItems();
        }

        private void Buy()
        {
            SaveItems();
            LoadItems();
        }

        //private List<string> EquipSelected()
        //{
        //    List<Item> itemsToEquip = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Inventory && item.Selected).ToList();
        //    foreach (Item item in itemsToEquip) item.BelongsTo = ItemBelongsTo.Equipped;
        //    return itemsToEquip.Select(item => item.Name).ToList();
        //}

        private void DeselectAll()
        {
            foreach (Item item in m_Items) item.Selected = false;
        }
        #endregion

        #region Subscriptions
        private void SubcribeToEvents()
        {
            if (m_ButtonBuy != null) m_ButtonBuy.onClick.AddListener(delegate { Buy(); });
            if (m_ButtonSell != null) m_ButtonSell.onClick.AddListener(delegate { Sell(); });
        }

        private void UnsubscribeFromEvents()
        {
            if (m_ButtonBuy != null) m_ButtonBuy.onClick.RemoveAllListeners();
            if (m_ButtonSell != null) m_ButtonSell.onClick.RemoveAllListeners();
        }
        #endregion
    }
}
