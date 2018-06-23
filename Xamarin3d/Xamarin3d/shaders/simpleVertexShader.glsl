﻿//A matrix model view projection, que transforma os vertices de objetos no espaço do modelo em objetos
//no espaço da tela
uniform uMVPMatrix;
//A posição espacial do vertice atual.
attribute vec3 vPosition;

void main()
{
	gl_Position = uMVPMatrix * vec4(vPosition, 1.0);
}