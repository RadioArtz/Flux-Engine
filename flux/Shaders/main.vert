﻿#version 410 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;

out vec3 FragPos;
out vec3 NormalVec;


uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position =  vec4(aPosition, 1.0) * model * view * projection;
    FragPos = vec3(vec4(aPosition,1.0)*model);
	NormalVec = aNormal;
}