using Beecon.Components;
using Beecon.Input;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class PlayerMovementSystem : GameSystem
{
    private Vector2 _movement;
    public static float Acceleration => 5f;
    public static float MaxSpeed => 225f;

    public override void Update()
    {
        _movement.X = Inputs.HorizontalAxis.Value;
        _movement.Y = Inputs.VerticalAxis.Value;
        _movement = _movement.Normalize();
    }

    public override void FixedUpdate()
    {
        foreach (var (_, _, body) in Entries<Player, Body>())
            body.Seek(_movement * MaxSpeed, Acceleration);
    }
}
