using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public xo.levelSelector level;
    public List<GameObject> HighlightList=new List<GameObject>();
    public List<GameObject> WordOnButtons=new List<GameObject>();
    public List<GameObject> WordOffButtons=new List<GameObject>();
    public List<GameObject> LineOnButtons=new List<GameObject>();
    public List<GameObject> LineOffButtons=new List<GameObject>();
    public List<GameObject> TickOnButtons=new List<GameObject>();
    public List<GameObject> TickOffButtons=new List<GameObject>();
    List<GameObject> MainButtons=new List<GameObject>();
    List<GameObject> GameButtons=new List<GameObject>();
    void Start()
    {
       MainButtons.AddRange(WordOffButtons);
       GameButtons.AddRange(WordOnButtons);
       
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

        saveAndLoadGame("Word:1","thWordGame");
  


    }   
     public void OnLineGameClicked(int i){

         string gameLevel="";
         if(i==1){
            level.onePlayer();
            gameLevel="Line:1";
         }
         else if(i==2){
            level.multiPlayer();
            gameLevel="Line:2" ;
         }
         else if(i==3){
            level.AIPlayer();
            gameLevel="Line:3";
        }


        saveAndLoadGame(gameLevel,"theDotsGame");


    }    
     public void OnXOGameClicked(int i){

         string gameLevel="";
         if(i==1){
             level.onePlayer();
            gameLevel="XO:1" ;

         }
         else if(i==2){
            level.multiPlayer();
            gameLevel="XO:2" ;

         }
         else if(i==3){
             level.AIPlayer();
             gameLevel="XO:3" ;

         }
        saveAndLoadGame(gameLevel,"tictactoGame");

    }

    //saves the level selected for continue button and loads game's scene.
    void saveAndLoadGame(string gameLevelName,string sceneName){
        PlayerPrefs.SetString("GamePlayed",gameLevelName) ;
        SceneManager.LoadScene(sceneName);
    }
    public void OnXOLineClicked(){

        showAndHideButtons(TickOnButtons,TickOffButtons);
        HighlightList[0].SetActive(true);
        }
    public void OnBoxLineClicked(){

        showAndHideButtons(LineOnButtons,LineOffButtons);
        HighlightList[1].SetActive(true);
        }

    public void OnWordLineClicked(){

        showAndHideButtons(WordOnButtons,WordOffButtons);
        HighlightList[2].SetActive(true);
    }
    
    //in order to show the on buttons, offbuttons and other buttons must goto their defaul.
    void showAndHideButtons(List<GameObject> onButtons,List<GameObject> offButtons){

        resetButton();

        foreach(GameObject i in offButtons){
           i.SetActive(false);
        }
        foreach(GameObject i in onButtons){
           i.SetActive(true);
        }
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
