Shader "Futile/Masked" //Mask Texture with greyscale alpha texture
{
   Properties
   {
      _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
      _Mask ("Culling Mask", 2D) = "white" {}
      _Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
   }

    Category 
	{
      Tags {"Queue"="Transparent" }
      Lighting Off
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      AlphaTest GEqual [_Cutoff]

		BindChannels 
		{
			Bind "Vertex", vertex
			Bind "texcoord1", texcoord0
			Bind "texcoord", texcoord1
			Bind "Color", color 
		}

		SubShader   
		{
			Pass 
			{
				 
	        	 SetTexture [_Mask] 
	        	 {
	        	 	combine texture
	        	 }
	         	 SetTexture [_MainTex]
	         	 {
	         	 	//Combine texture, previous * texture
	         	 	Combine texture * primary, previous * texture
	         	 }
	     	}
     	 }
  	}
}