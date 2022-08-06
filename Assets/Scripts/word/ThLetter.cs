using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThLetter : MonoBehaviour
{
    // Start is called before the first frame update
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
                // Debug.Log("K "+controll.MainLetter+","+id);

        if(controll.MainLetter==id){
            controll.CorrectLetterEntered();
            Anim.SetBool("dundun",true);

        }
        // else
        // Debug.Log(controll.MainLetter+","+id);
    }
    // Update is called once per frame
    public void HighlightOn(bool on)
    {
        Highlight.SetActive(on);
    }
    public void Fin(){
        controll.LetterAnimFin(id.ToCharArray()[0]);
    }
}
