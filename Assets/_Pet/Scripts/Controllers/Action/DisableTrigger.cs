using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Pet
{
    [RequireComponent(typeof(Collider2D))]
    public class DisableTrigger : StepAction
    {
        protected override void DoAction()
        {
            GetComponent<Collider2D>().isTrigger = false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}