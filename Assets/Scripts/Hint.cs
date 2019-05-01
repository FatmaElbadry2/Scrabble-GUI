using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public GameObject hintPanel;
    public Text hintText;
    // Start is called before the first frame update
    public void HStart()
    {
        hintPanel.SetActive(false);
        StartCoroutine(ShowHint());
    }

    // Update is called once per frame
    public IEnumerator ShowHint()
    {
        GameManager.button = 4;
        hintPanel.SetActive(true);
        hintText.text = "Hint";

        yield return new WaitForSecondsRealtime(1);

        hintPanel.SetActive(false);

        StartCoroutine(Rack.getHint());

    }
}
