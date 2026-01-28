using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
partial struct NetcodePlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetcodePlayerInput>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach ((
            RefRO<NetcodePlayerInput> input, RefRW<LocalTransform> transform)
            in SystemAPI.Query<RefRO<NetcodePlayerInput>, RefRW<LocalTransform>>().WithAll<Simulate>()) {

            float2 moveInput = input.ValueRO.move;
            moveInput = math.normalizesafe(moveInput) * dt * 4.5f;
            transform.ValueRW.Position += new float3(moveInput.x, 0, moveInput.y);
        }
    }
}