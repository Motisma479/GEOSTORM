using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.ConfigFlags;
using System.Numerics;

namespace Geostorm.Renderer
{
    class Sprite
    {
        Texture2D mTexture;
        Vector2 mPos;
        Vector2 mSize;
        bool mShow;
        Color mColor;
        public Texture2D Texture { get { return mTexture; } set { mTexture = value; } }
        public Vector2 Pos { get { return mPos; } set { mPos = value; } }
        public Vector2 Size { get { return mSize; } set { mSize = value; } }
        public bool Show { get { return mShow; } set { mShow = value; } }
        public Raylib_cs.Color Color { get { return mColor; } set { mColor = value; } }

        public Sprite() { }
        public Sprite(Vector2 pos, Vector2 size, Color color, bool shown = true)
        {
            Show = shown;
            Pos = pos;
            Size = size;
            Color = color;
        }
        ~Sprite() { }
        public void Draw()
        {
            if (Texture.id != 0)
                DrawTexturePro(Texture, new Rectangle(0, 0, Texture.width, Texture.height), new Rectangle(Pos.X, Pos.Y, Size.X, Size.Y), new Vector2(), 0, Color);
            else
                DrawRectanglePro(new Rectangle(Pos.X, Pos.Y, Size.X, Size.Y), new Vector2(), 0, Color);
        }
    }

    //Button
    enum ButtonType
    {
        DEFAULT,
        TOGGLE,
        TEXT,
        TOGGLE_TEXT,
        MANUAL
    };

    class Button : Sprite
    {
        ButtonType mType;
        string mText;
        bool isOn;
        Vector2 mTextOffSet;
        Color mTextColor;
        int mTextSize;
        public string Text { get { return mText; } set { mText = value; } }

        public Button(Vector2 pos, Vector2 size, ButtonType type, Color color = new Color(), Texture2D texture = new Texture2D(), bool show = true)
        {
            Pos = pos;
            Size = size;
            Texture = texture;
            mType = type;
            Show = show;
            Color = color;
        }
        public void SetText(string text, Vector2 offset, int size, Color color)
        {
            mText = text;
            mTextOffSet = offset;
            mTextSize = size;
            mTextColor = color;
        }
        public new void Draw()
        {
            if (Texture.id != 0)
            {
                DrawTexturePro(Texture, new Rectangle(0, 0, Texture.width, Texture.height), new Rectangle(Pos.X, Pos.Y, Size.X, Size.Y), new Vector2(), 0, isOn ? DARKGRAY : IsMouseOn() ? GRAY : WHITE);
            }
            else
            {
                Color tmp = Color;
                if (tmp.r - 20 >= 0) tmp.r -= 20;
                if (tmp.g - 20 >= 0) tmp.g -= 20;
                if (tmp.b - 20 >= 0) tmp.b -= 20;
                Color tmp2 = tmp;
                if (tmp2.r - 20 >= 0) tmp2.r -= 20;
                if (tmp2.g - 20 >= 0) tmp2.g -= 20;
                if (tmp2.b - 20 >= 0) tmp2.b -= 20;
                DrawRectanglePro(new Rectangle(Pos.X, Pos.Y, Size.X, Size.Y), new Vector2(), 0, isOn ? tmp2 : IsMouseOn() ? tmp : Color);
            }
            if (mType == ButtonType.TEXT || mType == ButtonType.TOGGLE_TEXT)
                DrawText(mText, (int)(Pos.X + mTextOffSet.X), (int)(Pos.Y + mTextOffSet.Y), mTextSize, mTextColor);
        }
        public void Update()
        {
            switch (mType)
            {
                case ButtonType.TOGGLE:
                case ButtonType.TOGGLE_TEXT:
                    {
                        if (IsClicked())
                            isOn = !isOn;
                    }
                    break;
                case ButtonType.TEXT:
                case ButtonType.DEFAULT:
                    {
                        isOn = false;
                        if (IsClicked())
                            isOn = true;
                    }
                    break;
                default:
                    break;
            }
        }
        public bool IsClicked()
        {
            if (IsMouseOn() && IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
                return true;
            else
                return false;
        }
        public bool IsMouseOn()
        {
            Rectangle rec = new Rectangle((float)Pos.X, (float)Pos.Y, (float)Size.X, (float)Size.Y);
            if (CheckCollisionPointRec(GetMousePosition(), rec))
                return true;
            else
                return false;
        }
        public void SetState(bool state) { isOn = state; }
        public bool IsToggle() { return isOn; }
    }

    class InputBox : Sprite
    {
        int mLetterCount = 0;
        int mFrameCounter = 0;
        int mMaxInputs;
        string mText;
        int mTextSize;
        Vector2 mTextOffSet;
        Font mFont = GetFontDefault();
        Color mTextColor;
        bool clicked = false;

        public InputBox() { }
        public InputBox(Vector2 pos, Vector2 size, int maxInputs, Color color, Vector2 textOffSet, int textSize, Color textColor = new Color(), Font font = new Font())
        {
            Pos = pos;
            Size = size;
            mMaxInputs = maxInputs;
            Color = color;
            mTextOffSet = textOffSet;
            mTextSize = textSize;
            mTextColor = textColor;
            mText = "";
            mFont = GetFontDefault();
        }
        ~InputBox() { }
        public void Update()
        {
            ++mFrameCounter;
            if (IsClicked())
                clicked = true;
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && !IsClicked())
                clicked = false;
            // Check if more characters have been pressed on the same frame
            float tmp = GetCharPressed();
            if (clicked)
            {
                // NOTE: Only allow keys in range [32..125]
                if (((tmp >= 32 && tmp <= 125) || (tmp == 130)) && (mLetterCount < mMaxInputs))
                {
                    mText = new string(mText + (char)tmp);
                    mLetterCount++;
                }
                if (IsKeyDown(KeyboardKey.KEY_BACKSPACE) && ((mFrameCounter % 6) == 0))
                {
                    mLetterCount--;
                    if (mLetterCount < 0)
                        mLetterCount = 0;
                    if (mText.Length != 0)
                        mText = new string(mText.Remove(mText.Length - 1));
                }
                if (IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    clicked = false;
                }
            }
        }
        public new void Draw()
        {
            DrawRectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y, Color);
            DrawRectangleLines((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y, clicked ? RED : BLACK);
            DrawTextEx(mFont, mText, new Vector2((float)Pos.X + 5, (float)Pos.Y + 5), mTextSize, 1, BLACK);
            DrawText($"{mLetterCount }/{mMaxInputs}", (int)(Pos.X + Size.X - MeasureText($"{mLetterCount}/{mMaxInputs}", mTextSize)), (int)(Pos.Y + Size.Y + 20), mTextSize, BLACK);
            if (IsMouseOn())
            {
                if (((mFrameCounter / 20) % 2) == 0)
                    DrawText("|", (int)Pos.X + 8 + MeasureText(mText, mTextSize - 5), (int)Pos.Y + 12, mTextSize, MAROON);
            }
        }
        public bool IsMouseOn()
        {
            Rectangle rec = new Rectangle((float)Pos.X, (float)Pos.Y, (float)Size.X, (float)Size.Y);
            if (CheckCollisionPointRec(GetMousePosition(), rec) && Show)
            {
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_IBEAM);
                return true;
            }
            else
            {
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_DEFAULT);
                return false;
            }
        }
        public bool IsClicked()
        {
            if (IsMouseOn() && IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                return true;
            else
                return false;
        }
        public string GetText() { return mText; }
        public void ClearInput()
        {
            mText = new string("");
            mLetterCount = 0;
        }
        public void SetClicked(bool click) { clicked = click; }
    }
}
