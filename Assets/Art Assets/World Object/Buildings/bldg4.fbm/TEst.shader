Shader "Custom/TEst" {
	Properties {
         _Color ("Color Tint", Color) = (1,1,1,1)
         _MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"
          _Mask ("Culling Mask", 2D) = "white" {}
          _Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
     }
     Category {
        Lighting Off
        ZWrite Off
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha
        AlphaTest GEqual [_Cutoff]
        Tags {Queue=Transparent}
        SubShader {
             Pass {
                SetTexture [_MainTex] {
                       constantColor [_Color]
                       Combine Texture * constant, Texture * constant
//                       SetTexture [_Mask] {combine texture}
        			   //SetTexture [_MainTex] {combine texture, previous}
                 }
             }
         } 
     }
}
