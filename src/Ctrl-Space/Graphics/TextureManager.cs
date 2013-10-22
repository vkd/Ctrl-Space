using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public static void LoadTextures(ContentManager contentManager)
        {
            Font = contentManager.Load<SpriteFont>("Fonts/Font");

            var shipTexture = contentManager.Load<Texture2D>("Textures/Ship/Ship");
            var shipAnimation = contentManager.Load<Texture2D>("Textures/Ship/ShipAnimation");
            var shipOffTexture = contentManager.Load<Texture2D>("Textures/Ship/Ship-off");
            var asteroidTexture = contentManager.Load<Texture2D>("Textures/SpaceObjects/Asteroid");
            var rocketTexture = contentManager.Load<Texture2D>("Textures/Weapon/Rocket");
            var speedBonusTexture = contentManager.Load<Texture2D>("Textures/Bonuses/SpeedBonus");
            var medkitTexture = contentManager.Load<Texture2D>("Textures/Bonuses/Medkit");
            var spaceTexture = contentManager.Load<Texture2D>("Textures/Space/Space");
            var plasmaBulletTexture = contentManager.Load<Texture2D>("Textures/Weapon/PlasmaBullet");
            var simpleGlowTexture = contentManager.Load<Texture2D>("Textures/Particles/SimpleGlow");
            var enemyTexture = contentManager.Load<Texture2D>("Textures/Ship/Ship2");

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
        }
    }
}
