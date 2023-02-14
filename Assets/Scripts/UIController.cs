using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Characters.Shopkeeper;

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
        [SerializeField]
        private ShopKeeper m_ShopKeeper;
        [SerializeField]
        private UIStateData m_UIStateData;

        private bool m_canTrade = false;
        private UIScreens m_CurrentScreen = UIScreens.None;

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
            m_HUDCoins.text = m_CoinsData.Count.ToString();
            m_CoinsData.CoinsChanged += OnCoinsChange;
            m_ShopKeeper.TradePossibilityChanged += OnTradePossibilityChanged;
        }

        private void OnDestroy()
        {
            m_CoinsData.CoinsChanged -= OnCoinsChange;
        }
        #endregion

        #region Public
        public void OpenShop()
        {
            if (m_canTrade) CurrentScreen = UIScreens.Shop;
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
            m_UIStateData.ScreenState = m_CurrentScreen;
        }
        #endregion

        #region Delegates
        private void OnCoinsChange(int coins)
        {
            m_HUDCoins.text = coins.ToString();
        }

        private void OnTradePossibilityChanged(bool canTrade)
        {
            m_canTrade = canTrade;
        }
        #endregion
    }
}
