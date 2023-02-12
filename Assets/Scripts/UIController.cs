using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        private List<Button> m_CloeButtons = new List<Button>();
        [SerializeField]
        private GameObject m_ShopScreen;
        [SerializeField]
        private GameObject m_InventoryScreen;
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
            foreach (Button button in m_CloeButtons)
            {
                button.onClick.AddListener(delegate { CloseScreens(); });
            }
        }

        private void OnDestroy()
        {
            foreach (Button button in m_CloeButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        void Update()
        {

        }
        #endregion

        #region Public
        public void OpenShop()
        {
            CurrentScreen = UIScreens.Shop;
        }

        public void CloseScreens()
        {
            CurrentScreen = UIScreens.None;
        }

        public void OpenInventory()
        {
            CurrentScreen = UIScreens.Inventory;
        }

        public void Buy()
        {
            Debug.Log("BuyButton Pressed");
        }

        public void Sell()
        {
            Debug.Log("SellButton Pressed");
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
                    break;
                case UIScreens.Inventory:
                    m_InventoryScreen.SetActive(true);
                    m_ShopScreen.SetActive(false);
                    break;
                case UIScreens.None:
                    m_InventoryScreen.SetActive(false);
                    m_ShopScreen.SetActive(false);
                    break;
            }
        }
        #endregion
    }
}
