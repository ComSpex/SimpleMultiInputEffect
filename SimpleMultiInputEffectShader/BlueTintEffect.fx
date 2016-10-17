// Be forewarned: FXC is finicky about encoding types for the Input
// file. It prefers ASCII and doesn't like Unicode encoded text files.

sampler2D InputOne;//:register(s0);

float4 redTint(float2 uv:TEXCOORD):COLOR{
	float4 Color=tex2D(InputOne,uv.xy);
	Color.r += 1+uv.y;
	return Color;
}
float4 greenTint(float2 uv:TEXCOORD):COLOR{
	float4 Color=tex2D(InputOne,uv.xy);
	Color.g += 1+uv.y;
	return Color;
}
float4 blueTint(float2 uv:TEXCOORD):COLOR{
	float4 Color=tex2D(InputOne,uv.xy);
	Color.b += 1+uv.y;
	return Color;
}
float4 bluesy(float2 uv:TEXCOORD):COLOR{
	float4 color=tex2D(InputOne,uv);
	float4 invertedColor=float4(color.a-color.rgb,color.a);
	return invertedColor;
}
float4 main(float2 uv:TEXCOORD):COLOR{
	return blueTint(uv);
	//return redTint(uv);
	//return greenTint(uv);
	//return bluesy(uv);
}
