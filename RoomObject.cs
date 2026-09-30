
abstract class RoomObject
{
	public Vector2f position;
	public Vector2f size;
	public string spriteName;
	protected SpriteDrawer spriteDrawer;
	public CollisionBox collisionBox;
	public bool remove = false;

	public RoomObject()
	{
		//Game.currentRoom.RoomObjects.Add(this);
		spriteDrawer = new();
	}

	abstract public void RoomStart();

	abstract public void Update(float deltaTime);

	abstract public void Draw(RenderWindow window);
}
