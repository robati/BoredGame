using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace xo
{
    public enum gameState {single,multiplayer,AI};
    public class levelSelector : MonoBehaviour
    {

        static public gameState gameStatus;

        public void onePlayer()
        {
            gameStatus = gameState.single;
        }
        public void multiPlayer()
        {
            gameStatus = gameState.multiplayer;
        }
         public void AIPlayer()
        {
            gameStatus = gameState.AI;
        }
        public void onExitClicked()
        {
            Application.Quit();
        }
    }
}