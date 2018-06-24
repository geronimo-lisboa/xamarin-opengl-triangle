precision mediump float;
uniform vec4 vColor;

varying vec4 vertexColor;

void main()
{
	gl_FragColor = vertexColor;
}