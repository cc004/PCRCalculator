using System;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class TextEvent : Text
    {
        public event Action OnTextChanged;

        public override string text
        {
            get => base.text;
            set
            {
                base.text = value;
                OnTextChanged?.Invoke();
            }
        }
    }
}