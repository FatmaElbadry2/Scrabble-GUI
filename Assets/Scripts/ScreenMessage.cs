using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenMessage : MonoBehaviour
{
    public GameObject messagePanel;
    public Text message;


    public void Display(string message)
    {
        this.message.text = message;
     //   messagePanel.SetActive(false);
    }
}
