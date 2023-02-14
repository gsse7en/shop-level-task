using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private GameObject m_Sword;
        [SerializeField]
        private GameObject m_Helmet;
        [ReadOnlyAttribute, SerializeField]
        private Vector2 movement;
        private Rigidbody2D rb;
        private Dictionary<string, GameObject> m_ItemsDictionary;

        #region Lifecycle
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            m_ItemsDictionary = new Dictionary<string, GameObject>()
            {
                { "Sword", m_Sword },
                { "Helmet", m_Helmet }
            };
        }

        void FixedUpdate()
        {
            HandleInput();
        }
        #endregion

        #region Public
        public void Equip(List<string> items)
        {
            foreach (string name in items) m_ItemsDictionary[name].SetActive(true);
        }

        public void Strip(List<string> items)
        {
            foreach (string name in items) m_ItemsDictionary[name].SetActive(false);
        }

        public void Move(Vector2 value)
        {
            movement = value;
        }
        #endregion

        #region Private
        private void HandleInput()
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        #endregion
    }
}