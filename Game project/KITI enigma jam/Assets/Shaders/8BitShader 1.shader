Shader "Custom/8BitShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", Range(1, 10)) = 2
    }

        SubShader{
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _PixelSize;

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.r = floor(col.r * 255.0 / _PixelSize) / (255.0 / _PixelSize);
                    col.g = floor(col.g * 255.0 / _PixelSize) / (255.0 / _PixelSize);
                    col.b = floor(col.b * 255.0 / _PixelSize) / (255.0 / _PixelSize);
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}