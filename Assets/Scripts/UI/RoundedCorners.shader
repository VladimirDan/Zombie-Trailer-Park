Shader "UI/RoundedCorners" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _CornerRadius ("Corner Radius", Range(0, 100)) = 20
    }
    SubShader {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _CornerRadius;

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                // Определяем закруглённые углы
                float2 uv = (i.uv - 0.5) * 2.0;
                float len = length(max(abs(uv) - (1.0 - _CornerRadius), 0.0));
                // Если в пределах радиуса, возвращаем цвет
                if (len < 0.0) {
                    return col;
                }
                return fixed4(0, 0, 0, 0); // Полностью прозрачный
            }
            ENDCG
        }
    }
}
