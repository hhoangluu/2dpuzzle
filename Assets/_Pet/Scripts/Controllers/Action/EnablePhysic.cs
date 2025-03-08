using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Pet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnablePhysic : StepAction
    {
        protected override void DoAction()
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
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