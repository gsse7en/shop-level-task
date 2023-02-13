using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Item
{
    [System.Serializable]
    public struct ItemInfo
    {
        public GameObject Parent;
        public GameObject SelectHighlight;
        public Button ItemButton;
        public Image Icon;
        public bool Selected;
    }

    public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private ItemInfo m_ItemInfo;
        [SerializeField]
        private MouseCursor m_CursorHandler;

        public Sprite Icon
        {
            set { m_ItemInfo.Icon.sprite = value; }
        }

        public bool Selected
        {
            get { return m_ItemInfo.Selected; }
            set {
                m_ItemInfo.Selected = value;
                m_ItemInfo.SelectHighlight.SetActive(m_ItemInfo.Selected);
            }
        }

        #region Lifecycle
        private void Awake()
        {
            if (m_ItemInfo.ItemButton != null)
            {
                m_ItemInfo.ItemButton.onClick.AddListener(delegate { Selected = !Selected; });
            }
        }

        private void HandCursor(PointerEventData obj)
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            if (m_ItemInfo.ItemButton != null)
            {
                m_ItemInfo.ItemButton.onClick.RemoveAllListeners();
            }
        }
        #endregion

        #region Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_CursorHandler.HandCursor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_CursorHandler.PointerCursor();
        }
        #endregion
    }
}
