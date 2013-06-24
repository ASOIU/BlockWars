namespace BlockWars.Gameplay
{
    class Player
    {
        public PlayerData Resources { get; set; }
        public Gun Gun { get; set; }
        public string Name { get; set; }
        public EntityCategory PlayerType { get; private set; }
		

        public Player(EntityCategory playerType, string name = "Player")
        {
            Name = name;
            PlayerType = playerType;
			Resources = new PlayerData();
        }
    }
}
