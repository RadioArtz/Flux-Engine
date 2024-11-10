#version 410 core

layout(location = 0) out vec4 FragColor;

layout(location = 0) in vec3 FragPos;
layout(location = 1) in vec3 NormalVec;
layout(location = 2) in vec2 TextureCoords;

uniform vec4 color;
uniform sampler2D texture0;

void main()
{
	vec4 texture1Sample = texture(texture0,TextureCoords);
	//if(texture1Sample.a <0.1) //Cutout Transparency
	//	discard;
    


	float lighted = 0.0;
    lighted += NormalVec.x;
    lighted += NormalVec.y;
    lighted += NormalVec.z;
    lighted *= 0.75;
    lighted = clamp(lighted, 0.0, 1.0);
    lighted += 0.05;

    FragColor = vec4(0.5, 0.5, 0.5, 1.0);
    
    if (FragPos.y >= 64.001+32)
    {
        FragColor = vec4(1.0, 1.0, 1.0, 1.0);
    }
    if (FragPos.y <= 20.001+32)
    {
        FragColor = vec4(0.1, 1.0, 0.25, 1.0);
    }
    if (FragPos.y <= 6.001+32)
    {
        FragColor = vec4(0.79, 0.73, 0.57, 1.0);
    }
    if (FragPos.y <= 4.001+32)
    {
        FragColor = vec4(0.2, 0.0, 1.0, 1.0);
    }
    lighted = clamp(lighted,0f,1f);
    lighted + 0.35f;
    FragColor *= vec4(lighted, lighted, lighted, 1.0);

    FragColor *= texture1Sample;

}