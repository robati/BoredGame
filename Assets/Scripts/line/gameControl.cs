using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace xo
{
    

public class gameControl : MonoBehaviour {
    public struct tuple
    {
        public int x;
        public int y;
    }


    public Sprite startTurnPic;
    public Sprite lightningTurnPic;

    static public bool lightningTurn = false;
    public Image turnImage;
    public lineAndDots[] lineAndDots;
    // public List<lineAndDots> lineAndDots;
    public Transform box;
    public Text winner;
    private GameObject panel ;
    private int endOfColumn = 3, endOfLine = 3;
    private int starBoxCount = 0;
    bool done = false;
    bool lockedBoard=false;
    public Transform boxlineTransform;
    public Transform boxlinepanel;

        private bool isOnePlayer = false;
        private bool isAIgame = false;
    void Awake () {
        // if(menu.gameStatus == menu.gameState.multiplayer)
        // {
        //     endOfColumn = 1;
        //     endOfLine = 2;
        // }
            isOnePlayer = (levelSelector.gameStatus != gameState.multiplayer);
            isAIgame= (levelSelector.gameStatus == gameState.AI);

        // //panel = GameObject.Find("AnnounceWinner");
        ////panel.SetActive(false);

        createTable();
    }
	

	void Update () {
        // if (Screen.height != 600 || Screen.width != 960 || Screen.fullScreen)
        // {
        //     Screen.SetResolution(960, 600, false);
        // }


        if (lightningTurn)
            turnImage.sprite = lightningTurnPic; 
        else turnImage.sprite = startTurnPic;


        if (isGameFinsihed()&&!done)
        {
            done = true;
            // panel.SetActive(true);
            float isStarWinner = (float)starBoxCount / ((float)(endOfColumn + 1) * (float)(endOfLine + 1));
            winner.text = isStarWinner == 0.5f ? "scores tied!" : (isStarWinner > 0.5f ? "star won!!!" : "lightning won !!!");//) + starBoxCount + "-" + isStarWinner;
        }
       if (isOnePlayer)// && 
        if(lightningTurn && !lockedBoard)
            rivalPlay();
       //    // StartCoroutine(rivalPlay());

    }
    private void createTable()
    {
        int start = 0;
        int counter = 0;
         lineAndDots = boxlinepanel.gameObject.GetComponentsInChildren<lineAndDots>();

        for (int i = endOfColumn; i > start - 1; i--)
        {
            // Transform thisLineTransform = Instantiate(boxlineTransform);
                // thisLineTransform.SetParent(boxlinepanel, false);
                // thisLineTransform.gameObject.SetActive(true);
            for (int j = endOfLine; j > start - 1; j--)
            {


                //  Transform tempTransformObject = Instantiate(box);
                // tempTransformObject.SetParent(thisLineTransform, false);
                if (j != endOfLine)
                {
                    lineAndDots lineAndDotsScrip =lineAndDots[counter]; //tempTransformObject.GetComponent<lineAndDots>();
                    lineAndDots otherButtonlineAndDotsScrip =lineAndDots[counter-1];// getScript(counter - 1);
                     UnityEngine.Events.UnityAction leftButtonAction = () => { otherButtonlineAndDotsScrip.toggel((int)buttonname.leftButton); };
                    lineAndDotsScrip.rightButtonEvent.AddListener(leftButtonAction);
                
             
                }
                if (i != endOfColumn)
                {
                    lineAndDots lineAndDotsScrip = lineAndDots[counter];//tempTransformObject.GetComponent<lineAndDots>();
                    lineAndDots otherButtonlineAndDotsScrip =lineAndDots[counter - (endOfLine + 1)];// getScript(counter - (endOfLine + 1));
                    UnityEngine.Events.UnityAction bottomButtonAction = () => { otherButtonlineAndDotsScrip.toggel((int)buttonname.bottomButton); };
                    lineAndDotsScrip.topButtonEvent.AddListener(bottomButtonAction);
                }
                //lineAndDots.Insert(counter, tempTransformObject);
                counter++;
            }
        }
        
    }

    public void menuClicked()
    {
        SceneManager.LoadScene(0);
    }
    bool isGameFinsihed()
    {
        starBoxCount = 0;
        int lenght = (endOfColumn + 1) * (endOfLine + 1);
        for (int i = lenght - 1; i >= 0; i--)
        {
            lineAndDots lineAndDotsScrip = lineAndDots[i];//getScript(i);
            if (lineAndDotsScrip.isFinishedBox)
            {
                starBoxCount += lineAndDotsScrip.isStarWinner ? 1 : 0;
            }
            else
            {
                return false;
            }

        }
        return true;
    }
    /*lineAndDots getScript(int idx)
    {
        Transform box = lineAndDots[idx];
        lineAndDots lineAndDotsScript = box.GetComponent<lineAndDots>();
        return lineAndDotsScript;
    }*/
    private void rivalPlay()
   // IEnumerator rivalPlay()
    {
        lockedBoard = true;
        int w = endOfLine + 1;
        int boxNumber;
        int buttonNumber;
        do {
            boxNumber = findABox();
            buttonNumber = findALine(boxNumber);
        } while (buttonNumber == -1);

        lineAndDots lineAndDotsScript = lineAndDots[boxNumber];// getScript(boxNumber);
     //   yield return new WaitForSeconds(0.5f);
        lineAndDotsScript.toggel(buttonNumber,true);
        gameControl.lightningTurn = !gameControl.lightningTurn;
        lockedBoard = false;
    }
    
    int findABox()
    {
        lineAndDots lineAndDotsScript;
        int boxNumber;
        int w = endOfLine + 1;
        do
        {
            boxNumber = Random.Range(0, lineAndDots.Length);
            lineAndDotsScript = lineAndDots[boxNumber] ;//getScript(boxNumber);
        } while (lineAndDotsScript.isFinishedBox);
        Debug.Log(boxNumber + "box is chosen");
        return boxNumber;
    }
    int findALine(int boxNumber)
    {
        lineAndDots lineAndDotsScript =lineAndDots[boxNumber];// getScript(boxNumber);
        int buttonNumber=-1;
        int w = endOfLine + 1;
        bool isLeftButton;
        bool isBottomButton;
        bool isForbiddenButton;
        List<int> lines = new List<int> { 0, 1, 2, 3 };
        do
        {
            int idx = Random.Range(0, lines.Count);
            if (lines.Count == 0)
                return -1;

            buttonNumber = lines[idx];
            lines.RemoveAt(idx);
            isLeftButton = (buttonNumber == (int)buttonname.leftButton) && ((boxNumber + 1) % w != 0);
            isBottomButton = (buttonNumber == (int)buttonname.bottomButton) && (lineAndDots.Length - w > boxNumber);
            //Debug.Log("number box=" + boxNumber + " button=" + buttonNumber + " isLeftButton=" + isLeftButton + "isBottomButton" + isBottomButton);
            isForbiddenButton = isLeftButton || isBottomButton;
            Debug.Log(buttonNumber + " is chosen enabled:" + lineAndDotsScript.buttons[buttonNumber].enabled + " left:" + isLeftButton + " bottom:" + isBottomButton);
        } while (!lineAndDotsScript.buttons[buttonNumber].enabled || isForbiddenButton);
        Debug.Log(buttonNumber + "line is chosen");
        return buttonNumber;
    }

};
}
