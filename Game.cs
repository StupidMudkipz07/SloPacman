using System.Text.Json;

static class Game
{
    //matte snillet
    static public decimal E(int precision)
    {
        decimal result = 1;
        decimal item = 1;

        for (decimal i = 1; i < precision; ++i)
        {
            result += item /= i;
        }

        return result;
    }

    static public int health = 3;
    static public decimal score = 0;
    static public decimal highScore;
    public static Room currentRoom;
    public static Vector2u WindowSize = new(900, 900);
    public static string highScoreFilePath = "scores/scores.json";

    static void LoadRoom()
    {
        currentRoom = Room.MakeRoomFromRoomData();
        currentRoom.StartRoom();
    }

    static void SaveHighScore(string filePath, decimal value)
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(value));
        System.Console.WriteLine($"new highscore {value}");
    }

    static void LoadHighScore(string filePath)
    {
        highScore = JsonSerializer.Deserialize<decimal>(File.ReadAllText(filePath));
        System.Console.WriteLine($"this is the highscore {highScore}");
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

            LoadHighScore(highScoreFilePath);

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
                    CalculateHighScore(score, window);
                    foreach (RoomObject roomObject in currentRoom.RoomObjects) roomObject.remove = true;

                    LoadRoom();
                    LoadHighScore(highScoreFilePath);
                    health = 3;
                    score = 0;
                }

                List<Coin> coins = currentRoom.RoomObjects.Where(c => c is Coin).Select(c => c as Coin).ToList();
                if (AllCoinsCollected(coins))
                {   
                    foreach (RoomObject roomObject in currentRoom.RoomObjects) roomObject.remove = true;
                    LoadRoom();
                    /*
                    foreach (Coin coin in coins)
                    {
                        coin.Reset();
                        coin.isCollected = false;
                    }
                    */
                }

                window.Display();
            }
        }
    }

    public static void CalculateHighScore(decimal score, RenderWindow denLustigaSkärmen)
    {
        Text ScoreText = new()
        {
            CharacterSize = 30,
            Font = new Font("fonts/saturno.ttf"),
            FillColor = Color.Black,
            OutlineThickness = 2,
            OutlineColor = Color.Green,
            Position = new(0, 450),
        };

        if (score > highScore)
        {
            SaveHighScore(highScoreFilePath, score);
            highScore = score;

            ScoreText.DisplayedString = $"New Highscore {/* hej jag heter niklas sepand ashraf semnani lashed get*/highScore}!!!!11";
        }
        else
        {
            ScoreText.DisplayedString = $"Current Highscore: {highScore}\n your score: {score}";
        }
        denLustigaSkärmen.Draw(ScoreText);
        denLustigaSkärmen.Display();
        Thread.Sleep(6000+67);
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