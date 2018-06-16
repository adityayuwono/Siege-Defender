using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scripts.Contexts;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Models.Items;
using Scripts.ViewModels;
using Scripts.ViewModels.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Roots
{
	public class DataRoot : RootBase
	{
		private IIntervalRunner _intervalRunner;

		/// <summary>
		///     Text file that holds all the engine data
		/// </summary>
		public string EngineXML;

		/// <summary>
		///     Text file that holds all the player's progress
		/// </summary>
		public string PlayerSettingsXML;

		public TextAsset DefaultPlayerSettings;

		public Property<List<Inventory>> _inventories;

		public DataRoot()
			: base(new RootModel{Id = "DataRoot"}, null)
		{
			_inventories = new Property<List<Inventory>>();
			_inventories.SetValue(new List<Inventory>());
		}

		public DataRoot(RootModel model, BaseContext parent) : base(model, parent)
		{
			_inventories = new Property<List<Inventory>>();
			_inventories.SetValue(new List<Inventory>());
		}

		public EngineModel EngineModel { get; private set; }
		public PlayerDataModel PlayerDataModel { get; private set; }
		public string LevelId { get; set; }

		public override IIntervalRunner IntervalRunner
		{
			get { return _intervalRunner; }
		}

		public override IRoot Root
		{
			get { return this; }
		}

		public override void StartCoroutine(IEnumerator coroutine)
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			Serializer.SaveObjectToXML(PlayerDataModel);
		}

		public void InitializeEngine()
		{
			// Keep it for later, the rest of the scenes are going to need this cutie
			// TODO: improve to cloud saving, and encrypt
			PlayerSettingsXML = LoadFile(FilePaths.Loading + Values.Defaults.PlayerProgressFileName, DefaultPlayerSettings.text);

			var engineText = EngineXML;
			EngineModel = Deserializer<EngineModel>.GetObjectFromXML(engineText);

			var inventoryXML = PlayerSettingsXML;
			PlayerDataModel = Deserializer<PlayerDataModel>.GetObjectFromXML(inventoryXML);

			LoadData(EngineModel);

			LoadInventories(PlayerDataModel.Inventories);

			// Done, we simply load the next scene
			// it clears everything we put on scene, if for example we are editing a prefab
			SceneManager.LoadScene(1);
		}

		private void LoadInventories(List<InventoryModel> inventoryModels)
		{
			foreach (var inventoryModel in inventoryModels)
			{
				var inventory = new Inventory(inventoryModel, this);
				_inventories.GetValue().Add(inventory);
			}
		}

		private void LoadData(EngineModel model)
		{
			// Keep reference of every ObjectModel in a Dictionary for faster lookup
			if (model.Objects != null)
			{
				foreach (var objectModel in model.Objects)
				{
					ObjectModels.Add(objectModel.Id, objectModel);
				}
			}

			if (model.Items != null)
			{
				foreach (var objectModel in model.Items)
				{
					Items.Add(objectModel.Id, objectModel);
				}
			}
		}

		private string LoadFile(string path, string defaultIfNotFound)
		{
			string file;

			try
			{
				// Try to load save file, if any
				var sr = new StreamReader(path);
				file = sr.ReadToEnd();
				sr.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				// No save file yet, just create a default one
				file = defaultIfNotFound;
			}

			return file;
		}

		#region Object Models

		private readonly Dictionary<string, ObjectModel> ObjectModels = new Dictionary<string, ObjectModel>();

		public void AddNewObjectModel(ObjectModel newModel)
		{
			ObjectModels.Add(newModel.Id, newModel);
		}

		public ObjectModel GetObjectModel(IBase baseObject, string id)
		{
			if (ObjectModels.ContainsKey(id))
				return ObjectModels[id];

			throw new EngineException(baseObject, string.Format("ObjectModel not found, Id: {0}", id));
		}

		#endregion

		#region Items

		private readonly Dictionary<string, ItemModel> Items = new Dictionary<string, ItemModel>();

		public ItemModel GetItemModel(string itemId)
		{
			return Copier.CopyAs<ItemModel>(Items[itemId]);
		}

		#endregion

		public Inventory GetInventorySource(string sourceId)
		{
			return _inventories.GetValue().Find(i => i.Id == sourceId);
		}
	}
}
