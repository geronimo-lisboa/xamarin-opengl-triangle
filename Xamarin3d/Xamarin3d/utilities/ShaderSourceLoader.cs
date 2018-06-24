using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Xamarin3d.utilities
{
    /// <summary>
    /// Serve pra carregar o código fonte de fragment shader e vertex shader, com a arquivo determinado quando
    /// o objeto é criado. Ele é imutável. Sempre retornará o código dos arquivos passados como parâmetro no começo
    /// </summary>
    class ShaderSourceLoader
    {
        private string CreateFullyQualifiedName(string smallName)
        {
            string fullName = "Xamarin3d.shaders.__FILE__";
            fullName = fullName.Replace("__FILE__", smallName);
            return fullName;
        }

        private string ExtractTextFromFile(Assembly assembly, string fullName)
        {
            Stream stream = assembly.GetManifestResourceStream(fullName);
            if(stream==null)
            {
                throw new FileNotFoundException("Arquivo " + fullName + " não encontrado.");
            }
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vsFileName">O nome do arquivo. Não é o nome totalmente qualificado. A classe se vira pra saber onde ele está realmente.</param>
        /// <param name="fsFileName">O nome do arquivo. Não é o nome totalmente qualificado. A classe se vira pra saber onde ele está realmente.</param>
        public ShaderSourceLoader(string vsFileName, string fsFileName)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ShaderSourceLoader)).Assembly;
            string fullyQualifiedVSName = CreateFullyQualifiedName(vsFileName);
            VertexShaderSourceCode = ExtractTextFromFile(assembly, fullyQualifiedVSName);
            System.Diagnostics.Debug.WriteLine(VertexShaderSourceCode);

            string fullyQualifiedFSName = CreateFullyQualifiedName(fsFileName);
            FragmentShaderSourceCode = ExtractTextFromFile(assembly, fullyQualifiedFSName);
            System.Diagnostics.Debug.WriteLine(FragmentShaderSourceCode);

        }

        public string VertexShaderSourceCode { get; private set; }
        public string FragmentShaderSourceCode { get; private set; }
    }
}
