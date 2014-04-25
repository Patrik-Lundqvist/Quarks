using UnityEngine;
using System.Collections;

/// <summary>
/// A spell that pushes enemies away from the player ball
/// </summary>
public class Laser : Spell {

    // Horizontal beam size
    Vector2 startSizeX;
    Vector2 endSizeX;

    // Vertical beam size
    Vector2 startSizeY;
    Vector2 endSizeY;

	float moveTime = 0.0f;

    OTSprite spriteX;
    OTSprite spriteY;

    float accelRate = 1f;

    public Texture LaserTexture;

	/// <summary>
	/// Constructor
	/// </summary>
    public Laser()
	{
		// Set the powercost of the spell
		powerCost = 1.0f;
		// Set the sound clip
		soundClip = "Push";
		// Set the name of the spell
		spellName = "Laser";

		isSelfCast = false;
	}

	/// <summary>
	/// Find all the surrounding targets
	/// </summary>
	public override void Precast()
	{

        // Create two sprite objects
        spriteX = OT.CreateObject(OTObjectType.Sprite).GetComponent<OTSprite>();
        spriteY = OT.CreateObject(OTObjectType.Sprite).GetComponent<OTSprite>();

        // Setup general settings
        SpriteSetup(spriteX);
        SpriteSetup(spriteY);

        // Set the position and size for the horizontal beam
	    spriteX.position = new Vector2(0, caster.transform.position.y);
	    startSizeX = new Vector2(Screen.width, 50);
	    endSizeX = new Vector2(Screen.width, 0);
        spriteX.size = startSizeX;

        // Set the position and size for the vertical beam
        spriteY.rotation = 90;
        spriteY.position = new Vector2(caster.transform.position.x, 0);
        startSizeY = new Vector2(Screen.height,50);
        endSizeY = new Vector2(Screen.height,0 );
        spriteY.size = startSizeY;

        // Add camera shake
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(Random.Range(6.0F, 10.0F), Random.Range(3.0F, 5.0F), 0), 0.5f);
	    
        base.Precast();
	}

	/// <summary>
	/// Move all the surround targets
	/// </summary>
	public override void Casting()
	{
        accelRate += (moveTime * 2f);
        moveTime += Time.deltaTime * accelRate;
	    
        // Lerp the width of the beam
        spriteX.size = Vector2.Lerp(startSizeX, endSizeX, moveTime);
        spriteY.size = Vector2.Lerp(startSizeY, endSizeY, moveTime);

        // End casting if the beam has 0 width
        if (spriteX.size.y <= 0)
		{
			EndCasting();
		}

		base.Casting();

	}

    /// <summary>
    /// Destory the sprites
    /// </summary>
    public override void EndCasting()
    {
        Destroy(spriteX.gameObject);
        Destroy(spriteY.gameObject);

        base.EndCasting();
    }

    /// <summary>
	/// If we trigger any of the trigger colliders
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)	
	{
        if (spriteX.size.y <= 40)
        {
            return;
        }

		// Get the object name of the object which we collided with
		var hitobject = other.gameObject.tag;
		
		if(hitobject == "ObstacleBall")
		{
		    Ball obstacleball = other.GetComponent<ObstacleBall>();

            // Vaporize the obstacle ball
            obstacleball.Vaporize();
		}
		
	}

    /// <summary>
    /// Setup sprite settings
    /// </summary>
    /// <param name="sprite"></param>
    void SpriteSetup(OTSprite sprite)
    {
        sprite.image = LaserTexture;
        sprite.collidable = true;
        sprite.depth = -5;
        sprite.transparent = true;
        sprite.onTrigger += OnTriggerEnter;
    }
}
