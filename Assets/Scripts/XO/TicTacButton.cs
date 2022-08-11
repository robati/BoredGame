using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xo
{
   public enum state { unUsed,heart,ex}

   
    public class TicTacButton : MonoBehaviour
    {
        public Sprite heartSprite;
        public Sprite exSprite;
        public state state = state.unUsed;

        public Image _image;
        public Sprite noneImage;
        private Button _button;
        void Awake()
        {
            _button = GetComponent<Button>();
        }
         //when box marked it's sprite and state change and player turn changes.
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