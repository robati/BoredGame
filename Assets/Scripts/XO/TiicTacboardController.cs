using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace xo
{
        public enum PLAYER {X,O,None};

    public class TiicTacboardController : MonoBehaviour
    {
        static public bool heartTurn = true;
        public List<TicTacButton> Buttons;
        public Text text;
        private bool isOnePlayer = false;
        private bool isAIgame = false;
        private bool hasSomeoneWon = false;
        private bool locked = false;
        static public bool needToUpdateGame = false;
        // Use this for initialization
        void Start()
        {
            isOnePlayer = (levelSelector.gameStatus != gameState.multiplayer);
            isAIgame= (levelSelector.gameStatus == gameState.AI);
            // if(isAIgame){//pakkon
            //     heartTurn=false;
            // }
            playAgain();
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasSomeoneWon)
            {
                 if (needToUpdateGame)//if player changed something
                {
                    checkForUpdate();
                    needToUpdateGame = false;
                }
                else if (isOnePlayer && !locked)//the AI has not already locked the game
                {
                    if (!heartTurn && !hasSomeoneWon)//double check needed unless when o wins x plays
                    {
                        int number=0;
                        if(isAIgame){
                             number= AIfindBestNumber();

                        }  
                        else {
                             number = Random.Range(0, 9);//random AI.
                        }
                        if (Buttons[number].state == state.unUsed)
                        {
                            StartCoroutine(rivalPlay(number));
                        }
                    
                    }
                }

               

               
            }

            
        }
        PLAYER checkDiagon(List<TicTacButton> Buttons){
            if (isSame(Buttons[0], Buttons[4], Buttons[8]) || isSame(Buttons[2], Buttons[4], Buttons[6])){
                text.text = Buttons[4].state == state.ex ? "x wins!!!":"o wins!!!";
                return  Buttons[4].state == state.ex ? PLAYER.X:PLAYER.O;
            }
            else{
                return PLAYER.None;
            }
        }
        
        PLAYER checkHorizonal(List<TicTacButton> Buttons){
            for (int i = 0; i < 3; i++){
                if (isSame(Buttons[i], Buttons[i + 3], Buttons[i + 6])){

                    text.text = Buttons[i].state == state.ex ? "X wins!!!" : "O wins!!!";
                    return  Buttons[i].state == state.ex ? PLAYER.X:PLAYER.O;
                }               
            }
                return PLAYER.None;
        }
        
        PLAYER checkVertical(List<TicTacButton> Buttons){
            for (int i = 0; i < 8; i += 3){
                if (isSame(Buttons[i], Buttons[i + 1], Buttons[i + 2])){
                   
                    text.text = Buttons[i].state == state.ex ? "X wins!!!" : "O wins!!!";
                    return  Buttons[i].state == state.ex ?  PLAYER.X:PLAYER.O;
                }
            }
                return PLAYER.None;
            }
        

        void checkForUpdate(){

            PLAYER a=checkDiagon(Buttons);
            PLAYER b=checkVertical(Buttons);
            PLAYER c=checkHorizonal(Buttons);

            if(!(a==PLAYER.None && b==PLAYER.None && c==PLAYER.None)){
                finishGame();
                return;
            }
           
            if (isGameFinished(Buttons))
            {
                finishGame();
                text.text = "it's a tie";
                return;
            }

        }
        bool isSame(TicTacButton a, TicTacButton b , TicTacButton c)
        {
            if (a.state == b.state && b.state == c.state && b.state !=state.unUsed)
                return true;
            return false;
        }
        void finishGame()
        {
            hasSomeoneWon = true;
            lockButtons(true);

        }
        void lockButtons(bool flag)
        {
            foreach (TicTacButton button in Buttons)
            {
                if(button.state==state.unUsed)
                    button.enable(!flag);
            }
        }
         int AIfindBestNumber(){
             bool flag=false;
             for(int i=0;i<9;i++){
                 if(Buttons[i].state!=state.unUsed)
                 flag=true;
             }
            if(!flag)
                return 4;
            int a =findBestMove(Buttons);
            Debug.Log(a);
            return a;
        }
        bool canMarkButton(int number,List<TicTacButton> buttons){

            return buttons[number].state == state.unUsed;
        }
        public void playAgain()
        {
            hasSomeoneWon = false;
            text.text = "game on!";
            foreach (TicTacButton button in Buttons)
            {
                button.reset();
            }
            //  if(isAIgame){//pakkon
            //     heartTurn=false;
            // }
        }

        public void back()
        {
            SceneManager.LoadScene(0);
        }
        IEnumerator rivalPlay(int number)
        {
            
            lockGame(true);
            text.text = "wait";
            yield return new WaitForSeconds(0.5f);
            text.text = "wait is over";
            Buttons[number].toggle();
            text.text = "your turn";

            lockGame(false);
        }
        bool isGameFinished(List<TicTacButton> Buttons)
        {
            for (int i = 0; i < 9; i++){
                if (Buttons[i].state == state.unUsed)

                    return false;
            }
            return true;
        }
        void lockGame(bool flag)
        {
            locked = flag;
            lockButtons(flag);

        }
    
    int evaluate(List<TicTacButton> Buttons)
        {
            PLAYER a=checkDiagon(Buttons);
            PLAYER b=checkVertical(Buttons);
            PLAYER c=checkHorizonal(Buttons);

            if(a==PLAYER.X||b==PLAYER.X||c==PLAYER.X)
                return 10;
            else if(a==PLAYER.O||b==PLAYER.O||c==PLAYER.O)
                return -10;
            else
                return 0 ;
        }
 
    int minimax(List<TicTacButton> Buttons, int depth, bool isMax)
    {
        int score = evaluate(Buttons);
    // Debug.Log(depth+" "+score);
        if (score == 10)
            return score-depth;

        if(score == -10)
            return score-depth;//?

        if (isGameFinished(Buttons))
            return 0;

        if (isMax){

            int best = -1000;
            for (int i = 0; i<9; i++){
                if (canMarkButton(i,Buttons)){

                    Buttons[i].state=state.ex;
                    int best2=minimax(Buttons, depth+1, !isMax);
                    best = best2>best?best2:best;
                    Buttons[i].state=state.unUsed; 
                }
            }
            return best;
        }
        else{

            int best = 1000;
            for (int i = 0; i<9; i++){
                if (canMarkButton(i,Buttons)){

                    Buttons[i].state=state.heart;
                    int best2 = minimax(Buttons, depth+1, !isMax) ;
                    best = best2<best?best2:best;
                    Buttons[i].state=state.unUsed;  
                }
            }
            return best;
        }
    }
    
    // This will return the best possible move for the player
    int findBestMove(List<TicTacButton> Buttons)
    {
        int bestVal = -1000;
        int bestVal2 = -1000;
        int bestMove = -1;
        for (int i = 0; i<9; i++){
            if (canMarkButton(i,Buttons)){

                Buttons[i].state=state.ex;
                bestVal2 = minimax(Buttons, 0, false);
                Debug.Log("x"+i+"="+bestVal2);
                Buttons[i].state=state.unUsed;
                }
            if (bestVal2 > bestVal){

                bestMove= i;
                bestVal = bestVal2;
                }
        }
         
        return bestMove;
    }                   
 
   };
}