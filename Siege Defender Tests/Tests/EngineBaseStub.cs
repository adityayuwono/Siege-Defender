using Scripts.Models;

namespace SiegeDefenderTests.Tests
{
    public class EngineBaseStub
    {
        protected readonly Stubs.EngineBaseStub EngineBase;

        public EngineBaseStub()
        {
            var engineStub = new Stubs.EngineBaseStub(new EngineModel {Id = "TestEngineRoot"});
            engineStub.MapInjections();
            engineStub.Activate();
            engineStub.Show();

            EngineBase = engineStub;
        }
    }
}
