abstract class MovableObject : RoomObject
{
    protected Vector2f direction = new();

    public float moveSpeed;

    protected Vector2f GetRandomDirection()
    {
        Random random = new Random();
        switch (random.Next(1, 5))
        {
            case 1:
                return new Vector2f(1, 0);
            case 2:
                return new Vector2f(-1, 0);
            case 3:
                return new Vector2f(0, 1);
            case 4:
                return new Vector2f(0, -1);
            default:
                return new Vector2f(0, 0);
        }
    }

    protected Vector2f GetDirection()
    {
        Vector2f inputDirection = new(0, 0);
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.A))
        {
            inputDirection = new Vector2f(-1, 0);
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.D))
        {
            inputDirection = new Vector2f(1, 0);
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.W))
        {
            inputDirection = new Vector2f(0, -1);
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.S))
        {
            inputDirection = new Vector2f(0, 1);
        }

        return inputDirection;
    }

    protected void Move(float deltaTime)
    {
        IfOutOfBounds();
        position = FixAlignment(position);

        Vector2f positionToMove = position;
        positionToMove += direction * deltaTime * moveSpeed;

        if (WillIntersect(positionToMove))
        {
            positionToMove = position;
        }

        position = positionToMove;
    }

    public bool IsAligned() => MathF.Floor(position.X) % 36 == 0 && MathF.Floor(position.Y) % 36 == 0;

    public float Cloasest36(float positionXorY)
    {
        float currentPos = MathF.Floor(positionXorY);

        float posetivCheck = 0;
        float negativeCheck = 0;

        while (MathF.Floor(currentPos + posetivCheck) % 36 != 0)
        {
            posetivCheck++;
        }
        while (MathF.Floor(currentPos + negativeCheck) % 36 != 0)
        {
            negativeCheck--;
        }

        float temp = MathF.Min(posetivCheck, MathF.Abs(negativeCheck));
        if (temp == MathF.Abs(negativeCheck))
            temp = negativeCheck;

        return currentPos + temp;
    }

    public bool WillIntersect(Vector2f position)
    {
        FloatRect positionFloatRect = new FloatRect(position.X, position.Y, 35, 35);

        foreach (RoomObject wall in Game.currentRoom.RoomObjects)
        {
            if (wall is Wall)
            {
                if (wall.collisionBox.collisionBoxRect.Intersects(positionFloatRect))
                    return true;
            }
        }
        return false;
    }

    public Vector2f FixAlignment(Vector2f position)
    {
        if (!IsAligned())
        {
            if (direction.X != 0)
            {
                if (MathF.Floor(position.Y) % 36 != 0)
                {
                    position.Y = Cloasest36(position.Y);
                }
            }
            if (direction.Y != 0)
            {
                if (MathF.Floor(position.X) % 36 != 0)
                {
                    position.X = Cloasest36(position.X);
                }
            }
        }

        return position;
    }

    public void IfOutOfBounds()
    {
        if (position.X < 0 - size.X)
            position.X = 900;
        if (position.X > 900)
            position.X = 0 - size.X;
    }

    public List<Vector2f> GetValidDirections(float deltaTime)
    {
        List<Vector2f> validDirections = new List<Vector2f>();
        Vector2f[] directions = new Vector2f[4];
        { directions[0] = new Vector2f(1, 0); directions[1] = new Vector2f(-1, 0); directions[2] = new Vector2f(0, 1); directions[3] = new Vector2f(0, -1); }

        foreach (Vector2f direction in directions)
        {
            Vector2f dirToMove = direction;
            //prevent going backwards
            if (dirToMove == this.direction * -1)
                continue;

            Vector2f positionToMove = position;
            positionToMove += direction * deltaTime * moveSpeed;

            if (WillIntersect(positionToMove))
                continue;

            validDirections.Add(direction);
        }
        return validDirections;
    }

    //denna funktion låter bara objekt kollidera med andra objekt som skapades innan sig själv
    //detta beror på denna lustiga maze
    //spelaren kan bara äta coins som skapades innan honom men inte de n
    public RoomObject? CheckCollision()
    {
        for (int i = 0; i <  Game.currentRoom.RoomObjects.Count; i++)
        {
            RoomObject? roomObject =  Game.currentRoom.RoomObjects[i];
            CollisionBox? otherCollisionBox = roomObject.collisionBox;
            //väggar har annan kollisions logik
            if(roomObject is Wall) continue;

            //fakitiska collisions checken
            if (collisionBox.collisionBoxRect.Intersects(otherCollisionBox.collisionBoxRect)) return roomObject;
        }

        return null;
    }
}