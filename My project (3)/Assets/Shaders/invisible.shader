Shader "Custom/invisible"
{
    SubShader
    {
        Tags { "Queue" = "Geometry+1"}
        Pass {
            Blend Zero One
        }
    }
}