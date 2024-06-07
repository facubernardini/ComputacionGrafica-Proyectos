Shader "Multitextura"
{
    Properties
    {
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

		[Header(Material)]
			_MaterialKa("MaterialKa", Color) = (0, 0, 0, 0)
			_MaterialKd("MaterialKd", Color) = (0, 0, 0, 0)
			_MaterialKs("MaterialKs", Color) = (0, 0, 0, 0)
			_Roughness("Roughness", Range(0.001, 1)) = 0.1

		_F0("Fresnel Reflectance", Range(0, 1000)) = 0.8

        [NoScaleOffset] _MainTex1 ("Texture", 2D) = "white" {}
        [NoScaleOffset] _MainTex2 ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass {
			CGPROGRAM

			#pragma vertex vertexShader
			#pragma fragment fragmentShader 
			#include "UnityCG.cginc"

			struct vertexData {
				float4 position : POSITION; // Object space
				float3 normal : NORMAL; // Object space

                float2 uv : TEXCOORD3;
			};

			struct v2f {
				float4 position : SV_POSITION; // Clipping space
				float4 position_w : TEXCOORD0;
				float3 normal_w : TEXCOORD1;

                float2 uv : TEXCOORD3;
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

			float4 _MaterialKa;
			float4 _MaterialKd;
			float4 _MaterialKs;
			float _F0;
			float _Roughness;

			v2f vertexShader(vertexData v) {

				v2f output;
				output.position = UnityObjectToClipPos(v.position);
				output.position_w = mul(unity_ObjectToWorld, v.position);
				output.normal_w = UnityObjectToWorldNormal(v.normal);

                output.uv = v.uv;

				return output;
			}

            sampler2D _MainTex1;
            sampler2D _MainTex2;

			fixed4 fragmentShader (v2f f) : SV_Target { 

				fixed4 fragColor = 0; 
				float3 ambient = 0;
				float3 diffuse = 0;
				float3 specular = 0;

				fixed4 luzSpot, luzPuntual, luzDireccional;

				float3 L = normalize(_PointLightPosition_w.xyz - f.position_w.xyz);
				float3 Ldir = normalize(-_DirectionalLightDirection.xyz);
				float3 Lspot = normalize(_SpotLightPosition_w.xyz - f.position_w.xyz);
				float3 LspotDir = normalize(-_SpotLightDirection);
				float3 N = normalize(f.normal_w);
				float3 V = normalize(_WorldSpaceCameraPos.xyz - f.position_w.xyz);
				float3 H = normalize(L + V);

				float NdotH = max(0, dot(N, H));
				float NdotV = max(0, dot(N, V));
				float NdotL = max(0, dot(N, L));
				float VdotH = max(0, dot(L, H));

				// Aplico las texturas
                fixed4 col1 = tex2D(_MainTex1, f.uv);
                fixed4 col2 = tex2D(_MainTex2, f.uv);

				// LUZ DIRECCIONAL
                float diffCoefDir = max(0, dot(N, Ldir));
                luzDireccional.rgb = diffCoefDir * _DirectionalLightColor;

				// LUZ PUNTUAL
				// Fresnel reflectance (Schlick)
				float F1 = pow(1.0 - VdotH, 5.0);
				float F2 = (1.0 - _F0);
				float F = _F0 + (F1*F2);

				// Microfacet distribution (Beckmann)
				float pi = 3.1415f;
				float alpha = _Roughness * _Roughness;
				float r1 = 1.0 / (pi * alpha * pow(NdotH, 4.0));
				float r2 = (NdotH * NdotH - 1.0) / (alpha * NdotH * NdotH);
				float D = r1 * exp(r2);

				// Geometric shadowing (Smith | Schlick-GGX)
				float k = alpha/2;
				float g1 = NdotV / (NdotV * (1-k) + k);
				float g2 = NdotL / (NdotL * (1-k) + k);
				float G = g1*g2;	

				float Rs = (F * D * G) / (4.0f * NdotL * NdotV);

				specular = Rs * _MaterialKs * _PointLightIntensity;
				diffuse = _PointLightIntensity * _MaterialKd * NdotL;
				ambient = _PointAmbientLight * _MaterialKa;

				luzPuntual.rgb = max(0, specular) + diffuse + ambient;

				// LUZ SPOT
                float diffCoefSpot = 0;

                float angulo = dot(Lspot, LspotDir);
				if (angulo > 1 - (_Apertura/90))
					diffCoefSpot = max(0, dot(N, L));

				luzSpot.rgb = diffCoefSpot * _SpotLightColor;

				fragColor.rgb = (max(0, luzSpot) + max(0, luzPuntual) + max(0, luzDireccional)) * (col1 + col2);

				return fragColor;
			}
			ENDCG
		} 
    }
}
