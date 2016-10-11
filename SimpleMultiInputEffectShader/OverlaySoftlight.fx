﻿float mixInAmount : register(C0);
sampler2D input : register(s0);
sampler2D tex1 : register(s1);
float4 main(float2 uv : TEXCOORD) : COLOR
{
float c = 0;
float4 clr1;
clr1 = tex2D(tex1, uv.xy);

float4 Color;
Color = tex2D(input , uv.xy);
c = clr1.r * Color.r;
Color.r = c + Color.r*(1 - ((1 - Color.r)*(1 - clr1.r) - c));

c = clr1.g * Color.g;
Color.g = c + Color.g*(1 - ((1 - Color.g)*(1 - clr1.g) - c));

c = clr1.b * Color.b;
Color.b = c + Color.b*(1 - ((1 - Color.b)*(1 - clr1.b) - c));

Color.r = Color.r * mixInAmount;
Color.g = Color.g * mixInAmount;
Color.b = Color.b * mixInAmount;

return Color;
}