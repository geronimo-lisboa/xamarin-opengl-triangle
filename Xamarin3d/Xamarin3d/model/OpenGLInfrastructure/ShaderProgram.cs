using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin3d.model.OpenGLInfrastructure
{
    class ShaderProgram
    {
        private Shader vertexShader;
        private Shader fragmentShader;

        public int ProgramId { get; private set; }

        public ShaderProgram(string vsSrc, string fsSrc)
        {
            vertexShader = new Shader(All.VertexShader, vsSrc);
            fragmentShader = new Shader(All.FragmentShader, fsSrc);
            int idProgram = GL.CreateProgram();
            if (idProgram == 0)
                throw new InvalidOperationException("Não foi possivel criar o programa");
            GL.AttachShader(idProgram, vertexShader.ShaderId);
            GL.AttachShader(idProgram, fragmentShader.ShaderId);
            //TODO: puxar uma lista de atributos pra realizar o bind em cima da lista ao invés de hardcoded.
            GL.BindAttribLocation(idProgram, 0, "vPosition");
            //TODO: puxar uma lista de uniforms pra realizar o bind em cima da lista ao invés de hardcoded.
            GL.LinkProgram(idProgram);
            int linked = 0;
            GL.GetProgram(idProgram, All.LinkStatus, out linked);
            if (linked == 0)
            {
                int length = 0;
                GL.GetProgram(idProgram, All.InfoLogLength, out length);
                var log = new StringBuilder(length);
                if (length > 0)
                {
                    GL.GetProgramInfoLog(idProgram, length, out length, log);
                    
                }
                GL.DeleteProgram(idProgram);
                throw new InvalidOperationException(log.ToString());
            }
            ProgramId = idProgram;
        }


        ~ShaderProgram()
        {
            //TODO: implementar destrutor
        }
    }
}
