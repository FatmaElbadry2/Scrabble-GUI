using System;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GUIInterface {

     static string time;
     static string Pname;
     static string Oname;
     static string Pscore;
     static string Oscore;
     static string ScreenMessage;
     static List<string> MyRack=new List<string>();
     static List<List<string>> Letter=new List<List<string>>();
     static void SetRack(string rack){
         MyRack.Clear();
         foreach (char letter in rack)
         {  if (letter!='0') {
           MyRack.Add(letter.ToString()); 
         } 
         }
       }
     static void SetBoard(string playedword,int row,int column,string direction){
          Letter.Clear();
           foreach (char letter in playedword){ 
            if (letter!='0') {
            List<string> addedletter=new List<string>();
            addedletter.Add(letter.ToString());
            addedletter.Add((row).ToString());
            addedletter.Add((column).ToString());
            Letter.Add(addedletter);
           
            if (direction=="0")  //horizontal
              column++;
            else if (direction=="1")  //vertical
            row--;
          }
          }
       }
       public static List<string> Messages=new List<string>(); 
        static GUIInterface(){
            Pscore="0";
            Oscore="0";
            Pname="player1";
            Oname="player2";
            ScreenMessage="Hello";
        }

        public static void InterpretMessage(string message){
          string [] parameters=message.Split(',');
            // if (parameters[0]!="-1"){
            //    time=parameters[1];
            // }
          if (parameters[0]=="0"){   // names only
             time=parameters[1];
              Pname=parameters[2];
              Oname=parameters[3];       
              ScreenMessage="Connected";
          }
          else if (parameters[0]=="1"){  //receive the rack
            ScreenMessage="Hellooo";
            time=parameters[1];
            Pscore=parameters[2];
            Oscore=parameters[3];
            SetRack(parameters[4]);
          //  SetBoard(parameters[5],int.Parse(parameters[6]),int.Parse(parameters[7]),parameters[8]);  
          }
          else if (parameters[0]=="2" || parameters[0]=="3"){   // play of the agent or opponent in AI mode
              Pscore=parameters[2];
              time=parameters[1];
              Oscore=parameters[3];
              ScreenMessage="Agent played";
              SetRack(parameters[4]);
              SetBoard(parameters[5],int.Parse(parameters[6]),int.Parse(parameters[7]),parameters[8]);
          }
        //   else if (parameters[0]=="3"){
        //       Pscore=parameters[2];
        //       Oscore=parameters[3];
        //       // display pass
        //       ScreenMessage="Pass";
             
        //   }
            else if (parameters[0]=="5"){
               time=parameters[1];
               ScreenMessage=parameters[10];
               Pscore=parameters[2];
               Oscore=parameters[3];  
               SetRack(parameters[4]);
             //  ConvertMessageToHint(parameters[5],int.Parse(parameters[6]) , int.Parse(parameters[7]),int.Parse(parameters[8]));
               Rack.Answer(parameters[9]);
          }
           else if (parameters[0]=="-1"){
            // terminate connnection
             ScreenMessage="Connection terminated";
          }
        }
        public static string ConvertMessage(int button){
           string message="\0";
            if (button==1){
             message="0,"+ConvertPlayToMessage()+"\0";
             int x=0;
            }
            else if (button==5){
                message="1,"+ConvertExchangeToMessage()+",\0";
            }
            else if (button==3){
                 message="2,\0";
            }
            else if (button==4){
                message="3,\0";
            }
            return message;
        }

        public static bool CheckIfInorder (){
            int flag=0;
            for (int i=1;i<Rack.rows.Count;i++){
                if (Rack.rows[i-1]!=Rack.rows[i]){
                    flag=1;
                    break; 
                }        
            }
            if (flag==0){
                return true;
            }
                flag=0;
             for (int i=1;i<Rack.colomuns.Count;i++){
                if (Rack.colomuns[i-1]!=Rack.colomuns[i]){
                    flag=1;
                    break; 
                }         
            }
            if (flag==0){
                return true;
            }
               return false;
        }
        public static void SortPlays(){
            if (CheckIfInorder()){
                if (Rack.rows.Count<=1){
                        return;
                }
                if (Rack.rows[0]==Rack.rows[1]){
                    for (int i=0;i<Rack.colomuns.Count;i++){
                        for (int j=i;j<Rack.colomuns.Count;j++){
                            if (Rack.colomuns[j]<Rack.colomuns[i]){
                                int tempcol=Rack.colomuns[i];
                                Rack.colomuns[i]=Rack.colomuns[j];
                                Rack.colomuns[j]=tempcol;
                                string templetter=Rack.letters[i];
                                Rack.letters[i]=Rack.letters[j];
                                Rack.letters[j]=templetter;
                            }
                            
                        }
                    }
                }
                else if (Rack.colomuns[0]==Rack.colomuns[1]){
                        for (int i=0;i<Rack.rows.Count;i++){
                        for (int j=i;j<Rack.rows.Count;j++){
                            if (Rack.rows[j]>Rack.rows[i]){
                                int tempcol=Rack.rows[i];
                                Rack.rows[i]=Rack.rows[j];
                                Rack.rows[j]=tempcol;
                                string templetter=Rack.letters[i];
                                Rack.letters[i]=Rack.letters[j];
                                Rack.letters[j]=templetter;
                            }
                            
                        }
                    } 
                }
            }
        }
        public static string ConvertPlayToMessage(){    
            SortPlays();
            string message=Rack.letters[0]+(14-Rack.rows[0]).ToString("D2")+Rack.colomuns[0].ToString("D2")+",";
              for (int i=1;i<Rack.letters.Count;i++){
                message=message+Rack.letters[i]+(14-Rack.rows[i]).ToString("D2")+Rack.colomuns[i].ToString("D2")+",";
              }
              return message;
        }
        public static string ConvertExchangeToMessage(){
            string message=Rack.Swappedletters[0];
           for (int i=0;i<Rack.Swappedletters.Count;i++){
               message=message+Rack.Swappedletters[i];
           } 
           return message;
        }
        public static void ConvertMessageToHint(string hint,int row , int col, int direction){
            // d=0 horizontal , 1 vertical
            foreach (char letter in hint){
               GameManager.HintRow.Add(row);
               GameManager.HintCol.Add(col);
               GameManager.HintWord.Add(letter.ToString());
               if (direction==0)
               col++;
               else
               row--;
            } 
        }

        public static string Gettime(){
            return time;
        }
        public static string GetPname(){
            return Pname;
        }
        public static string GetOname(){
           return Oname;
        }
        public static List<string> GetRack(){
            return MyRack;
        } 
        public static List<List<string>> GetLetters(){
            return Letter;
        }
        public static string GetPscore(){
            return Pscore;
        }
        public static string GetOscore(){
            return Oscore;
        }
        public static string GetScreenMessage(){
            return ScreenMessage;
        }

        public static string CheckButton(){
         if (GameManager.button==1){
             GameManager.button=0;
            return ConvertMessage(1);
        }
        else if (GameManager.button==5){  //exchange 
              GameManager.button=0;
             return ConvertMessage(5);
        }
        else if (GameManager.button==2){  //pass
           GameManager.button=0;
           return ConvertMessage(3);
        }
        else if (GameManager.button==4){   //hint
           GameManager.button=0;
           return ConvertMessage(4);
        }
        return "\0";
        }

 
    }

   