using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class MouseCursor : MonoBehaviour
    {
        [SerializeField]
        private Texture2D pointerCursos;
        [SerializeField]
        private Texture2D handCursos;
        private SpriteRenderer rend;
        private Vector3 cursorPos;

        #region Lyfecicle
        private void Start()
        {
            HideCursor();
            rend = GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Public
        public void HideCursor()
        {
            OnMouseOut();
            Cursor.visible = false;
        }

        public void ShowCursor()
        {
            Cursor.visible = false;
        }

        public void OnMouseOver()
        {
            Cursor.SetCursor(handCursos, new Vector2(14f, 16f), CursorMode.Auto);
        }

        public void OnMouseOut()
        {
            Cursor.SetCursor(pointerCursos, new Vector2(20f, 20f), CursorMode.Auto);
        }
        #endregion
    }
}
