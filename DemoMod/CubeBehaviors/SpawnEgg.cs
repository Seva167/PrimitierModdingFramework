﻿using PrimitierModdingFramework.SubstanceModding;
using PrimitierModdingFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DemoMod.CubeBehaviors
{
	//If you are here to learn the basics of PMF look at the DecayThing. This class is more complicated
	public class SpawnEgg : MonoBehaviour, ICustomCubeBehaviour
	{

		public SpawnEgg(System.IntPtr ptr) : base(ptr) {}

		public Il2CppSystem.Action<SpawnEgg> OnSpawn;

		private CubeBase _cubeBase;

		private int _notMovingFrames = 0;

		private void Start()
		{
			_cubeBase = GetComponent<CubeBase>();

			_cubeBase.SplitEvent.AddListener(new System.Action(OnSplited));
		}


		private void FixedUpdate()
		{
			if(_cubeBase.rb.velocity.magnitude < 0.1f)
			{
				_notMovingFrames++;
			}
			else
			{
				_notMovingFrames = 0;
			}
			if (_notMovingFrames >= 200)
			{
				_notMovingFrames = 0;
				if (SpawnEggSettings.AutoHatchEntry.Value)
				{
					_cubeBase.ReceiveDamage(1000, transform.position, true);
				}
			}

		}


		private void OnSplited()
		{
			OnSpawn?.Invoke(this);
		}

	}
}
