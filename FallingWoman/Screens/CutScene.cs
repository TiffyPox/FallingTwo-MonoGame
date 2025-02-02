﻿using FallingWoman.Entities;
using FallingWoman.Graphics;
using FallingWoman.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace FallingWoman.Screens
{
    public class CutScene : BaseScreen
    {
        private Texture2D _spriteSheet;

        private Animation _animation;

        private Color _color;

        private bool _screenEnded;
        
        private const float MaxTime = 3.0f;
        private float _currentTime = MaxTime;
        private SpriteFont _font;
        
        private readonly SoundSystem _soundSystem;
        private readonly SoundEffectSystem _soundEffectSystem;

        private Song _song;
        
        private SoundEffect _explosion;

        public PlayerState State { get; private set; }

        public CutScene(SoundSystem soundSystem, SoundEffectSystem soundEffectSystem)
        {
            _soundSystem = soundSystem;
            _soundEffectSystem = soundEffectSystem;
            BackgroundColor = new Color(255, 255, 255, 0);
        }

        public override void Load(ContentManager content)
        {
            _spriteSheet = content.Load<Texture2D>("FallingAnimation-Sheet");
            
            _font = content.Load<SpriteFont>("AltText");
            
            _song = content.Load<Song>("cutsceneMusic");
            
            _explosion = content.Load<SoundEffect>("explosion");
        }

        public override void Initialize()
        {
            base.Initialize();

            State = PlayerState.Idle;

            _color = new Color(255, 255, 255);

            _animation = new Animation(new Vector2(300, 500), _spriteSheet.Width / 300, false)
            {
                OnFinish = () =>
                {
                    _screenEnded = true;
                }
            };
        }

        public override void OnShow()
        {
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            var currentFrame = _animation.GetFrameNumber();
            
            if (currentFrame == 25)
            {
                _soundSystem.Stop();
                _soundEffectSystem.Play(_explosion);
            }

            if (currentFrame == 27 || currentFrame == 29 || currentFrame == 31 || currentFrame == 33) // Need to sort out animations
            {
                _soundEffectSystem.Play(_explosion);
            }

            _animation.Update(gameTime);
            
            if (!_screenEnded) return;
            
            _currentTime -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            var currentAlpha = (_currentTime / MaxTime) * 255;
            _color.A = (byte) currentAlpha;
                
            if (_currentTime <= 0)
            {
                AddScreen?.Invoke(new GameScreen(_soundSystem));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(_spriteSheet, new Vector2(0, 0), _animation.GetFrame(), _color);

            spriteBatch.End();
        }
    }
}
