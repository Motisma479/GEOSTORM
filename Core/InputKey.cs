using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Geostorm.Core
{
    enum KeyType
    {
        KeyBoard = 0,
        MouseButton,
        MouseAxis,
        GamepadButton,
        GamepadAxis,
    }
    class InputKey
    {
        private KeyType type;
        private int id;
        public int Id { get { return id; } set { id = value; } }
        public KeyType Type { get { return type; } set { type = value; } }

        public InputKey()
        {
            type = 0;
            id = 0;
        }

        public InputKey(int typeIn, int idIn)
        {
            type = (KeyType)typeIn;
            id = idIn;
        }

        public bool ReadButtonKey()
        {
            switch (type)
                {
                case KeyType.MouseButton:
                    return IsMouseButtonDown((MouseButton)id);
                case KeyType.MouseAxis:
                    return GetMouseWheelMove() is < 0.2f or > 0.2f;
                case KeyType.GamepadButton:
                    return IsGamepadButtonDown(0,(GamepadButton)id);
                case KeyType.GamepadAxis:
                    if (id < 0)
                    {
                        return GetGamepadAxisMovement(0, (GamepadAxis)(-id-2)) < 0.5f; // Negative value axis
                    }
                    else
                    {
                        return GetGamepadAxisMovement(0, (GamepadAxis)(id-2)) > 0.5f; // Positive value axis
                    }
                default:
                    return IsKeyDown((KeyboardKey)id);
            }
        }

        public float ReadAxisKey()
        {
            switch (type)
            {
                case KeyType.MouseButton:
                    return MathHelper.BoolToInt(IsMouseButtonDown((MouseButton)id));
                case KeyType.MouseAxis:
                    return MathF.Abs(GetMouseWheelMove()*2);
                case KeyType.GamepadButton:
                    return MathHelper.BoolToInt(IsGamepadButtonDown(0, (GamepadButton)id));
                case KeyType.GamepadAxis:
                    if (id < 0)
                    {
                        return MathHelper.CutFloat(-GetGamepadAxisMovement(0, (GamepadAxis)(-id-2)),0.0f,1.0f); // Negative value axis
                    }
                    else
                    {
                        return MathHelper.CutFloat(GetGamepadAxisMovement(0, (GamepadAxis)(id-2)),0.0f,1.0f); // Positive value axis
                    }
                default:
                    return MathHelper.BoolToInt(IsKeyDown((KeyboardKey)id));
            }
        }

        public bool AutoBindKey()
        {
            int key = GetKeyPressed();
            if (key > 0)
            {
                type = KeyType.KeyBoard;
                id = key;
                return true;
            }
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                type = KeyType.MouseButton;
                id = 0;
                return true;
            }
            if (IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                type = KeyType.MouseButton;
                id = 1;
                return true;
            }
            if (IsMouseButtonPressed(MouseButton.MOUSE_MIDDLE_BUTTON))
            {
                type = KeyType.MouseButton;
                id = 2;
                return true;
            }
            if (GetMouseWheelMove() is > 0.1f or < -0.1f)
            {
                type = KeyType.MouseAxis;
                return true;
            }
            key = GetGamepadButtonPressed();
            if (key > 0)
            {
                type = KeyType.GamepadButton;
                id = key;
                return true;
            }
            for (int i = 0; i < GetGamepadAxisCount(0); i++)
            {
                float value = GetGamepadAxisMovement(0, (GamepadAxis)i);
                if (value is > 0.1f or < -0.1f && value > -0.9f)
                {
                    i = i + 2;
                    type = KeyType.GamepadAxis;
                    id = value < 0 ? -i : i;
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return (int)type + " " + id;
        }

        public string GetDesc()
        {
            switch (type)
            {
                case KeyType.MouseButton:
                    return ((MouseButton)id).ToString().Replace("MOUSE_","");
                case KeyType.MouseAxis:
                    return "MOUSE_WHEEL";
                case KeyType.GamepadButton:
                    return ((GamepadButton)id).ToString().Replace("GAMEPAD_BUTTON_","");
                case KeyType.GamepadAxis:
                    if (id < 0)
                        return "NEGATIVE_" + ((GamepadAxis)(-id - 1)).ToString().Replace("GAMEPAD_AXIS_", "");
                    else
                        return ((GamepadAxis)(id - 1)).ToString().Replace("GAMEPAD_AXIS_", "");
                default:
                    return ((KeyboardKey)id).ToString().Replace("KEY_", "");
            }
        }
    }
}
