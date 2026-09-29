public class CollisionBox
{
	public Vector2f position;
	public Vector2f size;
	public Vector2f center => new Vector2f(position.X + collisionBoxRect.Width / 2, position.Y + collisionBoxRect.Height / 2);
	public FloatRect collisionBoxRect => new FloatRect(position, size);

	public CollisionBox(Vector2f position, Vector2f size)
	{
		this.position = position;
		this.size = size;
	}

	public void DrawCollisionbox(RenderWindow window)
	{
		if (KeyboardHandler.IsKeyDown(Keyboard.Key.LShift))
		{
			RectangleShape shape = new RectangleShape()
			{
				Size = size,
				Position = position,
				FillColor = new Color(30, 235, 30, 180)
			};

			CircleShape point = new CircleShape(2f)
			{
				Position = center,
				FillColor = Color.Red
			};

			window.Draw(shape);
			window.Draw(point);
		}
	}



}