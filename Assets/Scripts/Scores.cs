using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scores : MonoBehaviour
{
    public GameObject scoreboardrprefab;
    public GameObject scoreboardrprefab1;
   
    
   
    public Sprite[] Scoreboards;
    string Pl1name,Pl2name;
    string word1, word2;
    public Text P1name, P2name,P1Score, P2Score,  P1word, P2word;
    Vector3 pos1 = new Vector3(-6.5f,2,0);
    Vector3 pos2 = new Vector3(-6.5f, 0.8f, 0);
    Vector3 pos3 = new Vector3(-6.5f, -0.4f, 0);
    public int score;
    public void start(List<string> names)
    {
      
        Pl1name = names[0];
        Pl2name = names[1];
        P1name.text = " ";
        P2name.text = " ";
       
        P1Score.text = " ";
        P2Score.text = " ";
       
        P1word.text = " ";
        P2word.text = " ";
      
        scoreboardrprefab.SetActive(false);
        scoreboardrprefab1.SetActive(false);
        

        



    }
    public void update1(int newscore,string word)
    {
    
            scoreboardrprefab.SetActive(true);
            P1name.text = Pl1name;
            P1Score.text =  newscore.ToString();
           
    }   
        public void update2(int newscore,string word)
    {
    
             scoreboardrprefab1.SetActive(true);
            P2name.text = Pl2name;
            P2Score.text =  newscore.ToString();
    }     

       
        
      
    
}
