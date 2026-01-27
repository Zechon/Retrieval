using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
partial struct NetcodePlayerMovementSystem : ISystem
{
    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach ((
            RefRO<NetcodePlayerInput> input, RefRW<PlayerMovementData> move, RefRW<LocalTransform> transform)
                in SystemAPI.Query<
                    RefRO<NetcodePlayerInput>, RefRW<PlayerMovementData>, RefRW<LocalTransform>>().WithAll<Simulate>()) {

            float3 moveDir = new float3(input.ValueRO.move.x, 0f, input.ValueRO.move.y);

            transform.ValueRW.Position += moveDir * move.ValueRO.WalkSpeed * dt;

            // gravity
            move.ValueRW.Velocity.y += -25f * dt;
            transform.ValueRW.Position += move.ValueRW.Velocity * dt;

            float groundY = move.ValueRO.GroundY;

            if (transform.ValueRW.Position.y <= groundY)
            {
                transform.ValueRW.Position.y = groundY;
                move.ValueRW.Velocity.y = 0f;
                move.ValueRW.Grounded = true;
            }
            else
            {
                move.ValueRW.Grounded = false;
            }

            // jump
            if (move.ValueRO.Grounded && input.ValueRO.jump)
            {
                move.ValueRW.Velocity.y = move.ValueRO.JumpForce;
            }
            UnityEngine.Debug.Log(input.ValueRO.move);
        }
    }
}