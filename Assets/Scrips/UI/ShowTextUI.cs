using System.Collections;
using System.Collections.Generic;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.UI;
public class ShowTextUI : MonoBehaviour
{
    public Text text;
    public GameObject bossDetailEditdr;
    public int index;
    public void Set(string str)
    {
        text.text = str;
    }
    public void Close()
    {
        Destroy(gameObject);
    }
    public void OpenBossDetailEditdr()
    {
        Destroy(gameObject);
        GuildManager.Instance.GuildBossDataEditButton(index);
    }
}
