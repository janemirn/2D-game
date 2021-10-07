using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SpriteAnimatorController : IDisposable
    {
        private sealed class Animation
        {
            public AnimationState Track;
            public List<Sprite> Sprites;
            public bool Loop = false;
            public float Speed;
            public float Counter = 0;
            public bool Sleep = false;

            public void Update()
            {
                if (Sleep) return;
                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while (Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count - 1;
                    Sleep = true;
                }
            }
        }
        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();
        public SpriteAnimatorController (SpriteAnimatorConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimationState track, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Sleep = false;
                animation.Loop = loop;
                animation.Speed = speed;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Loop = loop,
                    Speed = speed,
                    Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites
                }) ;
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }
        public void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();
                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }
        public void Dispose()
        {
            _activeAnimations.Clear();
        }
       
    }
}
