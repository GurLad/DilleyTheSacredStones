//https://www.ronja-tutorials.com/2018/10/27/improved-toon.html

Shader "Custom/Toon3"
{
    Properties
    {
        [Header(Base Parameters)]
        _Color("Tint", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}
        [HDR] _Emission("Emission", color) = (0 ,0 ,0 , 1)

        [Header(Lighting Parameters)]
        _ShadowTint("Shadow Color", Color) = (0, 0, 0, 1)
    }

    SubShader
    {
        Tags { "LightMode" = "Deferred" }
        CGPROGRAM

        //the shader is a surface shader, meaning that it will be extended by unity in the background to have fancy lighting and other features
        //our surface shader function is called surf and we use our custom lighting model
        //fullforwardshadows makes sure unity adds the shadow passes the shader might need
        #pragma surface surf Stepped fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
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

            float3 projNormal = saturate(pow(i.worldNormal * 1.4, 4));

            // SIDE X
            float3 x = tex2D(_MainTex, frac(i.worldPos.zy)) * abs(i.worldNormal.x);

            // TOP / BOTTOM
            float3 y = 0;
            if (i.worldNormal.y > 0) {
                y = tex2D(_MainTex, frac(i.worldPos.zx)) * abs(i.worldNormal.y);
            }
            else {
                y = tex2D(_MainTex, frac(i.worldPos.zx)) * abs(i.worldNormal.y);
            }

            // SIDE Z	
            float3 z = tex2D(_MainTex, frac(i.worldPos.xy)) * abs(i.worldNormal.z);

            o.Albedo = z;
            o.Albedo = lerp(o.Albedo, x, projNormal.x);
            o.Albedo = lerp(o.Albedo, y, projNormal.y);
            //sample and tint albedo texture
            //fixed4 col = tex2D(_MainTex, i.uv_MainTex);
            //col *= _Color;
            fixed4 col = _Color;
            o.Albedo *= col.rgb;

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
