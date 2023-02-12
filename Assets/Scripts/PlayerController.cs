using UI;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private UIController uIController;
        [ReadOnlyAttribute, SerializeField]
        private Vector2 movement;
        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            HandleInput();
        }

        private void OnMovement(InputValue value)
        {
            movement = value.Get<Vector2>();
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

        private void HandleInput()
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}