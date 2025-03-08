using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Pet
{
    [RequireComponent(typeof(Collider2D))]
    public class InstanceObject : MonoBehaviour
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

        private Vector2 screenPoint;
        private Vector2 offset;
        private GameObject objectInstance;

        private void OnMouseUpAsButton()
        {
            //   offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
          //  if (objectInstance != null) return;
            Vector2 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 curPosition =  (Vector2)Camera.main.ScreenToWorldPoint(curScreenPoint) ;
           
            StartCoroutine(CR_InstanceObject(curPosition));
        }

        private IEnumerator CR_InstanceObject(Vector2 position)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(objectReference, position, Quaternion.identity,LevelManager.Instance.CurLevel.transform);
            yield return handle;
            objectInstance = handle.Result;
            objectInstance.transform.position = position;
            if (autoHide)
            {
                yield return new WaitForSeconds(duration);
                Destroy(objectInstance);
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
    }
}