using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Code.Managers
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance;
        public Sprite spriteHuman;
        public Sprite spriteMask;
        public SpriteRenderer spriteRenderer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void SwapSprites()
        {
            GameManager.Instance.categoryResolver = "none";
            if (spriteRenderer.sprite.name == spriteHuman.name)
            {
                spriteRenderer.sprite = spriteMask;
            }
            else
            {
                spriteRenderer.sprite = spriteHuman;
            }
            
        }
    }
}