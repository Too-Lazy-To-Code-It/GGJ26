using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        public TextMeshProUGUI PlayGameText;
        
       
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void Start()
        {

            var button =playButton.GetComponent<Button>();
            button.onClick.AddListener(goToGame);
        }
        public void goToGame()
        {
            SceneManager.LoadScene("Tutorial");
        }
        public void MoveCurtain()
        {
            rightCurtain.GetComponent<Animator>().SetInteger("Condition",1);
                leftCurtain.GetComponent<Animator>().SetInteger("Condition",1);
                
                startButton.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
                settingsButton.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
                exitButton.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
                playButton.SetActive(true);
            playButton.transform.DOScale(1,1f).SetEase(Ease.InOutSine);

        }
       public void ChangeNameModeGame(string name)
        {
            PlayGameText.text = name;
        }
    }
}