using System.Collections;
using System.Collections.Generic;
using EzBoost;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Pet
{
    public class NumberCheckWrong : EventConditionChecker
    {
        [SerializeField]
        private TMP_InputField input;
        [SerializeField]
        private int numberResult;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Check()
        {
            int number;
            if (int.TryParse(input.text, out number) == false)
            {
                ConditionTrue();
            }
            else {
                if (number != numberResult)
                {
                    ConditionTrue();
                }
            }
        }

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new CheckerEvent(gameObject));
        }
    }
}