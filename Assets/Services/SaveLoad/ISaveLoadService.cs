using Data;
using Infrastructure.Services;

namespace Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        public void SaveSettings();
        Settings LoadSettings();
    }
}