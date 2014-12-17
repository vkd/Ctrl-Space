using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Ctrl_Space.Graphics
{
    public static class TextureManager
    {
        public static SpriteFont Font { get; private set; }

        public static MetaTexture ShipTexture { get; private set; }
        public static MetaTexture ShipAnimation { get; private set; }
        public static MetaTexture ShipOffTexture { get; private set; }
        public static MetaTexture RocketTexture { get; private set; }
        public static MetaTexture AsteroidTexture { get; private set; }
        public static MetaTexture SpeedBonusTexture { get; private set; }
        public static MetaTexture MedkitTexture { get; private set; }
        public static MetaTexture SpaceTexture { get; private set; }
        public static MetaTexture PlasmaBulletTexture { get; private set; }
        public static MetaTexture SimpleGlowTexture { get; private set; }
        public static MetaTexture EnemyTexture { get; private set; }
        public static MetaTexture[] FontTexture { get; private set; }

        public static void LoadTextures(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            Font = contentManager.Load<SpriteFont>("Fonts/Font");

            var shipTexture = LoadTexture(graphicsDevice, "Textures/Ship/Ship.png");
            var shipAnimation = LoadTexture(graphicsDevice, "Textures/Ship/ShipAnimation.png");
            var shipOffTexture = LoadTexture(graphicsDevice, "Textures/Ship/Ship-off.png");
            var asteroidTexture = LoadTexture(graphicsDevice, "Textures/SpaceObjects/Asteroid.png");
            var rocketTexture = LoadTexture(graphicsDevice, "Textures/Weapon/Rocket.png");
            var speedBonusTexture = LoadTexture(graphicsDevice, "Textures/Bonuses/SpeedBonus.png");
            var medkitTexture = LoadTexture(graphicsDevice, "Textures/Bonuses/Medkit.png");
            var spaceTexture = LoadTexture(graphicsDevice, "Textures/Space/Space.jpg");
            var plasmaBulletTexture = LoadTexture(graphicsDevice, "Textures/Weapon/PlasmaBullet.png");
            var simpleGlowTexture = LoadTexture(graphicsDevice, "Textures/Particles/SimpleGlow.png");
            var enemyTexture = LoadTexture(graphicsDevice, "Textures/Ship/Ship2.png");
            var fontTexture = LoadTexture(graphicsDevice, "Fonts/Font.png");

            ShipTexture = new MetaTexture(shipTexture);
            ShipAnimation = new MetaTexture(shipAnimation);
            ShipOffTexture = new MetaTexture(shipOffTexture);
            RocketTexture = new MetaTexture(rocketTexture);
            AsteroidTexture = new MetaTexture(asteroidTexture);
            SpeedBonusTexture = new MetaTexture(speedBonusTexture);
            MedkitTexture = new MetaTexture(medkitTexture);
            SpaceTexture = new MetaTexture(spaceTexture);
            PlasmaBulletTexture = new MetaTexture(plasmaBulletTexture);
            SimpleGlowTexture = new MetaTexture(simpleGlowTexture);
            EnemyTexture = new MetaTexture(enemyTexture);
            FontTexture = new MetaTexture[256];
            for (int j = 0; j < 16; j++)
                for (int i = 0; i < 16; i++)
                    FontTexture[j * 16 + i] = new MetaTexture(fontTexture, new Rectangle(i * 16, j * 16, 16, 16));
        }

        public static Texture2D LoadTexture(GraphicsDevice graphicsDevice, string name)
        {
            using (var fs = new FileStream("Content\\" + name, FileMode.Open, FileAccess.Read))
            {
                return FromStream(graphicsDevice, fs);
            }
        }

        // premultiplied alpha dirty workaround
        public static Texture2D FromStream(GraphicsDevice graphicsDevice, Stream stream)
        {
            Texture2D texture = Texture2D.FromStream(graphicsDevice, stream);
            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData(data);
            for (int i = 0; i != data.Length; ++i)
                data[i] = Color.FromNonPremultiplied(data[i].ToVector4());
            texture.SetData(data);
            return texture;
        }
    }
}
