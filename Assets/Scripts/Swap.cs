using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Swap : MonoBehaviour
{
    public GameObject swapPanel;
   static public int push=0;
    public Text swapText;
    // Start is called before the first frame update
    public void SStart()
    {
        push++;
        swapPanel.SetActive(false);
        StartCoroutine(SwapRack());
    }

    // Update is called once per frame
    public IEnumerator SwapRack()
    {
        if (push==1)
        {
            GameManager.button = 3;
            swapPanel.SetActive(true);
            swapText.text = "Swap";
            
            yield return new WaitForSecondsRealtime(1);
            swapPanel.SetActive(false);

        }
        else if (push==2)
        {
            swapPanel.SetActive(false);
            push=push-2;
            Rack.FSwap();
            GameManager.button = 5;
        }
        
        

    }
}
