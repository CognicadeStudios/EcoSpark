using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public int index;
    public void SendHighlightRequest()
    {
        QuestSystem.instance.questUI.HighlightQuest(index);
    }
}