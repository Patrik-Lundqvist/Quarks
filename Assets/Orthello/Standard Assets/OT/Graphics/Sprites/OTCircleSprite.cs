using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// A circular sprite that consists of a number of divisions and rings.
/// </summary>
public class OTCircleSprite : OTSprite {	
	
	public int _divisions = 32;
	int divisions_ = 32;
	/// <summary>
	/// Gets or sets the number of divisions (slices)
	/// </summary>
	/// <remarks>
	/// Divisions must always be bigger than 2. A value of 3 will create a triangle shaped sprite. 
	/// A value of 4 will create a rectangular shaped sprite.
	/// </remarks>
	public int divisions
	{
		get
		{
			return _divisions;
		}
		set
		{
			_divisions = value;
			if (divisions<3)
				_divisions = 3;
			divisions_ = _divisions;
						
			meshDirty = true;
			Update();
		}
	}
			
	public float _fillFactor = 1;
	float fillFactor_ = 1;
	/// <summary>
	/// Gets or sets the fill factor.
	/// </summary>
	/// <remarks>
	/// The fill factor determines the percentage (0-1 == 100%) of the sprite
	/// that will be shown circular. Use this factor to create a circular filler.
	/// </remarks>
	public float fillFactor
	{
		get
		{
			return _fillFactor;
		}
		set
		{
			_fillFactor = value;
			if (fillFactor<0)
				_fillFactor = 0;
			else
			if (fillFactor>1)
				_fillFactor = 1;
			fillFactor_ = value;
			meshDirty = true;
			Update();
		}
	}
	
	
    public OTGradientSpriteColor[] _gradientColors;	
	/// <summary>
	/// Gets or sets the gradient colors.
	/// </summary>
	/// <remarks>
	/// The gradient will 
	/// </remarks>
    public OTGradientSpriteColor[] gradientColors
	{
		get
		{
			return _gradientColors;
		}
		set
		{
			_gradientColors = value;			
			meshDirty = true;			
			isDirty = true;
		}
	}
	
    private OTGradientSpriteColor[] _gradientColors_;
	void CloneGradientColors()
	{
		_gradientColors_ = new OTGradientSpriteColor[_gradientColors.Length];
		for (var c=0; c<_gradientColors.Length; c++)
		{
			_gradientColors_[c] = new OTGradientSpriteColor();
			_gradientColors_[c].position = _gradientColors[c].position;
			_gradientColors_[c].size = _gradientColors[c].size;
			_gradientColors_[c].color = _gradientColors[c].color;
		}		
	}
	
	bool GradientColorChanged()
	{
		for (var c = 0; c < _gradientColors.Length; c++)
		{
			if (!_gradientColors[c].color.Equals(_gradientColors_[c].color))
			{
				return true;			
			}
		}
		return false;	
	}	
	
	bool GradientMeshChanged()
	{
		var res = false;
		for (var c = 0; c < _gradientColors.Length; c++)
		{
			if (_gradientColors[c].position < 0) _gradientColors[c].position = 0;
			if (_gradientColors[c].position > 100) _gradientColors[c].position = 100;
			if (_gradientColors[c].size < 0) _gradientColors[c].size = 0;
			if (_gradientColors[c].size > 100) _gradientColors[c].size = 100;
			if (_gradientColors[c].position+_gradientColors[c].size > 100) 
				_gradientColors[c].position = 100-_gradientColors[c].size;
			
			if (_gradientColors[c].position!=_gradientColors_[c].position || _gradientColors[c].size!=_gradientColors_[c].size)
				res = true;			
		}
		return res;	
	}	
		
    protected override void CheckDirty()
    {
		if (_gradientColors==null)
			return;
		
		base.CheckDirty();
		
		if (_gradientColors.Length!=_gradientColors_.Length || GradientMeshChanged())
			meshDirty = true;			
		else
		if (GradientColorChanged())
			HandleColors();
	}	
	
	
	protected override void CheckSettings ()
	{
		base.CheckSettings ();
					
		if (_divisions!=divisions_ || _fillFactor != fillFactor_)
		{			
			if (divisions<3)
				_divisions = 3;
			
			if (fillFactor<0)
				_fillFactor = 0;
			else
			if (fillFactor>1)
				_fillFactor = 1;
			
			meshDirty = true;
			divisions_ = _divisions;
			fillFactor_ = _fillFactor;		
		}
				
	}
	
	protected override string GetTypeName ()
	{
        return "CircleSprite";
	}	
	
	// Use this for initialization
	new protected void Awake () {
		divisions_ = _divisions;
		fillFactor_ = _fillFactor;
		CloneGradientColors();
		
		base.Awake();	
	}

	protected override void HandleColors()
	{
		var gColors = new Color[mesh.vertexCount];
		
		if (_gradientColors.Length<=1)
		{
			for (var i=0; i<mesh.vertexCount; i++)
				gColors[i] = ( _gradientColors.Length == 0)?tintColor:_gradientColors[0].color;
		}
		else
		{
			var d =  (1f / divisions);								
			var di = 1;
			for (var i=0; i<divisions; i++)
			{
				if (fillFactor< 1 && fillFactor<(i+1)*d)
					break;
				di++;
			}
									
			var r=0;
			for (var gc=0; gc<_gradientColors.Length; gc++)
			{
				var gCol = _gradientColors[gc];
				
				if (gc == 0)
					gColors[0] = gCol.color;
													
				for (var i=0; i<di+1; i++)
				{
					if (gc>0)
						gColors[1+((r-1)*(di+1))+i] = gCol.color;
					if (gCol.size>0)
						gColors[1+(r*(di+1))+i] = gCol.color;						
				}
				
				if (gCol.size>0)
					r+=2;
				else
					r++;
			}
		}
						
		mesh.colors = gColors;
		
	}
	
	protected override void HandleUV ()
	{		
		AdjustUV(mesh,GetUV());
	}
	
	List<float> GradientFact()
	{
		var rfact = new List<float>();
		if (_gradientColors.Length>0)
		{
			for (var i=0; i<_gradientColors.Length; i++)
			{
				var gc = _gradientColors[i];
				rfact.Add((float)gc.position/100);
				if (gc.size>0)
				{
					var _pos = gc.position+gc.size;
					rfact.Add((float)_pos/100);
				}
			}
		}			
		if (rfact.Count==0)
		{
			rfact.Add(0); rfact.Add(1);
		}
		else
		if (rfact.Count==1)
			rfact.Add(1);

		return rfact;
	}
		
	void AdjustUV(Mesh mesh, Vector2[] mesh_uv)
	{
		if (mesh == null || mesh_uv.Length==0)
			return;
		
		if (divisions>0 && fillFactor>0)
		{
			var rfact = GradientFact();
			
			var d =  (1f / divisions);									
			var di = 1;
			for (var i=0; i<divisions; i++)
			{
				if (fillFactor< 1 && fillFactor<(i+1)*d)
					break;
				di++;
			}
						
			var uvs = new Vector2[1+((di+1)*rfact.Count-1)];
			
			
			var rotated = false;
	        if (spriteContainer != null && spriteContainer.isReady && spriteContainer is OTSpriteAtlas)
	        {	
				if (frameIndex<(spriteContainer as OTSpriteAtlas).atlasData.Length)
					rotated = (spriteContainer as OTSpriteAtlas).atlasData[frameIndex].rotated;
			}
					
			Rect uvRect;						
			if (rotated)
			{
				uvRect = new Rect(mesh_uv[3].y,mesh_uv[3].x,mesh_uv[1].y - mesh_uv[3].y, mesh_uv[1].x - mesh_uv[3].x);				
				uvs[0] = new Vector2(uvRect.yMin + uvRect.height/2, uvRect.xMin + uvRect.width/2);				
			}
			else
			{
				uvRect = new Rect(mesh_uv[3].x,mesh_uv[3].y,mesh_uv[1].x - mesh_uv[3].x, mesh_uv[1].y - mesh_uv[3].y);																	
				uvs[0] = new Vector2(uvRect.xMin + uvRect.width/2, uvRect.yMin + uvRect.height/2);
			}
						
			for (var r=1; r<rfact.Count; r++)
			{
				for (var i=0; i<di; i++)
				{
					var v = new Vector3(0, 0.5f * rfact[r], 0);
	            	var m = new Matrix4x4();
				
	            	m.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, i * d * -360), Vector3.one);
	            	var v1 = m.MultiplyPoint3x4(v);
					
					var id = (i+1) * d;
					
	            	m.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, id * -360), Vector3.one);
	            	var v2 = m.MultiplyPoint3x4(v);
					
					if (fillFactor<id)
					{
						var f = (id-fillFactor)/d;																				
		            	v2 = v2 - ((v2-v1) * f);
					}
					
					if (r==1)
					{
						if (rotated)
						{
							if (i==0)
								uvs[i+1] = uvs[0] - new Vector2(-v1.y * uvRect.height, -v1.x * uvRect.width);
							uvs[i+2] = uvs[0] - new Vector2(-v2.y * uvRect.height, -v2.x * uvRect.width);
						}
						else
						{
							if (i==0)
								uvs[i+1] = uvs[0] + new Vector2(v1.x * uvRect.width, v1.y * uvRect.height);
							uvs[i+2] = uvs[0] + new Vector2(v2.x * uvRect.width, v2.y * uvRect.height);
						}
					}
					else
					{
						var dd = di+1;
						if (rotated)
						{
							uvs[i+((r-1)*dd)+1] = uvs[0] - new Vector2(-v1.y * uvRect.height, -v1.x * uvRect.width);
							uvs[i+((r-1)*dd)+2] = uvs[0] - new Vector2(-v2.y * uvRect.height, -v2.x * uvRect.width);
						}
						else
						{
							uvs[i+((r-1)*dd)+1] = uvs[0] + new Vector2(v1.x * uvRect.width, v1.y * uvRect.height);
							uvs[i+((r-1)*dd)+2] = uvs[0] + new Vector2(v2.x * uvRect.width, v2.y * uvRect.height);
						}
												
					}
									
				}		
			}
			mesh.uv = uvs;
		}
	}	
	

	protected override Mesh GetMesh ()
	{
		var mesh = base.GetMesh();
		var mesh_uv = mesh.uv;
						
		if (divisions>0 && fillFactor>0)
		{
			var rfact = GradientFact();
			var d =  (1f / divisions);			
						
			var di = 1;
			for (var i=0; i<divisions; i++)
			{
				if (fillFactor< 1 && fillFactor<(i+1)*d)
					break;
				di++;
			}
						
			var vertices = new Vector3[1+((di+1)*rfact.Count-1)];
			var triangles = new int[(di * 3)+((rfact.Count-2) * di * 6)];
			
			vertices[0] = (Vector3)mCenter;	
			var tri = 0;
			for (var r=1; r<rfact.Count; r++)
			{
				for (var i=0; i<di; i++)
				{
										
					var v = new Vector3(0, 0.5f * rfact[r], 0);
	            	var m = new Matrix4x4();
				
	            	m.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, i * d * -360), Vector3.one);
	            	var v1 = m.MultiplyPoint3x4(v);
					
					var id = (i+1) * d;
					
	            	m.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, id * -360), Vector3.one);
	            	var v2 = m.MultiplyPoint3x4(v);
					
					if (fillFactor<id)
					{
						var f = (id-fillFactor)/d;																				
		            	v2 = v2 - ((v2-v1) * f);
					}
					
					if (r==1)
					{
						if (i==0)
							vertices[i+1] = (Vector3)mCenter + v1;
						vertices[i+2] = (Vector3)mCenter + v2;
						
						triangles[tri++] = 0;
						triangles[tri++] = i+1;
						triangles[tri++] = i+2;									
					}
					else
					{
						var dd = di+1;
						vertices[i+1+(dd * (r-1))] = (Vector3)mCenter + v1;
						vertices[i+2+(dd * (r-1))] = (Vector3)mCenter + v2;
						
						triangles[tri++] = i+1+(dd * (r-2));
						triangles[tri++] = i+1+(dd * (r-1));
						triangles[tri++] = i+2+(dd * (r-1));
						triangles[tri++] = i+1+(dd * (r-2));
						triangles[tri++] = i+2+(dd * (r-1));									
						triangles[tri++] = i+2+(dd * (r-2));
					}
				}
			}
			
			mesh.triangles = new int[]{};
			
			mesh.vertices = vertices;
			mesh.triangles = triangles;

			AdjustUV(mesh,mesh_uv);	
			return mesh;
		}
		
		mesh.triangles = new int[] { };
		mesh.uv = new Vector2[] { };
		mesh.colors = new Color[] { };
		mesh.vertices = new Vector3[] { };
		return mesh;
	}
	
	
					
}
