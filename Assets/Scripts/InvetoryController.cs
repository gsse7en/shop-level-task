using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UI.ShopItem;
using Game.Player;

namespace UI.Inventory
{
    public class InvetoryController : MonoBehaviour
    {
        [SerializeField]
        private ItemsData m_ItemsData;
        [SerializeField]
        private PlayerController m_Player;
        [SerializeField]
        private RectTransform m_EquippedContent;
        [SerializeField]
        private RectTransform m_InventoryContent;
        [SerializeField]
        private Button m_ButtonEquip;
        [SerializeField]
        private Button m_ButtonStrip;
        [SerializeField]
        private Item m_ItemPrefab;
        private List<Item> m_Items = new List<Item>();
        private List<ItemData> m_ShopItems = new List<ItemData>();

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
            Item item = Instantiate(m_ItemPrefab, belongsTo == ItemBelongsTo.Inventory ? m_InventoryContent : m_EquippedContent);

            item.BelongsTo = belongsTo;
            item.Name = name;
            item.Cost = cost;
            item.Icon = icon;

            return item;
        }

        private void SaveItems()
        {
            List<ItemData> items = new List<ItemData>();

            m_Items.ForEach(item => items.Add(new ItemData(item.Name, item.Cost, item.Icon, item.BelongsTo)));
            m_ItemsData.ItemList.Clear();
            m_ItemsData.ItemList.AddRange(items);
            m_ItemsData.ItemList.AddRange(m_ShopItems);
        }

        private void LoadItems()
        {
            ClearItems();

            m_ItemsData.ItemList.ForEach(item =>
            {
                if (item.BelongsTo == ItemBelongsTo.Shop) m_ShopItems.Add(item);
                else m_Items.Add(CreateItem(item.Name, item.Cost, item.Icon, item.BelongsTo));
            });
        }

        private void ClearItems()
        {
            m_Items.ForEach(item => Destroy(item.Parent));
            m_Items.Clear();
            m_ShopItems.Clear();
        }

        private void Equip()
        {
            m_Player.Equip(EquipSelected());
            SaveItems();
            LoadItems();
        }

        private void Strip()
        {
            m_Player.Strip(StripSelected());
            SaveItems();
            LoadItems();
        }

        private List<string> EquipSelected()
        {
            List<Item> itemsToEquip = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Inventory && item.Selected).ToList();
            itemsToEquip.ForEach(item => item.BelongsTo = ItemBelongsTo.Equipped);

            return itemsToEquip.Select(item => item.Name).ToList();
        }

        private List<string> StripSelected()
        {
            List<Item> itemsToStrip = m_Items.Where(item => item.BelongsTo == ItemBelongsTo.Equipped && item.Selected).ToList();
            itemsToStrip.ForEach(item => item.BelongsTo = ItemBelongsTo.Inventory);

            return itemsToStrip.Select(item => item.Name).ToList();
        }

        private void DeselectAll()
        {
            m_Items.ForEach(item => item.Selected = false);
        }
        #endregion

        #region Subscriptions
        private void SubcribeToEvents()
        {
            if (m_ButtonEquip != null) m_ButtonEquip.onClick.AddListener(delegate { Equip(); });
            if (m_ButtonStrip != null) m_ButtonStrip.onClick.AddListener(delegate { Strip(); });
        }

        private void UnsubscribeFromEvents()
        {
            if (m_ButtonEquip != null) m_ButtonEquip.onClick.RemoveAllListeners();
            if (m_ButtonStrip != null) m_ButtonStrip.onClick.RemoveAllListeners();
        }
        #endregion
    }
}
