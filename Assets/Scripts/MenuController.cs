using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public xo.levelSelector level;
    public List<GameObject> HighlightList;
    public List<GameObject> WordOnButtons;
    public List<GameObject> WordOffButtons;
    public List<GameObject> LineOnButtons;
    public List<GameObject> LineOffButtons;
    public List<GameObject> TickOnButtons;
    public List<GameObject> TickOffButtons;
    List<GameObject> MainButtons=null;
    List<GameObject> GameButtons=null;
    void Start()
    {
       MainButtons=WordOffButtons;
       GameButtons=WordOnButtons;
       
       MainButtons.AddRange(TickOffButtons);
       GameButtons.AddRange(TickOnButtons);
       
       MainButtons.AddRange(LineOffButtons);
       GameButtons.AddRange(LineOnButtons);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnWordGameClicked(){
        PlayerPrefs.SetString("GamePlayed","Word:1") ;
                SceneManager.LoadScene("thWordGame");
                            


    }   
     public void OnLineGameClicked(int i){
         if(i==1){
             level.onePlayer();
            PlayerPrefs.SetString("GamePlayed","Line:1") ;

         }
         else if(i==2){
             level.multiPlayer();
                         PlayerPrefs.SetString("GamePlayed","Line:2") ;

         }
         else if(i==3){
             level.AIPlayer();
                         PlayerPrefs.SetString("GamePlayed","Line:3") ;

         }
                    //  PlayerPrefs.SetString("GamePlayed","Line:1") ;

                SceneManager.LoadScene("theDotsGame");

    }    
     public void OnXOGameClicked(int i){
         if(i==1){
             level.onePlayer();
            PlayerPrefs.SetString("GamePlayed","XO:1") ;

         }
         else if(i==2){
             level.multiPlayer();
                         PlayerPrefs.SetString("GamePlayed","XO:2") ;

         }
         else if(i==3){
             level.AIPlayer();
                         PlayerPrefs.SetString("GamePlayed","XO:3") ;

         }
                SceneManager.LoadScene("tictactoGame");

    }
         public void OnXOLineClicked(){
            resetButton();
            HighlightList[0].SetActive(true);

            foreach(GameObject i in TickOffButtons){
                                i.SetActive(false);


             }
             foreach(GameObject i in TickOnButtons){
                                i.SetActive(true);


             }
         }
         public void OnBoxLineClicked(){
            resetButton();
            HighlightList[1].SetActive(true);

            foreach(GameObject i in LineOffButtons){
                                i.SetActive(false);


             }
             foreach(GameObject i in LineOnButtons){
                                i.SetActive(true);


             }
         }

          public void OnWordLineClicked(){
            resetButton();
            HighlightList[2].SetActive(true);
            
            WordOffButtons[0].SetActive(false);
            WordOnButtons[0].SetActive(true);

         }
         public void resetButton(){
                         foreach(GameObject i in HighlightList){
                i.SetActive(false);

            }
            foreach(GameObject i in MainButtons){
                i.SetActive(true);

            }
            foreach(GameObject i in GameButtons){
                i.SetActive(false);

            }
         }
         public void OnContClicked(){
                    string GameIDX=PlayerPrefs.GetString("GamePlayed"," ") ;
                    if (GameIDX=="XO:1"){
                        OnXOGameClicked(1);
                    }
                    else if (GameIDX=="XO:2"){
                        OnXOGameClicked(2);
                    }
                    else if (GameIDX=="XO:3"){
                        OnXOGameClicked(3);
                    }
                    else if (GameIDX=="Line:1"){
                        OnLineGameClicked(1);
                    }
                    else if (GameIDX=="Line:2"){
                        OnLineGameClicked(2);
                    }
                    else if (GameIDX=="Word:1"){
                        OnWordGameClicked();
                    }
                    else{
                        OnXOGameClicked(1);
                    }

         }


}
