using System;
using System.Collections;
using System.Collections.Generic;
using EzBoost;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Pet
{
    struct LoadLevelDoneEvent
    {
        public int level;

        public LoadLevelDoneEvent (int level){
            this.level = level;
        }
    }
    public class LevelManager : PersistentSingleton<LevelManager>, IEventListener<GameStateChangedEvent>
    {
        public int MaxLevel;

        public int CurLevelIndex
        {
            get
            {
                return ES3.Load("CUR_LEVEL_INDEX", 1);
            }
            private set
            {
                if (value > MaxLevel)
                {
                    ES3.Save("CUR_LEVEL_INDEX", 1);
                }
                else
                {
                    ES3.Save("CUR_LEVEL_INDEX", value);
                }
            }
        }
        public static event System.Action<int> onLevelChange;
        private GameObject NextLevel;
        [HideInInspector]
        public Level CurLevel;
       
        private GameObject CurLevelPrefab;
#if UNITY_EDITOR
        [SerializeField]
        private bool isHackLevel;
        [ShowIf("isHackLevel")]
        [SerializeField]
        public int hackLevel;
#endif
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR
            if (isHackLevel)
            CurLevelIndex = hackLevel;
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnEnable()
        {
            EventCenter.AddListener<GameStateChangedEvent>(this);
            UIPlaying.OnHomeButtonClickEvent += UIPlaying_OnHomeButtonClickEvent;
        }

        private void UIPlaying_OnHomeButtonClickEvent()
        {
            NextLevel = CurLevelPrefab;
            Destroy(CurLevel.gameObject);
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener<GameStateChangedEvent>(this);
            UIPlaying.OnHomeButtonClickEvent -= UIPlaying_OnHomeButtonClickEvent;

        }

        public void LoadLevel(int level)
        {
            Debug.Log("LoadLevel " + level);
           
            StartCoroutine(CR_LoadLevelAsync(level));
        }

        IEnumerator CR_LoadLevelAsync(int level)
        {
            if (NextLevel == null && CurLevel == null)
            {
                AsyncOperationHandle<GameObject> loadWithIResourceLocations = Addressables.LoadAssetAsync<GameObject>("Assets/_Pet/Prefabs/Levels/Level" + level + ".prefab");
                yield return loadWithIResourceLocations;

                CurLevel = Instantiate(loadWithIResourceLocations.Result).GetComponent<Level>();
                CurLevelPrefab = loadWithIResourceLocations.Result;
            }
            else 
            {
                Debug.Log("Load by pre " + CurLevel);
                if (CurLevel)
                {
                    Addressables.ReleaseInstance(CurLevel.gameObject);

                    Destroy(CurLevel.gameObject);
                }
                yield return new WaitWhile(() => NextLevel == null);
                CurLevel = Instantiate(NextLevel).GetComponent<Level>();
                CurLevelPrefab = NextLevel;
            }
            EventCenter.TriggerEvent(new LoadLevelDoneEvent(level));
            PreLoadNextLevel();
        }

        public void LoadCurLevel()
        {
            LoadLevel(CurLevelIndex);
        }

        void IEventListener<GameStateChangedEvent>.OnEzEvent(GameStateChangedEvent eventType)
        {
            if (eventType.newState == GameState.GameWin)
            {
                CurLevelIndex++;
            }

        }
        public void RestartLevel()
        {
            
                if (CurLevel)
                {
                    Destroy(CurLevel.gameObject);
                }
                CurLevel = Instantiate(CurLevelPrefab).GetComponent<Level>();

                EventCenter.TriggerEvent(new LoadLevelDoneEvent(CurLevelIndex));
            
        }
        public void LoadPrevLevel()
        {
            CurLevelIndex--;
            if (CurLevelIndex <= 0) CurLevelIndex = MaxLevel;
            if (CurLevel)
            {
                if (CurLevel)
                {
                    Destroy(CurLevel.gameObject);
                }
                CurLevel = Instantiate(CurLevelPrefab).GetComponent<Level>();
                Debug.Log("Load restart level " + CurLevelIndex);
                EventCenter.TriggerEvent(new LoadLevelDoneEvent(CurLevelIndex));
            }
        }

        void PreLoadNextLevel()
        {
            NextLevel = null;
            if (CurLevelIndex == MaxLevel)
            {
                CurLevelIndex = 0;
            }
            AsyncOperationHandle<GameObject> loadWithIResourceLocations = Addressables.LoadAssetAsync<GameObject>("Assets/_Pet/Prefabs/Levels/Level" +( CurLevelIndex + 1 ).ToString()+ ".prefab");
            loadWithIResourceLocations.Completed += LoadWithIResourceLocations_Completed;
        }

        public AsyncOperationHandle<GameObject> LoadLastLevel()
        {
            NextLevel = null;
            if (CurLevelIndex >= MaxLevel)
            {
                CurLevelIndex = 1;
            }
            Debug.Log("Load Last level " + CurLevelIndex);

            AsyncOperationHandle<GameObject> loadWithIResourceLocations = Addressables.LoadAssetAsync<GameObject>("Assets/_Pet/Prefabs/Levels/Level" + (CurLevelIndex).ToString() + ".prefab");
            loadWithIResourceLocations.Completed += LoadWithIResourceLocations_Completed;
            return loadWithIResourceLocations;
        }

        private void LoadWithIResourceLocations_Completed(AsyncOperationHandle<GameObject> obj)
        {
            Debug.Log("Loadded " + obj.Result.name);
            NextLevel = obj.Result;
        }
    }
}