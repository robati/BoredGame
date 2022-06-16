using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xo
{
   public enum state { unUsed,heart,ex}

   
    public class TicTacButton : MonoBehaviour
    {

        // Use this for initialization
        public Sprite heartSprite;
        public Sprite exSprite;
        public state state = state.unUsed;

        public Image _image;
        public Sprite noneImage;
        private Button _button;
        void Awake()
        {
            // _image = GetComponentInChildren<Image>();
            _button = GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {

        }
         
        public void toggle()
        {
                if (TiicTacboardController.heartTurn)
                {
                    _image.sprite = heartSprite;
                    state = state.heart;
                }
                else
                {
                    _image.sprite = exSprite;
                    state = state.ex;
                }
                _button.enabled = false;
            TiicTacboardController.needToUpdateGame = true;
            TiicTacboardController.heartTurn = !TiicTacboardController.heartTurn;
        }
        public void enable(bool flag)
        {
           _button.enabled = flag;
        }
        public void reset()
        {
            enable(true);
            state = state.unUsed;
            _image.sprite = noneImage;
        }
    }
    
}