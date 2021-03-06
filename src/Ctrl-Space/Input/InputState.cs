﻿namespace Ctrl_Space.Input
{
    class InputState
    {
        public float Up;
        public float Down;
        public float Left;
        public float Right;
        public float RotateCW;
        public float RotateCCW;
        public bool PrimaryWeapon;
        public bool SecondaryWeapon;
        public bool Exit;

        public void Reset()
        {
            Up = 0.0f;
            Down = 0.0f;
            Left = 0.0f;
            Right = 0.0f;
            RotateCW = 0.0f;
            RotateCCW = 0.0f;
            PrimaryWeapon = false;
            SecondaryWeapon = false;
            Exit = false;
        }

        public void Process(string method, float data)
        {
            switch (method)
            {
                case "Up":
                    Up += data;
                    break;
                case "Down":
                    Down += data;
                    break;
                case "Left":
                    Left += data;
                    break;
                case "Right":
                    Right += data;
                    break;
                case "RotateCW":
                    RotateCW += data;
                    break;
                case "RotateCCW":
                    RotateCCW += data;
                    break;
                case "PrimaryWeapon":
                    PrimaryWeapon |= data > 0.5f;
                    break;
                case "SecondaryWeapon":
                    SecondaryWeapon |= data > 0.5f;
                    break;
                case "Exit":
                    Exit |= data > 0.5f;
                    break;
            }
        }
    }
}
