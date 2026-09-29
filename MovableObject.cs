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
        Vector2f inputDirection = new(0,0);
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

        //restrict movement here

         return inputDirection;
    }

    protected void Move(float deltaTime)
    {
        position += direction * deltaTime * moveSpeed;
    }
}