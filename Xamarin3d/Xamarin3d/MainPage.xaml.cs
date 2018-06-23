using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OpenTK.Graphics.ES30;


namespace Xamarin3d
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            openglView.HasRenderLoop = true;//Sem isso aqui ele não vai usar meu ponteiro de função.
            openglView.OnDisplay = this.OpenGLViewOnDisplay;//Passa o ponteiro de função.
		}

        private float currentR = 0;
        /// <summary>
        /// Essa aqui é a função que trata da renderização da view de opengl;
        /// </summary>
        /// <param name="rect"></param>
        private void OpenGLViewOnDisplay(Rectangle rect)
        {
            GL.ClearColor(currentR, 0, 0, 1.0f);
            GL.Clear((ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
            currentR += 0.01f;
            if (currentR >= 1.0f)
                currentR = 0;
        }
	}
}
