//https://www.ronja-tutorials.com/2018/10/27/improved-toon.html

Shader "Custom/ToonGhost"
{
    Properties
    {
        [Header(Base Parameters)]
        _Color("Tint", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}
        [HDR] _Emission("Emission", color) = (0, 0, 0, 1)

        [Header(Ghost Parameters)]
        _MaxAlpha("Max Alpha", Float) = 1
        _DistortionStrength("Distortion Strength", Float) = 0
        _NoiseTex("Noise", 2D) = "white" {}

        [Header(Lighting Parameters)]
        _ShadowTint("Shadow Color", Color) = (0, 0, 0, 1)
    }

    SubShader
    {
        Tags { "RenderType" = "Fade" "Queue" = "Transparent"  "LightMode" = "Deferred" }
        CGPROGRAM

        //the shader is a surface shader, meaning that it will be extended by unity in the background to have fancy lighting and other features
        //our surface shader function is called surf and we use our custom lighting model
        //fullforwardshadows makes sure unity adds the shadow passes the shader might need
        #pragma surface surf Stepped fullforwardshadows alpha
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;
        float _MaxAlpha;
        float _DistortionStrength;
        fixed4 _Color;
        half3 _Emission;
        float3 _ShadowTint;

        //input struct which is automatically filled by unity
        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
        };

        //the surface shader function which sets parameters the lighting function then uses
        void surf(Input i, inout SurfaceOutput o) {
            //sample and tint albedo texture
            float4 offset = float4(tex2Dlod(_NoiseTex, float4(_Time[0] + i.worldPos.x, i.worldPos.y, 0, 0)).r, tex2Dlod(_NoiseTex, float4(i.worldPos.x, _Time[0] + i.worldPos.y, 0, 0)).r, 0, 0);
            float4 offset2 = float4(tex2Dlod(_NoiseTex, float4(_Time[0] + i.worldPos.x, i.worldPos.y + 50, 0, 0)).r, tex2Dlod(_NoiseTex, float4(i.worldPos.x + 50, _Time[0] + i.worldPos.y, 0, 0)).r, 0, 0);
            
            fixed4 col = tex2D(_MainTex, float2(i.uv_MainTex.x + offset.r * _DistortionStrength - _DistortionStrength / 2, i.uv_MainTex.y + offset.g * _DistortionStrength - _DistortionStrength / 2));
            col *= _Color;
            o.Albedo = col.rgb;

            o.Alpha = col.a * (offset2.r * _MaxAlpha / 2 + offset2.g * _MaxAlpha / 2);

            float3 shadowColor = col.rgb * _ShadowTint;
            o.Emission = _Emission + shadowColor;
        }

        //our lighting function. Will be called once per light
        float4 LightingStepped(SurfaceOutput s, float3 lightDir, half3 viewDir, float shadowAttenuation) {
            //how much surface normal points towards the light
            float towardsLight = dot(s.Normal, lightDir);
            float towardsLightChange = fwidth(towardsLight);
            float lightIntensity = smoothstep(0, towardsLightChange, towardsLight);
#ifdef USING_DIRECTIONAL_LIGHT
            float attenuationChange = fwidth(shadowAttenuation) * 0.5;
            float shadow = smoothstep(0.5 - attenuationChange, 0.5 + attenuationChange, shadowAttenuation);
#else
            float attenuationChange = fwidth(shadowAttenuation);
            float shadow = smoothstep(0, attenuationChange, shadowAttenuation);
#endif

            lightIntensity = lightIntensity * shadow;

            float3 shadowColor = s.Albedo * _ShadowTint;
            float4 color;
            color.rgb = s.Albedo * lightIntensity * _LightColor0.rgb;
            color.a = s.Alpha;
            return color;

        }

        ENDCG
    }
    FallBack "Standard"
}
