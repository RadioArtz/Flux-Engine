#version 410 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTextureCoords;

layout(location = 0) out vec3 FragPos;
layout(location = 1) out vec3 NormalVec;
layout(location = 2) out vec2 TextureCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;

    FragPos = vec3(model * vec4(aPosition, 1.0));
    NormalVec = mat3(transpose(inverse(model))) * aNormal;
    TextureCoords = aTextureCoords;
}