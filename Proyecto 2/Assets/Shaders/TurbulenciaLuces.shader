Shader "TurbulenciaLuces"
{
	Properties
	{
        [Header(Propiedades)]
            _xPeriod("Repeticion X", float) = 4.0
            _yPeriod("Repeticion Y", float) = 4.1
            _turbPower("Poder de Turbulencia", float) = 0.95
            _turbSize("Tamano de la Turbulencia", float) = 80.0
            _uNoiseScale("Ruido", float) = 0.85
            _uVeinColor("Color Vetas", Color) = (0.3,0.2,0.2,1)
            _uMarbleColor("Color Fondo", Color) = (0.6,0.6,0.6,1)

        [Header(Luz Puntual)]
			_PointLightIntensity("Point Light Intensity", Color) = (1, 1, 1, 1)
			_PointLightPosition_w("Point Light Position (Word)", Vector) = (0, 5, 0, 1)
			_PointAmbientLight("Point Ambient Light", Color) = (1, 1, 1, 1)

        [Header(Luz Direccional)]
            _DirectionalLightColor("Directional Light Color", Color) = (1, 1, 1, 1)
		    _DirectionalLightDirection("Directioanl Light Direction", Vector) = (0, 5, 0, 1)

        [Header(Luz Spot)]
            _SpotLightColor("Spot Light Color", Color) = (1, 1, 1, 1)
            _SpotLightPosition_w("Spot Light Position (World)", Vector) = (0, 5, 0, 1)
            _SpotLightDirection("Spot Light Direction", Vector) = (0, 5, 0, 1)
            _Apertura("Apertura", Range(0.0, 90)) = 1
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

            struct appdata
            {
                float2 uv : TEXCOORD0; // texture coordinate

                float4 position : POSITION;
				float3 normal : NORMAL;
            };

            float4 _SpotLightPosition_w;
			float4 _SpotLightDirection;
			float4 _SpotLightColor;
			float _Apertura;

			float4 _DirectionalLightColor;
			float4 _DirectionalLightDirection;

			float4 _PointLightIntensity;
			float4 _PointLightPosition_w;
			float4 _PointAmbientLight;

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;

                float4 position_w : TEXCOORD2;
				float3 normal_w : TEXCOORD1;
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

			v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0, appdata v)
			{
				v2f output;

				output.vertex = UnityObjectToClipPos(v.position);
				output.position_w = mul(unity_ObjectToWorld, v.position);
				output.normal_w = normalize(UnityObjectToWorldNormal(v.normal));

				output.uv = uv;
                
                return output;
			}

			fixed4 frag(v2f f) : SV_Target
			{
				// Genero el color
                float2 vi = f.uv;
                float4 vv = float4 (vi * _uNoiseScale, 1.0f, 1.0f);
                float xyValue = vi.x * _xPeriod + vi.y * _yPeriod + _turbPower * clamp(turbulencia(vv) * _turbSize / 256.0, 0.0, 1.0);
                float sineValue = abs(sin(xyValue * 3.14159));
                float3 color = lerp(_uVeinColor.rgb,_uMarbleColor.rgb , sineValue);

				fixed4 fragColor = 0; 
				float3 ambient = 0;
				float3 diffuse = 0;

				fixed4 luzSpot, luzPuntual, luzDireccional;

				float3 L = normalize(_PointLightPosition_w.xyz - f.position_w.xyz);
				float3 N = normalize(f.normal_w);

				float3 Ldir = normalize(-_DirectionalLightDirection.xyz);

				float3 Lspot = normalize(_SpotLightPosition_w.xyz - f.position_w.xyz);
				float3 LspotDir = normalize(-_SpotLightDirection);

				// LUZ DIRECCIONAL
                float diffCoefDir = max(0, dot(N, Ldir));
                luzDireccional.rgb = diffCoefDir * color * _DirectionalLightColor; 

				// LUZ PUNTUAL
				diffuse = _PointLightIntensity * color * dot(L, N);
				luzPuntual.rgb = ambient + diffuse;

				// LUZ SPOT
                float diffCoefSpot = 0;
                float angulo = dot(Lspot, LspotDir);
				
				if (angulo > 1 - (_Apertura/90))
					diffCoefSpot = max(0, dot(N, Lspot));

				luzSpot.rgb = diffCoefSpot * color * _SpotLightColor;

				fragColor.rgb = max(0, luzSpot) + max(0, luzPuntual) + max(0, luzDireccional);

		        return fragColor;       
			}
			ENDCG
		}
	}
}