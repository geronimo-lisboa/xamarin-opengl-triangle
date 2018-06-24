using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OpenTK.Graphics.ES30;
using System.Reflection;
using System.IO;
using Xamarin3d.utilities;
using Xamarin3d.model;

namespace Xamarin3d
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Initialized = false;
            openglView.HasRenderLoop = true;//Sem isso aqui ele não vai usar meu ponteiro de função.
            openglView.OnDisplay = this.OpenGLViewOnDisplay;//Passa o ponteiro de função.
		}

        private float currentR = 0;
        public bool Initialized { get; private set; }
        /// <summary>
        /// Essa aqui é a função que trata da renderização da view de opengl;
        /// </summary>
        /// <param name="rect"></param>
        private void OpenGLViewOnDisplay(Rectangle rect)
        {
            if(!Initialized)
            {
                InitializeScene();
            }
            GL.ClearColor(currentR, 0, 0, 1.0f);
            GL.Clear((ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
            currentR += 0.01f;
            if (currentR >= 1.0f)
                currentR = 0;
            if(Initialized)
            {
                RenderScene();
            }
        }

        int LoadShader(All type, string source)
        {
            int shader = GL.CreateShader(type);
            if (shader == 0)
                throw new InvalidOperationException("Unable to create shader");

            int length = 0;
            GL.ShaderSource(shader, source);//  (shader, 1, new string[] { source }, (int[])null);
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
                   
                }

                GL.DeleteShader(shader);
                throw new InvalidOperationException("Unable to compile shader of type : " + type.ToString());
            }

            return shader;
        }


        private void InitializeScene()
        {
            ShaderSourceLoader shaderSource = new ShaderSourceLoader("simpleVertexShader.glsl", "simpleFragmentShader.glsl");
            Shader s = new Shader(All.VertexShader, shaderSource.VertexShaderSourceCode);


            Initialized = true;
        }

        private void RenderScene()
        {

        }
	}
}
