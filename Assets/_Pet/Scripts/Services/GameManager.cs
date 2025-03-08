using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using EzBoost;
namespace _Pet
{
    public enum GameState
    {
        Prepare,
        Playing,
        Paused,
        GameLose,
        GameWin
    }

    struct  GameStateChangedEvent
    {
        public GameState oldState;
        public GameState newState;

        public GameStateChangedEvent(GameState oldState, GameState newState)
        {
            this.oldState = oldState;
            this.newState = newState;
        }
    }
    public class GameManager : PersistentSingleton<GameManager>, IEventListener<LoadLevelDoneEvent>,
        IEventListener<GameWinEvent>, IEventListener<GameLoseEvent>
    {
       
        private static bool isRestart;

        public GameState GameState
        {
            get
            {
                return _gameState;
            }
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;

                    EventCenter.TriggerEvent(new GameStateChangedEvent(oldState, _gameState));
                }
            }
        }

        public static int GameCount
        {
            get { return _gameCount; }
            private set { _gameCount = value; }
        }

		public float TimeBetweenInterstitialAd { get; internal set; }

		private static int _gameCount = 0;

        [Header("Set the target frame rate for this game")]
        [Tooltip("Use 60 for games requiring smooth quick motion, set -1 to use platform default frame rate")]
        public int targetFrameRate = 30;

        [Header("Current game state")]
        [SerializeField]
        private GameState _gameState = GameState.Prepare;

        // List of public variable for gameplay tweaking
        [Header("Gameplay Config")]

        [SerializeField]
        private Vector3 startPlayerPosition;



      

      

        void OnEnable()
        {
            EventCenter.AddListener<LoadLevelDoneEvent>(this);
            EventCenter.AddListener<GameWinEvent>(this);
            EventCenter.AddListener<GameLoseEvent>(this);
            UIPlaying.OnHomeButtonClickEvent += UIPlaying_OnHomeButtonClickEvent;
            UIPlaying.OnSkipButtonClickEvent += UIPlaying_OnSkipButtonClickEvent; 

        }

       

        void OnDisable()
        {
            EventCenter.RemoveListener<LoadLevelDoneEvent>(this);
            EventCenter.RemoveListener<GameWinEvent>(this);
            EventCenter.RemoveListener<GameLoseEvent>(this);

            UIPlaying.OnHomeButtonClickEvent -= UIPlaying_OnHomeButtonClickEvent;
            UIPlaying.OnSkipButtonClickEvent += UIPlaying_OnSkipButtonClickEvent;

        }
        private void UIPlaying_OnSkipButtonClickEvent()
        {
            GameWin();
        }
        private void UIPlaying_OnHomeButtonClickEvent()
        {
            GameState = GameState.Prepare;
        }
        void OnDestroy()
        {
          
        }

        // Use this for initialization
        void Start()
        {
            // Initial setup
            Application.targetFrameRate = targetFrameRate;
          //  ScoreManager.Instance.Reset();

            PrepareGame();
        }

        // Update is called once per frame
        void Update()
        {

        }
     
        // Make initial setup and preparations before the game can be played
        public void PrepareGame()
        {
            GameState = GameState.Prepare;

            // Automatically start the game if this is a restart.
            if (isRestart)
            {
                isRestart = false;
               // StartGame();
            }
        }

        // A new game official starts
        public void StartGame()
        {
            StartCoroutine(DelayStartGame());
        }


        IEnumerator DelayStartGame()
        {

            yield return new WaitForEndOfFrame();
            LevelManager.Instance.LoadCurLevel();
            GameState = GameState.Playing;

            //if (SoundManager.Instance.background != null)
            //{
            //    SoundManager.Instance.PlayMusic(SoundManager.Instance.background);
            //}
        }

        // Called when the player died
        public void GameOver()
        {
            Debug.Log("@GAMEMANAGER GAMELOSE");

            GameState = GameState.GameLose;
            GameCount++;
            NextLevel();

        }
        public void GameWin()
        {
            Debug.Log("@GAMEMANAGER GAMEWIN");
            GameState = GameState.GameWin;
            GameCount++;

        }
       
        // Start a new game
        public void RestartLevel(float delay = 0)
        {
            isRestart = true;
            StartCoroutine(CRRestartGame(delay));
        }
        public void ResetLevel(float delay = 0)
        {
            isRestart = true;
            StartCoroutine(CRResetGame(delay));
        }
        public void NextLevel(float delay = 0)
        {
            StartCoroutine(CRNextLevel(delay));
        }
        IEnumerator CRRestartGame(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            LevelManager.Instance.LoadPrevLevel();
        }
        IEnumerator CRResetGame(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            LevelManager.Instance.RestartLevel();
        }
        IEnumerator CRNextLevel(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            LevelManager.Instance.LoadCurLevel();
        }

        public void PauseGame()
        {
            GameState = GameState.Paused;
        }

      

        void IEventListener<LoadLevelDoneEvent>.OnEzEvent(LoadLevelDoneEvent eventType)
        {
            Debug.Log("@GAMEMANAGER Load level done" +eventType.level);
            GameState = GameState.Playing;
        }

        void IEventListener<GameWinEvent>.OnEzEvent(GameWinEvent eventType)
        {
            GameWin();
        }

        void IEventListener<GameLoseEvent>.OnEzEvent(GameLoseEvent eventType)
        {
            ResetLevel();
        }
    }
}