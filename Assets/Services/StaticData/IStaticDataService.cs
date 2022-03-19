using Infrastructure.Services;
using StaticData;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData GetStaticData(EnemyTypeId typeId);
        PickableObjectStaticData GetPickableObjectStaticData(PickableObjectTypeId typeId);
        void LoadPickableObjects();
        LevelStaticData GetLevelStaticData(string levelKey);
        void LoadLevelStaticData();
    }
}