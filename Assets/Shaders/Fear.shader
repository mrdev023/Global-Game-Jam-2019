Shader "Custom/PostProcess" {
  Properties {
    _MainTex("Texture", 2D) = "white" {}
    _Fear ("Fear", Float) = 100 
  }

  SubShader {
    Pass {
      CGPROGRAM
      #pragma vertex vert_img
      #pragma fragment frag
      #include "UnityCG.cginc"

      // Properties
      sampler2D _MainTex;
      float _Fear;

      float4 frag(v2f_img input) : COLOR {
        float4 baseCol = tex2D(_MainTex, input.uv);
        float ratio = _Fear/100.f;
        float inverseRatio = 1 - ratio;
        float fearBaseColor = (baseCol.x + baseCol.y + baseCol.z) / 3;
        baseCol = float4(baseCol.x * inverseRatio + fearBaseColor * ratio, baseCol.y * inverseRatio + fearBaseColor * ratio, baseCol.z * inverseRatio + fearBaseColor * ratio, 1.0);
        float distFromCenter = distance(input.uv.xy, float2(0.5, 0.5)) * 4 * ratio;
        return float4(1 - distFromCenter, 1 - distFromCenter, 1 - distFromCenter, 1.0) * baseCol;
      }
      ENDCG
}}}