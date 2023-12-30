using System;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Ehm93.VintageStory.ThermalFracturing 
{
    class BlockBehaviorThermalFracturingTarget : BlockBehavior
    {
        private float transitionTemperature;
        private AssetLocation targetBlockCode;
        private float transitionProbability;

        public BlockBehaviorThermalFracturingTarget(Block block) : base(block)
        {
        }

        public void CheckTransition(IWorldAccessor world, BlockPos blockPos, float currentTemperature) {
            if (currentTemperature < transitionTemperature) return;

            var transitionTest = world.Rand.NextDouble();
            if (transitionProbability < transitionTest) return;

            var transitionBlock = world.GetBlock(targetBlockCode);
            if (transitionBlock == null)
            {
                world.Logger.Error($"The block code {targetBlockCode} does not exist but was used in configuring the ThermalFracturingTarget behavior for block {block.Code}.");
                return;
            };

            world.BlockAccessor.SetBlock(transitionBlock.Id, blockPos);
        }

        public override void Initialize(JsonObject properties)
        {
            base.Initialize(properties);

            transitionTemperature = properties["transitionTemperature"].AsFloat();
            if (transitionTemperature == 0f) throw MissingPropertyException("transitionTemperature");

            var transitionBlockCodeStr = properties["targetBlockCode"].AsString();
            if (transitionBlockCodeStr == null) throw MissingPropertyException("targetBlockCode");
            targetBlockCode = new AssetLocation(transitionBlockCodeStr);

            transitionProbability = properties["transitionProbability"].AsFloat();
            if (transitionProbability == 0f) throw MissingPropertyException("transitionProbability");
        }

        private Exception MissingPropertyException(string name)
        {
            return new InvalidOperationException($"The ThermalFracturingTarget behavior property {name} on block {block.Code} was not set but is required.");
        }
    }
}