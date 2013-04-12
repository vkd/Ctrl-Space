using Microsoft.Xna.Framework;

namespace Ctrl_Space
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

        public Matrix GetTransform()
        {
            Matrix translation = Matrix.CreateTranslation(-_followedObject.Position.X, -_followedObject.Position.Y, 0f);
            Matrix rotation = Matrix.CreateRotationZ(-_followedObject.Rotation);
            Matrix view = Matrix.CreateTranslation(512, 384, 0f);
            return translation * rotation * view;
        }

        public Matrix GetParallaxTransform()
        {
            Matrix translation = Matrix.CreateTranslation(-_followedObject.Position.X / 2 + 512, -_followedObject.Position.Y / 2 + 512, 0f);
            Matrix rotation = Matrix.CreateRotationZ(-_followedObject.Rotation);
            Matrix view = Matrix.CreateTranslation(512, 384, 0f);
            return translation * rotation * view;
        }
    }
}
