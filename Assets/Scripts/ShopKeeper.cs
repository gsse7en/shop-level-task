using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Characters.Shopkeeper
{
    public class ShopKeeper : MonoBehaviour
    {
        [SerializeField]
        private GameObject buyButton;
        [SerializeField]
        private GameObject activityBalloon;
        [SerializeField]
        private UIController uIController;

        #region Events
        private void OnCollisionEnter2D(Collision2D other)
        {
            ShowBuyButton();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            HideBuyButton();
            if (uIController.CurrentScreen == UIScreens.Shop)
            {
                uIController.CloseScreens();
            }
        }
        #endregion

        #region Private
        private void ShowBuyButton()
        {
            buyButton.SetActive(true);
            activityBalloon.SetActive(false);
            uIController.ShowBuyButton = true;
        }

        private void HideBuyButton()
        {
            buyButton.SetActive(false);
            activityBalloon.SetActive(true);
            uIController.ShowBuyButton = false;
        }
        #endregion
    }
}
