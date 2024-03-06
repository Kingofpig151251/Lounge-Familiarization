Shader "Unlit/DoubleSide"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MirrorU("Flip",float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 100

        Cull off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _MirrorU;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = v.texcoord;
                if (_MirrorU > 0)
                {
                    o.uv.x = 1 - o.uv.x;
                }

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}