class Ghost : MovableObject
{
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

    
}
