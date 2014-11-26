using Scripts.Core;

namespace Scripts.Interfaces
{
    public interface IContext : IBase
    {
        PropertyLookup PropertyLookup { get; }
    }
}
