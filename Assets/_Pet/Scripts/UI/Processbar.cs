using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Pet.UI
{
    public class Processbar : MonoBehaviour
    {
        [SerializeField]
        Image processer;
       
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Add(float amount)
        {
            processer.fillAmount += amount;
        }
    }
}