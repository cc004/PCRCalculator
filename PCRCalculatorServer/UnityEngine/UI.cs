using PCRCaculator.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.UI
{
    public class ToggleGroup : MonoBehaviour { }
    public enum TextAnchor
    {
        MiddleCenter, MiddleLeft, UpperLeft
    }
    public class Text : Graphic
    {
        internal TextAnchor alignment;
        public bool supportRichText;
        public float preferredHeight => throw new NotImplementedException();
        public virtual string text { get; set; }
    }
    public class Slider : MonoBehaviour
    {
        public float maxValue, minValue, value;
        public string text;
        internal bool interactable;
        internal InvokeExtended<Action<float>> onValueChanged;

        internal void SetValueWithoutNotify(float value)
        {
            throw new NotImplementedException();
        }
    }
    public class Renderer : MonoBehaviour { }
    public class InvokeExtended<T>
    {
        internal void AddListener(T onInputField1ValueChanged)
        {
            throw new NotImplementedException();
        }

        internal void RemoveAllListeners()
        {
            throw new NotImplementedException();
        }

        internal void RemoveListener(T onInputField1ValueChanged)
        {
            throw new NotImplementedException();
        }
    }
    public class InputField : MonoBehaviour { public string text;
        public enum ContentType { Standard, Autocorrected, IntegerNumber, DecimalNumber, Alphanumeric, Name, EmailAddress, Password, Pin, Custom }
        public ContentType contentType;
        internal InvokeExtended<Action<string>> onValueChanged;
        internal InvokeExtended<Action<string>> onEndEdit;
        internal bool interactable;
        public void SetTextWithoutNotify(string text) {}
    }
    public class Image : Graphic { public Sprite sprite;
        public bool preserveAspect;
        internal float fillAmount;
    }
    public class Button : MonoBehaviour
    {
        internal bool interactable;
        internal InvokeExtended<Action> onClick;
    }
    public class Dropdown : MonoBehaviour
    {
        public Text itemText;
        internal int value;
        internal List<Dropdown.OptionData> options;
        internal InvokeExtended<Action<int>> onValueChanged;

        internal void AddOptions(object optionDatas)
        {
            throw new NotImplementedException();
        }

        internal void ClearOptions()
        {
            throw new NotImplementedException();
        }

        public class OptionData { public string text;
            private string option;

            public Sprite image;
            public OptionData() { }
            public OptionData(string option)
            {
                this.option = option;
            }
        }
    }
    public class ScrollRect : MonoBehaviour
    {
        internal float verticalNormalizedPosition;
        internal Vector2 normalizedPosition;
        internal float horizontalNormalizedPosition;
        internal RectTransform content;
    }
    public struct ColorBlock
    {
        public Color normalColor, highlightedColor, pressedColor, selectedColor, disabledColor;
        public float colorMultiplier, fadeDuration;
    }
    public class Graphic : MonoBehaviour
    {
        public Color color;
        public bool raycastTarget;
        public RectTransform rectTransform => (RectTransform)transform;
    }
    public class Selectable : MonoBehaviour
    {
        public enum Transition { None, ColorTint, SpriteSwap, Animation }
        public Graphic targetGraphic;
        public ColorBlock colors;
        public Transition transition;
    }
    public class Toggle : Selectable
    {
        public ToggleGroup group;
        public void SetIsOnWithoutNotify(bool value) { isOn = value; }
        internal bool interactable;
        internal bool isOn;
        internal InvokeExtended<Action<bool>> onValueChanged;
    }
    public class CanvasScaler { }
    public class GridLayoutGroup : MonoBehaviour { }

}
