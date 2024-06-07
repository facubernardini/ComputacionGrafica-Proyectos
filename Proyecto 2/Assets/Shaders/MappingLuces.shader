Shader "MappingLuces"
{
	Properties{

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

		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
	}

	SubShader{

		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass {
			CGPROGRAM

			#pragma vertex vertexShader
			#pragma fragment fragmentShader 
			#include "UnityCG.cginc"

			struct vertexData {
				float4 position : POSITION; 
				float3 normal : NORMAL; 
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION; 
				float4 position_w : TEXCOORD0;
				float3 normal_w : TEXCOORD1;
				float2 uv: TEXCOORD2;

			};

			// Luz Spot
			float4 _SpotLightPosition_w;
			float4 _SpotLightDirection;
			float4 _SpotLightColor;
			float _Apertura;

			// Luz Direccional
			float4 _DirectionalLightColor;
			float4 _DirectionalLightDirection;

			// Luz Puntual
			float4 _PointLightIntensity;
			float4 _PointLightPosition_w;
			float4 _PointAmbientLight;

			sampler2D _MainTex;

			v2f vertexShader(vertexData v) {

				float4 position_w = mul(unity_ObjectToWorld, v.position);
				float3 normal_w = normalize(UnityObjectToWorldNormal(v.normal));

				v2f output;
				output.position = UnityObjectToClipPos(v.position);
				output.position_w = position_w;
				output.normal_w = normal_w;
				output.uv = v.uv;

				return output;
			}

			fixed4 fragmentShader (v2f f) : SV_Target
			{
				fixed4 textura = tex2D(_MainTex, f.uv);

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
                luzDireccional.rgb = diffCoefDir * textura * _DirectionalLightColor; 

				// LUZ PUNTUAL
				diffuse = _PointLightIntensity * textura * dot(L, N);
				luzPuntual.rgb = ambient + diffuse;

				// LUZ SPOT
                float diffCoefSpot = 0;
                float angulo = dot(Lspot, LspotDir);

				if (angulo > 1 - (_Apertura/90))
					diffCoefSpot = max(0, dot(N, L));

				luzSpot.rgb = diffCoefSpot * textura * _SpotLightColor;

				fragColor.rgb = max(0, luzSpot) + max(0, luzPuntual) + max(0, luzDireccional);

				return fragColor;
			}
			ENDCG
		}
	}
}