using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin3d.model.OpenGLInfrastructure
{
    class Shader
    {
        /// <summary>
        /// Muitos objetos do opengl tem um id, que é o nome desse objeto no mundo do opengl. Esse aqui 
        /// é o id do shader.
        /// </summary>
        public int ShaderId { get; private set; }
        public Shader(All type, string source)
        {
            int shader = GL.CreateShader(type);
            if (shader == 0)
                throw new InvalidOperationException("Unable to create shader");
            int length = 0;
            GL.ShaderSource(shader, 1, new string[] { source }, (int[])null);
            GL.CompileShader(shader);
            int compiled = 0;
            GL.GetShader(shader, All.CompileStatus, out compiled);  
            if (compiled == 0)
            {
                length = 0;
                GL.GetShader(shader, All.InfoLogLength, out length);
                var log = new StringBuilder(length);
                if (length > 0)
                {
                    GL.GetShaderInfoLog(shader, length, out length, log);
                    System.Diagnostics.Debug.WriteLine(log.ToString());
                }

                GL.DeleteShader(shader);
                throw new ShaderCompilationErrorException(log.ToString());
            }
            ShaderId = shader;
        }
        ~Shader()
        {
            //TODO: Implementar destrutor
        }
    }
}
