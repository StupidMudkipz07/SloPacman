class Ghost : MovableObject
{
    IntRect tilesetPos = new(36,0,18,18);
    public Ghost()
    {
        size = new(36, 36);
        spriteName = "pacman";
        collisionBox = new(new(100, 100), size);
        moveSpeed = 36;
    }

    public override void Update(float deltaTime)
    {
        
		IfOutOfBounds();
		collisionBox.position = position;
        collisionBox.size = size;

		position = FixAlignment(position);

        Random random = new Random();
        var validDirctions = GetValidDirections(deltaTime);
        foreach (var dir in validDirctions)
		    Console.WriteLine(dir);
        if (validDirctions.Count == 0)
            direction = new Vector2f(0, 0);
        else
		    direction = validDirctions[random.Next(0, validDirctions.Count - 1)];

		Vector2f positionToMove = position;
		positionToMove += direction * deltaTime * moveSpeed;
		positionToMove = new(MathF.Round(positionToMove.X), MathF.Round(positionToMove.Y));

		position = positionToMove;

		//Move(deltaTime);
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

    public List<Vector2f> GetValidDirections(float deltaTime)
    {
        List<Vector2f> validDirections = new List<Vector2f>();
        Vector2f[] directions = new Vector2f[4]; { directions[0] = new Vector2f(1, 0); directions[1] = new Vector2f(-1, 0); directions[2] = new Vector2f(0, 1); directions[3] = new Vector2f(0, -1); }

        foreach (Vector2f direction in directions)
        {
			Vector2f dirToMove = direction;
            if (dirToMove == this.direction * -1)
                continue;

			Vector2f positionToMove = new (MathF.Round(position.X), MathF.Round(position.Y));
			positionToMove += direction * deltaTime * moveSpeed;
			positionToMove = new(MathF.Round(positionToMove.X), MathF.Round(positionToMove.Y));


			if (WillIntersect(positionToMove))
                continue;

            validDirections.Add(direction);
		}
		
        return validDirections;
	}
}
