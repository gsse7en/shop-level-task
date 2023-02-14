using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Player;
using UI;

namespace Game.Controls
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private UIController uIController;
        [SerializeField]
        private PlayerController PlayerController;
        
        private void OnMovement(InputValue value)
        {
            PlayerController.Move(value.Get<Vector2>());
        }

        private void OnOpenShop()
        {
            uIController.OpenShop();
        }

        private void OnClosePopups()
        {

            uIController.CloseScreens();
        }

        private void OnOpenInventory()
        {
            uIController.OpenInventory();
        }
    }

}
