using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Pet
{
    public class UIHome : MonoBehaviour
    {
        [SerializeField]
        Button startButton;
        // Start is called before the first frame update
        void Start()
        {
            startButton.onClick.AddListener(() => UIManager.Instance.StartButtonClick());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
