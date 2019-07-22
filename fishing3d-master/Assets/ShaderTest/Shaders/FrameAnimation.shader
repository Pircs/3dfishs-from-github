Shader "Custom/FrameAnimation" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TexWidth("Sheet Width",float) = 0.0
		_CellAmount("Cell Amount",float) = 0.0
		_Speed("Speed",Range(0.01,32)) = 12
		_Row("行",int) = 1
		_Column("列",int) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _CellAmount;
		float _TexWidth;
		float _Speed;
		int _Row;
		int _Column;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float2 uv = IN.uv_MainTex;
			float cellX = uv.x / _Column;
			float cellY = uv.y / _Row;
			int count = _Row * _Column;
			int spriteIndex = fmod(_Time.w * _Speed,count);
			int spriteRowIndex = spriteIndex / _Column;
			int spriteColumnIndex = fmod(float(spriteIndex),_Column);
			spriteRowIndex = (_Row - 1) - fmod((float)spriteRowIndex,_Row);
			uv.x = cellX + spriteColumnIndex * 1.0 / _Column;
			uv.y = cellY + spriteRowIndex * 1.0 / _Row;

			float cellPixelWidth = _TexWidth / _CellAmount;
			float cellUVPercentage = cellPixelWidth / _TexWidth;
			float timeVal = fmod(_Time.y * _Speed,_CellAmount);
			timeVal = ceil(timeVal);

			float xValue = uv.x;
			xValue += cellUVPercentage * timeVal * _CellAmount;
			xValue *= cellUVPercentage;
			//uv = float2(xValue,uv.y);
			half4 c = tex2D (_MainTex, uv);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
