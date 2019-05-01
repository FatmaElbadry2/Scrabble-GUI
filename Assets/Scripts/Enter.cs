using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Enter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playPanel;
    public Text playText;
    // Start is called before the first frame update
    public void EStart()
    {
        playPanel.SetActive(false);
        StartCoroutine(SendWordCheck());
    }


    // Update is called once per frame
    public IEnumerator SendWordCheck()
    {
        GameManager.button = 1;
        playPanel.SetActive(true);
        playText.text = "Play";

        yield return new WaitForSecondsRealtime(1);

        playPanel.SetActive(false);
        
        Rack.getpos();
         
    }
}
