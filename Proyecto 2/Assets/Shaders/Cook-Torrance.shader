Shader "Cook-Torrance"
{
    Properties
    {
		[Header(Light)]
			_LightIntensity("Light Intensity", Color) = (1, 1, 1, 1)
			_LightPosition_w("Light Position (Word)", Vector) = (0, 5, 0, 1)
			_AmbientLight("Ambient Light", Color) = (1, 1, 1, 1)

		[Header(Material)]
			_MaterialKa("MaterialKa", Color) = (0, 0, 0, 0)
			_MaterialKd("MaterialKd", Color) = (0, 0, 0, 0)
			_MaterialKs("MaterialKs", Color) = (0, 0, 0, 0)
			_Roughness("Roughness", Range(0.001, 1)) = 0.1

		_F0("Fresnel Reflectance", Range(0, 1000)) = 0.8
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
			};

			struct v2f {
				float4 position : SV_POSITION; // Clipping space
				float4 position_w : TEXCOORD0;
				float3 normal_w : TEXCOORD1;
			};

			float4 _LightIntensity; 
			float4 _LightPosition_w;
			float4 _AmbientLight;

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

				return output;
			}

			fixed4 fragmentShader (v2f f) : SV_Target { 

				fixed4 fragColor = 0; 
				float3 ambient = 0;
				float3 diffuse = 0;
				float3 specular = 0;

				float3 L = normalize(_LightPosition_w.xyz - f.position_w.xyz);
				float3 N = normalize(f.normal_w);
				float3 V = normalize(_WorldSpaceCameraPos.xyz - f.position_w.xyz);
				float3 H = normalize(L + V);

				float NdotH = max(0, dot(N, H));
				float NdotV = max(0, dot(N, V));
				float NdotL = max(0, dot(N, L));
				float VdotH = max(0, dot(L, H));

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

				specular = Rs * _MaterialKs * _LightIntensity;
				diffuse = _LightIntensity * _MaterialKd * NdotL;
				ambient = _AmbientLight * _MaterialKa;

				fragColor.rgb = max(0, specular) + diffuse + ambient;

				return fragColor;
			}
			ENDCG
		} 
    }
}