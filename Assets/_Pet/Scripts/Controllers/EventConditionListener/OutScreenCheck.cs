using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    struct OutScreenEvent
    {
        public GameObject gameObject;

        public OutScreenEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
    public class OutScreenCheck : EventConditionChecker
    {
        Renderer renderer;
        private  bool checking = true;

        private int count;

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent<OutScreenEvent>(new OutScreenEvent(this.gameObject));
        }

      


        // Start is called before the first frame update
        void Start()
        {

            renderer = objectTarget.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            
            Debug.Log("renderer.isVisible " +renderer.isVisible);
            if (checking)
            {
                if (renderer.isVisible == false)
                {
                    count++; ;
                    Debug.Log("invi");
                    if (count >= 3)
                    {
                        ConditionTrue();
                        checking = false;
                    }
                }
            }

        }
    }
}