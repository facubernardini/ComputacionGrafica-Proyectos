Shader "LucesJuntas"
{
    Properties
    {
        _MaterialColor("Material Color", Color) = (0.25, 0.5, 0.5, 1)

        _LightSpotColor("Light Spot Color", Color) = (1, 1, 1, 1)
        _LightSpotPosition_w("Light Spot Position (World)", Vector) = (0, 5, 0, 1)
        _LightSpotDirection("Light Spot Direction", Vector) = (0, 5, 0, 1)
		_Apertura("Apertura Spot", Range(0.0, 90)) = 1

		_LightDirectionalColor("Light Directional Color", Color) = (1, 1, 1, 1)
		_LightDirectionalDirection("Light Directional Direction", Vector) = (0, 5, 0, 1)

		_LightPointColor("Light Point Color", Color) = (1, 1, 1, 1)
		_LightPointPosition_w("Light Point Position (World)", Vector) = (0, 5, 0, 1)
    }
    SubShader
    {
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

			float4 _LightSpotColor;
			float4 _LightSpotPosition_w;
			float4 _LightSpotDirection;
			float _Apertura;

			float4 _LightDirectionalColor;
			float4 _LightDirectionalDirection;

			float4 _LightPointColor;
			float3 _LightPointPosition_w;

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

				fixed4 fragColor = 0;
				float diffCoefDirectional = 0;
				float diffCoefPoint = 0;
				float diffCoefSpot = 0;
				
				// Luz Direccional
				float3 Ldirec = normalize(-_LightDirectionalDirection.xyz);
				float3 N = normalize(f.normal_w);
				diffCoefDirectional = max(0, dot(N, Ldirec));

				// Luz Puntual
				float3 Lpoint = normalize(_LightPointPosition_w.xyz - f.position_w.xyz);
				diffCoefPoint = max(0, dot(N, Lpoint));

				// Luz Spot
				float3 Lspot = normalize(_LightSpotPosition_w.xyz - f.position_w.xyz);
				float3 Ldir = normalize(-_LightSpotDirection);
				
				float angulo = dot(Lspot, Ldir);
				if (angulo > 1 - (_Apertura/90))
					diffCoefSpot = max(0, dot(N, Ldir));


				fragColor.rgb += (diffCoefSpot * _MaterialColor * _LightSpotColor);
				fragColor.rgb += (diffCoefDirectional * _MaterialColor * _LightDirectionalColor);
				fragColor.rgb += (diffCoefPoint * _MaterialColor * _LightPointColor);

				return fragColor;
			}
			ENDCG
		} 
    }
}