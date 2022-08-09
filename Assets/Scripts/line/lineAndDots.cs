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

    public Sprite startWin;
    public Sprite lightningWin;

    public Image insidePic;

    public UnityEvent rightButtonEvent = new UnityEvent();
    public UnityEvent topButtonEvent = new UnityEvent();


    public Button[] buttons = new Button[4];
    public Image[] buttonsIm = new Image[4];
    public List<int> buttonnumbers= new List<int> { 0, 1, 2, 3 };
    public bool isFinishedBox = false;
    public bool isStarWinner = false;

	void Update () {

	}
    public void abtest()
    {

    }
    public void OnLineClicked(int buttonnumber)
    {
        toggel(buttonnumber);
        gameControl.lightningTurn = !gameControl.lightningTurn;
    }
    public void toggel(int buttonnumber, bool otherColor = false)
    {
        Debug.Log(buttonnumber +"is on");
        if (buttonnumber== (int)buttonname.rightButton)
            rightButtonEvent.Invoke();
        if (buttonnumber == (int)buttonname.topButton)
            topButtonEvent.Invoke();

       int idx= buttonnumbers.LastIndexOf((int)buttonnumber);
        //Image _image = buttons[idx].GetComponent<Image>();

        if(otherColor)
            buttonsIm[idx].sprite = lineselected2;
            // _image.sprite = lineselected2;
        else
            buttonsIm[idx].sprite = lineselected1;
        buttons[idx].enabled=false;
        if (isFinished())
        {
            isFinishedBox = true;
            if (gameControl.lightningTurn)
            {
                insidePic.sprite = lightningWin;
                isStarWinner = false;
            }
            else
            {
                insidePic.sprite = startWin;
                isStarWinner = true;
            }
        }
    }

public void back()
        {
            SceneManager.LoadScene(0);
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
