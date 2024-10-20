#version 410 core

layout(location = 0) out vec4 FragColor;

layout(location = 0) in vec2 texCoord;

uniform vec4 tintColor;
uniform sampler2D texture0;

void main()
{
	vec4 texture1Sample = texture(texture0,texCoord);
	if(texture1Sample.a <0.1) //Cutout Transparency
		discard;
    FragColor = texture1Sample;
}