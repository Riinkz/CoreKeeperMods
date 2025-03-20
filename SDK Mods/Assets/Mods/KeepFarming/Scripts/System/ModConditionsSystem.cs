using KeepFarming.Components;
using Unity.Entities;
using Unity.NetCode;

namespace KeepFarming
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation | WorldSystemFilterFlags.ClientSimulation)]
    [UpdateInGroup(typeof(BeforePredictedFixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SummarizeConditionsSystem))]
    public partial class ModConditionsSystem : PugSimulationSystemBase
    {
        protected override void OnCreate()
        {
            NeedDatabase();
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            var databaseLocal = database;

            Entities.ForEach((
                    ref DynamicBuffer<SummarizedConditionsBuffer> sumConditionsBuffer,
                    in DynamicBuffer<ContainedObjectsBuffer> container,
                    in EquippedObjectCD equippedObjectCd) =>
                {
                    var equippedSlotIndex = equippedObjectCd.equippedSlotIndex;
                    var objectDataCD = equippedObjectCd.containedObject.objectData;
                    
                    Entity itemEntity = PugDatabase.GetPrimaryPrefabEntity(objectDataCD.objectID, databaseLocal, objectDataCD.variation);
                    if (itemEntity == Entity.Null) return;

                    if (equippedSlotIndex >= 0 &&
                        equippedSlotIndex < container.Length &&
                        SystemAPI.HasComponent<GoldenSeedCD>(itemEntity))
                    {
                        int id = (int)ConditionID.ChanceToGainRarePlant;

                        sumConditionsBuffer[id] = new SummarizedConditionsBuffer
                        {
                            value = sumConditionsBuffer[id].value + 100
                        };
                    }
                })
                .WithNone<EntityDestroyedCD>()
                .Schedule();

            base.OnUpdate();
        }
    }
}