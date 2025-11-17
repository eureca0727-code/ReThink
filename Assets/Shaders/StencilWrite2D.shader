Shader "Custom/StencilWrite2D"
{
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Geometry" }
        ColorMask 0
        ZWrite Off
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
        }
    }
}
