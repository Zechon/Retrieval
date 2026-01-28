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
        }
    }
}

public struct NetcodePlayerInput : IInputComponentData
{
    public float2 move;
    public InputEvent jump;
}

