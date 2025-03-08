using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Pet {
    public class Move : StepAction
    {
        [GUIColor(1, 1, 1)]
        [SerializeField]
        private Transform targetMove;

        [GUIColor(1, 1, 1)]
        [SerializeField]
        private float duration;

        private bool canMove;

        private float startTime;
        private Vector2 startPosition;
        protected override void DoAction()
        {
            canMove = true;
            startPosition = transform.position;
            startTime = Time.time;

        }

        [SerializeField]

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                if ((Time.time - startTime) / duration >= 1)
                {
                    transform.position = targetMove.position;
                    canMove = false;
                }
                else
                {
                    transform.position = Vector2.Lerp(startPosition, targetMove.position, (Time.time - startTime) / duration);
                }
            }
        }
    }
}
