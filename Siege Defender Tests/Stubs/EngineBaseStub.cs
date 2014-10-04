using System;
using Scripts;
using Scripts.Components;
using Scripts.Models;

namespace SiegeDefenderTests.Stubs
{
    public class EngineBaseStub : EngineBase
    {
        public EngineBaseStub(EngineModel model) : base(model, null)
        {
        }

        public override IntervalRunner IntervalRunner
        {
            get { throw new NotImplementedException(); }
        }
    }
}
