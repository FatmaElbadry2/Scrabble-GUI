using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour
{ 
    Vector3 dist;
    Vector3 OriginalPos;
     float posX;
     Vector3 curPos;
     Vector3 worldPos;
     float posY;
 
    void OnMouseDown()
    {
        OriginalPos=transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

        transform.localScale = new Vector3(0.8f, 0.8f, 0);
        
    }

    Vector3 GetPos(string a, string b)
    {
        float row =0;
        float coloumn=0;
        Vector3 NewPos;
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
        return NewPos = new Vector3(coloumn, row, -2);
    }
    void OnMouseUp()
    {
        int row=0;
        int col=0;
        if ((worldPos.x < 0 || worldPos.x > 9.1) /*&& (worldPos.y<-4.55 || worldPos.y>4.55)*/)
        {
            if(OriginalPos.x>=0 && OriginalPos.x<=9.1 && OriginalPos.y>=-5.2 && OriginalPos.y <=5.2)
            {
                for (int i = 0; i < 7; i++)
                {
                    if(Rack.EmptyPlaces[i]==0)
                    {
                        OriginalPos=Rack.Rpos[i];
                        Rack.EmptyPlaces[i]=1;
                        break;

                    }
                }
            }
            transform.position = OriginalPos;
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if(worldPos.x >= 0 && worldPos.x <= 9.1 && worldPos.y >= -5.2 && worldPos.y <= 5.2)
        {
            col = (int)((worldPos.x) / 0.65f);
            if (worldPos.y > 0)
            {
                row = (int)(((worldPos.y) / 0.65f) + 7);
            }
            else if (worldPos.y < 0)
            {
                row = (int)(((worldPos.y) / 0.65f)) + 7;
                Debug.Log(row.ToString());
            }
            transform.position=GetPos(row.ToString(),col.ToString());
            bool move=true;
            int x=0;
            for (int K = 0; K <7; K++)
            {
                if (transform.position==Rack.LetterList[K].transform.position)
                {
                    x++;
                }
            }
            if (x>1)
            {
                move=false;
                transform.position=OriginalPos;
                transform.localScale = new Vector3(1, 1, 0);

            }
            if(move==true)
            {
            for (int i = 0; i < 7; i++)
            {
                if (Rack.LetterList[i].transform.position==GetPos(row.ToString(), col.ToString()))
                {
                    Rack.EmptyPlaces[i]=0;
                    break;
                }
            }
            }

        }


    }
    



    void OnMouseDrag()
    {
         curPos = new Vector3(Input.mousePosition.x - posX,
                  Input.mousePosition.y - posY, dist.z);

      worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
        transform.localScale = new Vector3(0.8f, 0.8f, 0);
       
    }
}