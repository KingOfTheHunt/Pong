using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Sprites
{
    public abstract class Component
    {
        protected Texture2D _texture;
        protected float _speed = 100f;
        public Vector2 Position;
        public Color Color = Color.White;

        public int Width { get { return _texture.Width; } }
        public int Height { get { return _texture.Height; } }
        public float Speed { get { return _speed; } }

        public Component(Texture2D texture)
        {
            _texture = texture;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime, Component[] components);
    }
}
