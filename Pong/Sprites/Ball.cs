using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Managers;
using System;

namespace Pong.Sprites
{
    public class Ball : Component
    {
        private bool _started = false;
        private float _initialSpeed;
        private Vector2 _initialPosition;
        private Vector2 _velocity;
        private Vector2 _direction;
        private Random _rand = new Random();

        public Ball(Texture2D texture) : base(texture)
        {
            Color = Color.Green;
            Position = new Vector2(MainGame.ScreenWidth / 2 - Width / 2, MainGame.ScreenHeight / 2 - Height / 2);

            _initialPosition = Position;
            _initialSpeed = Speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color);
        }

        public override void Update(GameTime gameTime, Component[] components)
        {

            if (_started == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    _started = true;
                    Initialize();
                }
                else
                {
                    return;
                } 
            }

            if (Position.X <= 0 || Position.X + Width >= MainGame.ScreenWidth)
            {
                _direction.X = -_direction.X;
            }
            if (Position.Y <= 0 || Position.Y + Height >= MainGame.ScreenHeight)
            {
                _direction.Y = -_direction.Y;
            }

            Move(gameTime);

            foreach (var component in components)
            {
                if (component == this)
                {
                    continue;
                }

                if (Collide(component) && _velocity.X > 0)
                {
                    _direction.X = -_direction.X;
                }
                else if (Collide(component) && _velocity.X < 0)
                {
                    _direction.X = -_direction.X;
                }
            }

            Position += _velocity;
            _velocity = Vector2.Zero;

            if (Position.X < 25)
            {
                ScoreManager.PlayerTwoScore++;
                Restart();
            }
            else if (Position.X + Width > MainGame.ScreenWidth - 25)
            {
                ScoreManager.PlayerOneScore++;
                Restart();
            }
        }

        private void Move(GameTime gameTime)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _velocity.X = _direction.X * Speed * elapsedTime;
            _velocity.Y = _direction.Y * Speed * elapsedTime;
        }

        private bool Collide(Component component)
        {
            if (Position.X + _velocity.X < component.Position.X + component.Width && Position.X + _velocity.X + Width > component.Position.X &&
                Position.Y + _velocity.Y < component.Position.Y + component.Height && Position.Y + _velocity.Y + Height > component.Position.Y)
            {
                return true;
            }

            return false;
        }

        private void Initialize()
        {
            int direction = _rand.Next(0, 4);

            switch (direction)
            {
                case 0:
                    _direction = new Vector2(1, 1);
                    break;
                case 1:
                    _direction = new Vector2(-1, 1);
                    break;
                case 2:
                    _direction = new Vector2(1, -1);
                    break;
                case 3:
                    _direction = new Vector2(-1, -1);
                    break;
                default:
                    break;
            }
        }

        private void Restart()
        {
            Position = _initialPosition;
            _started = false;
        }
    }
}
