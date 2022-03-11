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
        public Vector2 mPos { get; set; }
        public Vector2 mSize { get; set; }
        public bool mShow = true;
        public Raylib_cs.Color mColor { get; set; }

        public Sprite() { }
        ~Sprite() { }
    }

    //Button
    enum ButtonType
    {
        DEFAULT,
        TOGGLE,
        TEXT,
        TOGGLE_TEXT
    };

    class Button : Sprite
    {
        private ButtonType mType;
        private bool mOn;
        public string text;
        Vector2 mTextOffSet;
        int mTextSize;
        Color mTextColor;

        public Button(Vector2 pos, Vector2 size, Raylib_cs.Color color, ButtonType type, string text = "", int textSize = 12, Vector2 textOffSet = new Vector2(), Color textColor =  new Color(), bool on = false, bool show = true)
        {
            mPos = pos;
            mSize = size;
            mColor = color;
            mType = type;
            this.text = text;
            mOn = on;
            mShow = show;
            mTextSize = textSize;
            mTextOffSet = textOffSet;
            mTextColor = textColor;
        }
        ~Button() { }

        public void Draw()
        {
            if (mShow)
            {
                System.Numerics.Vector3 tmp = ColorToHSV(mColor);
                tmp.Z -= 50;
                Raylib_cs.Color tmpColor = ColorFromHSV(tmp.X, tmp.Y, tmp.Z);
                DrawRectangle((int)mPos.X, (int)mPos.Y, (int)mSize.X, (int)mSize.Y, mOn == true ? tmpColor : mColor);
                if (mType == ButtonType.TEXT || mType == ButtonType.TOGGLE_TEXT)
                {
                    DrawText(text, (int)(mPos.X + mTextOffSet.X), (int)(mPos.Y + mTextOffSet.Y), mTextSize, mTextColor);
                }
            }
        }
        public void Update()
        {
            if (mShow)
            {
                switch (mType)
                {
                    case ButtonType.TOGGLE_TEXT:
                    case ButtonType.TOGGLE:
                        if (IsClicked())
                            mOn = !mOn;
                        break;
                    default:
                        if (IsClicked())
                            mOn = true;
                        else
                            mOn = false;
                        break;
                }
            }
        }
        public bool IsMouseOn()
        {
            Raylib_cs.Rectangle tmp = new Raylib_cs.Rectangle(mPos.X, mPos.Y, mSize.X, mSize.Y);
            if (CheckCollisionPointRec(GetMousePosition(), tmp))
                return true;
            else
                return false;
        }
        public bool IsClicked()
        {
            if (IsMouseOn() && IsMouseButtonPressed(Raylib_cs.MouseButton.MOUSE_LEFT_BUTTON))
                return true;
            else
                return false;
        }
    }

    // Slider
    class Slider : Sprite
    {
        private int mValue;
        private int mMax;
        private int mMin;
        private Vector2 sliderPos;
        private Vector2 sliderSize;
        public Slider(Vector2 pos, Vector2 size, Color color, int defaultvalue, int min, int max, bool show = true)
        {
            if (min > max)
            {
                int tmp = min;
                min = max;
                max = tmp;
            }
            mPos = pos;
            mSize = size;
            mColor = color;
            mValue = defaultvalue;
            mMin = min;
            mMax = max;
            mShow = show;
            sliderSize = new Vector2(mSize.Y - (mSize.Y * 0.15f));
            sliderPos = new Vector2((pos.X + size.Y * 0.07f) * defaultvalue / min, (pos.Y + size.Y * 0.07f));
        }
        ~Slider() { }

        public void Draw()
        {
            if (mShow)
            {
                System.Numerics.Vector3 tmp = ColorToHSV(mColor);
                tmp.X -= 20;
                Raylib_cs.Color tmpColor = ColorFromHSV(tmp.X, tmp.Y, tmp.Z);
                DrawRectangle((int)mPos.X, (int)mPos.Y, (int)mSize.X, (int)mSize.Y, tmpColor);
                DrawRectangleLinesEx(new Rectangle(mPos.X, mPos.Y, mSize.X, mSize.Y), 5, mColor);
                DrawRectangle((int)sliderPos.X, (int)sliderPos.Y, (int)sliderSize.X, (int)sliderSize.Y, mColor);
            }
        }

        public void Update()
        {
            if (mShow)
            {
                if (IsClicked())
                {
                    if (GetMouseX() >= (mPos.X + mPos.X * 0.06) && GetMouseX() <= (mPos.X + mSize.X - (mPos.X * 0.06)))
                    {
                        sliderPos.X = GetMouseX() - (sliderSize.X / 2);
                        mValue = (int)sliderPos.X * mMin / mMax;
                    }
                }
            }
        }

        public bool IsMouseOn()
        {
            Raylib_cs.Rectangle tmp = new Raylib_cs.Rectangle(mPos.X, mPos.Y, mSize.X, mSize.Y);
            if (CheckCollisionPointRec(GetMousePosition(), tmp))
                return true;
            else
                return false;
        }
        public bool IsClicked()
        {
            if (IsMouseOn() && IsMouseButtonDown(Raylib_cs.MouseButton.MOUSE_LEFT_BUTTON))
                return true;
            else
                return false;
        }

        public int GetValue() { return mValue; }

    }
    class InputBox : Sprite
    {
        private int mLetterCount = 0;
        private int mFrameCounter = 0;
        private int mMaxInputs;
        public char[] mText;
        private int mTextSize;
        private Vector2 mTextOffset;
        private Font mFont = GetFontDefault();
        Color mTextColor;
        bool clicked = false;
        public InputBox() { }
        public InputBox(Vector2 pos, Vector2 size, int maxInputs, Color color, Vector2 textOffset, int textSize, Color textColor, Font font)
        {
            mPos = pos;
            mSize = size;
            mMaxInputs = maxInputs;
            mColor = color;
            mTextOffset = textOffset;
            mTextSize = textSize;
            mTextColor = textColor;
            mText = new char[128];
            mFont = font;
        }
        ~InputBox() { }

        public void Draw()
        {
            string temp = new string(mText);
            DrawRectangle((int)mPos.X, (int)mPos.Y, (int)mSize.X, (int)mSize.Y, mColor);
            DrawRectangleLines((int)mPos.X, (int)mPos.Y, (int)mSize.X, (int)mSize.Y, clicked ? RED : BLACK);
            DrawTextEx(mFont, temp, new System.Numerics.Vector2(mPos.X + 5, mPos.Y + 5), mTextSize, 1, BLACK);
            DrawText($"{mLetterCount}/{mMaxInputs}", (int)(mPos.X + mSize.X - MeasureText($"{mLetterCount}/{mMaxInputs}", mTextSize)), (int)(mPos.Y + mSize.Y + 20), mTextSize, BLACK);
        }

        public void Update()
        {
            ++mFrameCounter;
            if (isClicked())
                clicked = true;
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && !isClicked())
                clicked = false;

            // Check if more characters have been pressed on the same frame
            float tmp = GetCharPressed();
            if (clicked)
            {
                // NOTE: Only allow keys in range [32..125]
                if (((tmp >= 32 && tmp <= 125) || (tmp == 130)) && (mLetterCount < mMaxInputs))
                {
                    mText[mLetterCount] = 'c';
                    mText[mLetterCount] = (char)tmp;
                    mText[mLetterCount + 1] = '\0'; // Add null terminator at the end of the string.
                    mLetterCount++;
                }
                if (IsKeyDown(KeyboardKey.KEY_BACKSPACE) && ((mFrameCounter % 6) == 0))
                {
                    mLetterCount--;
                    if (mLetterCount < 0)
                        mLetterCount = 0;
                    mText[mLetterCount] = '\0';
                }
                if (IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    clicked = false;
                }
            }
        }

        public bool isMouseOn()
        {
            Rectangle rec = new Rectangle((float)mPos.X, (float)mPos.Y, (float)mSize.X, (float)mSize.Y);
            if (CheckCollisionPointRec(GetMousePosition(), rec) && mShow)
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

        public bool isClicked()
        {
            if (isMouseOn() && IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                return true;
            else
                return false;
        }

        public bool isValid(string text)
        {
            int string_size2, good = 0;
            int string_size = GetText().Length;
            char[] tmp = GetText().ToCharArray();
            // Convert to char.
            string_size2 = text.Length;
            char[] tmp2 = text.ToCharArray();
            // Count how many characters are good.
            for (int i = 0; (string_size > string_size2 ? tmp[i] != '\0' : tmp2[i] != '\0'); i++)
            {
                tmp[i] = Char.ToLower(tmp[i]);
                tmp2[i] = Char.ToLower(tmp[i]);
                if (tmp[i] == tmp2[i] && (tmp[i] >= 'a' && tmp[i] <= 'z'))
                    good++;
            }
            if (good >= 75 * string_size2 / 100)
                return true;
            else
                return false;
        }

        public string GetText() { return mText.ToString(); }

        public void ClearInput()
        {
            mText = new char[128];
            mLetterCount = 0;
        }

        public void setClicked(bool click) { clicked = click; }
    }
}
