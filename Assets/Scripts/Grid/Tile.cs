using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

	[SerializeField]
	int _x, _y;

	RectTransform _position;
	Unit _unit;
	Building _building;

	[SerializeField]
	int _movementModifier, _defenseModifier, _attackModifier;

	bool _isTraversable; // used for unit movement

	Image tileImage; // UI Image component for tile. Will be used for fading alpha

	Color originalColor;

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

	public bool isTraversable
	{
		get
		{
			return _isTraversable;
		}
	}

	public Vector3 gridPosition
	{
		get
		{
			return _position.anchoredPosition;
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

	private void Awake()
	{
		tileImage = GetComponent<Image>();
		if (tag == "Building")
			_building = GetComponent<Building>();
		originalColor = tileImage.color;
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
			_movementModifier = 0;
			_defenseModifier = 0;
			_attackModifier = 0;
		}
		else if (gameObject.tag == "Mountain")
		{
			// mountains should be hard to move through, provide high defense, and moderate attack boost
			_movementModifier = 2;
			_defenseModifier = 3;
			_attackModifier = 2;
		}
		else if(gameObject.tag == "Forest")
		{
			// forests are slightly harder to move through, provide decent defense, and have a high attack mod.
			_movementModifier = 1;
			_defenseModifier = 2;
			_attackModifier = 3;
		}
		else if(gameObject.tag == "Grass")
		{
			// grass should be slightly harder to move through, provide small defense boost, and not change attack
			_movementModifier = 1;
			_defenseModifier = 1;
			_attackModifier = 1;
		}
		else if(gameObject.tag == "River")
		{
			// river should heavily hinder movement, provide no defense change, and reduce attack
			_movementModifier = 3;
			_defenseModifier = 0;
			_attackModifier = 0;
		}
		else if(gameObject.tag == "Building")
		{
			// does not currently differentiate between building types.
			_defenseModifier = 2;
			_attackModifier = 0;
			_movementModifier = 0;
		}

		// Include buildings. Forts/buildings

	}

	public void SetTraversable()
	{
		/// <summary>
		/// public function that a unit will call when trying to find tile distances. If the tile is in range, the unit will call this function to fade alpha transparency
		/// </summary>
		
		_isTraversable = true;
		//StopFlash();
		StartCoroutine(FlashTile());
	}

	public void SetEnemy()
	{
		StartCoroutine(FlashEnemyTile());
	}

	public void StopFlash()
	{
		/// <summary>
		/// Stops previous flash tile coroutine and sets tile alpha back to full. This will happen when a player moves and still has movement left over
		/// </summary>
		StopCoroutine(FlashTile());
		StopCoroutine(FlashEnemyTile());
		_isTraversable = false;
		if(_building != null && _building.GetOwner() != null)
		{
			tileImage.color = _building.GetOwner().color;
		}
		else
			tileImage.color = originalColor;
		//tileImage.color += new Color(0, 0, 0, 1f);
	}

	private void OnMouseDown()
	{
		if (UnitManager.unitManager.selectedUnit != null && _isTraversable)
			// if we have a unit selected and we click on this tile, move the selected unit to this tile.
			UnitManager.unitManager.selectedUnit.StartMove(this);
		/*else if (UnitManager.unitManager.selectedUnit == null && this.building != null && GridManager.gridMan.selectedBuilding == null, && this.building.owner == PlayerManager.playerManager.selectedPlayer && this._unit == null)
			// assign this building as the selected building.
			GridManager.gridMan.selectedBuilding = this;*/

		// GridManager.gridMan.selectedBuilding.gridPosition;
		// myGameObject = (GameObject) Instantiate (prefab, buildingPosition, Quaternion.identity);
	}

	IEnumerator FlashEnemyTile()
	{
		Unit selectedUnit = UnitManager.unitManager.selectedUnit;
		tileImage.color = Color.red;

		while(selectedUnit == UnitManager.unitManager.selectedUnit && !selectedUnit.gethasAttacked())
		{
			while (tileImage.color.r > 0.5f && selectedUnit == UnitManager.unitManager.selectedUnit && !selectedUnit.gethasAttacked())
			{
				tileImage.color -= new Color(Time.deltaTime / 0.7f, 0, 0, 0f);
				yield return null;
			}
			while (tileImage.color.r < 1f && selectedUnit == UnitManager.unitManager.selectedUnit && !selectedUnit.gethasAttacked())
			{
				tileImage.color += new Color(Time.deltaTime / 0.7f, 0, 0, 0f);
				yield return null;
			}
		}

		tileImage.color = originalColor;
	}

	IEnumerator FlashTile()
	{
		/// <summary>
		/// Flash tile alpha value to indicate unit can move to it.
		/// </summary>
		
		while (isTraversable && UnitManager.unitManager.selectedUnit != null && !UnitManager.unitManager.selectedUnit.getHasMoved())
		{
			while(isTraversable && tileImage.color.a > 0.5f && UnitManager.unitManager.selectedUnit != null && !UnitManager.unitManager.selectedUnit.getHasMoved())
			{
				tileImage.color -= new Color(0, 0, 0, Time.deltaTime / 0.7f);
				yield return null;
			}
			while(isTraversable && tileImage.color.a < 1f && UnitManager.unitManager.selectedUnit != null && !UnitManager.unitManager.selectedUnit.getHasMoved())
			{
				tileImage.color += new Color(0, 0, 0, Time.deltaTime / 0.7f);
				yield return null;
			}
		}

		if (_building != null && _building.GetOwner() != null)
			tileImage.color = _building.GetOwner().getColor();
		else
			tileImage.color = originalColor;
		//tileImage.color += new Color(0, 0, 0, 1f); // set tile transparency back to full
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_unit = collision.GetComponent<Unit>(); // get the component from unit that just stepped onto the tile
		_unit.SetX(this.x);
		_unit.SetY(this.y);
		_unit.increaseStats(_attackModifier, _defenseModifier);
		if (gameObject.tag == "Building")
		{
			_unit.setBuilding(gameObject.GetComponent<Building>());
			_building.SetCurrentUnit(_unit);
		}
        _unit.transform.GetComponent<UnitSound>().PlaySound(this.tag);

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (_unit != null)
		{
			_unit.resetStats(_attackModifier, _defenseModifier);
			if (gameObject.tag == "Building")
			{
				_unit.setBuilding(null);
				_building.SetCurrentUnit(null);
			}
            _unit = null; // when unit leaves, set unit to null
		}
	}
}
