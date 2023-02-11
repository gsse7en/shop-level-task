using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Button m_button;
        [SerializeField]
        private GameObject m_ShopScreen;

        #region Lifecycle
        void Start()
        {
            m_ShopScreen.SetActive(true);
        }

        void Update()
        {

        }
        #endregion

        #region Public
        public void CloseShop()
        {
            m_ShopScreen.SetActive(false);
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
    }
}
