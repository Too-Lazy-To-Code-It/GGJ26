using System;
using UnityEngine;

namespace Code.Managers
{
    public class UiManager : MonoBehaviour
    {
        public GameObject rightCurtain;
        public GameObject leftCurtain;
        public GameObject startButton;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
    }
}