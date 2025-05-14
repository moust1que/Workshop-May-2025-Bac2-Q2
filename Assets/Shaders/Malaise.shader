
Shader "Hidden/Malaise"
{
    Properties { _MainTex ("Texture", 2D) = "white" {} }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize; 

            struct Varyings {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(uint vertexID : SV_VertexID)
            {
                Varyings o;
                float2 quad[4] = {
                    float2(-1, -1), float2(1, -1),
                    float2(-1, 1),  float2(1, 1)
                };
                o.position = float4(quad[vertexID], 0, 1);
                o.uv = quad[vertexID] * 0.5 + 0.5;
                return o;
            }

            float _TimeFactor;

            float4 frag(Varyings i) : SV_Target
            {
                float2 uv = i.uv;
                uv.x += cos(uv.y * 2.0 + _TimeFactor) * 0.05;
                uv.y += sin(uv.x * 2.0 + _TimeFactor) * 0.05;
                float offset = sin(_TimeFactor * 0.5) * 0.01;

                float4 a = tex2D(_MainTex, uv);
                float4 b = tex2D(_MainTex, uv - float2(sin(offset), 0.0));
                float4 c = tex2D(_MainTex, uv + float2(sin(offset), 0.0));
                float4 d = tex2D(_MainTex, uv - float2(0.0, sin(offset)));
                float4 e = tex2D(_MainTex, uv + float2(0.0, sin(offset)));
                return (a + b + c + d + e) / 5.0;
            }
            ENDHLSL
        }
    }
}