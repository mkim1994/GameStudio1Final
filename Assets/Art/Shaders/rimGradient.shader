// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33570,y:33219,varname:node_4013,prsc:2|diff-4193-OUT,emission-8808-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4997,x:29709,y:33466,varname:node_4997,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-9980-UVOUT;n:type:ShaderForge.SFN_Lerp,id:7066,x:30767,y:33179,varname:node_7066,prsc:2|A-9893-RGB,B-1209-RGB,T-6505-OUT;n:type:ShaderForge.SFN_Blend,id:5829,x:32189,y:32766,varname:node_5829,prsc:2,blmd:1,clmp:True|SRC-8724-RGB,DST-7066-OUT;n:type:ShaderForge.SFN_Color,id:9893,x:30158,y:32884,ptovrint:False,ptlb:gradient top color,ptin:_gradienttopcolor,varname:node_9893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9044118,c2:0.1662522,c3:0.1662522,c4:1;n:type:ShaderForge.SFN_Color,id:1209,x:30170,y:33077,ptovrint:False,ptlb:gradient btm color,ptin:_gradientbtmcolor,varname:node_1209,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1543044,c2:0.9485294,c3:0.04882136,c4:1;n:type:ShaderForge.SFN_Tex2d,id:8724,x:31558,y:32438,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_8724,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5d207c57ce01703409144b76214c4cc3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:6179,x:30095,y:33432,varname:node_6179,prsc:2|IN-2206-OUT;n:type:ShaderForge.SFN_ScreenPos,id:4209,x:29149,y:33431,varname:node_4209,prsc:2,sctp:1;n:type:ShaderForge.SFN_Clamp01,id:2206,x:29882,y:33432,varname:node_2206,prsc:2|IN-4997-OUT;n:type:ShaderForge.SFN_Panner,id:9980,x:29415,y:33497,varname:node_9980,prsc:2,spu:1,spv:1|UVIN-4209-UVOUT,DIST-3992-OUT;n:type:ShaderForge.SFN_Slider,id:3992,x:29049,y:33628,ptovrint:False,ptlb:gradient position,ptin:_gradientposition,varname:node_3992,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.5129614,max:2;n:type:ShaderForge.SFN_Divide,id:6505,x:30356,y:33475,varname:node_6505,prsc:2|A-6179-OUT,B-9735-OUT;n:type:ShaderForge.SFN_Slider,id:9735,x:29882,y:33677,ptovrint:False,ptlb:gradient falloff,ptin:_gradientfalloff,varname:node_9735,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.01,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:5797,x:30818,y:34245,ptovrint:False,ptlb:rim color,ptin:_rimcolor,varname:node_5797,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9724138,c3:0,c4:1;n:type:ShaderForge.SFN_ChannelBlend,id:9133,x:32142,y:33793,varname:node_9133,prsc:2,chbt:0|M-491-OUT,R-5797-RGB;n:type:ShaderForge.SFN_NormalVector,id:5089,x:30845,y:33515,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:6595,x:30829,y:33667,varname:node_6595,prsc:2;n:type:ShaderForge.SFN_Dot,id:1706,x:31136,y:33525,varname:node_1706,prsc:2,dt:0|A-5089-OUT,B-6595-OUT;n:type:ShaderForge.SFN_OneMinus,id:4858,x:31318,y:33645,varname:node_4858,prsc:2|IN-1706-OUT;n:type:ShaderForge.SFN_Divide,id:3168,x:31566,y:33672,varname:node_3168,prsc:2|A-4858-OUT,B-2773-OUT;n:type:ShaderForge.SFN_Slider,id:2773,x:31046,y:33848,ptovrint:False,ptlb:rim position,ptin:_rimposition,varname:node_2773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.01,cur:1.135316,max:5;n:type:ShaderForge.SFN_Power,id:491,x:31869,y:33617,varname:node_491,prsc:2|VAL-3168-OUT,EXP-7430-OUT;n:type:ShaderForge.SFN_Slider,id:7430,x:31409,y:33887,ptovrint:False,ptlb:rim falloff,ptin:_rimfalloff,varname:node_7430,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.070219,max:10;n:type:ShaderForge.SFN_Multiply,id:8546,x:32954,y:33624,varname:node_8546,prsc:2|A-1622-OUT,B-8660-OUT;n:type:ShaderForge.SFN_Slider,id:8660,x:32563,y:33961,ptovrint:False,ptlb:rim light amount,ptin:_rimlightamount,varname:node_8660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:8808,x:33183,y:33420,varname:node_8808,prsc:2|A-9895-OUT,B-8546-OUT;n:type:ShaderForge.SFN_Multiply,id:9895,x:32879,y:33268,varname:node_9895,prsc:2|A-5829-OUT,B-8786-OUT;n:type:ShaderForge.SFN_Slider,id:8786,x:32205,y:33382,ptovrint:False,ptlb:emissive amount,ptin:_emissiveamount,varname:node_8786,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_OneMinus,id:8895,x:32683,y:33441,varname:node_8895,prsc:2|IN-8786-OUT;n:type:ShaderForge.SFN_Multiply,id:4193,x:33118,y:33088,varname:node_4193,prsc:2|A-5829-OUT,B-8895-OUT;n:type:ShaderForge.SFN_Fresnel,id:2843,x:32142,y:33642,varname:node_2843,prsc:2;n:type:ShaderForge.SFN_ChannelBlend,id:1622,x:32429,y:33831,varname:node_1622,prsc:2,chbt:0|M-2843-OUT,R-9133-OUT;n:type:ShaderForge.SFN_ObjectScale,id:5674,x:31725,y:33154,varname:node_5674,prsc:2,rcp:False;proporder:8724-9893-1209-3992-9735-5797-2773-7430-8660-8786;pass:END;sub:END;*/

Shader "Shader Forge/rimGradient" {
    Properties {
        _texture ("texture", 2D) = "white" {}
        _gradienttopcolor ("gradient top color", Color) = (0.9044118,0.1662522,0.1662522,1)
        _gradientbtmcolor ("gradient btm color", Color) = (0.1543044,0.9485294,0.04882136,1)
        _gradientposition ("gradient position", Range(-1, 2)) = 0.5129614
        _gradientfalloff ("gradient falloff", Range(0.01, 1)) = 1
        _rimcolor ("rim color", Color) = (1,0.9724138,0,1)
        _rimposition ("rim position", Range(0.01, 5)) = 1.135316
        _rimfalloff ("rim falloff", Range(0, 10)) = 2.070219
        _rimlightamount ("rim light amount", Range(0, 1)) = 0
        _emissiveamount ("emissive amount", Range(0, 1)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _gradienttopcolor;
            uniform float4 _gradientbtmcolor;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _gradientposition;
            uniform float _gradientfalloff;
            uniform float4 _rimcolor;
            uniform float _rimposition;
            uniform float _rimfalloff;
            uniform float _rimlightamount;
            uniform float _emissiveamount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 node_5829 = saturate((_texture_var.rgb*lerp(_gradienttopcolor.rgb,_gradientbtmcolor.rgb,((1.0 - saturate((float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+_gradientposition*float2(1,1)).g))/_gradientfalloff))));
                float3 diffuseColor = (node_5829*(1.0 - _emissiveamount));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_2843 = (1.0-max(0,dot(normalDirection, viewDirection)));
                float node_491 = pow(((1.0 - dot(i.normalDir,lightDirection))/_rimposition),_rimfalloff);
                float3 emissive = ((node_5829*_emissiveamount)+((node_2843.r*(node_491.r*_rimcolor.rgb))*_rimlightamount));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _gradienttopcolor;
            uniform float4 _gradientbtmcolor;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _gradientposition;
            uniform float _gradientfalloff;
            uniform float4 _rimcolor;
            uniform float _rimposition;
            uniform float _rimfalloff;
            uniform float _rimlightamount;
            uniform float _emissiveamount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 node_5829 = saturate((_texture_var.rgb*lerp(_gradienttopcolor.rgb,_gradientbtmcolor.rgb,((1.0 - saturate((float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+_gradientposition*float2(1,1)).g))/_gradientfalloff))));
                float3 diffuseColor = (node_5829*(1.0 - _emissiveamount));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
