using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

	int _x, _y;
	RectTransform _position;
	Unit _thisUnit;
	Building _thisBuilding;

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
		get
		{
			return new Vector3(_position.position.x, _position.position.y, 0);
		}
	}

	public Unit thisUnit
	{
		get
		{
			return _thisUnit;
		}
		set
		{
			_thisUnit = value;
		}
	}

	public Building thisBuilding
	{
		get
		{
			return _thisBuilding;
		}
		set
		{
			_thisBuilding = value;
		}
	}

	public Tile(int newX, int newY, RectTransform newPosition)
	{
		_x = newX;
		_y = newY;
		_position = newPosition;
		
	}
}
