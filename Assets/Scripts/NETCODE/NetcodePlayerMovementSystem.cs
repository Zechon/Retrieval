using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
partial struct NetcodePlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //PROGRESS:
        //Getting Started with Netcode for Entities (DOTS Multiplayer Tutorial Unity 6) - 43:22
    }
}


public struct PlayerMovementData : IComponentData
{
    public float3 Velocity;
    public float WalkSpeed;
    public float SprintSpeed;
    public float JumpForce;
    public bool Grounded;
}


