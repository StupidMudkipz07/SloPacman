class Ghost : MovableObject
{
    public bool IsEatable = true;
    IntRect tilesetPos = new(36,0,18,18);
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

        Random random = new Random();
        var validDirctions = GetValidDirections(deltaTime);
        if (validDirctions.Count == 0)
            direction = new Vector2f(0, 0);
        else
            direction = validDirctions[random.Next(0, validDirctions.Count)];

		Move(deltaTime);
        Collide();
	}

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window,tilesetPos);

        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
        System.Console.WriteLine(E(500));
    }

    public void Collide()
    {
        var pacMannens = Game.currentRoom.RoomObjects.Where(c => c is PacMannen).ToList();
        PacMannen pacMannen = pacMannens[0] as PacMannen;
        
        if (collisionBox.collisionBoxRect.Intersects(pacMannen.collisionBox.collisionBoxRect))
        {
            if (IsEatable)
            {
                IsEatable = false;
                  //spriteDrawer.GetSprite(spriteName) = spriteDrawer.GetSprite(spriteName)
                Game.score += E(100);
            }
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
}
