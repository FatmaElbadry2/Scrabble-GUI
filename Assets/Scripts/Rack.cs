using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Rack : MonoBehaviour
{
    public GameObject Letterprefab;

    public static List<string> letters = new List<string>();

    public static List<string> Swappedletters = new List<string>();
    public static List<int> rows = new List<int>();
    public static List<int> colomuns = new List<int>();
    public Sprite[] LetterSprite;
    public int m = letters.Count;
    public static List<GameObject> LetterList = new List<GameObject>();
    public static List<string> RackList = new List<string>();
    public static List<int> RorB = new List<int>(); //the letter on board(0) or on rack(1)
    public static int[] EmptyPlaces = new int[7];
    Vector3 pos = new Vector3(0, 0, 0);
    int arrayIdx = 0;
    int x;
    float row = -4.25f;
    float coloumn = -8.9f;
    static List<string> HintWord = new List<string>();
    static List<int> HintRow = new List<int>();
    static List<int> HintCol = new List<int>();
    static string[] Line;
    public static List<Vector3> Rpos = new List<Vector3>();

   
    static public void getpos()
    {
        int row1 = 0;
        int col = 0;

        for (int i = 0; i < LetterList.Count; i++)
        {
            if (LetterList[i].transform.position.x >= 0 && LetterList[i].transform.position.x <= 9.1 && RorB[i] == 1)
            {
                letters.Add(LetterList[i].GetComponent<SpriteRenderer>().sprite.name);
                col = (int)((LetterList[i].transform.position.x) / 0.65f);
                
                if (LetterList[i].transform.position.y > 0)
                {
                    row1 = (int)(((LetterList[i].transform.position.y) / 0.65f) + 7);
                }
                else if (LetterList[i].transform.position.y < 0)
                {
                    row1 = (int)(((LetterList[i].transform.position.y) / 0.65f) )+7;
                    Debug.Log(row1.ToString());
                }
                rows.Add(row1);
                colomuns.Add(col);
            }
        }

    }

    static public IEnumerator getHint()
    {
        StreamReader sr = new StreamReader("Hints.txt");
        Line = sr.ReadLine().Split();
        for (int i = 0; i < Line.Length; i++)
        {
            HintWord.Add(Line[i]);
        }

        Line = sr.ReadLine().Split();
        for (int i = 0; i < Line.Length; i++)
        {
            HintRow.Add(int.Parse(Line[i]));
        }

        Line = sr.ReadLine().Split();
        for (int i = 0; i < Line.Length; i++)
        {
            HintCol.Add(int.Parse(Line[i]));
        }

        List<Vector3> originalPos = new List<Vector3>();
        List<int> index = new List<int>();

        for (int i = 0; i < HintWord.Count; i++)
        {
            Vector3 pos;
            
            Rack R = new Rack();
            for (int j = 0; j < LetterList.Count; j++)
            {
                if ((HintWord[i] == LetterList[j].GetComponent<SpriteRenderer>().sprite.name) && (LetterList[j].transform.position.y == -4.25f) && (LetterList[j].transform.position.x > -9.8f) && (LetterList[j].transform.position.x < -5))
                {
                    originalPos.Add(LetterList[j].transform.position);
                    index.Add(j);

                    LetterList[j].GetComponent<SpriteRenderer>().color = new Color(1f, 0.7568628f, 0.6901961f);
                    pos = R.GetPos(HintRow[i].ToString(), HintCol[i].ToString());
                    //pos=new Vector3(0,0,0);
                    LetterList[j].transform.position = pos;
                    LetterList[j].transform.localScale = new Vector3(0.8f, 0.8f, 0);
                    
                    break;
                }
                
            }


        }


        

        for (int i = 0; i < HintWord.Count; i++)
        {
            yield return new WaitForSecondsRealtime(1);
            LetterList[index[i]].transform.position = originalPos[i];
            LetterList[index[i]].transform.localScale = new Vector3(1f, 1f, 0);
            LetterList[index[i]].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
           
        } 
        HintWord.Clear();
        HintCol.Clear();
        HintRow.Clear();
        sr.Close();
    }
    static public void FSwap()
    {
        for (int i = 0; i <7; i++)
        {
          if ( LetterList[i].GetComponent<SpriteRenderer>().color ==new Color(1f, 0.7568628f, 0.6901961f))
          {
              Swappedletters.Add(LetterList[i].GetComponent<SpriteRenderer>().sprite.name);
          }
        }
        
    }
    public void Create(ref List<string> input)
    {
        LetterList.Clear();
        Rpos.Clear();
        RorB.Clear();
        Swappedletters.Clear();
        //xpos.Add(pos1);
        for (int i = 0; i < 7; i++)
        {

            for (int j = 0; j < 26; j++)
            {
                if (LetterSprite[j].name == input[i])
                {
                    arrayIdx = j;


                }
            }
            EmptyPlaces[i] = 1;
            pos = new Vector3(coloumn + (i * 0.8f), row, -2);
            Sprite nLetter = LetterSprite[arrayIdx];
            GameObject newLetter = Instantiate(Letterprefab);
            newLetter.GetComponent<SpriteRenderer>().sprite = nLetter;
            newLetter.transform.position = pos;
            newLetter.transform.localScale = new Vector3(1, 1, 0);
            if (SceneManager.GetActiveScene().name == "HumanMode")
            {

                newLetter.AddComponent<ItemDragHandler>();
                newLetter.AddComponent<BoxCollider2D>();
            }
            LetterList.Add(newLetter);
            RackList.Add(input[i]);
            Rpos.Add(pos);
            RorB.Add(1);
        }
        input.Clear();
    }
    int ReturnIndx(string letter)
    {
        for (int i = 0; i < 26; i++)
        {
            if (letter == LetterSprite[i].name)
            {
                arrayIdx = i;
                break;
            }
        }
        return arrayIdx;
    }
    Vector3 GetPos(string a, string b)
    {
        if (int.Parse(a) == 7)
        {
            row = 0;
        }
        else if (int.Parse(a) > 7)
        {
            row = ((int.Parse(a)) - 7) * 0.65f;

        }
        else if (int.Parse(a) < 7)
        {
            row = (7 - (int.Parse(a))) * 0.65f;
            row = -row;
        }

        if (int.Parse(b) == 0)
        {
            coloumn = 0;
        }
        else if (int.Parse(b) > 0)
        {
            coloumn = (int.Parse(b)) * 0.65f;
        }
        return pos = new Vector3(coloumn, row, -2);
    }
      

    public void Answer(string ans)
    {
        if (ans=="YES")
        {
            for (int i = 0; i < 7; i++)
            {
                if(LetterList[i].transform.position.x >= 0 && LetterList[i].transform.position.x <= 9.1 && LetterList[i].transform.position.y >= -5.2 && LetterList[i].transform.position.y <= 5.2)
                {
                   Destroy(LetterList[i].GetComponent<ItemDragHandler>());
                    Destroy(LetterList[i].GetComponent<BoxCollider2D>()); 
                }
            }
            return;
        }
        else if (ans == "NO")
        {
            for (int i = 0; i < 7; i++)
            {
                if (LetterList[i].transform.position.x >= 0 && LetterList[i].transform.position.x <= 9.1 && LetterList[i].transform.position.y >= -5.2 && LetterList[i].transform.position.y <= 5.2)
                {
                    for (int K = 0; K < 7; K++)
                    {
                        if (EmptyPlaces[K]==0)
                        {
                            LetterList[i].transform.position=Rpos[K];
                            LetterList[i].transform.transform.localScale=new Vector3(1,1,0);
                            EmptyPlaces[K]=1;
                        }
                    }
                }
            } 
        }
    }

    public void update(int type, ref List<string> input) //1:to add a letter //2:to remove a letter
    {
        if (type == 2)
        {
            arrayIdx = ReturnIndx(input[0]);
            pos = GetPos(input[1], input[2]);

            for (int i = 0; i < LetterList.Count; i++)
            {
                if (LetterList[i].GetComponent<SpriteRenderer>().sprite.name == input[0])
                {
                    LetterList[i].transform.localScale = new Vector3(0.8f, 0.8f, 0);
                    LetterList[i].GetComponent<SpriteRenderer>().transform.position = pos;
                    EmptyPlaces[i] = 0;
                    RackList[i] = " ";
                    RorB[i] = 0;
                    break;
                }
            }
            input.Clear();
        }
        else if (type == 1)
        {
            row = -4.25f;
            coloumn = -8.9f;

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (EmptyPlaces[j] == 0)
                    {
                        pos = new Vector3(coloumn + (j * 0.8f), row, -2);
                        EmptyPlaces[j] = 1;
                        x = j;
                        break;
                    }
                }
                for (int k = 0; k < 26; k++)
                {
                    if (LetterSprite[k].name == input[i])
                    {
                        arrayIdx = k;

                    }
                }
                Sprite nLetter = LetterSprite[arrayIdx];
                GameObject newLetter = Instantiate(Letterprefab);
                newLetter.GetComponent<SpriteRenderer>().sprite = nLetter;
                newLetter.transform.position = pos;
                newLetter.transform.localScale = new Vector3(1, 1, 0);
                if (SceneManager.GetActiveScene().name == "HumanMode")
                {
                    newLetter.AddComponent<ItemDragHandler>();
                    newLetter.AddComponent<BoxCollider2D>();
                }
                LetterList.Add(newLetter);
                RackList[x] = input[i];
                RorB.Add(1);
            }
        }

    }
    public void Swap(int numberofletters, List<string> old, List<string> newl)
    {
        for (int j = 0; j < numberofletters; j++)
        {
            for (int i = 0; i < LetterList.Count; i++)
            {
                if (LetterList[i].GetComponent<SpriteRenderer>().sprite.name == old[j] && RorB[i] == 1)
                {
                    x = i;
                    break;
                }
            }
            for (int k = 0; k < 7; k++)
            {
                if (RackList[k] == old[j])
                {
                    RackList[k] = newl[j];
                }
            }
            for (int k = 0; k < 26; k++)
            {
                if (LetterSprite[k].name == newl[j])
                {
                    arrayIdx = k;

                }
            }

            Sprite nLetter = LetterSprite[arrayIdx];
            LetterList[x].GetComponent<SpriteRenderer>().sprite = nLetter;
        }

    }

}
