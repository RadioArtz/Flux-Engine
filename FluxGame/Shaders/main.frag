#version 410 core
layout(location = 0) out vec4 FragColor;

layout(location = 0) in vec3 NormalVec;

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