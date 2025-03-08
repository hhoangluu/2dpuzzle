using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace _Pet
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class PlayAnimation : StepAction
    {
        [GUIColor(1, 1, 1)]
        [SerializeField]
        [SpineAnimation]
        private string _animationName;

        [GUIColor(1, 1, 1)]
        [SerializeField]
        private bool loop;

       

        protected override void DoAction()
        {
           SkeletonAnimation skeleton = GetComponent<SkeletonAnimation>();
            skeleton.AnimationState.SetAnimation(0,_animationName,loop);
        }

        
    }
}
