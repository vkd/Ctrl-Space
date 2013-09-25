using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    public static class TextureManager
    {
        public static Texture2D ShipTexture { get; set; }
        public static Texture2D ShipAnimation { get; set; }
        public static Texture2D ShipOffTexture { get; set; }
        public static Texture2D RocketTexture { get; set; }
        public static Texture2D AsteroidTexture { get; set; }
        public static Texture2D SpeedBonusTexture { get; set; }
        public static Texture2D SpaceTexture { get; set; }
        public static Texture2D PlasmaBulletTexture { get; set; }
        public static Texture2D SimpleGlowTexture { get; set; }
        public static Texture2D EnemyTexture { get; set; }

        public static void LoadTextures(ContentManager contentManager)
        {
            ShipTexture = contentManager.Load<Texture2D>("Textures/Ship/Ship");
            ShipAnimation = contentManager.Load<Texture2D>("Textures/Ship/ShipAnimation");
            ShipOffTexture = contentManager.Load<Texture2D>("Textures/Ship/Ship-off");
            AsteroidTexture = contentManager.Load<Texture2D>("Textures/SpaceObjects/Asteroid");
            RocketTexture = contentManager.Load<Texture2D>("Textures/Weapon/Rocket");
            SpeedBonusTexture = contentManager.Load<Texture2D>("Textures/Bonuses/SpeedBonus");
            SpaceTexture = contentManager.Load<Texture2D>("Textures/Space/Space");
            PlasmaBulletTexture = contentManager.Load<Texture2D>("Textures/Weapon/PlasmaBullet");
            SimpleGlowTexture = contentManager.Load<Texture2D>("Textures/Particles/SimpleGlow");
            EnemyTexture = contentManager.Load<Texture2D>("Textures/Ship/Ship2");
        }
    }
}
