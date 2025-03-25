using System;
using System.Collections.Generic;
using CoreLib;
using CoreLib.Equipment;
using CoreLib.Submodules.ModEntity;
using CoreLib.Util.Extensions;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using PlayerEquipment;

namespace SecureAttachment
{
    public class WrenchEquipmentSlot : PlaceObjectSlot, IModEquipmentSlot
    { 
        private int size = 0;

        public const string WrenchObjectType = "SecureAttachment:Wrench";

        protected override EquipmentSlotType slotType =>
            EquipmentModule.GetEquipmentSlotType<WrenchEquipmentSlot>();

        public ObjectType GetSlotObjectType()
        {
            return EntityModule.GetObjectType(WrenchObjectType);
        }

        private ContainedObjectsBuffer AsBuffer(ObjectDataCD objectDataCd)
        {
            return new ContainedObjectsBuffer()
            {
                objectData = objectDataCd
            };
        }

        public void UpdateSlotVisuals(PlayerController controller)
        {
            ObjectDataCD objectDataCd = controller.GetHeldObject();
            ObjectInfo objectInfo = PugDatabase.GetObjectInfo(objectDataCd.objectID, objectDataCd.variation);

            controller.carryablePlaceItemSprite.gameObject.SetActive(true);
            
            Sprite iconOverride = Manager.ui.itemOverridesTable.GetIconOverride(controller.visuallyEquippedContainedObject.objectData, true);
            controller.carryablePlaceItemSprite.sprite = iconOverride != null ? iconOverride : objectInfo?.smallIcon;

            controller.carryablePlaceItemColorReplacer.UpdateColorReplacerFromObjectData(controller.visuallyEquippedContainedObject);
        }
    }
}