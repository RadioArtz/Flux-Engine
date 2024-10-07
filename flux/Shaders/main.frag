#version 410 core
out vec4 FragColor;

in vec3 NormalVec;

uniform vec4 color;

void main()
{
	float lighted = 0;
	lighted += NormalVec.x;
	lighted += NormalVec.y;
	lighted += NormalVec.z;
	lighted *= 0.333;
	FragColor = vec4(lighted,lighted,lighted,1);
}