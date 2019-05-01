using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Startmenu : MonoBehaviour

{
    public int scene;
    public void changemenuscene(string scenename)
    {

        //Application.LoadLevel(scenename);
        SceneManager.LoadScene(scenename);
       

    }
   
    

}
