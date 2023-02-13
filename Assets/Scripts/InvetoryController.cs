using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Inventory
{
    public struct Item
    {
        public GameObject Parent;
        public int Cost;
        public string Name;
        public Sprite Icon;
        public bool Selected;

        public Item(GameObject parent, int cost, string name, Sprite icon)
        {
            Parent = parent;
            Cost = cost;
            Name = name;
            Icon = icon;
            Selected = false;
        }
    }

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
        private RectTransform m_EquippedContent;
        [SerializeField]
        private RectTransform m_InventoryContent;
        [SerializeField]
        private GameObject m_ItemPrefab;
        private List<Item> m_EquippedItems = new List<Item>();
        private List<Item> m_InventoryItems = new List<Item>();


        #region Lyfecicle
        private void Awake()
        {

            //m_InventoryItems.Add(Sword);
            //m_InventoryItems.Add(Helmet);
        }

        private void OnDestroy()
        {
            Clean();
        }
        #endregion

        #region Public
        //public void HideCursor()
        //{
        //    OnMouseOut();
        //    Cursor.visible = false;
        //}

        //public void ShowCursor()
        //{
        //    Cursor.visible = false;
        //}

        //public void OnMouseOver()
        //{
        //    Cursor.SetCursor(handCursos, new Vector2(14f, 16f), CursorMode.Auto);
        //}

        //public void OnMouseOut()
        //{
        //    Cursor.SetCursor(pointerCursos, new Vector2(20f, 20f), CursorMode.Auto);
        //}
        #endregion

        #region Private
        private void Clean()
        {
            foreach (Item item in m_EquippedItems)
            {
                Destroy(item.Parent);
            }

            foreach (Item item in m_InventoryItems)
            {
                Destroy(item.Parent);
            }

            m_EquippedItems.Clear();
            m_InventoryItems.Clear();
        }

        private void Equip(Item item)
        {
            m_EquippedItems.Add(item);
        }

        private void Equip(List<Item> items)
        {
            m_EquippedItems.AddRange(items);
        }
        #endregion

        #region Delegates
        private void OnEquipButtonClicked()
        {

        }

        private void OnCloseButtonClicked()
        {

        }
        #endregion
    }
}
