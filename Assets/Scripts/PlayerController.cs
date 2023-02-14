using System.Collections.Generic;
using UnityEngine;
using UI;
using System.Linq;
using UI.ShopItem;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private ItemsData m_ItemsData;
        [SerializeField]
        private float m_Speed = 5f;
        [SerializeField]
        private GameObject m_Sword;
        [SerializeField]
        private GameObject m_Helmet;
        [SerializeField]
        private GameObject m_Armor;
        [SerializeField]
        private UIStateData m_UIStateData;
        [SerializeField]
        private Animator m_PlayerAnimator;
        [SerializeField]
        private Vector2 m_Movement;
        [SerializeField]
        private Rigidbody2D m_Rigidbody;
        private Dictionary<string, GameObject> m_ItemsDictionary;
        private bool m_CanPlayerMove = true;
        private bool m_PlayerRecentlyMoved = false;
        private bool m_PlayerFacingRight = true;

        #region Lifecycle
        void Awake()
        {
            m_ItemsDictionary = new Dictionary<string, GameObject>()
            {
                { "Sword", m_Sword },
                { "Helmet", m_Helmet },
                { "Armor", m_Armor }
            };

            m_ItemsData.ItemList.ForEach(item =>
            {
                if (item.BelongsTo == ItemBelongsTo.Equipped) m_ItemsDictionary[item.Name].SetActive(true);
            });

            m_UIStateData.UIScreenChanged += OnUIScreenChanged;
        }

        void FixedUpdate()
        {
            if (m_CanPlayerMove) HandleInput();
            else m_PlayerAnimator.SetBool("Movement", false);
        }
        #endregion

        #region Public
        public void Equip(List<string> items)
        {
            items.ForEach(name => m_ItemsDictionary[name].SetActive(true));
        }

        public void Strip(List<string> items)
        {
            items.ForEach(name => m_ItemsDictionary[name].SetActive(false));
        }

        public void Move(Vector2 value)
        {
            m_Movement = value;
        }
        #endregion

        #region Private
        private void HandleInput()
        {
            bool isMoving = Mathf.Abs(m_Movement.x + m_Movement.y) > 0;
            bool changedDirection = m_Movement.x > 0 && !m_PlayerFacingRight || m_Movement.x < 0 && m_PlayerFacingRight;
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Speed * Time.fixedDeltaTime);
            m_PlayerAnimator.SetBool("Movement", isMoving);
            m_PlayerRecentlyMoved = isMoving;

            if (changedDirection) FlipDirection();
            if ((isMoving == m_PlayerRecentlyMoved)) return;
        }

        private void FlipDirection()
        {
            m_PlayerFacingRight = !m_PlayerFacingRight;
            transform.Rotate(0, 180, 0);
        }
        #endregion

        #region Delegates
        private void OnUIScreenChanged(UIScreens currentScreen)
        {
            m_CanPlayerMove = currentScreen == UIScreens.None;
        }
        #endregion
    }
}