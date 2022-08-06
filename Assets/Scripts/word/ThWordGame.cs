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
    string[] thWords={"abcdefghijklmnop","DREAMS","TRUE","REALLY","GREAT","KIND","LOVE","PINK","MUSIC","PIANO","OCEAN","RAINBOWS","CUPCAKES"};
    char[] thLetters={'Q','W','E','R','T','Y','U','I','O','P','L','K',
                'J','H','G','F','D','S','A','Z','X','C','V','B','N','M'};
    public string MainLetter;
    public Text MainTxt;
    public Text MainTxt2;
    int WordIDX;
    int LetterIndex;
    List<ThLetter> listOfLet=new List<ThLetter>();
    int AllLettersNumber=7;
    char[] MainWordLetters;
    void Start()
    {
        Time.timeScale = 0f;

        WordIDX=PlayerPrefs.GetInt("wordIDX",0) ;
        if(WordIDX>thWords.Length-1){
            WordIDX=0;
            }
        LetterIndex=0;
        MainWordLetters=thWords[WordIDX].ToCharArray();
        MainLetter=thWords[WordIDX][LetterIndex].ToString();

        PrepareLetters(thWords[WordIDX]); 

        MainTxt.text=thWords[WordIDX];
        MainTxt2.text=thWords[WordIDX];
        }
    public void CorrectLetterEntered(){
        LetterIndex++;
        if(LetterIndex<MainWordLetters.Length){
            MainLetter=thWords[WordIDX][LetterIndex].ToString();
            }            
    }
    public void LetterAnimFin(char inCharacter){
        //    Debug.Log(MainWordLetters[MainWordLetters.Length-1]+"  "+r.ToCharArray()[0]);
        // if(inCharacter==MainWordLetters[MainWordLetters.Length-1]){
        if(LetterIndex>=MainWordLetters.Length){
                StartCoroutine(ShowWinPanel(0.5f));
                WordIDX++;
                PlayerPrefs.SetInt("wordIDX",WordIDX) ;

        }
    }
    public void again(){
        SceneManager.LoadScene("thWordGame");

    }
    public ThLetter getMainLetter(){
        foreach ( ThLetter i in listOfLet)
        {
            if(i!=null)
            if(i.id==MainLetter){
            
                Debug.Log(i.id);
                return i;
            }
        }
        return null;
    }
    public void mainLetterHighlight(bool on){
        ThLetter i =getMainLetter();
        i.HighlightOn(on);
    }
    public void onToMenu(){
        SceneManager.LoadScene("Menu");

    }
    public void PrepareLetters(string word){
        char[] letters= word.ToCharArray();
        int n= word.ToCharArray().Length;
       CreateLetter(n,letters);

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
        Time.timeScale = 0f;

        WinPanel.SetActive(true);


    }
    public void CreateLetter(int n,char[] letters){
        for(int i=0 ; i<n ; i++){
            Vector3 position= new Vector3(UnityEngine.Random.Range(-10,10),UnityEngine.Random.Range(-2,4),0);
            GameObject letterPref=Instantiate<GameObject>(TheLetterprefab,position,new Quaternion(),Box.transform);
            letterPref.transform.localPosition=position;
            letterPref.SetActive(true);
            ThLetter let = letterPref.GetComponent<ThLetter>();
            let.id=letters[i].ToString();
            listOfLet.Add(let);
            // letterPref.GetComponent<ThLetter>().id=letters[i].ToString();
            letterPref.GetComponent<Rigidbody2D>().AddForce(new Vector2(300,200));
        }
    }
    public void CheckWord(string id){
        Debug.Log("ey "+id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(bool play){
        if(play)
        Time.timeScale = 1f;
        else
                Time.timeScale = 0f;
        

    }
}
