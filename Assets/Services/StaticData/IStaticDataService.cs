﻿using Infrastructure.Services;
using StaticData;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData GetStaticData(EnemyTypeId typeId);
    }
}