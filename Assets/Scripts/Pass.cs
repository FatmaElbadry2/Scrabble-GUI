using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Pass : MonoBehaviour
{
    public GameObject passPanel;
    public Text passText;
    // Start is called before the first frame update
    public void PStart()
    {
        passPanel.SetActive(false);
        StartCoroutine(PassTurn());
    }

    // Update is called once per frame
    public IEnumerator PassTurn()
    {
        GameManager.button = 2;
        passPanel.SetActive(true);
        passText.text = "Pass";

        yield return new WaitForSecondsRealtime(1);

          passPanel.SetActive(false);
        //   string message=GUIInterface.CheckButton();
        //         if (message!="\0"){
        //           Client.Send(message);
        //         }
    
        

    }
}
