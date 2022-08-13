using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace xo
{
public enum buttonname { leftButton=0, rightButton=1, bottomButton=2, topButton=3 };
public class lineAndDots : MonoBehaviour {


    public Sprite lineselected1;
    public Sprite lineselected2;

    public Sprite xWin;
    public Sprite oWin;

    public Image insidePic;

    public UnityEvent rightButtonEvent = new UnityEvent();
    public UnityEvent topButtonEvent = new UnityEvent();


    public Button[] buttons = new Button[4];
    public Image[] buttonsIm = new Image[4];
    public bool isFinishedBox = false;
    public bool isStarWinner = false;

    public void OnLineClicked(int buttonnumber)
    {
        toggel(buttonnumber);
        gameControl.oTurn = !gameControl.oTurn;
    }
    //call event to select the line in next box too. set color for the selected line. set isFinishedBox variable.
    public void toggel(int buttonnumber, bool otherColor = false)
    {
        if (buttonnumber== (int)buttonname.rightButton)
            rightButtonEvent.Invoke();

        if (buttonnumber == (int)buttonname.topButton)
            topButtonEvent.Invoke();



        if( otherColor || gameControl.oTurn )
            buttonsIm[buttonnumber].sprite = lineselected2;
        else
            buttonsIm[buttonnumber].sprite = lineselected1;

        buttons[buttonnumber].enabled=false;

        if (isFinished())
        {
            isFinishedBox = true;
            if (gameControl.oTurn)
            {
                insidePic.sprite = oWin;
                isStarWinner = false;
            }
            else
            {
                insidePic.sprite = xWin;
                isStarWinner = true;
            }
        }
    }




    bool isFinished()
    {
        foreach (Button button in buttons)
        {
            if (button.enabled)
                return false; 
        }
        return true;
    }


    
};
}
