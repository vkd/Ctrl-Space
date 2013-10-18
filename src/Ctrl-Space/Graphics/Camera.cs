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
            Matrix translation = Matrix.CreateTranslation(-_followedObject.Position.X, -_followedObject.Position.Y, 0f);
            Matrix rotation = Matrix.CreateRotationZ(-_followedObject.Rotation);
            Matrix view = Matrix.CreateTranslation(512, 384, 0f);
            return translation * rotation * view;
        }

        public Matrix GetParallaxTransform(float ratioX, float ratioY)
        {
            Matrix translation = Matrix.CreateTranslation(-_followedObject.Position.X / ratioX , -_followedObject.Position.Y / ratioY, 0f);
            Matrix rotation = Matrix.CreateRotationZ(-_followedObject.Rotation);
            Matrix view = Matrix.CreateTranslation(512, 384, 0f);
            return translation * rotation * view;
        }
    }
}
