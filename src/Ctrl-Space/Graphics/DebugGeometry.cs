using System.Collections.Generic;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class DebugGeometry
    {
        private GraphicsDevice _graphicsDevice;
        private BasicEffect _effect;

        public DebugGeometry(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _effect = new BasicEffect(_graphicsDevice);
            InitGeometry();
        }

        private int _lineStart;
        private int _lineCount;
        private int _circleStart;
        private int _circleCount;
        private int _circleSegments = 64;
        private int _rectangleStart;
        private int _rectangleCount;

        private VertexBuffer _vertexBuffer;

        private void InitGeometry()
        {
            List<VertexPositionColor> vertexData = new List<VertexPositionColor>();

            // line
            _lineStart = vertexData.Count;
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(0f, 0f, 0f), Color.White));
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(1f, 0f, 0f), Color.White));
            _lineCount = vertexData.Count - _lineStart - 1;

            // circle
            _circleStart = vertexData.Count;
            for (int i = 0; i <= _circleSegments; i++)
            {
                vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(Maf.Cos(MathHelper.TwoPi * i / _circleSegments), Maf.Sin(MathHelper.TwoPi * i / _circleSegments), 0f), Color.White));
            }
            _circleCount = vertexData.Count - _circleStart - 1;

            // rectangle
            _rectangleStart = vertexData.Count;
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(0f, 0f, 0f), Color.White));
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(1f, 0f, 0f), Color.White));
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(1f, 1f, 0f), Color.White));
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(0f, 1f, 0f), Color.White));
            vertexData.Add(new VertexPositionColor(new Microsoft.Xna.Framework.Vector3(0f, 0f, 0f), Color.White));
            _rectangleCount = vertexData.Count - _rectangleStart - 1;

            _vertexBuffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionColor), vertexData.Count, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionColor>(vertexData.ToArray());
        }

        public void Prepare(Matrix view)
        {
            _graphicsDevice.SetVertexBuffer(_vertexBuffer);

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0, -1, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0f);

            _effect.Projection = halfPixelOffset * projection;
            _effect.View = view;
            _effect.TextureEnabled = false;
        }

        public void DrawLine(Vector2 start, float lenght, float direction, Color color)
        {
            _effect.World = Matrix.CreateScale(lenght) * Matrix.CreateRotationZ(direction) * Matrix.CreateTranslation(start.X, start.Y, 0f);
            _effect.DiffuseColor = color.ToVector3();
            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawPrimitives(PrimitiveType.LineStrip, _lineStart, _lineCount);
            }
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            var delta = end - start;
            DrawLine(start, delta.Length(), Maf.Atan2(delta.Y, delta.X), color);
        }

        public void DrawCircle(Vector2 center, float radius, Color color)
        {
            _effect.World = Matrix.CreateScale(radius) * Matrix.CreateTranslation(center.X, center.Y, 0f);
            _effect.DiffuseColor = color.ToVector3();
            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawPrimitives(PrimitiveType.LineStrip, _circleStart, _circleCount);
            }
        }

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            _effect.World = Matrix.CreateScale(rectangle.Width, rectangle.Height, 0f) * Matrix.CreateTranslation(rectangle.X, rectangle.Y, 0f);
            _effect.DiffuseColor = color.ToVector3();
            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawPrimitives(PrimitiveType.LineStrip, _rectangleStart, _rectangleCount);
            }
        }
    }
}
