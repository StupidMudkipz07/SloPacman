using System.Diagnostics;

class Pacman : MovableObject
{
    public Pacman()
    {
        int kerki6 = 36;
        size = new(kerki6, kerki6);

        collisionBox = new(new(), size);
        spriteName = "pacman";
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, new(0, 0, 18, 18));

        collisionBox.DrawCollisionbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }
}
