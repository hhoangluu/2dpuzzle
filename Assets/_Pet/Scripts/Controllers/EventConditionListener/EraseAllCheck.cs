using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    struct EraseAllEvent
    {
        public GameObject gameObject;

        public EraseAllEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
    public class EraseAllCheck : EventConditionChecker
    {
       
        private SpriteRenderer targetRenderer;
        private Texture2D texture;

        private int countAlphaPixel;
        private int countPixel;
        private int colorPixelCount;

        private int eraseCount;
        private int notEraseCount;

        private int targetErase;

        private bool checking = true;
        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new EraseAllEvent(this.gameObject));
        }

        // Start is called before the first frame update
        void Start()
        {
            targetRenderer = objectTarget.GetComponent<SpriteRenderer>();
            texture = targetRenderer.sprite.texture;
            countAlphaPixel = 0;
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    if (texture.GetPixel(x,y).a == 0)
                    {
                        countAlphaPixel++;
                    }
                }
            }
            countPixel = texture.height * texture.width;
            colorPixelCount = countPixel - countAlphaPixel;
            targetErase = colorPixelCount / 2 + countAlphaPixel; eraseCount = 0;
        }

        // Update is called once per frame
        public void StartCheck()
        {
            if (checking)
            {
                if (Check() == true)
                {
                    ConditionTrue();
                    checking = false;
                }
            }
        }

        bool Check()
        {
            Debug.Log("ERASE COUNT "+ eraseCount+ " " + targetErase);
            texture = targetRenderer.sprite.texture;
            eraseCount = 0;
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    if (texture.GetPixel(x,y).a == 0)
                    {
                        eraseCount++;
                        if (eraseCount >= targetErase)
                        {
                            return true;
                        }
                    }
                    //else
                    //{
                    //    notEraseCount++;
                    //    if (notEraseCount >= colorPixelCount)
                    //    {
                    //        return false;
                    //    }
                    //}
                }
            }
            return false;
        }
    }
}