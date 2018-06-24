using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin3d.model.OpenGLInfrastructure
{
    struct AttributeProperties
    {
        int id;
        string nome;
        //É um glEnum, tem que consultar uma tabela em algum lugar pra saber qual é qual.
        int type;
    }

    struct UniformProperties
    {
        int id;
        string nome;
        //É um glEnum, tem que consultar uma tabela em algum lugar pra saber qual é qual.
        int type;
    }

    class ShaderProgram
    {
        private Shader vertexShader;
        private Shader fragmentShader;
        private List<AttributeProperties> attributes = new List<AttributeProperties>();
        private List<UniformProperties> uniforms = new List<UniformProperties>();

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
            //TODO: ver se isso aqui deveria estar é em outro lugar            
            GL.BindAttribLocation(idProgram, 0, "vPosition");
            
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
            //TODO: Fazer baseado em https://stackoverflow.com/questions/440144/in-opengl-is-there-a-way-to-get-a-list-of-all-uniforms-attribs-used-by-a-shade
            //TODO: puxar uma lista de atributos pra realizar o bind em cima da lista ao invés de hardcoded.

            //TODO: puxar uma lista de uniforms pra realizar o bind em cima da lista ao invés de hardcoded.
        }

        internal void Use()
        {
            //TODO: ver se isso aqui deveria estar é em outro lugar            
            GL.BindAttribLocation(ProgramId, 0, "vPosition");
            GL.UseProgram(ProgramId);
        }

        ~ShaderProgram()
        {
            //TODO: implementar destrutor
        }
    }
}
