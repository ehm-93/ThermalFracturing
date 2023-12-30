using System;
using Ehm93.VintageStory.ThermalFracturing.Util;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Ehm93.VintageStory.ThermalFracturing 
{
    /// <summary>
    /// The BlockBehaviorThermalFracturingTarget encapsulates the behavior logic which allows a particular block to
    /// probabilistically transition to another state via thermal fracturing.
    /// </summary>
    class BlockBehaviorThermalFracturingTarget : BlockBehavior
    {
        private float fractureTemperature;
        private float fractureProbability;
        private IFractureMode fractureMode;

        public BlockBehaviorThermalFracturingTarget(Block block) : base(block)
        {
        }

        /// <summary>
        /// The CheckTransition method allows external code to trigger the probabilistic check for block destruction or replacement.
        /// </summary>
        /// <param name="world">The Vintage Story world API</param>
        /// <param name="blockPos">The position of the block instance to check</param>
        /// <param name="currentTemperature">The temperature of the triggering object</param>
        public void CheckTransition(IWorldAccessor world, BlockPos blockPos, float currentTemperature) {
            if (currentTemperature < fractureTemperature) return;

            var transitionTest = world.Rand.NextDouble();
            if (fractureProbability < transitionTest) return;

            fractureMode.Fracture(world, blockPos);
        }

        public override void Initialize(JsonObject properties)
        {
            base.Initialize(properties);

            fractureTemperature = properties["fractureTemperature"].AsFloat();
            if (fractureTemperature == 0f) throw Exceptions.MissingProperty("fractureTemperature");

            fractureProbability = properties["fractureProbability"].AsFloat();
            if (fractureProbability == 0f) throw Exceptions.MissingProperty("fractureProbability");

            var fractureModeStr = properties["fractureMode"].AsString();
            if (fractureModeStr == null) throw Exceptions.MissingProperty("fractureMode");

            switch (fractureModeStr) {
                case "destroy":
                    fractureMode = DestroyFracture.Parse(properties);
                    break;
                case "replace":
                    fractureMode = ReplaceFracture.Parse(properties);
                    break;
                default:
                    throw new InvalidOperationException($"Value '{fractureMode}' is invalid for fractureMode property. Supported values are 'destroy' or 'replace'");
            }
        }

        
        private interface IFractureMode
        {
            public void Fracture(IWorldAccessor world, BlockPos blockPos);
        }

        private class DestroyFracture : IFractureMode
        {
            public static DestroyFracture Parse(JsonObject properties)
            {
                return new DestroyFracture();
            }

            public void Fracture(IWorldAccessor world, BlockPos blockPos)
            {
                world.BlockAccessor.BreakBlock(blockPos, null);
            }
        }

        private class ReplaceFracture : IFractureMode
        {
            
            private readonly AssetLocation targetBlockCode;

            public ReplaceFracture(AssetLocation targetBlockCode)
            {
                this.targetBlockCode = targetBlockCode;
            }

            public static ReplaceFracture Parse(JsonObject properties)
            {
                var transitionBlockCodeStr = properties["targetBlockCode"].AsString();
                if (transitionBlockCodeStr == null) throw Exceptions.MissingProperty("targetBlockCode");
                var targetBlockCode = new AssetLocation(transitionBlockCodeStr);
                return new ReplaceFracture(targetBlockCode);
            }

            public void Fracture(IWorldAccessor world, BlockPos blockPos)
            {
                var transitionBlock = world.GetBlock(targetBlockCode);
                if (transitionBlock == null)
                {
                    world.Logger.Error($"The block code {targetBlockCode} does not exist but was used to configure a ThermalFracturingTarget behavior.");
                    return;
                };

                world.BlockAccessor.SetBlock(transitionBlock.Id, blockPos);
            }
        }
    }

}