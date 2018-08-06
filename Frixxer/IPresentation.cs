using FCore.Foundations;

namespace Frixxer
{
    public interface IPresentation : IIdentifiable<long>
    {
        string Name { get; set; }
        string Data { get; set; }
    }
}
