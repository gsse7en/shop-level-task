using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class MouseCursor : MonoBehaviour
    {
        [SerializeField]
        private Texture2D pointerCursor;
        [SerializeField]
        private Texture2D handCursor;
        private SpriteRenderer rend;
        private Vector3 cursorPos;

        #region Lyfecicle
        private void Start()
        {
            ShowCursor(false);
            rend = GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Public
        public void ShowCursor(bool show)
        {
            if (!show) PointerCursor();
            Cursor.visible = show;
        }

        public void HandCursor()
        {
            Cursor.SetCursor(handCursor, new Vector2(14f, 16f), CursorMode.Auto);
        }

        public void PointerCursor()
        {
            Cursor.SetCursor(pointerCursor, new Vector2(20f, 20f), CursorMode.Auto);
        }
        #endregion
    }
}
