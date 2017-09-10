using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
	int x = 0, y = 0;

	void Start ()
	{
			
	}
	
	void Update ()
	{
		Move();
		//if(Input.GetButtonDown("Jump"))
		//	transform.position = new Vector3(GridManager.gridMan.gridPosition[4, 5].position.x, GridManager.gridMan.gridPosition[4, 5].position.y, 0);
	}

	void Move()
	{
		if (Input.GetKeyDown(KeyCode.W) && y < GridManager.gridMan.columns - 1)
		{
			y++;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
		if (Input.GetKeyDown(KeyCode.S) && y > 0 )
		{
			y--;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
		if (Input.GetKeyDown(KeyCode.A) && x > 0)
		{
			x--;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
		if (Input.GetKeyDown(KeyCode.D) && x < GridManager.gridMan.rows - 1)
		{
			x++;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
	}
	
}
