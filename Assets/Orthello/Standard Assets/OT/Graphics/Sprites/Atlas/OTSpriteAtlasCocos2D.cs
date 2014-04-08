using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Sprite altlas imported from a Cocos2D XML data file
/// </summary>
/// <remarks>
/// Suports trimmed and rotated images. Make sure the data file is saved with the .xml extension
/// because Unity3D will not detect it as a TextAsset. Cocos2D export defaults to a .plist extension
/// so this need to be changed.
/// </remarks>
public class OTSpriteAtlasCocos2D : OTSpriteAtlasImportXML 
{

    Vector2 StringToVector2(string s)
    {
        var _s = s.Substring(1, s.Length - 2);
        var sa = _s.Split(',');
        return new Vector2(System.Convert.ToSingle(sa[0]), System.Convert.ToSingle(sa[1]));
    }

    Rect StringToRect(string s)
    {
        var _s = s.Substring(1, s.Length - 2);
        var sa = _s.Split(new string[] { "},{" }, System.StringSplitOptions.None);
        var v1 = StringToVector2(sa[0]+"}");
        var v2 = StringToVector2("{"+sa[1]);
        return new Rect(v1.x, v1.y, v2.x, v2.y);
    }

    Rect GetRect(OTDataset dict, string name)
    {
		var el = xml.FindValue(dict,"key",name);
        if (el != null)
            return StringToRect(xml.Value(xml.Next(el)));	
		return new Rect(0, 0, 0, 0);
    }

    Vector2 GetVector2(OTDataset dict, string name)
    {
		var el = xml.FindValue(dict,"key",name);
        if (el != null)
            return StringToVector2(xml.Value(xml.Next(el)));
        return Vector2.zero;
    }

    bool GetBool(OTDataset dict, string name)
    {
		var el = xml.FindValue(dict,"key",name);
		if (el!=null)
        	return (xml.Name(xml.Next(el)).ToLower() == "true");
		else
			return false;
    }

    /// <summary>
    /// Import atlasData from sparrow xml
    /// </summary>
    protected override OTAtlasData[] Import()
    {
		
        if (!ValidXML())
            return new OTAtlasData[] { };

        var data = new List<OTAtlasData>();
        if (xml.rootName == "plist")
        {
			var dsKeys = xml.Dataset("dict/key");
			while (!dsKeys.EOF)
			{
				var nodeText = xml.Value(dsKeys);
				if (nodeText == "frames")
				{
					
					var dict = xml.Next(dsKeys);
					if (xml.Name(dict) == "dict")
					{
						var dsTextureNames = xml.Dataset(dict,"key");
						var dsTextures = xml.Dataset(dict,"dict");
						if (!dsTextureNames.EOF && !dsTextures.EOF && dsTextureNames.rowCount == dsTextures.rowCount)
						{
			                try
			                {
								while (!dsTextureNames.EOF && !dsTextures.EOF)
								{														
			                        var ad = new OTAtlasData();
			
			                        var rotated = GetBool(dsTextures, "rotated");
			                        var frame = GetRect(dsTextures, "frame");
			                        var colorRect = GetRect(dsTextures, "sourceColorRect");
																		
			                        var sourceSize = GetVector2(dsTextures, "sourceSize");
			                        try
			                        {
			                            ad.name = xml.Value(dsTextureNames).Split('.')[0];
			                        }
			                        catch (System.Exception)
			                        {
			                            ad.name = xml.Value(dsTextureNames);
			                        }
			                        ad.position = new Vector2(frame.xMin, frame.yMin);
			                        if (rotated)
			                            ad.rotated = true;
			
			                        ad.size = new Vector2(colorRect.width, colorRect.height);
			                        ad.frameSize = sourceSize;
			                        ad.offset = new Vector2(colorRect.xMin, colorRect.yMin);
			
			                        data.Add(ad);
									
									dsTextureNames.Next();
									dsTextures.Next();							
			                    }
			                }
			                catch (System.Exception ERR)
			                {
			                    Debug.LogError("Orthello : Cocos2D Atlas Import error!");
			                    Debug.LogError(ERR.Message);
								break;
			                }										
						}
					}
				}
				else
				if (nodeText == "metadata")
	            {
					var dict = xml.Next(dsKeys);
					if (xml.Name(dict) == "dict")
					{
						var el = xml.FindValue(dict,"key","size");
						if (el!=null)
							sheetSize = StringToVector2(xml.Value(xml.Next(el)));
						el = xml.FindValue(dict,"key","realTextureFileName");
						if (el==null)
							el = xml.FindValue(dict,"key","textureFileName");
						if (el!=null)
						{
							var sa = xml.Value(xml.Next(el)).Split('.');
							if (sa.Length>0 && (name=="" || name.IndexOf("(id=-")>=0))
								name = sa[0];
						}
					}											
				}			
				dsKeys.Next();
			}
        }
        return data.ToArray();
    }

}