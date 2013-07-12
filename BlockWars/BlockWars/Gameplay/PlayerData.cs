using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars.Gameplay
{
	class PlayerData
	{
		int CurrentResources;
		public static PlayerData Default
		{
			get
			{
				return mDefault;
			}
		}
		private static PlayerData mDefault;

		const int Block1Price = 10;
		const int Block2Price = 100;
		const int Block3Price = 500;
        const int GunPrice = 1000;
        const int BulletAPrice = 200;
        int BlockDestroyPercent = 10;
        int[,] UpgradesPriceAndMaxLevels = {{3, 100, 200, 300 }, 
                                            {3, 100, 200, 300 }, 
                                            {3, 100, 200, 300 }};
        const int UPGRADESCOUNT = 3;
        public int GunMagazineSize = 3;
        int ResourcesForTurn = 200;
        int[] CurrentUpgradeLevels;
        
        public void AddResourcesForTurn()
        {
            CurrentResources += ResourcesForTurn;
        }
		static PlayerData()
		{
			mDefault = new PlayerData(null);
			Default.CurrentResources = 1000000;
		}
		private PlayerData(PlayerData playerData)
		{
			mDefault = playerData;
		}
		public PlayerData()
		{
			CurrentResources = Default.CurrentResources;
            CurrentUpgradeLevels = new int[UPGRADESCOUNT];
		}
		public int GetResources() { return CurrentResources; }
		public bool RemoveResources(ObjectType obj)
		{
			if (!CheckResourceAvaliabe(obj))
			{
				return false;
			}
			switch (obj)
			{
				case ObjectType.Block1:
					CurrentResources -= Block1Price;
					break;
				case ObjectType.Block2:
					CurrentResources -= Block2Price;
					break;
				case ObjectType.Block3:
					CurrentResources -= Block3Price;
					break;
                case ObjectType.Gun:
                    CurrentResources -= GunPrice;
					break;
                case ObjectType.BulletA:
                    CurrentResources -= BulletAPrice;
                    break;
			}
			return true;
		}
		public bool CheckResourceAvaliabe(ObjectType obj)
		{
			switch (obj)
			{
				case ObjectType.Block1:
					if (CurrentResources>=Block1Price)
					{
						return true;
					}
					break;
				case ObjectType.Block2:
					if (CurrentResources >= Block2Price)
					{
						return true;
					}
					break;
				case ObjectType.Block3:
					if (CurrentResources >= Block3Price)
					{
						return true;
					}
					break;
                case ObjectType.Gun:
                    if (CurrentResources >= GunPrice)
                    {
                        return true;
                    }
                    break;
                case ObjectType.BulletA:
                    if (CurrentResources >= BulletAPrice)
                    {
                        return true;
                    }
                    break;
			}
			return false;
		}
        public void AddResourcesBlockDestroy(int startHealth)
        {
            CurrentResources += startHealth * BlockDestroyPercent / 100;
        }
        public bool BuyUpgrade(int upgradeType)
        {
            if (upgradeType<0||upgradeType>=UPGRADESCOUNT)
            {
                throw new ArgumentException("\"upgradeType\" is not valid");
                return false;
            }
            else if (CurrentUpgradeLevels[upgradeType]==UpgradesPriceAndMaxLevels[upgradeType,0])
            {
                return false;
            }
            else if (CurrentResources<UpgradesPriceAndMaxLevels[upgradeType,CurrentUpgradeLevels[upgradeType]+1])
            {
                return false;
            }
            else
            {
                CurrentUpgradeLevels[upgradeType]++;
                CurrentResources -= UpgradesPriceAndMaxLevels[upgradeType, CurrentUpgradeLevels[upgradeType]];
                switch (upgradeType)
                {
                    case 0:
                        GunMagazineSize++;
                        break;
                    case 1:
                        ResourcesForTurn += CurrentUpgradeLevels[upgradeType] * 50;
                        break;
                    case 2:
                        BlockDestroyPercent += CurrentUpgradeLevels[upgradeType] * 2;
                        break;
                }
                return true;
            }
        }
        public int[] GetUpgradeLevels()
        {
            return CurrentUpgradeLevels;
        }
		public enum ObjectType
		{
			Block1,
			Block2,
			Block3,
            Gun,
            BulletA
		}
	}
}
