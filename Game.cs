
// this class should handle room switching
class Game
{
    static public int health = 3;
    static public decimal score = 0;
    public static Room currentRoom;
    public static Vector2u WindowSize = new(900, 900);
    public static string saveFilePath = "scores/scores.json";

    static void LoadRoom()
    {
        currentRoom = Room.MakeRoomFromRoomData();
        currentRoom.StartRoom();
    }

    static void SaveGame(string filePath)
    {

    }

    internal static void StartGame()
    {
        using (var window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), "Pacslopper"))
        {
            window.SetFramerateLimit(60);
            window.Closed += (o, e) => window.Close();

            SpriteDrawer.InitilizeAllSprites();
            Room.InitilizeEpicSongs();

            LoadRoom();

            Clock clock = new Clock();
            //mainloop
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();

                window.Clear();

                currentRoom.Update(deltaTime);
                currentRoom.Draw(window);

                if (health <= 0)
                {
                    foreach (RoomObject roomObject in currentRoom.RoomObjects)
                        roomObject.remove = true;

                    LoadRoom();
                    health = 3;
                    score = 0;
                }

                List<Coin> coins = currentRoom.RoomObjects.Where(c => c is Coin).Select(c => c as Coin).ToList();
				if (AllCoinsCollected(coins))
                {
                    foreach (Coin coin in coins)
                    {
                        coin.Reset();
                        coin.isCollected = false;
                    }
                }

                window.Display();
            }
        }
    }

    public static bool AllCoinsCollected(List<Coin> coins)
    {
        int coinsCollected = 0;

        foreach (Coin coin in coins)
        {
            if (coin.isCollected == true)
                coinsCollected++;
        }

        if (coinsCollected == coins.Count())
            return true;

        return false;
    }
}