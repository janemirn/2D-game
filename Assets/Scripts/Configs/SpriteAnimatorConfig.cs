using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public enum AnimationState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
        Crouch = 3,
        Hurt = 4
    }

    [CreateAssetMenu(fileName = "SpriteAnimationCfg", menuName ="Configs/ Animation Cfg", order =1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimationState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }
        public List<SpriteSequence> Sequence = new List<SpriteSequence>();
    }

}
