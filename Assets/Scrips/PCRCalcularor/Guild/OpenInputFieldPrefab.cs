using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PCRCaculator.Guild
{
    public class OpenInputFieldPrefab : MonoBehaviour
    {
        public InputField nameInput;


        public Action<string> OnFinish;

        public void CancelButton()
        {
            Destroy(gameObject);
        }
        public void SaveButton()
        {
            OnFinish?.Invoke(nameInput.text);
            Destroy(gameObject);
        }
    }
}