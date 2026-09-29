class Border : RoomObject
{
    public Border()
    {
        size = new(36, 36);
        collisionBox = new(new(), size);
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
    }

    public override void Draw(RenderWindow window)
    {
        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {

    }
}
