
class SpriteDrawer
{
    static string spriteDirectory = "sprites";

    static Dictionary<string, Sprite> AllSprites = new(StringComparer.OrdinalIgnoreCase);

    public static void InitilizeAllSprites()
    {
        AllSprites.Clear();

        foreach (string filePath in Directory.EnumerateFiles(spriteDirectory, "*.png"))
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            AllSprites[name] = new Sprite(new Texture(filePath));
        }
    }

    static Sprite GetStaticSprite(string spriteName)
    {
        if (AllSprites.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        throw new KeyNotFoundException($"Hittade inte {spriteName} inuti sloppet: {spriteDirectory}.");
    }

    Dictionary<string, Sprite> sprites = new(StringComparer.OrdinalIgnoreCase);

    public Sprite GetSprite(string spriteName)
    {
        if (sprites.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }

        return GetStaticSprite(spriteName);
    }

    public void InitializeSprites(string[] spriteNames)
    {
        for (int i = 0; i < spriteNames.Length; i++)
        {
            string spriteName = spriteNames[i];

            //dubbelcheckar att den finns så att inget blir fel
            if (!sprites.ContainsKey(spriteName))
            {
                sprites[spriteName] = GetStaticSprite(spriteName);
            }
        }
    }

    public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window)
    {
        sprite.Position = position;
        sprite.Scale = new Vector2f(size.X / sprite.Texture.Size.X, size.Y / sprite.Texture.Size.Y);
        window.Draw(sprite);
    }

    public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window, bool flip, IntRect region)
    {
        Sprite spriteToDraw = DivideSprite(position, size, sprite, region);

        Vector2f scale = new Vector2f(size.X / region.Width, size.Y / region.Height);
        scale.X = flip ? -Math.Abs(scale.X) : Math.Abs(scale.X);

        spriteToDraw.Scale = scale;
        spriteToDraw.Position = position + new Vector2f(size.X / 2f, size.Y / 2f);

        window.Draw(spriteToDraw);
    }

    Sprite DivideSprite(Vector2f position, Vector2f size, Sprite sprite, IntRect region)
    {
        Sprite spriteToDraw = new Sprite(sprite);
        spriteToDraw.TextureRect = region;
        spriteToDraw.Origin = new Vector2f(region.Width / 2f, region.Height / 2f);
        spriteToDraw.Position = position + new Vector2f(size.X / 2f, size.Y / 2f);
        spriteToDraw.Scale = new Vector2f(size.X / region.Width, size.Y / region.Height);

        return spriteToDraw;
    }

    public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window, IntRect region)
    {
        Sprite sprite1 = DivideSprite(position, size, sprite, region);

        window.Draw(sprite1);
    }
}