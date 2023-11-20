Shader "Custom/TextureRevealShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _RevealTex ("Reveal Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;
        sampler2D _RevealTex;
        float _Cutoff;
        
        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
        };
        
        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 baseColor = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 revealColor = tex2D(_RevealTex, IN.uv_MainTex);
            float lighting = dot(IN.worldNormal, _WorldSpaceLightPos0);
            
            if (lighting > _Cutoff)
            {
                o.Albedo = baseColor * revealColor;
            }
            else
            {
                o.Albedo = baseColor;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
