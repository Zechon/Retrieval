using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

[UpdateInGroup(typeof(GhostInputSystemGroup))]
partial struct NetcodePlayerInputSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkStreamInGame>();
        state.RequireForUpdate<NetcodePlayerInput>();
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.HasSingleton<PlayerInputSingleton>())
            return;

        PlayerInputSingleton input =
           SystemAPI.GetSingleton<PlayerInputSingleton>();

        UnityEngine.Debug.Log($"Has input singleton: {SystemAPI.HasSingleton<PlayerInputSingleton>()}");

        foreach (RefRW<NetcodePlayerInput> netcodePlayerInput 
            in SystemAPI.Query<RefRW<NetcodePlayerInput>>().WithAll<GhostOwnerIsLocal>()) {

            netcodePlayerInput.ValueRW.move = input.move;
            netcodePlayerInput.ValueRW.jump = input.jump;
        }
    }
}
