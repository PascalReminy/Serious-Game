Shader "Custom/UnlitTransparentSurfaceShader" {
	Properties {
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		Cull off
		Blend SrcAlpha OneMinusSrcAlpha
		
		LOD 200
		
		CGPROGRAM
		#pragma surface surf NoLight

		sampler2D _MainTex;
	
		half4 LightingNoLight ( SurfaceOutput s, fixed3 lightDir, half atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}
		
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
