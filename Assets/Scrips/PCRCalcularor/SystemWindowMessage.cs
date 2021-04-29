using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SystemWindowMessage : MonoBehaviour
{
    public enum WindowType { normal=0,input=1,async=2}
    public WindowType windowType;
    public Text messageText;
    public InputField inputField;
    public delegate void configDelegate();
    public configDelegate config;
    public configDelegate cancel;
    public Action<string> finishAction;
    public Action cancelAction;
    public void SetWindowMassage(string word,configDelegate config,configDelegate cancel = null)
    {
        messageText.text = word;
        this.config = config;
        this.cancel = cancel;
    }
    public void SetWindowInputMassage(string word,Action<string> finish,Action cancel)
    {
        messageText.text = word;
        finishAction = finish;
        cancelAction = cancel;
    }
    public void ConfigButton()
    {
        switch (windowType)
        {
            case WindowType.normal:
                config?.Invoke();
                break;
            case WindowType.input:
                finishAction?.Invoke(inputField.text);
                break;
            case WindowType.async:
                config?.Invoke();
                return;
        }
        Destroy(gameObject);
    }
    public void CancalButton()
    {
        switch (windowType)
        {
            case WindowType.normal:
                cancel?.Invoke();
                break;
            case WindowType.input:
                cancelAction?.Invoke();
                break;
            case WindowType.async:
                return;
        }
        Destroy(gameObject);
    }
    public void Close()
    {
        Destroy(gameObject);
    }

}
