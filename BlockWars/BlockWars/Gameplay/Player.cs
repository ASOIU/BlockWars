using System.Collections.Generic;
namespace BlockWars.Gameplay
{
    class Player
    {
        public PlayerData Resources { get; set; }

        //public Gun Gun { get; set; }

        public List<Gun> Guns { get; private set; }
        
        public string Name { get; set; }
        
        public EntityCategory PlayerType { get; private set; }

        public Player(EntityCategory playerType, string name = "Player")
        {
            Name = name;
            PlayerType = playerType;
			Resources = new PlayerData();
            Guns = new List<Gun>();
        }

        public bool CheckBorder(double x, double y, EntityCategory playerType)
        {
            switch (playerType)
            {
                case EntityCategory.Player1:
                    {
                        if ((x <= 0 && x >= -350)&&(y>=-20 && y<=250))
                        {
                            return true;
                            break;
                        }
                        else
                        {
                            return false;
                            break;
                        }
                    }
                case EntityCategory.Player2:
                    {
                        if ((x >= 0 && x <= 350)&&(y>=-20 && y<=250))
                        {
                            return true;
                            break;
                        }
                        else
                        {
                            return false;
                            break;
                        }
                    }
                default:
                    return false;
                    break;
            }
        }
    }
}
