using UnityEngine;
using System.Collections;

/// <summary>
/// A spell that places an exploding ball on the playing field
/// </summary>
public class Bomb : Spell {

	Vector2 startSize;
	Vector2 endSize;
	
	float explodingRange;
	float moveTime = 0.0f;
	float explodeTime = 2.5f;

	bool isExploding;



	public Texture bombTexture;
	public Texture bombTextureBlink;
	public Texture bombExplosion;

	OTSprite sprite;     

	/// <summary>
	/// Constructor
	/// </summary>
	public Bomb()
	{
		// Set the powercost of the spell
		powerCost = 1.0f;
		// Set the sound clip
		soundClip = "PlaceBomb";
		// Set the name of the spell
		spellName = "Bomb";

		isSelfCast = false;

		isExploding = false;
	}

	/// <summary>
	/// Find all the surrounding targets
	/// </summary>
	public override void Precast()
	{
		explodingRange = 300;

		// get this star's sprite class
		sprite = GetComponent<OTSprite>();

		sprite.image = bombTexture;

		sprite.size = new Vector2(35,36);

		sprite.position = new Vector2(caster.transform.position.x,caster.transform.position.y);

		startSize = sprite.size;

		endSize = new Vector2(explodingRange, explodingRange);

		StartCoroutine(DoTextureLoop());

		base.Precast();
	}

	/// <summary>
	/// Move all the surround targets
	/// </summary>
	public override void Casting()
	{

		if(!isExploding)
		{
            explodeTime -= Time.deltaTime;
			if ( explodeTime <= 0 )
			{
				isExploding = true;
				sprite.size = new Vector2(0,0);
				sprite.image = bombExplosion;
				sprite.additive = false;
				new OTSound("BombExplode");
				iTween.ShakePosition(Camera.main.gameObject, new Vector3(Random.Range(3.0F, 5.0F),Random.Range(3.0F, 5.0F),0), 0.5f);
			}
		}
		else
		{
			// We are exploding
			moveTime += Time.deltaTime * 3f;
			
			sprite.size = Vector2.Lerp (startSize, endSize, moveTime);


			if(sprite.size.x >= explodingRange)
			{
				EndCasting();
			}
		}


		base.Casting();

	}

	/// <summary>
	/// If we trigger any of the trigger colliders
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)	
	{	
		if(isExploding)
		{
			// Get the object name of the object which we collided with
			var hitobject = other.gameObject.tag;
			
			if(hitobject == "ObstacleBall")
			{
				Destroy(other.gameObject);
			}
		}

	}


	public IEnumerator DoTextureLoop(){
		var isBright = false;
		while (!isExploding){
			// Play the sound clip
			new OTSound("BombClick");

			if(isBright)
			{
				sprite.image = bombTexture;
				isBright = false;
			}
			else
			{
				sprite.image = bombTextureBlink;
				isBright = true;
			}

			yield return new WaitForSeconds(0.5f);
		}
	}
}
