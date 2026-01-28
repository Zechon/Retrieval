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

        foreach (var playerInput in SystemAPI.Query<RefRW<NetcodePlayerInput>>().WithAll<GhostOwnerIsLocal>()) {

            playerInput.ValueRW.move = movement;
            if (jump) playerInput.ValueRW.jump.Set();
        }
    }
}
