class Wall : RoomObject
{

    IntRect antonKirkigasteNicklas = new(54,54,18,18);
    public Wall()
    {
        size = new(36, 36);
        spriteName = "pacman";
        collisionBox = new(new(), size);
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
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
