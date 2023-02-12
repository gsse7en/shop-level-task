using UnityEngine;
using CommandPattern;
using System;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {
        private Command buttonW, buttonS, buttonA, buttonD;
        private KeyCode keyCode;

        void Start()
        {
            buttonW = new MoveForward();
            buttonS = new MoveReverse();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
        }

        void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.inputString.Length == 0) return;
            if (Input.inputString.Length > 1)
            {
                Debug.LogError("split string");
                return;
            }

            keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), Input.inputString.ToUpper());

            switch (keyCode)
            {
                case KeyCode.D:
                    buttonD.Execute(transform);
                    break;
                case KeyCode.A:
                    buttonA.Execute(transform);
                    break;
                case KeyCode.W:
                    buttonW.Execute(transform);
                    break;
                case KeyCode.S:
                    buttonS.Execute(transform);
                    break;
                default:
                    break;
            }
        }
    }
}