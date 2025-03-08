using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using EzBoost;
namespace _Pet
{
    struct GameWinEvent
    {

    }
    struct GameLoseEvent
    {

    }
    struct CheckerEvent
    {
        public GameObject gameObject;

        public CheckerEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
    public class Condition : MonoBehaviour, IEventListener<ObjectTriggerEvent>,
        IEventListener<DistanceCheckEvent>, IEventListener<GameStateChangedEvent>,
        IEventListener<TouchTriggerEvent>, IEventListener<OutScreenEvent>,
        IEventListener<EraseAllEvent>, IEventListener<DeviceRotationEvent>,
        IEventListener<ObjectExitTriggerEvent>, IEventListener<RotationCheckEvent>,
        IEventListener<CountDownCheckEvent>, IEventListener<CheckerEvent>

    {
        [SerializeField]
        float delayWin;
        
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public List<EventConditionChecker> ListEventConditionWin;
        [SerializeField]
        public bool IsHasOrder;

        public bool IsHasConditionLose;
        [ShowIf("IsHasConditionLose")]
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public List<EventConditionChecker> ListEventConditionLose;
        
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public List<StepAction> ActionsWin;
        [GUIColor(1, 0, 0)]
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        [ShowIf("IsHasConditionLose")]
        public List<StepAction> ActionsLose;

        private int stepIndex = 0;

        private bool listenChecker = true;

        private void OnEnable()
        {
            EventCenter.AddListener<DistanceCheckEvent>(this);
            EventCenter.AddListener<ObjectTriggerEvent>(this);
            EventCenter.AddListener<GameStateChangedEvent>(this);
            EventCenter.AddListener<TouchTriggerEvent>(this);
            EventCenter.AddListener<OutScreenEvent>(this);
            EventCenter.AddListener<EraseAllEvent>(this);
            EventCenter.AddListener<DeviceRotationEvent>(this);
            EventCenter.AddListener<ObjectExitTriggerEvent>(this);
            EventCenter.AddListener<RotationCheckEvent>(this);
            EventCenter.AddListener<CountDownCheckEvent>(this);
            EventCenter.AddListener<CheckerEvent>(this);

        }
        private void OnDisable()
        {
            EventCenter.RemoveListener<DistanceCheckEvent>(this);
            EventCenter.RemoveListener<ObjectTriggerEvent>(this);
            EventCenter.RemoveListener<GameStateChangedEvent>(this);
            EventCenter.RemoveListener<TouchTriggerEvent>(this);
            EventCenter.RemoveListener<OutScreenEvent>(this);
            EventCenter.AddListener<DeviceRotationEvent>(this);
            EventCenter.RemoveListener<EraseAllEvent>(this);
            EventCenter.AddListener<ObjectExitTriggerEvent>(this);
            EventCenter.RemoveListener<RotationCheckEvent>(this);
            EventCenter.RemoveListener<CountDownCheckEvent>(this);
            EventCenter.RemoveListener<CheckerEvent>(this);
        }
        private void Start()
        {

        }

        private bool CheckValidGameObjectFireEventWin(GameObject gameObjectEvent)
        {

            foreach (var checker in ListEventConditionWin)
            {
               if (checker.gameObject.GetInstanceID() == gameObjectEvent.GetInstanceID())
                {
                    //  int index = ListEventConditionWin.FindIndex(0, ListEventConditionWin.Count, condition => condition == eventType.gameObject.GetComponent<EventConditionChecker>());
                    int index = ListEventConditionWin.IndexOf(checker);

                    if (IsHasOrder && index != stepIndex)
                    {
                        return false;
                    }
                    return true;
                }

            }
            return false;
        }

        private bool CheckValidGameObjectFireEventLose(GameObject gameObjectEvent)
        {

            foreach (var checker in ListEventConditionLose)
            {
                if (checker.gameObject.GetInstanceID() == gameObjectEvent.GetInstanceID())
                {
                    //  int index = ListEventConditionWin.FindIndex(0, ListEventConditionWin.Count, condition => condition == eventType.gameObject.GetComponent<EventConditionChecker>());
                    int index = ListEventConditionLose.IndexOf(checker);
                    return true;
                }

            }
            return false;
        }

        private int GetIndexEventWin(GameObject gameObjectEvent)
        {
            foreach (var checker in ListEventConditionWin)
            {
                if (checker.gameObject.GetInstanceID() == gameObjectEvent.GetInstanceID())
                {
                    int index = ListEventConditionWin.IndexOf(checker);
                    return index;
                }
            }
            return -1;
        }

        private int GetIndexEventLose(GameObject gameObjectEvent)
        {
            foreach (var checker in ListEventConditionLose)
            {
                if (checker.gameObject.GetInstanceID() == gameObjectEvent.GetInstanceID())
                {
                    int index = ListEventConditionLose.IndexOf(checker);
                    return index;
                }
            }
            return -1;
        }

        private void StopAllCheckerListener()
        {
            listenChecker = false;
        }

        private void CheckEventFromGameObject(GameObject gameObjectEvent)
        {
            if (listenChecker == false || stepIndex >= ListEventConditionWin.Count) return;
            if (CheckValidGameObjectFireEventWin(gameObjectEvent))
            {
                Debug.Log("CONDITION TRUE");
                ListEventConditionWin[GetIndexEventWin(gameObjectEvent)].DoAllAction();
                if (stepIndex == ListEventConditionWin.Count - 1)
                {
                    StartCoroutine(DelayGameWin());
                    foreach (var action in ActionsWin)
                    {
                        action.DelayAction();
                    }
                }
                ListEventConditionWin.Remove(ListEventConditionWin[GetIndexEventWin(gameObjectEvent)]);
            }

            if (CheckValidGameObjectFireEventLose(gameObjectEvent))
            {
                ListEventConditionLose[GetIndexEventLose(gameObjectEvent)].DoAllAction();
               
                    StartCoroutine(DelayGameLose());
                    foreach (var action in ActionsLose)
                    {
                        action.DelayAction();
                    }
                
                ListEventConditionLose.Remove(ListEventConditionLose[GetIndexEventLose(gameObjectEvent)]);
            }

        }

        IEnumerator DelayGameWin()
        {
            yield return new WaitForSeconds(delayWin);
            EventCenter.TriggerEvent(new GameWinEvent());

        }

        IEnumerator DelayGameLose()
        {
            Debug.Log("CONDITION Lose TRUE");

            yield return new WaitForSeconds(delayWin);
            EventCenter.TriggerEvent(new GameLoseEvent());

        }

        #region Event Listener
        void IEventListener<DistanceCheckEvent>.OnEzEvent(DistanceCheckEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }
        void IEventListener<ObjectTriggerEvent>.OnEzEvent(ObjectTriggerEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }
        void IEventListener<TouchTriggerEvent>.OnEzEvent(TouchTriggerEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }
        void IEventListener<GameStateChangedEvent>.OnEzEvent(GameStateChangedEvent eventType)
        {
            if (eventType.newState == GameState.GameWin || eventType.newState == GameState.GameLose)
            {
                StopAllCheckerListener();
            }
        }

        void IEventListener<OutScreenEvent>.OnEzEvent(OutScreenEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }

        void IEventListener<EraseAllEvent>.OnEzEvent(EraseAllEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }

        void IEventListener<DeviceRotationEvent>.OnEzEvent(DeviceRotationEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }

        void IEventListener<ObjectExitTriggerEvent>.OnEzEvent(ObjectExitTriggerEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }

        void IEventListener<RotationCheckEvent>.OnEzEvent(RotationCheckEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }

        void IEventListener<CountDownCheckEvent>.OnEzEvent(CountDownCheckEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);

        }

        void IEventListener<CheckerEvent>.OnEzEvent(CheckerEvent eventType)
        {
            CheckEventFromGameObject(eventType.gameObject);
        }


        #endregion

    }
}