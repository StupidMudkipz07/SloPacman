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

    protected void Move(float deltaTime)
    {
        position += direction * deltaTime * moveSpeed;
    }
}