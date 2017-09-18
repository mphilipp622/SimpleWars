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

	public Vector3 position
	{
		// This property will convert RectTransform world coordinates into a Vector3 that units can use for movement
		get
		{
			return new Vector3(_position.position.x, _position.position.y, 0);
		}
	}

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
		// used at runtime to populate script with x, y and position data
		_x = newX;
		_y = newY;
		_position = newPosition;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.name);
		_unit = collision.GetComponent<Unit>(); // get the component from unit that just stepped onto the tile
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		_unit = null; // when unit leaves, set unit to null
	}
}
