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
		static PlayerData()
		{
			mDefault = new PlayerData(null);
			Default.CurrentResources = 1000;
		}
		private PlayerData(PlayerData playerData)
		{
			mDefault = playerData;
		}
		public PlayerData()
		{
			CurrentResources = Default.CurrentResources;
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
			}
			return false;
		}
		
		public enum ObjectType
		{
			Block1,
			Block2,
			Block3
		}

	    public void AddResourcesBlockDestroy(int startHealth)
	    {
            //TODO:
	    }
	}
}
