using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Code.Managers
{
    public class UiManager : MonoBehaviour
    {
        public GameObject rightCurtain;
        public GameObject leftCurtain;
        public GameObject startButton;
        public GameObject settingsButton;
        public GameObject exitButton;
        public GameObject playButton;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void MoveCurtain()
        {
            rightCurtain.GetComponent<Animator>().SetInteger("Condition",1);
                leftCurtain.GetComponent<Animator>().SetInteger("Condition",1);
                
                startButton.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
                settingsButton.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
                exitButton.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
                playButton.SetActive(true);
                exitButton.transform.DOScale(1,1f).SetEase(Ease.InOutSine);
        }
       
    }
}