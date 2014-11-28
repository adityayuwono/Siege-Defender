using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace SiegeDefenderTests.Stubs
{
    public class EngineBaseStub : EngineBase
    {
        public EngineBaseStub(EngineModel model) : base(model, null)
        {
        }

        public override PlayerSettingsModel PlayerSettingsModel
        {
            get { return _playerSettingsModel; }
        }
        private PlayerSettingsModel _playerSettingsModel;
        public void SetPlayerSettingsModel(PlayerSettingsModel playerSettingsModel)
        {
            _playerSettingsModel = playerSettingsModel;
        }

        private readonly IntervalRunnerStub _intervalRunner = new IntervalRunnerStub();
        public override IIntervalRunner IntervalRunner
        {
            get { return _intervalRunner; }
        }
        
        public void AddLootTable(LootTable lootTable)
        {
            _lootTables.Add(lootTable.Id, lootTable);
        }

        public void AddItemModel(ItemModel itemModel)
        {
            _items.Add(itemModel.Id, itemModel);
        }

        public override void Save()
        {
            Console.WriteLine("Saving...");
        }
    }

    public class IntervalRunnerStub : IIntervalRunner
    {
        private readonly Dictionary<Action, float> _subscribedActions = new Dictionary<Action, float>();

        public void SubscribeToInterval(Action action, float delay = 0, bool startImmediately = true)
        {
            _subscribedActions.Add(action, delay);
        }

        public bool UnsubscribeFromInterval(Action action)
        {
            foreach (var subscribedAction in _subscribedActions.ToArray())
                if (subscribedAction.Key == action)
                    return _subscribedActions.Remove(subscribedAction.Key);

            return false;
        }

        public void UpdateTime(float timeElapsed)
        {
            foreach (var subscribedAction in _subscribedActions.ToArray())
            {
                if (subscribedAction.Value < timeElapsed)
                    subscribedAction.Key();
            }
        }
    }
}
