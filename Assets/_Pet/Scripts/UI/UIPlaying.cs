using System;
using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;
using UnityEngine.UI;

namespace _Pet
{
    public class UIPlaying : MonoBehaviour
    {
        public int Level { get => LevelManager.Instance.CurLevelIndex; }
        [SerializeField]
        private Button homeBtn;
        [SerializeField]
        private Button hintBtn;
        [SerializeField]
        private Button skipBtn;
        [SerializeField]
        private Button videoBtn;
        [SerializeField]
        private HintPopUp hintPopUp;
        public static event System.Action OnHintButtonClickEvent;
        public static event System.Action OnHomeButtonClickEvent;
        public static event System.Action OnSkipButtonClickEvent;
        


        // Start is called before the first frame update
        void Start()
        {
            homeBtn.onClick.AddListener(() => OnHomeButtonClick());
            hintBtn.onClick.AddListener(() => OnHintButtonClick());
            skipBtn.onClick.AddListener(() => OnSkipButtonClick());
        }

	

		private void OnDestroy()
        {
            homeBtn.onClick.RemoveListener(() => OnHomeButtonClick());
            hintBtn.onClick.RemoveListener(() => OnHintButtonClick());
            skipBtn.onClick.RemoveListener(() => OnSkipButtonClick());
        }

        // Update is called once per frame
        private void OnHomeButtonClick()
        {
            OnHomeButtonClickEvent?.Invoke();
        }

        private void OnHintButtonClick()
        {
            if (KeyManager.Instance.RemoveKey(20) == true)
            {
                OnHintButtonClickEvent?.Invoke();
            }
        }

        private void OnSkipButtonClick()
        {
            if (KeyManager.Instance.RemoveKey(50) == true)
            {
                OnSkipButtonClickEvent?.Invoke();
            }
        }


    }
}
