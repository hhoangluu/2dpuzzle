using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{

    struct CountDownCheckEvent
    {
        public GameObject gameObject;

        public CountDownCheckEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    public class CountDownCheck : EventConditionChecker     
    {
        [SerializeField]
        private float amount;
        private bool checking;
        private float originAmount;
        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new CountDownCheckEvent(gameObject));
        }

        // Start is called before the first frame update
        void Start()
        {
            checking = true;
            originAmount = amount;
        }

        // Update is called once per frame
        void Update()
        {
            if (amount > 0)
            {
                amount -= Time.deltaTime;
            }
            else if (checking)
            {
                ConditionTrue();
                checking = false;
            }
        }

        public void ResetCountDown()
        {
            amount = originAmount;
        }

    }
}