// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learning Unity Shader/Lecture 15/RapidBlurEffect"
{
    //-----------------------------------▽扽俶 || Properties▼------------------------------------------  
    Properties
    {
        //翋恇燴
        _MainTex("Base (RGB)", 2D) = "white" {}
    }

    //----------------------------------▽赽覂伎 || SubShader▼---------------------------------------  
    SubShader
    {
        ZWrite Off
        Blend Off

        //---------------------------------------▽籵耋0 || Pass 0▼------------------------------------
        //籵耋0ㄩ蔥粒欴籵耋 ||Pass 0: Down Sample Pass
        Pass
        {
            ZTest Off
            Cull Off

            CGPROGRAM
            //硌隅森籵耋腔階萸覂伎峈vert_DownSmpl
            #pragma vertex vert_DownSmpl
            //硌隅森籵耋腔砉匼覂伎峈frag_DownSmpl
            #pragma fragment frag_DownSmpl
            ENDCG

        }

        //---------------------------------------▽籵耋1 || Pass 1▼------------------------------------
        //籵耋1ㄩ晶眻源砃耀緇揭燴籵耋 ||Pass 1: Vertical Pass
        Pass
        {
            ZTest Always
            Cull Off

            CGPROGRAM
            //硌隅森籵耋腔階萸覂伎峈vert_BlurVertical
            #pragma vertex vert_BlurVertical
            //硌隅森籵耋腔砉匼覂伎峈frag_Blur
            #pragma fragment frag_Blur
            ENDCG
        }

        //---------------------------------------▽籵耋2 || Pass 2▼------------------------------------
        //籵耋2ㄩ阨源砃耀緇揭燴籵耋 ||Pass 2: Horizontal Pass
        Pass
        {
            ZTest Always
            Cull Off

            CGPROGRAM
            //硌隅森籵耋腔階萸覂伎峈vert_BlurHorizontal
            #pragma vertex vert_BlurHorizontal
            //硌隅森籵耋腔砉匼覂伎峈frag_Blur
            #pragma fragment frag_Blur
            ENDCG
        }
    }


    //-------------------------CG覂伎逄晟汒隴窒煦 || Begin CG Include Part----------------------  
    CGINCLUDE
    //▽1▼芛恅璃婦漪 || include
    #include "UnityCG.cginc"

    //▽2▼曹講汒隴 || Variable Declaration
    sampler2D _MainTex;
    //UnityCG.cginc笢囀离腔曹講ㄛ恇燴笢腔等砉匼喜渡|| it is the size of a texel of the texture
    uniform half4 _MainTex_TexelSize;
    //C#褐掛諷秶腔曹講 || Parameter
    uniform half _DownSampleValue;

    //▽3▼階萸怀賦凳极 || Vertex Input Struct
    struct VertexInput
    {
        //階萸弇离釴梓
        float4 vertex : POSITION;
        //珨撰恇燴釴梓
        half2 texcoord : TEXCOORD0;
    };

    //▽4▼蔥粒欴怀堤賦凳极 || Vertex Input Struct
    struct VertexOutput_DownSmpl
    {
        //砉匼弇离釴梓
        float4 pos : SV_POSITION;
        //珨撰恇燴釴梓ㄗ衵奻ㄘ
        half2 uv20 : TEXCOORD0;
        //媼撰恇燴釴梓ㄗ酘狟ㄘ
        half2 uv21 : TEXCOORD1;
        //撰恇燴釴梓ㄗ衵狟ㄘ
        half2 uv22 : TEXCOORD2;
        //侐撰恇燴釴梓ㄗ酘奻ㄘ
        half2 uv23 : TEXCOORD3;
    };


    //▽5▼袧掘詢佴耀緇笭撻淝統杅7x4腔撻淝 ||  Gauss Weight
    static const half4 GaussWeight[7] =
    {
        half4(0.0205, 0.0205, 0.0205, 0),
        half4(0.0855, 0.0855, 0.0855, 0),
        half4(0.232, 0.232, 0.232, 0),
        half4(0.324, 0.324, 0.324, 1),
        half4(0.232, 0.232, 0.232, 0),
        half4(0.0855, 0.0855, 0.0855, 0),
        half4(0.0205, 0.0205, 0.0205, 0)
    };


    //▽6▼階萸覂伎滲杅 || Vertex Shader Function
    VertexOutput_DownSmpl vert_DownSmpl(VertexInput v)
    {
        //▽6.1▼妗瞰趙珨跺蔥粒欴怀堤賦凳
        VertexOutput_DownSmpl o;

        //▽6.2▼沓喃怀堤賦凳
        //蔚峎諾潔笢腔釴梓芘荌善媼峎敦諳  
        o.pos = UnityObjectToClipPos(v.vertex);
        //勤芞砉腔蔥粒欴ㄩ砉匼奻狟酘衵笚峓腔萸ㄛ煦梗湔衾侐撰恇燴釴梓笢
        o.uv20 = v.texcoord + _MainTex_TexelSize.xy * half2(0.5h, 0.5h);;
        o.uv21 = v.texcoord + _MainTex_TexelSize.xy * half2(-0.5h, -0.5h);
        o.uv22 = v.texcoord + _MainTex_TexelSize.xy * half2(0.5h, -0.5h);
        o.uv23 = v.texcoord + _MainTex_TexelSize.xy * half2(-0.5h, 0.5h);

        //▽6.3▼殿隙郔笝腔怀堤賦彆
        return o;
    }

    //▽7▼僇覂伎滲杅 || Fragment Shader Function
    fixed4 frag_DownSmpl(VertexOutput_DownSmpl i) : SV_Target
    {
        //▽7.1▼隅砱珨跺還奀腔晇伎硉
        fixed4 color = (0, 0, 0, 0);

        //▽7.2▼侐跺眈邁砉匼萸揭腔恇燴硉眈樓
        color += tex2D(_MainTex, i.uv20);
        color += tex2D(_MainTex, i.uv21);
        color += tex2D(_MainTex, i.uv22);
        color += tex2D(_MainTex, i.uv23);

        //▽7.3▼殿隙郔笝腔歙硉
        return color / 4;
    }

    //▽8▼階萸怀賦凳极 || Vertex Input Struct
    struct VertexOutput_Blur
    {
        //砉匼釴梓
        float4 pos : SV_POSITION;
        //珨撰恇燴ㄗ恇燴釴梓ㄘ
        half4 uv : TEXCOORD0;
        //媼撰恇燴ㄗ痄講ㄘ
        half2 offset : TEXCOORD1;
    };

    //▽9▼階萸覂伎滲杅 || Vertex Shader Function
    VertexOutput_Blur vert_BlurHorizontal(VertexInput v)
    {
        //▽9.1▼妗瞰趙珨跺怀堤賦凳
        VertexOutput_Blur o;

        //▽9.2▼沓喃怀堤賦凳
        //蔚峎諾潔笢腔釴梓芘荌善媼峎敦諳  
        o.pos = UnityObjectToClipPos(v.vertex);
        //恇燴釴梓
        o.uv = half4(v.texcoord.xy, 1, 1);
        //數呾X源砃腔痄講
        o.offset = _MainTex_TexelSize.xy * half2(1.0, 0.0) * _DownSampleValue;

        //▽9.3▼殿隙郔笝腔怀堤賦彆
        return o;
    }

    //▽10▼階萸覂伎滲杅 || Vertex Shader Function
    VertexOutput_Blur vert_BlurVertical(VertexInput v)
    {
        //▽10.1▼妗瞰趙珨跺怀堤賦凳
        VertexOutput_Blur o;

        //▽10.2▼沓喃怀堤賦凳
        //蔚峎諾潔笢腔釴梓芘荌善媼峎敦諳  
        o.pos = UnityObjectToClipPos(v.vertex);
        //恇燴釴梓
        o.uv = half4(v.texcoord.xy, 1, 1);
        //數呾Y源砃腔痄講
        o.offset = _MainTex_TexelSize.xy * half2(0.0, 1.0) * _DownSampleValue;

        //▽10.3▼殿隙郔笝腔怀堤賦彆
        return o;
    }

    //▽11▼僇覂伎滲杅 || Fragment Shader Function
    half4 frag_Blur(VertexOutput_Blur i) : SV_Target
    {
        //▽11.1▼鳳埻宎腔uv釴梓
        half2 uv = i.uv.xy;

        //▽11.2▼鳳痄講
        half2 OffsetWidth = i.offset;
        //植笢陑萸痄3跺潔路ㄛ植郔酘麼郔奻羲宎樓濛樓
        half2 uv_withOffset = uv - OffsetWidth * 3.0;

        //▽11.3▼悜遠鳳樓綴腔晇伎硉
        half4 color = 0;
        for (int j = 0; j < 7; j++)
        {
            //痄綴腔砉匼恇燴硉
            half4 texCol = tex2D(_MainTex, uv_withOffset);
            //渾怀堤晇伎硉+=痄綴腔砉匼恇燴硉 x 詢佴笭
            color += texCol * GaussWeight[j];
            //痄善狟珨跺砉匼揭ㄛ袧掘狟珨棒悜遠樓
            uv_withOffset += OffsetWidth;
        }

        //▽11.4▼殿隙郔笝腔晇伎硉
        return color;
    }

    //-------------------賦旰CG覂伎逄晟汒隴窒煦  || End CG Programming Part------------------  			
    ENDCG

    FallBack Off
}