using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Pet
{
    public class InstanceObjectAction : StepAction
    {
        [GUIColor(1, 1, 1)]
        [SerializeField]
        AssetReferenceGameObject objectReference;

        [GUIColor(1, 1, 1)]
        [SerializeField]
        bool autoHide;

        [GUIColor(1, 1, 1)]
        [SerializeField]
        [ShowIf("@this.autoHide == true")]
        public float duration;

   
        private GameObject objectInstance;

      

        private IEnumerator CR_InstanceObject(Vector2 position)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(objectReference, position, Quaternion.identity, LevelManager.Instance.CurLevel.gameObject.transform);
            yield return handle;
            objectInstance = handle.Result;
            objectInstance.transform.position = position;
            if (autoHide)
            {
                yield return new WaitForSeconds(duration);
                Addressables.ReleaseInstance(objectInstance);
            }

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected override void DoAction()
        {
            StartCoroutine(CR_InstanceObject(transform.position));

        }
    }
}