using Infrastructure.Services;
using UI;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        public ControlsPanel CreateControlsPanel();
    }
}