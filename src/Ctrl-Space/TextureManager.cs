using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
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

        public static void LoadTextures(ContentManager contentManager)
        {
            ShipTexture = contentManager.Load<Texture2D>("Ship");
            ShipAnimation = contentManager.Load<Texture2D>("ShipAnimation");
            ShipOffTexture = contentManager.Load<Texture2D>("Ship-off");
            AsteroidTexture = contentManager.Load<Texture2D>("Asteroid");
            RocketTexture = contentManager.Load<Texture2D>("Rocket");
            SpeedBonusTexture = contentManager.Load<Texture2D>("SpeedBonus");
            SpaceTexture = contentManager.Load<Texture2D>("Space");
            PlasmaBulletTexture = contentManager.Load<Texture2D>("PlasmaBullet");
        }
    }
}