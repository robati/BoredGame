using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ThWordGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TheLetterprefab;
    public GameObject Box;
    public GameObject WinPanel;
    // string[] thWords={"aaaa","abab","bool"};
    string[] thWords={"DREAMS","TRUE","REALLY","GREAT","KIND","LOVE","PINK","MUSIC","PIANO","OCEAN","RAINBOWS","CUPCAKES"};
    char[] thLetters={'Q','W','E','R','T','Y','U','I','O','P','L','K',
                'J','H','G','F','D','S','A','Z','X','C','V','B','N','M'};
    [HideInInspector]
    public string MainLetter;
    public Text MainTxt;
    public Text HintTxt;
    int WordIDX;
    int LetterIndex;
    List<ThLetter> listOfLet=new List<ThLetter>();
    int AllLettersNumber=7;
    char[] MainWordLetters;
    bool gameStarted=false;
    void Start()
    {
        pauseGame(true);
        setInitWordAndLetter();

        PrepareLetters(thWords[WordIDX]); 

        MainTxt.text=thWords[WordIDX];
        HintTxt.text=thWords[WordIDX];
        }
    //if the entered letter is correct set the mainLetter again. 
    public void CorrectLetterEntered(){
        LetterIndex++;
        if(LetterIndex<MainWordLetters.Length){
            MainLetter=thWords[WordIDX][LetterIndex].ToString();
            }            
    }
    //set main word and its first letter from saved progress of player 
    void setInitWordAndLetter(){
        WordIDX=PlayerPrefs.GetInt("wordIDX",0) ;
        if(WordIDX>thWords.Length-1){
            WordIDX=0;
            }
        LetterIndex=0;
        MainWordLetters=thWords[WordIDX].ToCharArray();
        MainLetter=thWords[WordIDX][LetterIndex].ToString();
    }
    //if the letter is correct and its the last letter show win pannel and save the progress of player.
    public void LetterAnimFin(char inCharacter){
        if(LetterIndex>=MainWordLetters.Length){
                StartCoroutine(ShowWinPanel(0.5f));
                WordIDX++;
                PlayerPrefs.SetInt("wordIDX",WordIDX) ;

        }
    }
    public void again(){
        SceneManager.LoadScene("thWordGame");

    }
    public ThLetter getMainLetterScript(){
        foreach ( ThLetter i in listOfLet)
        {
            if(i!=null)
                if(i.id==MainLetter){

                    return i;
                }
        }
        return null;
    }
    //on hint button clicked event
    public void mainLetterHighlight(bool on){
        ThLetter i =getMainLetterScript();
        i.HighlightOn(on);
    }
    public void onToMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");

    }

    public void PrepareLetters(string word){
        //create letters for the main word
        char[] letters= word.ToCharArray();
        int n= word.ToCharArray().Length;
       CreateLetter(n,letters);

        //create random AllLettersNumber-n letter 
       char[] thLetters2={};
       if(AllLettersNumber-n>0){
            thLetters2=new char[AllLettersNumber-n];
            for (int i = 0; i < AllLettersNumber-n; i++){
                int index=UnityEngine.Random.Range(0,thLetters.Length);
                thLetters2[i]=thLetters[index];
            }
       }
       CreateLetter( AllLettersNumber>n? AllLettersNumber-n:0,thLetters2);

    }
    private IEnumerator ShowWinPanel(float second){

        yield return new WaitForSeconds(second);
        pauseGame(true);
        WinPanel.SetActive(true);


    }
    //create letter object and set a random position for it and add force to move around
    public void CreateLetter(int n,char[] letters){

        for(int i=0 ; i<n ; i++){

            Vector3 position= new Vector3(UnityEngine.Random.Range(-10,10),UnityEngine.Random.Range(-2,4),0);
            
            GameObject letterPref=Instantiate<GameObject>(TheLetterprefab,position,new Quaternion(),Box.transform);
            letterPref.transform.localPosition=position;
            letterPref.SetActive(true);

            ThLetter let = letterPref.GetComponent<ThLetter>();
            let.id=letters[i].ToString();
            listOfLet.Add(let);

            letterPref.GetComponent<Rigidbody2D>().AddForce(new Vector2(300,200));
        }
    }



    public void pauseGame(bool pause){
        if(!pause)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
        

    }
}
