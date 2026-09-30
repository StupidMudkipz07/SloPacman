class Candy : RoomObject
{
    IntRect tilesetPos = new(54,36,18,18);
    float collisionBoxOffset = 0.5f;
    public Candy()
    {
        size = new(36, 36);
        spriteName = "pacman";
        collisionBox = new(new(), size);
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position.X = position.X + 9;
        collisionBox.position.Y = position.Y + 9;
        collisionBox.size.X = size.X - 18;
        collisionBox.size.Y = size.Y - 18;

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
    }

    public void Collide()
    {
        var pacMannens = Game.currentRoom.RoomObjects.Where(c => c is PacMannen).ToList();
        PacMannen pacMannen = pacMannens[0] as PacMannen;
        
        if (collisionBox.collisionBoxRect.Intersects(pacMannen.collisionBox.collisionBoxRect))
        {
            Game.score++;
            remove = true;
        }
    }
}
