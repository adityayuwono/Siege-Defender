using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Core
{
	public class SpriteLookup
	{
		private readonly List<Sprite> _artSprites;
		private SpriteLookup()
		{
			_artSprites = Resources.LoadAll<Sprite>("GUIs/ProjectileIcons").ToList();
		}

		public static SpriteLookup Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new SpriteLookup();
				}

				return _instance;
			}
		}
		private static SpriteLookup _instance;

		public Sprite GetSpriteByName(string spriteName)
		{
			var sprite = _artSprites.Find(s => s.name == spriteName);
			if (sprite == null)
			{
				throw new Exception("No sprite found with id: " + spriteName);
			}

			return sprite;
		}
	}
}
