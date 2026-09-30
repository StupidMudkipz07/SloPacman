class Wall : RoomObject
{

    //IntRect antonKirkigasteNicklas = new(54,54,18,18);

    IntRect antonKirkigasteNicklas;

    public Wall()
    {
        size = new(36, 36);
        spriteName = "adolf";
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
        antonKirkigasteNicklas = new((Vector2i)position, (Vector2i)size);
        spriteDrawer.InitializeSprites([spriteName]);
    }
}
