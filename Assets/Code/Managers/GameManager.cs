using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

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

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState State { get; private set; }

        [Header("Player refs")] public int ActivePlayerIndex { get; private set; }
        public int PreviousPlayerIndex { get; private set; }
        public PlayerManager player;
        public SpriteResolver spriteResolver;
        public string labelResolver = "Idle";
        public string categoryResolver;


        float lastSwitchTime;
        [SerializeField] float switchCooldown = 0.3f;
        [SerializeField] private GameObject normalBackground;
        [SerializeField] private GameObject reversedBackground;

        const int PLAYER_COUNT = 2;


        [ContextMenu("Test Switch")]
        public void TestSwitch()
        {
            TryRandomSwitch(1f);
            Debug.Log("RNG switch forced via context menu. ActivePlayerIndex: " + ActivePlayerIndex);
        }

        public void InitBackground()
        {
            normalBackground.SetActive(ActivePlayerIndex == 0);
            reversedBackground.SetActive(ActivePlayerIndex == 1);
        }

        public void InitPlayerSprite()
        {
            if (ActivePlayerIndex == 0)
                categoryResolver = "Human";
            else if (ActivePlayerIndex == 1)
                categoryResolver = "Mask";
            spriteResolver.SetCategoryAndLabel(categoryResolver, labelResolver);
        }

        public void SetAnimationSprite(string spriteName)
        {
            spriteResolver.SetCategoryAndLabel(categoryResolver, spriteName);
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
            spriteResolver = player.GetComponent<SpriteResolver>();
            InitBackground();
            InitPlayerSprite();
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
            ChangeGravityAndSprite();
            PlayerInputSystem.instance.humanInCharge = !PlayerInputSystem.instance.humanInCharge;
            InitBackground();
            InitPlayerSprite();
        }

        public void TryRandomSwitch(float probability)
        {
            if (UnityEngine.Random.value < probability)
                SwitchActivePlayer(SwitchReason.RNG);
        }

        public void ChangeGravity()
        {
            player.ChangePlayerJumpPushToNegative();
        }

        public void ChangeGravityAndSprite()
        {
            ChangeGravity();
           if (ActivePlayerIndex == 1)
                player.transform.rotation = Quaternion.Euler(180, 0, 0);
            else
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}