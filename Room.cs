class Room
{
    static string filePath = "rooms/maze.txt";
    static Dictionary<string, Sound> songs = new(StringComparer.OrdinalIgnoreCase);

    public static void InitilizeEpicSongs()
    {
        //får alla wav filer i assets och sparar de i en dictionary
        foreach (string filePath in Directory.EnumerateFiles("musik", "*.wav"))
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            songs[name] = new Sound(new SoundBuffer(filePath));
        }
    }
    public static Room MakeRoomFromRoomData()
    {
        Room newRoom = new();
        char[] MazeData = File.ReadAllText(filePath).ToCharArray();

        for (int i = 0; i < MazeData.Length; i++)
        {
            switch (MazeData[i])
            {
                case 'p':
                    //spawn pacman
                    newRoom.RoomObjects.Add(new Pacman());
                    break;

                case 'g':
                    //spawn ghost
                    break;

                case 'c':
                    //spawn candy
                    break;

                case '.':
                    //spawn coin
                    break;

                case '#':
                    //spawn wall
                    break;

                case '|':
                    //spawn something
                    break;

                case ' ':
                    //empty space
                    break;
            }
        }

        System.Console.WriteLine(MazeData);

        return newRoom;
    }


    public List<RoomObject> RoomObjects;
    string spriteName;
    SpriteDrawer backGroundDrawer;
    public string songName;

    Room()
    {
        backGroundDrawer = new();
        RoomObjects = new();
        songName = "sng";
        spriteName = "red";
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
    }

    void PlayMusic(string songName)
    {
        //😂😂
        if (songs.TryGetValue(songName, out Sound sound)) sound.Play();
    }

}
