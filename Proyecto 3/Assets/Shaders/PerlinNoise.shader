Shader "PerlinNoise"
{
    Properties
    {
        _Density("Density", Range(1,500)) = 30
    }

    SubShader
{
    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        #define M_PI 3.14159265358979323846
        const float PI = 3.14159265;
        //	Classic Perlin 3D Noise 
        //	by Stefan Gustavson
        float2 random(float2 st) {
            st = float2(dot(st,float2(127.1,311.7)),
                      dot(st,float2(269.5,183.3)));
            return (-1.0 + 2.0 * frac(sin(st) * 43758.5453123));
        }

        // Gradient Noise by Inigo Quilez - iq/2013
        // https://www.shadertoy.com/view/XdXGW8
        float perlinNoise(float2 st) {
            float2 i = floor(st);
            float2 f = frac(st);

            //float2 u = f*f*(3.0-2.0*f);
            float2 u = f * f * f * (f * (f * 6. - 15.) + 10.);
            return lerp(lerp(dot(random(i + float2(0.0,0.0)), f - float2(0.0,0.0)),
                             dot(random(i + float2(1.0,0.0)), f - float2(1.0,0.0)), u.x),
                        lerp(dot(random(i + float2(0.0,1.0)), f - float2(0.0,1.0)),
                             dot(random(i + float2(1.0,1.0)), f - float2(1.0,1.0)), u.x), u.y);
        }

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };   

        v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(pos);
            o.uv = uv;
            return o;
        }

        float _Density;

        fixed4 frag(v2f i) : SV_Target
        {
            float f = 0.0;

            f = perlinNoise(_Density * i.uv);

            f = 0.5 + 0.5 * f;

            fixed4 fragColor = 0;
            fragColor = float4(f, f, f, 1.0);
            return fragColor;
        }
        ENDCG
        }
    }
}