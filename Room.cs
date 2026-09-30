class Room
{
    static string filePath = "rooms/maze.txt";
    static Dictionary<string, Sound> songs = new(StringComparer.OrdinalIgnoreCase);
    static int tileDistance = 36;

    public static void InitilizeEpicSongs()
    {
        //får alla wav filer i assets och sparar de i en dictionary
        foreach (string filePath in Directory.EnumerateFiles("musik", "*.wav"))
        {
            //Evidence-based life guidelines sorted by cost performance: longevity disease prevention, first aid, money-saving financial management, legal red lines, unemployment and work-related injuries, medical insurance social security, love marriage, pregnancy and parenting, entrepreneurship and platform compliance, going abroad and skills. Each article states the cost, benefits, eviden
            string name = Path.GetFileNameWithoutExtension(filePath);
            songs[name] = new Sound(new SoundBuffer(filePath));
        }
    }

    public static Room MakeRoomFromRoomData()
    {
        Room newRoom = new();
        char[] MazeData = File.ReadAllText(filePath).ToCharArray();

        Vector2f cursor = new(0, 0);

        for (int i = 0; i < MazeData.Length; i++)
        {
            if (cursor.X == 27)
            {
                cursor.Y++;
                cursor.X = 0;
                //System.Console.WriteLine("new row!!!!! xoxoxox");
            }
            //System.Console.WriteLine(cursor);
            //System.Console.WriteLine(cursor * tileDistance);
            switch (MazeData[i])
            {
                case 'p':
                    //spawn pacman
                    newRoom.RoomObjects.Add(new PacMannen()
                    {
                        position = cursor * tileDistance
                    });
                    //System.Console.WriteLine("player!!!");
                    break;

                case 'g':
                    newRoom.RoomObjects.Add(new Ghost()
                    {
                        position = cursor * tileDistance
                    });
                    break;

                case 'c':
                    newRoom.RoomObjects.Add(new Candy()
                    {
                        position = cursor * tileDistance
                    });
                    break;

                case '.':
                    //spawn coin
                    newRoom.RoomObjects.Add(new Coin()
                    {
                        position = cursor * tileDistance
                    });
                    break;

                case '#':
                    //spawn wall
                    newRoom.RoomObjects.Add(new Wall()
                    {
                        position = cursor * tileDistance
                    });
                    break;

                case '|':
                    newRoom.RoomObjects.Add(new Border()
                    {
                        position = cursor * tileDistance
                    });
                    break;
                case ' ':
                    //empty space
                    break;
            }
            cursor.X++;
        }

        System.Console.WriteLine(MazeData);

        return newRoom;
    }

    public List<RoomObject> RoomObjects;
    string spriteName;
    SpriteDrawer backGroundDrawer;
    public string songName;
    Text ScoreText;


    Room()
    {
        backGroundDrawer = new();
        RoomObjects = new();
        songName = "song";
        spriteName = "red";
        ScoreText = new()
        {
            CharacterSize = 30,
            Font = new Font("fonts/saturno.ttf"),
            FillColor = Color.Red,
            OutlineThickness = 10,
            OutlineColor = Color.Black
        };
    }

    public void StartRoom()
    {
        for (int i = 0; i < RoomObjects.Count; i++)
        {
            //först uppdatera alla värden
            RoomObjects[i].RoomStart();
        }
        PlayMusic(songName);
    }

    public void Update(float deltaTime)
    {

        for (int i = 0; i < RoomObjects.Count; i++)
        {
            //först uppdatera alla värden
            RoomObjects[i].Update(deltaTime);
        }
        // tar bort alla objekt efter man har itererat så inte listan förstörs
        RoomObjects.RemoveAll(obj => obj.remove == true);
    }

    public void Draw(RenderWindow window)
    {
        backGroundDrawer.DrawSprite(new(0, 0), (Vector2f)Game.WindowSize, backGroundDrawer.GetSprite(spriteName), window);
        for (int i = 0; i < RoomObjects.Count; i++)
        {
            RoomObjects[i].Draw(window);
        }

        ScoreText.Position = new Vector2f(0, 800-ScoreText.CharacterSize);
        ScoreText.DisplayedString = $"SCORE: {Game.score}";
        window.Draw(ScoreText);


    }

    void PlayMusic(string songName)
    {
        //😂😂
        if (songs.TryGetValue(songName, out Sound sound)) sound.Play();

    }
}
