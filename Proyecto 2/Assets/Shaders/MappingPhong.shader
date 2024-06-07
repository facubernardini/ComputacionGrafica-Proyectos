Shader "MappingPhong"
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
            _Material_n("Material_n", Float) = 0.5
            _MaterialKs("MaterialKs", Color) = (0, 0, 0, 0)

        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexShader
			#pragma fragment fragmentShader
            #include "UnityCG.cginc"

            struct appdata
            {
                float2 uv : TEXCOORD0; // texture coordinate

                float4 position : POSITION;
				float3 normal : NORMAL;
            };

            // Luz Puntual
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
            float4 _MaterialKs;
			float _Material_n;

            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate

                float4 position : SV_POSITION;
                float4 position_w : TEXCOORD2;
				float3 normal_w : TEXCOORD1;
            };

            v2f vertexShader(appdata v)
            {
                v2f output;
				output.position = UnityObjectToClipPos(v.position);
				output.position_w = mul(unity_ObjectToWorld, v.position);
				output.normal_w = UnityObjectToWorldNormal(v.normal);

                output.uv = v.uv;

				return output;
            }
            
            sampler2D _MainTex;

            fixed4 fragmentShader (v2f f) : SV_Target
            {
                fixed4 fragColor = 0; 
				float3 ambient = 0;
				float3 specular = 0;

                fixed4 textura = tex2D(_MainTex, f.uv);

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
				float3 R = normalize(reflect(-L, N));
				float3 V = normalize(_WorldSpaceCameraPos.xyz - f.position_w.xyz);
                float diffCoefPuntual = dot(L, N);
				specular = _PointLightIntensity * _MaterialKs * pow(max(0, dot(R, V)), _Material_n);
                luzPuntual.rgb = diffCoefPuntual * textura * _PointLightIntensity + specular;

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