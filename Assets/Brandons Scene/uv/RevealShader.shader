Shader "Custom/ThresholdTextureShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Threshold("Threshold", Range(0.0, 1.0)) = 0.5
        _HiddenColor("Hidden Color", Color) = (1, 0, 0, 1) // Default red color for hidden state
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Threshold;
            fixed4 _HiddenColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                // Calculate light intensity manually (example: using dot product with world normal)
                float3 worldNormal = normalize(mul(float4(0, 0, 1, 0), unity_WorldToObject).xyz);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz - i.vertex.xyz);
                float lightIntensity = dot(worldNormal, lightDirection);

                // Check if light intensity is higher than threshold
                if (lightIntensity >= _Threshold) {
                    color *= _HiddenColor; // Modify the color using the hidden color property
                }

                return color;
            }
            ENDCG
        }
    }
}