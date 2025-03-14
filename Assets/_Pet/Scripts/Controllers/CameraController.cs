﻿using UnityEngine;
using System.Collections;

namespace _Pet
{
    public class CameraController : MonoBehaviour
    {
        public Transform playerTransform;
        private Vector3 velocity = Vector3.zero;
        private Vector3 originalDistance;

        [Header("Camera Follow Smooth-Time")]
        public float smoothTime = 0.1f;

        [Header("Shaking Effect")]
        // How long the camera shaking.
    public float shakeDuration = 0.1f;
        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.2f;
        public float decreaseFactor = 0.3f;
        [HideInInspector]
        public Vector3 originalPos;

        //public Vector3 pointCenter;

        private float currentShakeDuration;
        private float currentDistance;

        void OnEnable()
        {
            
        }

        void OnDisable()
        {

        }

        void Start()
        {
           // pointCenter = Vector3.zero;
          //  StartCoroutine(WaitingPlayerController());
        }

        void Update()
        {
           
        }

        public void FixPosition()
        {
            transform.position = playerTransform.position + originalDistance;
        }

        public void ShakeCamera()
        {
            StartCoroutine(Shake());
        }

        IEnumerator Shake()
        {
            originalPos = transform.position;
            currentShakeDuration = shakeDuration;
            while (currentShakeDuration > 0)
            {
                transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
                currentShakeDuration -= Time.deltaTime * decreaseFactor;
                yield return null;
            }
            transform.position = originalPos;
        }

        void ChangeCharacter(int cur)
        {
          //  StartCoroutine(WaitingPlayerController());
        }

        //IEnumerator WaitingPlayerController()
        //{
        //    yield return new WaitForSeconds(0.05f);
        //    playerTransform = GameManager.Instance.playerController.transform;
        //    originalDistance = transform.position - playerTransform.transform.position;
        //}
    }
}