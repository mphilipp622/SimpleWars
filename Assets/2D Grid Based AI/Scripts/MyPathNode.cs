using UnityEngine;
using System.Collections;
using System;

public class MyPathNode : SettlersEngine.IPathNode<System.Object>
{
	public Int32 X { get; set; }
	public Int32 Y { get; set; }
	public Boolean IsWall {get; set;}
	
	public bool IsWalkable(System.Object unused)
	{
		return !IsWall;
	}
}
