using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	[SerializeField]
	int _x, _y;

	RectTransform _position;
	Unit _unit;
	Building _building;

	[SerializeField]
	float _movementModifier, _defenseModifier, _attackModifier;

	// Getters for our member variables
	public int x
	{
		get
		{
			return _x;
		}
	}

	public int y
	{
		get
		{
			return _y;
		}
	}

	public float movementModifier
	{
		get
		{
			return _movementModifier;
		}
	}

	public float defenseModifier
	{
		get
		{
			return _defenseModifier;
		}
	}

	public float attackModifier
	{
		get
		{
			return _attackModifier;
		}
	}

	public Vector3 position
	{
		// This property will convert RectTransform world coordinates into a Vector3 that units can use for movement
		get
		{
			return new Vector3(_position.position.x, _position.position.y, 0);
		}
	}

	// Getters and Setters for Unit and Building
	public Unit unit
	{
		get
		{
			return _unit;
		}
		set
		{
			_unit = value;
		}
	}

	public Building building
	{
		get
		{
			return _building;
		}
		set
		{
			_building = value;
		}
	}

	private void Start()
	{
	
	}

	private void Update()
	{
		
	}

	public void InitializeData(int newX, int newY, RectTransform newPosition)
	{
		// used at runtime to populate script with x, y, position, and terrain data
		_x = newX;
		_y = newY;
		_position = newPosition;

		InitTerrainData();
	}

	
	void InitTerrainData()
	{
		/// <summary>
		/// Check the terrain tag for this gameObject and set movement, defense, and attack modifiers accordingly.
		/// </summary>
		if (gameObject.tag == "Road")
		{
			// roads should heavily increase movement, lower defense, and provide no attack change
			_movementModifier = 2f;
			_defenseModifier = 0.5f;
			_attackModifier = 1;
		}
		else if (gameObject.tag == "Mountain")
		{
			// mountains should be hard to move through, provide high defense, and moderate attack boost
			_movementModifier = 0.5f;
			_defenseModifier = 1.8f;
			_attackModifier = 1.4f;
		}
		else if(gameObject.tag == "Grass")
		{
			// grass should be slightly harder to move through, provide small defense boost, and not change attack
			_movementModifier = 0.8f;
			_defenseModifier = 1.4f;
			_attackModifier = 1f;
		}
		else if(gameObject.tag == "River")
		{
			// river should heavily hinder movement, provide no defense change, and reduce attack
			_movementModifier = 0.2f;
			_defenseModifier = 1f;
			_attackModifier = 0.6f;
		}

		// Include buildings. Forts/buildings

	}

	private void OnMouseDown()
	{
		if(unit == null)
			UnitManager.unitMan.selectedUnit.UpdatePosition();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_unit = collision.GetComponent<Unit>(); // get the component from unit that just stepped onto the tile
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		_unit = null; // when unit leaves, set unit to null
	}
}
