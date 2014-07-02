using UnityEngine;
using System;

public class FMaskedSprite : FSprite
{
	protected FAtlasElement _maskElement;	

	protected FMaskedSprite() : base() //for overriding
	{
		shader = FShader.MaskedShader;
	}
	
	public FMaskedSprite (string elementName, string maskName) : base(elementName)
	{
		_maskElement = Futile.atlasManager.GetElementWithName(maskName);
		shader = FShader.MaskedShader;
	}
	
	public FMaskedSprite (FAtlasElement element, FAtlasElement maskElement) : base(element)
	{
		_maskElement = maskElement;
		shader = FShader.MaskedShader;
	}
	
	override public void Redraw(bool shouldForceDirty, bool shouldUpdateDepth)
	{
		base.Redraw(shouldForceDirty, shouldUpdateDepth);
		if(shouldUpdateDepth)
		{
			//Update mask element
			if(_maskElement != null)
			{
				UpdateMask();
			}
		}
	}

	public void UpdateMask()
	{
		if(_renderLayer != null)
		{
			_renderLayer.SetMaterialTex("_Mask", _maskElement.atlas.texture);
		}
	}

	override public void PopulateRenderLayer()
	{
		base.PopulateRenderLayer();
		if(_isOnStage && _firstFacetIndex != -1) 
		{
			Vector2[] uv2 = _renderLayer.uvs2;
			if(uv2.Length != _renderLayer.uvs.Length);
			{
				_renderLayer.ExpandUV2();
			}
			int vertexIndex0 = _firstFacetIndex*4;
			int vertexIndex1 = vertexIndex0 + 1;
			int vertexIndex2 = vertexIndex0 + 2;
			int vertexIndex3 = vertexIndex0 + 3;

			uv2[vertexIndex0] = _maskElement.uvTopLeft;
			uv2[vertexIndex1] = _maskElement.uvTopRight;
			uv2[vertexIndex2] = _maskElement.uvBottomRight;
			uv2[vertexIndex3] = _maskElement.uvBottomLeft;
		}
	}
}

