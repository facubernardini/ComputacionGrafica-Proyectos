Shader "Turbulencia"
{
	Properties
	{
		_xPeriod("Repeticion X",float) = 4.0
		_yPeriod("Repeticion Y",float) = 4.1
		_turbPower("Poder de Turbulencia",float) = 0.95
		_turbSize("Tamano de la Turbulencia",float) = 80.0
		_uNoiseScale("Ruido",float) = 0.85
		_uVeinColor("Color Vetas",Color) = (0.3,0.2,0.2,1)
		_uMarbleColor("Color Fondo",Color) = (0.6,0.6,0.6,1)

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

			float _xPeriod;
			float _yPeriod;
			float _turbPower;
			float _turbSize;
			float _uNoiseScale;
			float _uNoiseMag;

			float4 _uVeinColor;
			float4 _uMarbleColor;

			float2 random2(float2 st) {
				st = float2(dot(st, float2(127.1, 311.7)),
					dot(st, float2(269.5, 183.3)));
				return -1.0 + 2.0 * frac(sin(st) * 43758.5453123);
			}

			float noise(float2 st) {
				float2 i = floor(st);
				float2 f = frac(st);

				float2 u = f * f * f * (f * (f * 6. - 15.) + 10.);
				return lerp(lerp(dot(random2(i + float2(0.0, 0.0)), f - float2(0.0, 0.0)),
					dot(random2(i + float2(1.0, 0.0)), f - float2(1.0, 0.0)), u.x),
					lerp(dot(random2(i + float2(0.0, 1.0)), f - float2(0.0, 1.0)),
						dot(random2(i + float2(1.0, 1.0)), f - float2(1.0, 1.0)), u.x), u.y);
			}

			float turbulencia(float4 vvv)
			{
				float fr = 10.0f;
				float fg = 5.0f;
				float fb = 2.5f;
				float fa = 1.25f;
				return abs(noise(fr * vvv) - 0.5) +
					abs(noise(fg * vvv) - 0.5) +
					abs(noise(fb * vvv) - 0.5) +
					abs(noise(fa * vvv) - 0.5);
			}


			v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(pos);
				o.uv = uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
			   float2 vi = i.uv;
			   float4 vv = float4 (vi * _uNoiseScale, 1.0f, 1.0f);
			   float xyValue = vi.x * _xPeriod + vi.y * _yPeriod + _turbPower * clamp(turbulencia(vv) * _turbSize / 256.0, 0.0, 1.0);
			   float sineValue = abs(sin(xyValue * 3.14159));
			   float3 color = lerp(_uVeinColor.rgb,_uMarbleColor.rgb , sineValue);
			   return fixed4(color, 1.0f);       
			}
			ENDCG
		}
	}
}