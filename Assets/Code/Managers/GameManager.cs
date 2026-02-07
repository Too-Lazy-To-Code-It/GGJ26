

using System;
using UnityEngine;

namespace Code.Managers
{
    
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }

    public enum SwitchReason
    {
        RNG,
        PowerUp
    }
    public class GameManager : UnityEngine.MonoBehaviour
    {

        public static GameManager Instance { get; private set; }
        
        public GameState State { get; private set; }
        public int ActivePlayerIndex { get; private set; }   
        public int PreviousPlayerIndex { get; private set; }
        public PlayerManager player;
        
        float lastSwitchTime;
        [SerializeField] float switchCooldown = 0.3f;
        
        const int PLAYER_COUNT = 2;


        [ContextMenu("Test Switch")]
        public void TestSwitch()
        {
            TryRandomSwitch(1f); 
            Debug.Log("RNG switch forced via context menu. ActivePlayerIndex: " + ActivePlayerIndex);
            
        }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            State = GameState.Playing;
            SetInitialPlayer(0);
        }

        private void Update()
        {
            if (State != GameState.Playing)
                return;
            
        }

        void SetInitialPlayer(int index)
        {
            ActivePlayerIndex = index;
            PreviousPlayerIndex = -1;

            PlayerInputSystem.instance.SetActivePlayer(index);
        }
        
        public void SwitchActivePlayer(SwitchReason reason)
        {
            
            if (Time.time - lastSwitchTime < switchCooldown)
                return;

            lastSwitchTime = Time.time;
            
            if (State != GameState.Playing)
                return;

            PreviousPlayerIndex = ActivePlayerIndex;
            ActivePlayerIndex = 1 - ActivePlayerIndex;

            PlayerInputSystem.instance.SetActivePlayer(ActivePlayerIndex);
        }

        public void TryRandomSwitch(float probability)
        {

            if (UnityEngine.Random.value < probability)
                SwitchActivePlayer(SwitchReason.RNG);
        }
    }
}