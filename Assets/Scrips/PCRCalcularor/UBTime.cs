using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{


    public class UBTime : MonoBehaviour
    {
        public GameObject InputPrefab;
        public RectTransform parent;
        public Vector2 basePos;
        public Vector2 addPos;
        public Button addButton;
        public Button minButton;

        private bool isEditing;

        public List<InputField> inputFields;
        public void StartEdit()
        {
            isEditing = true;
            addButton.gameObject.SetActive(true);
            minButton.gameObject.SetActive(true);
            foreach(InputField input in inputFields)
            {
                input.interactable = true;
            }
        }
        public void FinishEdit()
        {
            isEditing = false;
            addButton.gameObject.SetActive(false);
            minButton.gameObject.SetActive(false);
            foreach (InputField input in inputFields)
            {
                input.interactable = false;
            }
        }
        public void AddButton()
        {
            GameObject a = Instantiate(InputPrefab);
            a.GetComponent<InputField>().interactable = isEditing;
            inputFields.Add(a.GetComponent<InputField>());
            a.transform.SetParent(parent, false);
            a.transform.localPosition = basePos + (inputFields.Count -1)* addPos;
            parent.sizeDelta = new Vector2(parent.sizeDelta.x, Mathf.Abs(a.transform.localPosition.y) + Mathf.Abs( addPos.y ));
        }
        public void MinusButton()
        {
            if (inputFields.Count > 0)
            {
                Destroy(inputFields[inputFields.Count - 1].gameObject);
                inputFields.RemoveAt(inputFields.Count - 1);
                parent.sizeDelta = new Vector2(parent.sizeDelta.x,Mathf.Abs(addPos.y*inputFields.Count) );
            }
        }
        public List<float> GetUBTimes()
        {
            List<float> list = new List<float>();
            foreach (InputField input in inputFields)
            {
                try
                {
                    list.Add(float.Parse(input.text));
                }
                catch
                {
                    MainManager.Instance.WindowMessage("输入的UB时间不对！");
                }
            }
            return list;
        }
        public void SetUBTimes(List<float> times)
        {
            int err = inputFields.Count - times.Count;
            if (err > 0)
            {
                for(int i = err; i > 0; i--)
                {
                    MinusButton();
                }
            }
            else if(err<0)
            {
                for(int i = 0; i < -err; i++)
                {
                    AddButton();
                }
            }
            for(int i = 0; i < times.Count; i++)
            {
                inputFields[i].text = times[i] + "";
            }
        }
    }
}