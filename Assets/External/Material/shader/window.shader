﻿Shader "Custom/Window"
{
	Properties
	{
		_ClearColor ("Clear Color", Color) = (1,1,1,1)
		_FogColor ("Fog Color", Color) = (1,1,1,1)
		_BlurRadius ("Blur Radius", float) = 3
		_MaxAge("Max Age", float) = 3
	}

	SubShader
	{
		Tags
        {
            "Queue" = "Transparent"
        }

        GrabPass
        {
            "_BGTex"
        }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "blur.cginc"
			
			// Properties
			// set in material
			uniform float4 _ClearColor;
			uniform float4 _FogColor;
			uniform float _BlurRadius;
			uniform float _MaxAge;
			// grab pass
			uniform sampler2D _BGTex;
			uniform float4 _BGTex_TexelSize;
			// set by script
			uniform sampler2D _MouseMap;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 texCoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 texCoord : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.grabPos = ComputeGrabScreenPos(output.pos);
				output.texCoord = input.texCoord;
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				float4 bg = tex2Dproj(_BGTex, input.grabPos);
				float timeDrawn = tex2D(_MouseMap, input.texCoord.xy).r;
				float age = clamp(_Time.y - timeDrawn, 0.0001, _Time.y);
				float percentMaxAge = saturate(age / _MaxAge); 
				
				float blurRadius = _BlurRadius * percentMaxAge;
				float4 color = (1-percentMaxAge)*_ClearColor + percentMaxAge*_FogColor;

				float4 blurX = gaussianBlur(float2(1,0), input.grabPos, _BGTex_TexelSize.z, _BGTex, blurRadius);
				float4 blurY = gaussianBlur(float2(0,1), input.grabPos, _BGTex_TexelSize.w, _BGTex, blurRadius);
				return (blurX + blurY) * color;

				
			}

			ENDCG
		}
	}
}
