// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Malaise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
    }
    SubShader
    {
        	Tags { "RenderType" = "Opaque" }
             Cull Off
             ZWrite Off
             ZTest Always

        Pass
        {
		
           
			 CGPROGRAM
            #pragma vertex vertex_shader
			#pragma fragment pixel_shader
			#pragma target 2.0


            sampler2D _MainTex;
			float4 _MainTex_ST;
            float4 _Time;
            float4 _ScreenParams;

			 struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
              struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.x += cos(uv.y * 2.0 + _Time.g) * 0.05;
                uv.y += sin(uv.x * 2.0 + _Time.g) * 0.05;

                float offset = sin(_Time.g * 0.5) * 0.01;

                fixed4 a = tex2D(_MainTex, uv);
                fixed4 b = tex2D(_MainTex, uv - float2(sin(offset), 0.0));
                fixed4 c = tex2D(_MainTex, uv + float2(sin(offset), 0.0));
                fixed4 d = tex2D(_MainTex, uv - float2(0.0, sin(offset)));
                fixed4 e = tex2D(_MainTex, uv + float2(0.0, sin(offset)));

                return (a + b + c + d + e) / 5.0;
            }
            ENDCG
        }
    }
}
