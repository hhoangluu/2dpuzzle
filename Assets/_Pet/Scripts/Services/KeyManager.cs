using UnityEngine;
using System;
using System.Collections;

namespace _Pet
{
    public class KeyManager : MonoBehaviour
    {
        public static KeyManager Instance;

        public int Key
        { 
            get { return key; }
            private set { key = value; }
        }

        public static event Action<int> KeysUpdated;

        [SerializeField]
        int initialCoins = 2000;

        // Show the current coins value in editor for easy testing
        [SerializeField]
        int key;

        // key name to store high score in PlayerPrefs
        const string PPK_COINS = "COINS";


        void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
		private void AdManager_OnRewardRewardAd(string placement)
		{
            if (placement == "Get_Key")
            {
                AddKey(20);
            }
		}

		void Start()
        {
            Reset();
        }

        public void Reset()
        {
            // Initialize coins
            Key = PlayerPrefs.GetInt(PPK_COINS, initialCoins);
           
        }

        public void AddKey(int amount)
        {
            Key += amount;


            // Store new coin value
            PlayerPrefs.SetInt(PPK_COINS, Key);

            // Fire event
            KeysUpdated(Key);
        }

        public bool RemoveKey(int amount)
        {
            if (key - amount < 0)
            {
                return false;
            }
            Key -= amount;

            // Store new coin value
            PlayerPrefs.SetInt(PPK_COINS, Key);

            // Fire event
            KeysUpdated(Key);
            return true;
        }
    }
}
