using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        public Button m_button;
        [SerializeField]
        public GameObject m_ShopScreen;

        #region Lifecycle
        void Start()
        {
            m_button?.onClick.AddListener(delegate
            {
                CloseShop();
            });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B)) OpenShop();
        }
        #endregion

        #region Public
        public void OpenShop()
        {
            if (m_ShopScreen == null)
                return;
            m_ShopScreen.SetActive(true);
        }

        public void CloseShop()
        {
            m_ShopScreen?.SetActive(false);
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
