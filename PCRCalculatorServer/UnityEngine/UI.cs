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
        MiddleCenter, MiddleLeft
    }
    public class Text : MonoBehaviour { internal TextAnchor alignment; internal Color color; public virtual string text { get; set; } }
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
        internal InvokeExtended<Action<string>> onValueChanged;
        internal InvokeExtended<Action<string>> onEndEdit;
        internal bool interactable;
    }
    public class Image : MonoBehaviour { public Sprite sprite;
        internal Color color;
        internal float fillAmount;
    }
    public class Button : MonoBehaviour
    {
        internal bool interactable;
        internal InvokeExtended<Action> onClick;
    }
    public class Dropdown : MonoBehaviour
    {
        internal int value;
        internal List<Dropdown.OptionData> options;
        internal InvokeExtended<Action> onValueChanged;

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
    public class Toggle : MonoBehaviour
    {
        internal bool interactable;
        internal bool isOn;
        internal InvokeExtended<Action<bool>> onValueChanged;
    }
    public class CanvasScaler { }
    public class GridLayoutGroup : MonoBehaviour { }

}
