using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public enum UIScreens
    {
        None,
        Shop,
        Inventory
    }

    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_ShopScreen;
        [SerializeField]
        private GameObject m_InventoryScreen;
        [SerializeField]
        private CoinsData m_CoinsData;
        [SerializeField]
        private TextMeshProUGUI m_HUDCoins;

        private UIScreens m_CurrentScreen = UIScreens.None;
        //TODO make event delegate listener
        private bool m_showBuyButton = false;
        public bool ShowBuyButton
        {
            get { return m_showBuyButton; }
            set { m_showBuyButton = value; }
        }

        public UIScreens CurrentScreen
        {
            get { return m_CurrentScreen; }
            set
            {
                m_CurrentScreen = value;
                UpdateScreens();
            }
        }

        #region Lifecycle
        void Awake()
        {
            m_CoinsData.CoinsChanged += OnCoinsChange;
            m_HUDCoins.text = m_CoinsData.Count.ToString();
        }

        private void OnDestroy()
        {
            m_CoinsData.CoinsChanged -= OnCoinsChange;
        }
        #endregion

        #region Public
        public void OpenShop()
        {
            if (m_showBuyButton) CurrentScreen = UIScreens.Shop;
        }

        public void CloseScreens()
        {
            CurrentScreen = UIScreens.None;
        }

        public void OpenInventory()
        {
            CurrentScreen = UIScreens.Inventory;
        }
        #endregion

        #region Private
        private void UpdateScreens()
        {
            switch(m_CurrentScreen)
            {
                case UIScreens.Shop:
                    m_InventoryScreen.SetActive(false);
                    m_ShopScreen.SetActive(true);
                    Cursor.visible = true;
                    break;
                case UIScreens.Inventory:
                    m_InventoryScreen.SetActive(true);
                    m_ShopScreen.SetActive(false);
                    Cursor.visible = true;
                    break;
                case UIScreens.None:
                    m_InventoryScreen.SetActive(false);
                    m_ShopScreen.SetActive(false);
                    Cursor.visible = false;
                    break;
            }
        }
        #endregion

        #region Delegates
        private void OnCoinsChange(int coins)
        {
            m_HUDCoins.text = coins.ToString();
        }
        #endregion
    }
}
