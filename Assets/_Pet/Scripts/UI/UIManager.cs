using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EzBoost;
namespace _Pet
{
    public class UIManager : PersistentSingleton<UIManager>, IEventListener<GameStateChangedEvent>
    {
        [SerializeField]
        UIHome UIHome;
        [SerializeField]
        UIWinBox UIWinBox;
        [SerializeField]
        UIPlaying UIPlaying;
        // Start is called before the first frame update

        private void OnEnable()
        {
            EventCenter.AddListener<GameStateChangedEvent>(this);
        }

       
        private void OnDisable()
        {
            EventCenter.RemoveListener<GameStateChangedEvent>(this);
        }
      

        void Start()
        {
            ShowHome();
        }

        public void ShowHome()
        {
            UIHome.gameObject.SetActive(true);
            UIWinBox.gameObject.SetActive(false);
            UIPlaying.gameObject.SetActive(false);

        }

        public void ShowWin()
        {
            UIHome.gameObject.SetActive(false);
            UIWinBox.gameObject.SetActive(true);
            UIPlaying.gameObject.SetActive(false);

        }

        public void ShowPlaying()
        {
            UIHome.gameObject.SetActive(false);
            UIWinBox.gameObject.SetActive(false);
            UIPlaying.gameObject.SetActive(true);
        }
        // Update is called once per frame
        void Update()
        {

        }
        #region Home
        public void StartButtonClick()
        {
            GameManager.Instance.StartGame();
        }
        #endregion 

        #region Win Box
        public void RestartButtonClick()
        {
            GameManager.Instance.RestartLevel();
        }

        public void NextButtonClick()
        {
            GameManager.Instance.NextLevel();
        }
        #endregion


        void IEventListener<GameStateChangedEvent>.OnEzEvent(GameStateChangedEvent eventType)
        {

            if (eventType.newState == GameState.GameWin)
            {
                ShowWin();
            }
            if (eventType.newState == GameState.Prepare)
            {
                ShowHome();
            }
            if (eventType.newState == GameState.Playing)
            {
                ShowPlaying();
            }
        }
    }
}