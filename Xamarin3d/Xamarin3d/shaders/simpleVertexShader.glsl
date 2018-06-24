//A matrix model view projection, que transforma os vertices de objetos no espaço do modelo em objetos
//no espaço da tela
uniform mat4 uMVPMatrix;
//A cor do vertice, serve pra testar se realmente está funcionando direito a introspecção
attribute vec4 vColor;
//A posição espacial do vertice atual.
attribute vec3 vPosition;

varying vec4 vertexColor;
void main()
{
	gl_Position =  vec4(vPosition, 1.0);
	vertexColor = vColor;
}