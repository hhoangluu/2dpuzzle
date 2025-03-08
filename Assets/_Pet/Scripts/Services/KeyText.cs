using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Pet {

    public class KeyText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        // Start is called before the first frame update
        void Start()
        {
            text.text = KeyManager.Instance.Key.ToString();
            KeyManager.KeysUpdated += KeyManager_KeysUpdated;
        }

        private void KeyManager_KeysUpdated(int key)
        {
            text.text = key.ToString();
        }

    }
}