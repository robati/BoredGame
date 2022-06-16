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
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void onePlayer()
        {
            // SceneManager.LoadScene("main");
            gameStatus = gameState.single;
        }
        public void multiPlayer()
        {
            // SceneManager.LoadScene("main");
            gameStatus = gameState.multiplayer;
        }
         public void AIPlayer()
        {
            // SceneManager.LoadScene("main");
            gameStatus = gameState.AI;
        }
        public void onExitClicked()
        {
            Application.Quit();
        }
    }
}