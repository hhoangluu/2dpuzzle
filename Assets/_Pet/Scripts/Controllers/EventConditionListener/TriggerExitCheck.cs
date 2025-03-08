using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    struct ObjectExitTriggerEvent
    {
        public GameObject gameObject;

        public ObjectExitTriggerEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class TriggerExitCheck : EventConditionChecker
    {
        void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (objectTarget)
            {
                if (collision.gameObject.GetInstanceID() == objectTarget.GetInstanceID())
                {
                    ConditionTrue();
                }
            }
        }

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new ObjectExitTriggerEvent(gameObject));
        }
    }
}