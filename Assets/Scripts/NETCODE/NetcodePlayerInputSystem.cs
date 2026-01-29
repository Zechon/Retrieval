using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using UnityEngine.InputSystem;
using UnityEngine;

[UpdateInGroup(typeof(GhostInputSystemGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
[BurstCompile]
public partial class NetcodePlayerInputSystem : SystemBase
{
    Player_Inputs input;
    Player_Inputs.PlayerActions actions;

    protected override void OnCreate()
    {
        input = new Player_Inputs();
        input.Enable();
        actions = input.Player;
        RequireForUpdate<NetworkStreamInGame>();
        RequireForUpdate<NetcodePlayerInput>();
    }

    protected override void OnUpdate()
    {
        InputSystem.Update();
        Vector2 rawMovement = actions.Move.ReadValue<Vector2>();
        float2 movement = new float2 (rawMovement.x, rawMovement.y);

        bool jump = actions.Jump.WasPerformedThisFrame();
        bool randomize = actions.Interact.WasPerformedThisFrame();

        foreach ((var playerInput, RefRW<MyValue> myValue) in SystemAPI.Query<RefRW<NetcodePlayerInput>, RefRW<MyValue>>().WithAll<GhostOwnerIsLocal>()) {

            playerInput.ValueRW.move = movement;
            if (jump) playerInput.ValueRW.jump.Set();
            if (randomize)
                {
                    myValue.ValueRW.value = UnityEngine.Random.Range(1, 999);
                }


        }
    }
}
