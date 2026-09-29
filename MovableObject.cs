abstract class MovableObject : RoomObject
{
    protected Vector2f velocity = new();

    public float moveSpeed;

    protected Vector2f GetRandomDirection()
    {
        return new(0, 1);
    }

    protected void Move(float deltaTime)
    {
        position += velocity * deltaTime * moveSpeed;
    }
}