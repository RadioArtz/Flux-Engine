#version 410 core

layout(location = 0) out vec4 FragColor;

layout(location = 0) in vec3 FragPos;
layout(location = 1) in vec3 NormalVec;
layout(location = 2) in vec2 TextureCoords;

uniform vec4 color;
uniform sampler2D texture0;
uniform float tiling;

void main()
{
	vec4 texture1Sample = texture(texture0,TextureCoords*tiling);
	//if(texture1Sample.a <0.1) //Cutout Transparency
	//	discard;
    

    
	float lighted = 0.0;
    lighted += NormalVec.x;
    lighted += NormalVec.y;
    lighted += NormalVec.z;
    lighted *= 0.75;
    lighted = clamp(lighted, 0.0f, 1.0f);
    lighted += 0.15;

    FragColor = vec4(1, 1, 1, 1.0);

    lighted = clamp(lighted,0.0f,1.0f);
    lighted + 0.45f;
    FragColor *= vec4(lighted, lighted, lighted, 1.0f);
    
    FragColor *= texture1Sample;
}