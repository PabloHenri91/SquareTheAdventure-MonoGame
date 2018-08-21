using System;

using Realms;

namespace Hydra
{
    public class PlayerData : RealmObject
    {
        public int points { get; set; }
    }

    public static class MemoryCardExtension
    {

        internal static PlayerData newPlayerData(this MemoryCard memoryCard)
        {
            PlayerData playerData = memoryCard.insert<PlayerData>();

            playerData.points = 0;

            return playerData;
        }
    }
}
