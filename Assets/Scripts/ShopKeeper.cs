using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Characters.Shopkeeper
{
    public class ShopKeeper : MonoBehaviour
    {
        public event Action<bool> TradePossibilityChanged;
        [SerializeField]
        private GameObject buyButton;
        [SerializeField]
        private GameObject activityBalloon;

        #region Events
        private void OnCollisionEnter2D(Collision2D other)
        {
            ShowBuyButton();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            HideBuyButton();
        }
        #endregion

        #region Private
        private void ShowBuyButton()
        {
            buyButton.SetActive(true);
            activityBalloon.SetActive(false);
            TradePossibilityChanged?.Invoke(true);
        }

        private void HideBuyButton()
        {
            buyButton.SetActive(false);
            activityBalloon.SetActive(true);
            TradePossibilityChanged?.Invoke(false);
        }
        #endregion
    }
}
