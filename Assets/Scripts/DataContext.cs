using System;
using System.Collections.Generic;
using System.IO;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Models.Weapons;
using Scripts.ViewModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
	/// <summary>
	///     Deserialize XML then store it
	/// </summary>
	public class DataContext : MonoBehaviour
	{
		/// <summary>
		///     Text file that holds all the engine data
		/// </summary>
		public static string EngineXML;

		/// <summary>
		///     Text file that holds all the player's progress
		/// </summary>
		public static string PlayerSettingsXML;

		public TextAsset DefaultPlayerSettings;

		public Dictionary<string, EquipmentSlotModel> Inventories = new Dictionary<string, EquipmentSlotModel>();

		public static EngineModel EngineModel { get; private set; }

		public static PlayerDataModel PlayerDataModel { get; private set; }
		public static DataContext Instance { get; set; }
		public static string LevelId { get; set; }

		private void Awake()
		{
			Instance = this;
		}

		public void Start()
		{
			// Load Engine from external file if there's a path defined, should be for development build only
			// If no path is defined, will return the engine included with build
			var enginePath = "";
#if UNITY_EDITOR
			enginePath = "Assets/Resources/Engine.xml";
#else
            enginePath = "Engine.xml";
#endif


#if !UNITY_ANDROID
            EngineXML = LoadFile(enginePath, "" /*No default engine, because it's checked earlier*/);
#else
			EngineXML = Resources.Load<TextAsset>("Engine").text;
#endif

			// Start preparing XML
			InitializeEngine();
		}

		public static void Save()
		{
			Serializer.SaveObjectToXML(PlayerDataModel);
		}

		private void InitializeEngine()
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
			foreach (var equipmentSlot in inventoryModel.EquipmentSlots)
				Inventories.Add(equipmentSlot.Id, equipmentSlot);
		}

		private void LoadData(EngineModel model)
		{
			// Keep reference of every ObjectModel in a Dictionary for faster lookup
			if (model.Objects != null)
				foreach (var objectModel in model.Objects)
					ObjectModels.Add(objectModel.Id, objectModel);

			if (model.Items != null)
				foreach (var objectModel in model.Items)
					Items.Add(objectModel.Id, objectModel);
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

		private static readonly Dictionary<string, ObjectModel> ObjectModels = new Dictionary<string, ObjectModel>();

		public static void AddNewObjectModel(ObjectModel newModel)
		{
			ObjectModels.Add(newModel.Id, newModel);
		}

		public static ObjectModel GetObjectModel(IBase baseObject, string id)
		{
			if (ObjectModels.ContainsKey(id)) return ObjectModels[id];

			throw new EngineException(baseObject, string.Format("ObjectModel not found, Id: {0}", id));
		}

		#endregion

		#region Items

		private static readonly Dictionary<string, ItemModel> Items = new Dictionary<string, ItemModel>();

		public static ItemModel GetItemModel(string itemId)
		{
			return Copier.CopyAs<ItemModel>(Items[itemId]);
		}

		#endregion
	}
}