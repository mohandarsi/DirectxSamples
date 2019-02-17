using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Windows;
using SharpDX.Mathematics.Interop;
using SharpDX.Mathematics;
using System.Diagnostics;

namespace SimpleDirectXForms
{

    class Direcr3dCubeDisplay : IDisposable
    {

        private RenderForm _renderForm;
        private Device _device;
        private VertexBuffer _vertexBuffer;
        private Effect _effect;

        public Direcr3dCubeDisplay(RenderForm form, Device device)
        {
            _renderForm = form;
            _device = device;
        }

        public void BuildCube()
        {
            _vertexBuffer = new VertexBuffer(_device, Utilities.SizeOf<RawVector4>() * 2 * 36, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
            _vertexBuffer.Lock(0, 0, LockFlags.None).WriteRange(new[]
                                  {
                                      new RawVector4(-1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 0.0f, 1.0f), // Front
                                      new RawVector4(-1.0f,  1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f,  1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new RawVector4(-1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f,  1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 0.0f, 1.0f),

                                      new RawVector4(-1.0f, -1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 0.0f, 1.0f), // BACK
                                      new RawVector4( 1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4(-1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4(-1.0f, -1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f, -1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 0.0f, 1.0f),

                                      new RawVector4(-1.0f, 1.0f, -1.0f,  1.0f), new RawVector4(0.0f, 0.0f, 1.0f, 1.0f), // Top
                                      new RawVector4(-1.0f, 1.0f,  1.0f,  1.0f), new RawVector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f, 1.0f,  1.0f,  1.0f), new RawVector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4(-1.0f, 1.0f, -1.0f,  1.0f), new RawVector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f, 1.0f,  1.0f,  1.0f), new RawVector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f, 1.0f, -1.0f,  1.0f), new RawVector4(0.0f, 0.0f, 1.0f, 1.0f),

                                      new RawVector4(-1.0f,-1.0f, -1.0f,  1.0f), new RawVector4(1.0f, 1.0f, 0.0f, 1.0f), // Bottom
                                      new RawVector4( 1.0f,-1.0f,  1.0f,  1.0f), new RawVector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4(-1.0f,-1.0f,  1.0f,  1.0f), new RawVector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4(-1.0f,-1.0f, -1.0f,  1.0f), new RawVector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f,-1.0f, -1.0f,  1.0f), new RawVector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new RawVector4( 1.0f,-1.0f,  1.0f,  1.0f), new RawVector4(1.0f, 1.0f, 0.0f, 1.0f),

                                      new RawVector4(-1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 1.0f, 1.0f), // Left
                                      new RawVector4(-1.0f, -1.0f,  1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4(-1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4(-1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4(-1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new RawVector4(-1.0f,  1.0f, -1.0f, 1.0f), new RawVector4(1.0f, 0.0f, 1.0f, 1.0f),

                                      new RawVector4( 1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 1.0f, 1.0f), // Right
                                      new RawVector4( 1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f, -1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f, -1.0f, -1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f,  1.0f, -1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new RawVector4( 1.0f,  1.0f,  1.0f, 1.0f), new RawVector4(0.0f, 1.0f, 1.0f, 1.0f),
                            });
            _vertexBuffer.Unlock();

            BuildVertexDeclaration();
        }

        public void BuildEffect(string fileName)
        {
            _effect = Effect.FromFile(_device, fileName, ShaderFlags.None);
        }

        void BuildVertexDeclaration()
        {
            // Allocate Vertex Elements
            var vertexElems = new[] {
                new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.Position, 0),
                new VertexElement(0, 16, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                VertexElement.VertexDeclarationEnd
            };

            // Creates and sets the Vertex Declaration
            var vertexDecl = new VertexDeclaration(_device, vertexElems);
            _device.SetStreamSource(0, _vertexBuffer, 0, Utilities.SizeOf<Vector4>() * 2);
            _device.VertexDeclaration = vertexDecl;
        }
        public void Run()
        {
            // Get the technique
            var technique = _effect.GetTechnique(0);
            var pass = _effect.GetPass(technique, 0);

            // Prepare matrices
            var view = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, _renderForm.ClientSize.Width / (float)_renderForm.ClientSize.Height, 0.1f, 100.0f);
            var viewProj = Matrix.Multiply(view, proj);

            var clock = new Stopwatch();
            clock.Start();
            RenderLoop.Run(_renderForm, () =>
            {
                var time = clock.ElapsedMilliseconds / 1000.0f;

                _device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, new RawColorBGRA(0,0,0,0), 1.0f, 0);
                _device.BeginScene();

                _effect.Technique = technique;
                _effect.Begin();
                _effect.BeginPass(0);

                var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;
                _effect.SetValue("worldViewProj", worldViewProj);

                _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);

                _effect.EndPass();
                _effect.End();

                _device.EndScene();
                _device.Present();
                 var result = _device.TestCooperativeLevel();
                if(result != Result.Ok)
                {
                    throw new Exception("Present failed");
                }

            });
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _effect.Dispose();
                    _vertexBuffer.Dispose();
                    _device.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Direcr3dDisplay() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
