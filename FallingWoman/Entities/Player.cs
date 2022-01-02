﻿using FallingWoman.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FallingWoman.Entities
{
    public class Player : IGameEntity
    {
        private Sprite Sprite { get; set; }

        private Vector2 Position { get; set; }

        private PlayerState State { get; set; }
        private bool IsAlive { get; set; }

        private float Speed = 5f;
        
        public int DrawOrder { get; set; }

        public Player(Sprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Position = position;
        }

        public void Initialize()
        {
            State = PlayerState.Idle;
            IsAlive = true;
        }

        private void Move(int x)
        {
            Position += new Vector2(x * Speed,0);
        }
        
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            { 
                Move(1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Move(-1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position, Color.White);
        }
    }
}
