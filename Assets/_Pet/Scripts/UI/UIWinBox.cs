using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Pet
{
    public class UIWinBox : MonoBehaviour
    {
        [SerializeField]
        Button nextLevelButton;
        [SerializeField]
        Button restartButton;
        // Start is called before the first frame update
        void Start()
        {
            nextLevelButton.onClick.AddListener(() => UIManager.Instance.NextButtonClick());
            restartButton.onClick.AddListener(() => UIManager.Instance.RestartButtonClick());

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
