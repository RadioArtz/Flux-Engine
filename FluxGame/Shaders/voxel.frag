#version 410 core
layout(location = 0) out vec4 FragColor;

layout(location = 0) in vec3 FragPos;
layout(location = 1) in vec3 NormalVec;
layout(location = 2) in vec2 TextureCoords;

uniform vec4 color;

void main()
{
    float lighted = 1f;

    FragColor = vec4(lighted, lighted, lighted, 1.0);
}