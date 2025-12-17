Shader "Custom/StencilVisible"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Stencil
        {
            Ref 1
            Pass replace      // 스텐실 값을 1로 기록
        }

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
}
