using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Managers;
using Pong.Sprites;
using System.Collections.Generic;

namespace Pong
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        private List<Component> _components;
        private SpriteFont _font;

        public static int ScreenWidth, ScreenHeight;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            _components = new List<Component>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _background = Content.Load<Texture2D>("Background");
            _font = Content.Load<SpriteFont>("File");
            _components.Add(new Bat(Content.Load<Texture2D>("Bat"))
            {
                Color = Color.Orange,
                Position = new Vector2(30, ScreenHeight / 2 - 50),
                Input = new Managers.BasicInput
                {
                    Up = Keys.Up,
                    Down = Keys.Down
                }
            }
            );
            _components.Add(new Bat(Content.Load<Texture2D>("Bat"))
            {
                Color = Color.Red,
                Position = new Vector2(ScreenWidth - 30 - 16, ScreenHeight / 2 - 50),
                Input = new Managers.BasicInput
                {
                    Up = Keys.W,
                    Down = Keys.S
                }
            });
            _components.Add(new Ball(Content.Load<Texture2D>("Ball")));
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            foreach (var component in _components)
            {
                component.Update(gameTime, _components.ToArray());
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_font, $"{ScoreManager.PlayerOneScore}", new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(_font, $"{ScoreManager.PlayerTwoScore}", new Vector2(660, 100), Color.White);
            foreach (var item in _components)
            {
                item.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
