class Pacman : MovableObject
{
    public Pacman()
    {
        size = new(200, 200);

        collisionBox = new(new(100, 100), size);
        spriteName = "pacman";
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, new(0, 0, 24, 24));

        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }
}
