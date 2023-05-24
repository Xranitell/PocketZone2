using System.Numerics;

public interface ISpawnable
{
    public float Priority { get; set; }

    public float Spawn(Vector2 pos);
}