#version 410 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTexCoord;

layout(location = 1) out vec3 FragPos;
layout(location = 0) out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position =  vec4(aPos, 1.0) * model * view * projection;
    FragPos = vec3(vec4(aPos,1.0)*model);
    texCoord = aTexCoord;
}   