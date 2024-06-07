Shader "Toon"
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

            // Luz puntual
			float4 _PointLightIntensity; 
			float4 _PointLightPosition_w;
			float4 _PointAmbientLight;

            // Luz Direccional
            float4 _DirectionalLightDirection;
			float4 _DirectionalLightColor;

            // Luz Spot
            float4 _SpotLightPosition_w;
			float4 _SpotLightColor;
			float3 _SpotLightDirection;
			float _Apertura;

			// Material
			float4 _MaterialKa;
			float4 _MaterialKd;
			float4 _MaterialKs;
			float _Material_n;

			v2f vertexShader(vertexData v) 
			{
				v2f output;
				output.position = UnityObjectToClipPos(v.position);
				output.position_w = mul(unity_ObjectToWorld, v.position);
				output.normal_w = normalize(UnityObjectToWorldNormal(v.normal));

				return output;
			}

			fixed4 fragmentShader (v2f f) : SV_Target 
			{ 
				fixed4 fragColor = 0; 
				float3 ambient = 0;
				float3 diffuse = 0;
				float3 specular = 0;

				fixed4 luzSpot, luzPuntual, luzDireccional;

				float3 L = normalize(_PointLightPosition_w.xyz - f.position_w.xyz);
				float3 N = normalize(f.normal_w);

                float3 Ldir = normalize(-_DirectionalLightDirection.xyz);

                float3 Lspot = normalize(_SpotLightPosition_w.xyz - f.position_w.xyz);
                float3 LspotDir = normalize(-_SpotLightDirection);

                // LUZ DIRECCIONAL
                float diffCoefDir = max(0, dot(N, Ldir));

				if (dot(N, Ldir) >= 0)
					diffCoefDir = 1;
				else
					diffCoefDir = 0;

                luzDireccional.rgb = diffCoefDir * _MaterialKd * _DirectionalLightColor; 
				
                // LUZ PUNTUAL
				// Ambient
				ambient = _PointAmbientLight * _MaterialKa;

				// Diffuse
				float diffCoefPoint = 0;
				if (dot(N, L) >= 0)
					diffCoefPoint = 1;
				else
					diffCoefPoint = 0;
				diffuse = _PointLightIntensity * _MaterialKd * diffCoefPoint;

				// Specular
				float3 V = normalize(_WorldSpaceCameraPos.xyz - f.position_w.xyz);
				float3 h = (L+V)/2;
				specular = _PointLightIntensity * _MaterialKs * pow(dot(h, N), _Material_n);
                
				luzPuntual.rgb = ambient + diffuse + specular;

                // LUZ SPOT
                float diffCoefSpot = 0;
                float angulo = dot(Lspot, LspotDir);
				
				if (angulo > 1 - (_Apertura/90))
				{
					if (dot(N, Lspot) >= 0)
						diffCoefSpot = 1;
					else
						diffCoefSpot = 0;
				}

				luzSpot.rgb = diffCoefSpot * _MaterialKd * _SpotLightColor;

				fragColor.rgb = max(0, luzSpot) + max(0, luzPuntual) + max(0, luzDireccional);

				return fragColor;
			}
			ENDCG
		} 
    }
}