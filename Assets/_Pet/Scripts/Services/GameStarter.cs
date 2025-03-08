using System.Collections;
using System.Collections.Generic;
using _Pet.UI;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

namespace _Pet
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField]
        Processbar processbar;

        private float loadingPercent = 0;

        public float LoadingPercent { get => loadingPercent; set  {
                loadingPercent = value;
                if (loadingPercent >= 1)
                {
                   StartCoroutine( LoadScene());
                }

            }
        }

        // Start is called before the first frame update
        void Start()
        {
            LocalizationSettings.InitializationOperation.Completed += InitializationOperation_Completed;
            LoadLastLevel();
        }

		private void InitializationOperation_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<LocalizationSettings> obj)
        {
            Debug.Log("@GAMESTARTER Init LocalizationSettings Complete");
            processbar.Add(0.65f);
            LoadingPercent += 0.65f;
        }

        private void LoadLastLevel()
        {
           var asyncOperationHandle = LevelManager.Instance.LoadLastLevel() ;
            asyncOperationHandle.Completed += LoadLastLevel_Completed;
        }

        private void LoadLastLevel_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            Debug.Log("@GAMESTARTER Load Last Level Complete");
            processbar.Add(0.35f);
            LoadingPercent += 0.35f;
        }

     

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        }
    }
}