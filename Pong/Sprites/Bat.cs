using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Managers;
using System;

namespace Pong.Sprites
{
    public class Bat : Component
    {
        private Vector2 _velocity;

        public BasicInput Input;

        public Bat(Texture2D texture) : base(texture)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color);  
        }

        public override void Update(GameTime gameTime, Component[] components)
        {
            if (Input is null)
            {
                throw new ArgumentNullException("Está faltando o input do jogador!");
            }

            Move(gameTime);
            Position += _velocity;
            _velocity = Vector2.Zero;
            Position.Y = MathHelper.Clamp(Position.Y, 0, MainGame.ScreenHeight - _texture.Height);
        }

        private void Move(GameTime gameTime)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Input.Up))
            {
                _velocity.Y -= Speed * elapsedTime;
            }
            else if (keyboard.IsKeyDown(Input.Down))
            {
                _velocity.Y += Speed * elapsedTime;
            }
        }
    }
}
