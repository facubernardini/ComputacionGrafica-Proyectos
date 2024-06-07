Shader "OndasVerdes"
{
	Properties
	{
		_Density("Density", Range(2,50)) = 30
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float _Density;

			v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(pos);
				o.uv = uv * _Density;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float N = _Density;
				float F = 1.0 / N;
				float x = fmod(i.uv.x, F) / F;
				float y = fmod(i.uv.y, F) / F;
				float r = smoothstep(0.4, 0.6, x);
				//float r = smoothstep(0.0, 0.2, x) - smoothstep(0.5, 0.7, x);

				fixed4 fragColor = 0;
				fragColor = fixed4(r, 1.0, 0.0, 1.0);

				return fragColor;
			}
			ENDCG
		}
	}
}