using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Levels;

namespace Scripts.Models
{
	[Serializable]
	public class LevelModel : BaseModel
	{
		public LevelModel()
		{
			LoopCount = 0;
		}

		[XmlAttribute]
		public float Interval { get; set; }

		[XmlAttribute]
		[DefaultValue(0)]
		public int LoopCount { get; set; }


		[XmlElement(ElementName = "Spawn", Type = typeof(SpawnModel))]
		[XmlElement(ElementName = "SpawnInterval", Type = typeof(SpawnIntervalModel))]
		public List<SpawnModel> SpawnSequence { get; set; }

		[XmlElement(ElementName = "Cache", Type = typeof(SpawnModel))]
		public List<SpawnModel> CacheList { get; set; }
	}

	[Serializable]
	public class SubLevelModel : LevelModel
    {

    }
}