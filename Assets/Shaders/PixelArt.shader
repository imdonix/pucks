// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PixelArt"
{
  Properties
  {
    _MainTex("Texture", 2D) = "" {}
  }

  SubShader
  {
    Tags
    {
      "Queue" = "Transparent"
      "IgnoreProjector" = "True"
      "RenderType" = "Transparent"
    }

    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass
    {
      CGPROGRAM
      #pragma vertex vertexShader
      #pragma fragment fragmentShader

      sampler2D _MainTex;
      float4 _MainTex_TexelSize;
      float texelsPerPixel;

      struct vertexInput
      {
        float4 vertex : POSITION;
        fixed4 color : COLOR;
        float2 textureCoords : TEXCOORD0;
      };

      struct vertexOutput
      {
        float4 vertex : SV_POSITION;
        fixed4 color : COLOR;
        float2 textureCoords : TEXCOORD0;
      };

      vertexOutput vertexShader(vertexInput input)
      {
        vertexOutput output;
        output.vertex = UnityObjectToClipPos(input.vertex);
        output.textureCoords = input.textureCoords * _MainTex_TexelSize.zw;
        output.color = input.color;
        return output;
      }

      fixed4 fragmentShader(vertexOutput input) : SV_Target
      {
        float2 locationWithinTexel = frac(input.textureCoords);
        float2 interpolationAmount = clamp(locationWithinTexel / texelsPerPixel,
          0, .5) + clamp((locationWithinTexel - 1) / texelsPerPixel + .5, 0,
          .5);
        float2 finalTextureCoords = (floor(input.textureCoords) +
          interpolationAmount) / _MainTex_TexelSize.zw;
        return tex2D(_MainTex, finalTextureCoords) * input.color;
      }

      ENDCG
    }
  }
}