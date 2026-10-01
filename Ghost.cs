class Ghost : MovableObject
{
    public bool IsAdolfKirkable { get; private set; } = false;
    public float IsAdolfKirkableTime = 0;
    public float MaxIsAdolfKirkableTime = 10;


	public void MakeAdolfKirkable()
    {
        IsAdolfKirkable = true;
        //start timer here
    }

    IntRect tilesetPos = new(36, 0, 18, 18);
    IntRect tilesetPos2 = new(54, 0, 18, 18);
    IntRect tilesetPosKirkable = new(36, 18, 18, 18);
    IntRect tilesetPosKirkable2 = new(54, 18, 18, 18);

    public Ghost()
    {
        size = new(36, 36);
        spriteName = "pacman";
        collisionBox = new(new(), size);
        moveSpeed = 105;
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;

        SetDirection(deltaTime);
        Move(deltaTime);

        if (IsAdolfKirkable)
            OutOfIsAdolfKirkableTime(deltaTime);
    }

    void SetDirection(float deltaTime)
    {
        Random random = new Random();
        var validDirctions = GetValidDirections(deltaTime);
        if (validDirctions.Count == 0)
            direction = new Vector2f(0, 0);
        else
            direction = validDirctions[random.Next(0, validDirctions.Count)];
    }


    int slop = 0;
    public override void Draw(RenderWindow window)
    {
        slop++;
        if (!IsAdolfKirkable)
        {
            if (slop < 10)
                spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, tilesetPos);
            else if (slop > 10)
            {
                spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, tilesetPos2);
            }
        }
        else
        {
            if (slop < 10)
                spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, tilesetPosKirkable);
            else if (slop > 10)
            {
                spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, tilesetPosKirkable2);
            }
        }
        if (slop > 20) slop = 0;

        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
        SetStartPos();
    }
    
    //den här funtioen funkar bara inte ibland
    //mer specifikt den funkar inte när isadolfkirkable blir true av någon anleding
    public void OnCollideWithPacman()
    {
        System.Console.WriteLine("hit");
        if (IsAdolfKirkable)
        {
            IsAdolfKirkable = false;
            Reset();
            System.Console.WriteLine("itadakimasu");
            IsAdolfKirkableTime = 0;
            Game.score += E(100);
        }
    }

    public decimal E(int precision)
    {
        decimal result = 1;
        decimal item = 1;

        for (decimal i = 1; i < precision; ++i)
        {
            result += item /= i;
        }

        return result;
    }

    public static void MakeAllAdolfKirkable()
    {
        Ghost[] sloppa = Game.currentRoom.RoomObjects.OfType<Ghost>().ToArray();

        foreach (var Slopparen in sloppa)
        {
            Slopparen.MakeAdolfKirkable();
        }
    }

    public void OutOfIsAdolfKirkableTime(float deltaTime)
    {
        IsAdolfKirkableTime += deltaTime;

        if (IsAdolfKirkableTime >= MaxIsAdolfKirkableTime)
        {
            IsAdolfKirkable = false;
            IsAdolfKirkableTime = 0;
        }
    }
}
