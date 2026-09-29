class Coin : RoomObject
{
    IntRect antonKirkigasteNicklas = new(36,36,18,18);
    public Coin()
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
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, antonKirkigasteNicklas);

        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }
}
