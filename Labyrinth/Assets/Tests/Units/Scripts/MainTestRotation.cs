using System;
using Scripts;
using Tests.Units.Scripts;
using Units.OneUnit.Base.GameObject.Rotation;
using UnityEngine;

public class MainTestRotation : MonoBehaviour
{
	public GameObject UnitPrefab;
	private OneUnitRotationController _oneUnitRotationController;
	// Use this for initialization
	void Start ()
	{
		var settings = new RotationUnitSettings();
		settings.SetGraphicObject(UnitPrefab);
		_oneUnitRotationController = new OneUnitRotationController(settings);
	}
	
	public void RotateTo(string param)
	{
		string [] parameters = param.Split(new Char [] {','});
		_oneUnitRotationController.Rotate(new IntVector2(Int32.Parse(parameters[0]), Int32.Parse(parameters[1])),
			new IntVector2(Int32.Parse(parameters[2]), Int32.Parse(parameters[3])));
		
		/*
		_oneUnitRotationController.Rotate(new IntVector2(Int32.Parse(parameters[1]), Int32.Parse(parameters[0])),
			new IntVector2(Int32.Parse(parameters[3]), Int32.Parse(parameters[2])));
			*/
	}
}
