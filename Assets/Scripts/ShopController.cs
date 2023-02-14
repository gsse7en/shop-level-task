using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UI.ShopItem;
using TMPro;

namespace UI.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField]
        private ItemsData m_ItemsData;
        [SerializeField]
        private CoinsData m_CoinsData;
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
        [SerializeField]
        private TextMeshProUGUI m_BuyPrice;
        [SerializeField]
        private TextMeshProUGUI m_SellPrice;
        private List<Item> m_Items = new List<Item>();
        private List<ItemData> m_EquippedItems = new List<ItemData>();

        #region Lyfecicle
        private void Awake()
        {
            LoadItems();
            SubcribeToEvents();
        }

        private void OnEnable()
        {
            LoadItems();
        }

        private void OnDisable()
        {
            DeselectAll();
            SaveItems();
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
            item.ItemSelected += OnItemSelected;
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
            m_ItemsData.ItemList.AddRange(m_EquippedItems);
        }

        private void LoadItems()
        {
            ClearItems();

            foreach (ItemData item in m_ItemsData.ItemList)
            {
                if (item.BelongsTo == ItemBelongsTo.Equipped) m_EquippedItems.Add(item);
                else m_Items.Add(CreateItem(item.Name, item.Cost, item.Icon, item.BelongsTo));
            }
        }

        private void ClearItems()
        {
            foreach (Item item in m_Items)
            {
                item.ItemSelected -= OnItemSelected;
                Destroy(item.Parent);
            }
            m_Items.Clear();
            m_EquippedItems.Clear();
            m_BuyPrice.text = m_SellPrice.text = "0";
        }

        private void Sell()
        {
            SellSelected();
            SaveItems();
            LoadItems();
        }

        private void Buy()
        {
            BuySelected();
            SaveItems();
            LoadItems();
        }

        private void BuySelected()
        {
            List<Item> itemsToBuy = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Shop && item.Selected).ToList();
            int cost = itemsToBuy.Select(item => item.Cost).Sum();
            if (cost > m_CoinsData.Count)
            {
                //TODO
                Debug.LogError("NOT ENOUGH CASH");
                return;
            }
            else
            {
                m_CoinsData.Count -= cost;
                foreach (Item item in itemsToBuy) item.BelongsTo = ItemBelongsTo.Inventory;
            }
        }

        private void SellSelected()
        {
            List<Item> itemsToSell = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Inventory && item.Selected).ToList();
            int cost = itemsToSell.Select(item => item.Cost).Sum();
            m_CoinsData.Count += cost;
            foreach (Item item in itemsToSell) item.BelongsTo = ItemBelongsTo.Shop;
        }

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

        private void OnItemSelected()
        {
            List<Item> itemsToSell = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Inventory && item.Selected).ToList();
            List<Item> itemsToBuy = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Shop && item.Selected).ToList();
            m_SellPrice.text = itemsToSell.Select(item => item.Cost).Sum().ToString();
            m_BuyPrice.text = itemsToBuy.Select(item => item.Cost).Sum().ToString();
        }
        #endregion
    }
}
