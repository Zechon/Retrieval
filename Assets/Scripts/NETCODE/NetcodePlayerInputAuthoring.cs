using Unity.NetCode;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class NetcodePlayerInputAuthoring : MonoBehaviour
{
    public float walkSpeed = 4.5f;
    public float sprintSpeed = 7.5f;
    public float jumpForce = 7f;

    public class Baker : Baker<NetcodePlayerInputAuthoring>
    {
        public override void Bake(NetcodePlayerInputAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new NetcodePlayerInput());
            AddComponent(entity, new PlayerMovementData
            {
                Velocity = float3.zero,
                WalkSpeed = authoring.walkSpeed,
                SprintSpeed = authoring.sprintSpeed,
                JumpForce = authoring.jumpForce,
                Grounded = true, // TEMP: force grounded to test movement
                GroundY = 0.5f
            });
        }
    }
}

public struct NetcodePlayerInput : IInputComponentData
{
    public float2 move;
    public bool jump;
}

public struct PlayerInputSingleton : IComponentData
{
    public float2 move;
    public bool jump;
}

public struct PlayerMovementData : IComponentData
{
    public float3 Velocity;
    public float WalkSpeed;
    public float SprintSpeed;
    public float JumpForce;
    public bool Grounded;

    public float GroundY; // usually 0
}
