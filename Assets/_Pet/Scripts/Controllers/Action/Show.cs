using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Pet
{
    public class Show : StepAction
    {
        protected override void DoAction()
        {

            GetComponent<Renderer>().enabled = true;
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