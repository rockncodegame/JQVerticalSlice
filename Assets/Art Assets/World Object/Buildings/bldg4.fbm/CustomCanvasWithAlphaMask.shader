 Shader "Custom/CanvasWithAlphaMask" {
     Properties {
        _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
        _Color ("Main Color", Color) = (0,0,0,0)
        _Stencil("Stencil Texture (RGB)", 2D) = "white" {}
     }
     Subshader {
        Tags {
          "Queue"="Transparent"
          "IgnoreProjector"="False"
          "RenderType"="Transparent"
        }
 
       Lighting Off //doesn't do anything
 
        CGPROGRAM
        #pragma surface surf Lambert alpha
          
          struct Input {
           float2 uv_MainTex;
          };
   
          half4 _Color;
          sampler2D _MainTex;
          sampler2D _Stencil;
          
            void surf (Input IN, inout SurfaceOutput o) {
             half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
             o.Albedo = c.rgb;
                o.Alpha = c.a - tex2D(_Stencil, IN.uv_MainTex).a ;
         }
        ENDCG
     }
 }
