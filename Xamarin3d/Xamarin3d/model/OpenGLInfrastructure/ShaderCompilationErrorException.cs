using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin3d.model.OpenGLInfrastructure
{
    class ShaderCompilationErrorException : Exception
    {
        public ShaderCompilationErrorException(string message) : base(message)
        {
        }
    }
}
