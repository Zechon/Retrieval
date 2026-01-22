using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
partial struct TestNetcodeEntitiesServerSystem : ISystem
{
    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        foreach ((
            RefRO<SimpleRPC> simpleRPC,
            RefRO<ReceiveRpcCommandRequest> receiveRpcCommandRequest,
            Entity entity)
            in SystemAPI.Query<
                RefRO<SimpleRPC>,
                RefRO<ReceiveRpcCommandRequest>>().WithEntityAccess()) {

            Debug.Log("Received RPC: " + simpleRPC.ValueRO.value + " :: " + receiveRpcCommandRequest.ValueRO.SourceConnection);
            entityCommandBuffer.DestroyEntity(entity);
        }
        
        entityCommandBuffer.Playback(state.EntityManager);
    }
}
