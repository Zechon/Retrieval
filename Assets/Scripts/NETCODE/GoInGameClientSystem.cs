using JetBrains.Annotations;
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
partial struct GoInGameClientSystem : ISystem
{
    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        foreach ((
            RefRO<NetworkId> NetworkId,
            Entity entity)
            in SystemAPI.Query<
                RefRO<NetworkId>>().WithNone<NetworkStreamInGame>().WithEntityAccess()) {

            entityCommandBuffer.AddComponent<NetworkStreamInGame>(entity);
            Debug.Log("Setting Client as InGame");

            Entity rpcEntity = entityCommandBuffer.CreateEntity();
        }
        entityCommandBuffer.Playback(state.EntityManager);


        //LEFT OFF AT 24:16 in "Getting Started with Netcode for Entities! (DOTS Multiplayer Tutorial Unity 6)"
    }
}

public struct GoInGameRequestRpc : IRpcCommand
{

}
