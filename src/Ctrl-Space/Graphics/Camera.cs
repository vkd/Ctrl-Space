using Ctrl_Space.Gameplay;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Graphics
{
    class Camera
    {
        private GameObject _followedObject = null;

        public Camera(GameObject followedObject)
        {
            _followedObject = followedObject;
        }

        public void Follow(GameObject followedObject)
        {
            _followedObject = followedObject;
        }

        public GameObject FollowedObject
        {
            get { return _followedObject; }
        }

        public Matrix GetTransform()
        {
            Matrix result;
            result = Matrix.CreateTranslation(-_followedObject.Position.X, -_followedObject.Position.Y, 0f);
            result *= Matrix.CreateRotationZ(-_followedObject.Rotation);
            result *= Matrix.CreateTranslation(Game.ResolutionX / 2, Game.ResolutionY / 2, 0f);
            return result;
        }

        public Matrix GetParallaxTransform(float ratioX, float ratioY)
        {
            Matrix result;
            result = Matrix.CreateTranslation(-_followedObject.Position.X / ratioX, -_followedObject.Position.Y / ratioY, 0f);
            result *= Matrix.CreateRotationZ(-_followedObject.Rotation);
            result *= Matrix.CreateTranslation(Game.ResolutionX / 2, Game.ResolutionY / 2, 0f);
            return result;
        }
    }
}
