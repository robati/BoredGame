using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace xo
{
    

public class gameControl : MonoBehaviour {

    public Sprite xTurnPic;
    public Sprite oTurnPic;

    static public bool oTurn = false;
    public Image turnImage;
    List<lineAndDots> lineAndDots=new List<lineAndDots>();
    public Transform box;
    public Text winner;
    public Image winnerImage;
    public GameObject panel ;
    int endOfColumn = 3, endOfLine = 3;
    int starBoxCount = 0;
    bool done = false;
    bool lockedBoard=false;
    public Transform boxlineTransform;
    public Transform boxlinepanel;
    bool isOnePlayer = false;
    bool isAIgame = false;
    void Awake () {

        isOnePlayer = (levelSelector.gameStatus != gameState.multiplayer);
        isAIgame= (levelSelector.gameStatus == gameState.AI);

        createTable();
    }
	

	void Update () {

        if (oTurn)
            turnImage.sprite = oTurnPic; 
        else turnImage.sprite = xTurnPic;

        if (isGameFinsihed() && !done)
        {
            done = true;
            showTheWinner();
        }
        
        if(isOnePlayer && oTurn && !lockedBoard)
            rivalPlay();
           // StartCoroutine(rivalPlay());

    }
    void showTheWinner(){
            panel.SetActive(true);
            float isStarWinner = (float)starBoxCount /((float)(endOfColumn + 1) * (float)(endOfLine + 1));
            winner.text = isStarWinner == 0.5f ? "No" :"";
           
            if (isStarWinner > 0.5f)
                 winnerImage.sprite = xTurnPic; 
            else if( isStarWinner == 0.5f )
                winnerImage.gameObject.SetActive(false);
            else winnerImage.sprite = oTurnPic;
    }
    //create the table and lineAndDots list and connect top and bottom lines in each box to the last box.
     private void createTable()
    {
        int start = 0;
        int counter = 0;

        for (int i = endOfColumn; i > start - 1; i--)
        {
            Transform thisLineTransform = Instantiate(boxlineTransform);
            thisLineTransform.SetParent(boxlinepanel, false);
            thisLineTransform.gameObject.SetActive(true);

            for (int j = endOfLine; j > start - 1; j--)
            {
                Transform tempTransformObject = Instantiate(box);
                tempTransformObject.SetParent(thisLineTransform, false);
                lineAndDots lineAndDotsScrip =tempTransformObject.GetComponent<lineAndDots>();

                if (j != endOfLine)
                {
                    lineAndDots otherButtonlineAndDotsScrip = getScript(counter - 1);
                    UnityEngine.Events.UnityAction leftButtonAction = () => { otherButtonlineAndDotsScrip.toggel((int)buttonname.leftButton); };
                    lineAndDotsScrip.rightButtonEvent.AddListener(leftButtonAction);
                }

                if (i != endOfColumn)
                {
                    lineAndDots otherButtonlineAndDotsScrip =getScript(counter - (endOfLine + 1));
                    UnityEngine.Events.UnityAction bottomButtonAction = () => { otherButtonlineAndDotsScrip.toggel((int)buttonname.bottomButton); };
                    lineAndDotsScrip.topButtonEvent.AddListener(bottomButtonAction);
                }

                lineAndDots.Insert(counter,lineAndDotsScrip);
                counter++;
            }
        }
        
    }

    public void menuClicked()
    {
        SceneManager.LoadScene(0);
    }
    //set the starBoxCount to determine the winner and check if all the boxes are finished then the game is finished
    bool isGameFinsihed()
    {
        starBoxCount = 0;
        int lenght = (endOfColumn + 1) * (endOfLine + 1);
        for (int i = lenght - 1; i >= 0; i--)
        {
            lineAndDots lineAndDotsScrip = getScript(i);

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
    lineAndDots getScript(int idx)
    {
        return lineAndDots[idx];
    }
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
        } while (buttonNumber == -1);// AI can only play right and top lines of each box,it miight find an unfinished box but cannot select any line in it

        lineAndDots lineAndDotsScript =  getScript(boxNumber);
     //   yield return new WaitForSeconds(0.5f);
        lineAndDotsScript.toggel(buttonNumber,true);
        gameControl.oTurn = !gameControl.oTurn;
        lockedBoard = false;
    }
    //choose a random box for AI that is not finished before.
    int findABox()
    {
        lineAndDots lineAndDotsScript;
        int boxNumber;
        int w = endOfLine + 1;
        do
        {
            boxNumber = Random.Range(0, lineAndDots.Count);
            lineAndDotsScript = getScript(boxNumber);
        } while (lineAndDotsScript.isFinishedBox);

        return boxNumber;
    }
    //choose a random line in the selected box for AI that is not forbidden or selected before.
    int findALine(int boxNumber)
    {
        lineAndDots lineAndDotsScript = getScript(boxNumber);

        int buttonNumber=-1;
        int w = endOfLine + 1;
        // int h = endOfColumn + 1;
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

            bool isInLastColumn = (boxNumber + 1) % w == 0 ;
            bool isInLastRow = lineAndDots.Count- w <= boxNumber ;//16-(3+1)=12 --> 12 <= boxNumber <= 15

            isLeftButton = (buttonNumber == (int)buttonname.leftButton); 
            isBottomButton = (buttonNumber == (int)buttonname.bottomButton);

            isForbiddenButton = (isLeftButton && !isInLastColumn) || (isBottomButton && !isInLastRow);

        } while (!lineAndDotsScript.buttons[buttonNumber].enabled || isForbiddenButton);

        return buttonNumber;
    }
    /*NOTES:
           _        _
     in a |_| only   | are selectable ( |_ are only selected in last row and column) 
     the left line in a box is selectable using right line in the next box in the next column
     the bottom line in a box is selectable using top line in the next box in the next row


    */

};
}
