using Unity.Burst;
using Unity.Entities;
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
