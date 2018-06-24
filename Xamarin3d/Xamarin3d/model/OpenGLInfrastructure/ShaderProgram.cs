using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin3d.model.OpenGLInfrastructure
{
    struct AttributeProperties
    {
        public int Id { get;set; }
        public string Name { get;set; }
        public All Type { get; set; }

        public AttributeProperties(int id, string nome, All type)
        {
            Id = id;
            Name = nome;
            Type = type;
        }
    }

    struct UniformProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public All Type { get; set; }

        public UniformProperties(int id, string nome, All type)
        {
            Id = id;
            Name = nome;
            Type = type;
        }

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
            //GL.BindAttribLocation(idProgram, 0, "vPosition");
            
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
            IntrospectAttributes();
            IntrospectUniforms();
        }
        /// <summary>
        /// Lembre-se que ele só acha atributos ativos. Um atributo só é considerado ativo pelo shader
        /// se ele for usado no código.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AttributeProperties GetAttributeByName(string name)
        {
            var result = attributes.Where(attr => attr.Name.Equals(name)).ToList();
            if (result.Count() == 0)
                throw new InvalidOperationException($"Attribute {name} not found");
            else
                return result[0];
        }
        /// <summary>
        /// Lembrando que ele só acha uniforms ativos. Um uniform só é ativo se ele for usado no código do shader.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UniformProperties GetUniformByName(string name)
        {
            var result = uniforms.Where(u => u.Name.Equals(name)).ToList();
            if (result.Count() == 0)
                throw new InvalidOperationException($"Uniform {name} not found");
            else
                return result[0];
        }

        private void IntrospectAttributes()
        {
            int count;
            GL.GetProgram(ProgramId, All.ActiveAttributes, out count);
            for(int i=0; i<count; i++)
            {
                const int bufSize = 64;
                int currAttrLen, currAttrSize; 
                All currType;
                StringBuilder currAttrName = new StringBuilder(bufSize);
                GL.GetActiveAttrib(ProgramId, i, bufSize, out currAttrLen, out currAttrSize, out currType, currAttrName);
                AttributeProperties currAttr = new AttributeProperties { Id = i, Name = currAttrName.ToString(), Type = currType };
                attributes.Add(currAttr);
            }
        }
        private void IntrospectUniforms()
        {
            int count;
            GL.GetProgram(ProgramId, All.ActiveUniforms, out count);
            for (int i = 0; i < count; i++)
            {
                const int bufSize = 64;
                int currAttrLen, currAttrSize;
                All currType;
                StringBuilder currAttrName = new StringBuilder(bufSize);
                GL.GetActiveAttrib(ProgramId, i, bufSize, out currAttrLen, out currAttrSize, out currType, currAttrName);
                UniformProperties currentUniform = new UniformProperties { Id = i, Name = currAttrName.ToString(), Type = currType };
                uniforms.Add(currentUniform);
            }
        }
        public void Use()
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
