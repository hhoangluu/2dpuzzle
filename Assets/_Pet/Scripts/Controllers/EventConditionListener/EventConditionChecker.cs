using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Pet
{
    [Serializable]
    public abstract class StepAction: MonoBehaviour
    {
        protected abstract void DoAction();
        public void DelayAction()
        {
            try
            {
                gameObject.SetActive(true);
                StartCoroutine(CR_DoAction());
            }
            catch (Exception ex)
            {
                Debug.LogError("Delay do action error" + ex.Message);
            }
        }

        public IEnumerator CR_DoAction()
        {
            yield return new WaitForSeconds(delay);
            DoAction();
        }
        [GUIColor(1, 1, 1)]
        [SerializeField]
        private float delay;
    }
    struct CheckEvent
    {
        public GameObject gameObject;

        public CheckEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    struct TouchEvent
    {

        public Vector2 position;

        public TouchEvent(Vector2 pos)
        {
            this.position = pos;
        }
    }

    [DisallowMultipleComponent]
    public abstract class EventConditionChecker : MonoBehaviour
    {
        [SerializeField]
        protected GameObject objectTarget;

        [DisableIf ("@this.objectTarget == null")]
        [GUIColor(1,0, 0)]
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public List<StepAction> stepAction;

        [SerializeField]
        protected bool AlwayDoActionAllowed = false;

        public void DoAllAction()
        {
            foreach (var step in stepAction)
            {
                step.DelayAction();
            }
        }

        protected virtual void ConditionTrue()
        {
            if (AlwayDoActionAllowed)
            {
                DoAllAction();
            }
            FireEvent();
        }

        protected abstract void FireEvent();
    }
}