using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenMessage : MonoBehaviour
{
    public GameObject messagePanel;
    public Text message;

    // Start is called before the first frame update
    /* public void MStart(string message)
    {
        
        StartCoroutine(MessageUpdate(message));
    } */
    // Update is called once per frame
    public IEnumerator MessageUpdate(string message)
    {
        this.message.text = message;
        yield return new WaitForSecondsRealtime(1);
        messagePanel.SetActive(false);
    }
}
