using Scripts.Core;
using Scripts.ViewModels;

namespace Scripts.Interfaces
{
    public interface IContext : IBase
    {
        PropertyLookup PropertyLookup { get; }
    }
}
