using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Pet
{
    public class GravityCheck : EventConditionChecker
    {
        [SerializeField]
        private Vector2 gravity;

        private bool checking = true;

        protected override void FireEvent()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (checking)
            {
                if (Physics2D.gravity == gravity)
                {
                    ConditionTrue();
                    checking = false;
                }
            }
        }
    }
}
