Shader "Blinn-Phong"
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
			_Material_n("Material_n", Float) = 0.5
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
			float _Material_n;

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
				
				// Ambient
				ambient = _AmbientLight * _MaterialKa;

				// Diffuse
				diffuse = _LightIntensity * _MaterialKd * dot(L, N);

				// Specular
				float3 V = normalize(_WorldSpaceCameraPos.xyz - f.position_w.xyz);
				float3 h = (L+V)/2;
				specular = _LightIntensity * _MaterialKs * pow(dot(h, N), _Material_n);

				fragColor.rgb = ambient + diffuse + specular;

				return fragColor;
			}
			ENDCG
		} 
    }
}