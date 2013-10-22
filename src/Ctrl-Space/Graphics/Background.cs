using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class Background
    {
        public int RepeatX = 1;
        public int RepeatY = 1;
        public float ParallaxFactorX = 2f;
        public float ParallaxFactorY = 2f;

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            var sourceRectangle = new Rectangle(0, 0, TextureManager.SpaceTexture.Texture.Width * RepeatX, TextureManager.SpaceTexture.Texture.Height * RepeatY);
            var scale = new Vector2(
                Game.WorldWidth / TextureManager.SpaceTexture.Texture.Width / ParallaxFactorX / RepeatX,
                Game.WorldHeight / TextureManager.SpaceTexture.Texture.Height / ParallaxFactorY / RepeatY);
            float w = Game.WorldWidth / ParallaxFactorX;
            float h = Game.WorldHeight / ParallaxFactorY;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, camera.GetParallaxTransform(ParallaxFactorX, ParallaxFactorY));
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(0, 0), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(w, 0), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(w, h), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(0, h), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(-w, h), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(-w, 0), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(-w, -h), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(0, -h), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(TextureManager.SpaceTexture.Texture, new Vector2(w, -h), sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }
    }
}
