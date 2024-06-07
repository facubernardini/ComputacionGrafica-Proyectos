Shader "LuzPuntual" {

	Properties {
		_MaterialColor("Material Color", Color) = (0.25, 0.5, 0.5, 1) 
		_LightColor("Light Color", Color) = (1, 1, 1, 1)
		_LightPosition_w("Light Position (World)", Vector) = (0, 5, 0, 1)
	}

	SubShader {
		Tags { "RenderType" = "Opaque" } 
		LOD 100

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

			float4 _MaterialColor; 
			float4 _LightPosition_w;
			float4 _LightColor;

			v2f vertexShader(vertexData v) {

				float4 position_w = mul(unity_ObjectToWorld, v.position); 
				float3 normal_w = normalize(UnityObjectToWorldNormal(v.normal));

				v2f output;

				output.position = UnityObjectToClipPos(v.position);
				output.position_w = position_w;
				output.normal_w = normal_w;

				return output;
			}

			fixed4 fragmentShader (v2f f) : SV_Target { 

				float3 L = normalize(_LightPosition_w.xyz - f.position_w.xyz);
				float3 N = normalize(f.normal_w);
				float diffCoef = max(0, dot(N, L));
				fixed4 fragColor = 0; 
				fragColor.rgb = diffCoef * _MaterialColor * _LightColor; 

				return fragColor;
			}
			ENDCG
		} 
	}
}