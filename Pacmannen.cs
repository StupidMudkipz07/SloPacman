class PacMannen : MovableObject
{
    Sprite sprite;

    public PacMannen()
    {
        int kerki6 = 36;
        size = new(kerki6, kerki6);

        collisionBox = new(new(), size);
        spriteName = "pacman";
        sprite = spriteDrawer.GetSprite(spriteName);
        moveSpeed = 100;
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;

        direction = GetDirection();
        Move(deltaTime);
        Collision();
    }

    int slop = 0;
    public override void Draw(RenderWindow window)
    {
        slop++;
        if (slop < 12)
        {
            spriteDrawer.DrawSprite(position, size, sprite, direction, window, new(0, 0, 18, 18));
        }
        else if (slop > 12)
        {
            spriteDrawer.DrawSprite(position, size, sprite, direction, window, new(18, 0, 18, 18));
        }
        if (slop > 24)
        {
            slop = 0;
        }
        collisionBox.DrawCollisionbox(window);
    }

    void Collision()
    {
        RoomObject? båt = CheckCollision();
        if (båt is Ghost)
        {
            var kött = båt as Ghost;
            if (!kött.IsAdolfKirkable)
            {
                kött.OnCollideWithPacman();
                Reset();
                Game.health--;
            }
        }
        else if (båt is Coin)
        {
            var kött = båt as Coin;
            Game.score++;
            kött.remove = true;
        }
        else if (båt is Candy)
        {
            var kött = båt as Candy;
            Ghost.MakeAllAdolfKirkable();
            kött.remove = true;
            Game.score++;
        }
    }


    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
        SetStartPos();
    }
}
