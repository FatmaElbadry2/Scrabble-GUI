using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Scores score;
   // public RackHM rack;
    public Rack rack;
    public Vector3 test= new Vector3(0, 0, 0);
    int frame;
    Startmenu s;
    public MakeLetter l;
    public Timer timer;

    public ScreenMessage message;
    string Letter;
    public string Scenename;
    int number;
    List<string> input = new List<string>();
    List<string> input2 = new List<string>();
    string[] line1;
    public static int button; //Which button was pressed in human mode: 1-Play 2-Swap 3-Pass 4-Hint
    StreamReader sr=new StreamReader("Game2.txt");
    void Start()
    {
        Scenename = SceneManager.GetActiveScene().name;
        
        line1 = sr.ReadLine().Split();
        input.Add(line1[0]);
        input.Add(line1[1]);
        score.start(input);
        input.Clear();
        frame = 0;

       
    }

    // Update is called once per frame
    void Update()
    {
        timer.TimerUpdate("10");
        //StartCoroutine(message.MessageUpdate("Hello"));
        if (!sr.EndOfStream && frame % 300000 == 0)
        {
            
            line1 = sr.ReadLine().Split();
            if (line1[0] == "R")
            {
                if (line1[1] == "CREATE")
                {
                    input.Add(line1[2]);
                    input.Add(line1[3]);
                    input.Add(line1[4]);
                    input.Add(line1[5]);
                    input.Add(line1[6]);
                    input.Add(line1[7]);
                    input.Add(line1[8]);
                    rack.Create(ref input);
                }
                else if (line1[1] == "ADD")
                {
                    for (int i = 2; i < line1.Length; i++)
                    {
                        input.Add(line1[i]);

                    }


                    rack.update(1, ref input);

                    input.Clear();

                }
            }
            else if (line1[0] == "M")
            {
                if (line1[1] == "P2")
                {
                    input.Add(line1[2]);
                    input.Add(line1[3]);
                    input.Add(line1[4]);
                    l.Create(ref input);
                }

                else if (line1[1] == "P1")
                {
                    input.Add(line1[2]);
                    input.Add(line1[3]);
                    input.Add(line1[4]);
                   
                        rack.update(2, ref input);
                    
                    
                }

                input.Clear();
            }
            else if (line1[0] == "S")
            {
                if (line1[1] == "P1")
                {
                    score.update(int.Parse(line1[2]), "dog");
                }
                else if (line1[1] == "P2")
                {
                    score.update(int.Parse(line1[2]), "dog");
                }
            }
            else if (line1[0] == "SW")
            {
                for (int i = 0; i < int.Parse(line1[1]); i++)
                {
                    input.Add(line1[i + 2]);
                    input2.Add(line1[i + 2 + int.Parse(line1[1])]);
                }
              
                    rack.Swap(int.Parse(line1[1]), input, input2); 
                
               
                input.Clear();
                input2.Clear();
            }
            
        }
        else if (frame % 300000 != 0) frame++;
        else if (sr.EndOfStream) sr.Close();
        
    }

}
