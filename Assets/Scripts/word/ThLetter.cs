using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThLetter : MonoBehaviour
{
    [HideInInspector]
    public string id="none";
    public Text name;
    public GameObject GameControl;
    public GameObject Highlight;
    ThWordGame controll;
    Animator Anim;

    void Start()
    {
        name.text=id;
        controll=GameControl.GetComponent<ThWordGame>();
        Anim=GetComponent<Animator>();
    }
    public void OnClick(){

        if(controll.MainLetter==id){
            controll.CorrectLetterEntered();
            Anim.SetBool("dundun",true);
        }
    }
    public void HighlightOn(bool on)
    {
        Highlight.SetActive(on);
    }
    public void Fin(){
        controll.LetterAnimFin(id.ToCharArray()[0]);
    }
}
