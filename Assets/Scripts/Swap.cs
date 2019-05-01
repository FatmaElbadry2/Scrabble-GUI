using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Swap : MonoBehaviour
{
    public GameObject swapPanel;
    public Text swapText;
    // Start is called before the first frame update
    public void SStart()
    {
        swapPanel.SetActive(false);
        StartCoroutine(SwapRack());
    }

    // Update is called once per frame
    public IEnumerator SwapRack()
    {
        GameManager.button = 3;
        swapPanel.SetActive(true);
        swapText.text = "Swap";

        yield return new WaitForSecondsRealtime(1);

        swapPanel.SetActive(false);
        

    }
}
