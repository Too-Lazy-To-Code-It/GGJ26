

using System;

namespace Code.Managers
{
    public class GameManager : UnityEngine.MonoBehaviour
    {
        public int gameState = 0;
        public int playerInCharge = 0;
        public int lastInCharge = 0;
        public PlayerManager player1;
        public PlayerManager player2;

        private void Start()
        {
            
        }
    }
}