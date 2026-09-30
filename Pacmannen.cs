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

        //direction = GetRandomDirection();

        direction = GetDirection();
        Move(deltaTime);
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, sprite, window, new(0, 0, 18, 18));

        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }
}
