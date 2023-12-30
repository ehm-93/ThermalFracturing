using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Ehm93.VintageStory.ThermalFracturing 
{
    class BlockEntityBehaviorThermalFracturingFirepit : BlockEntityBehavior
    {
        private long tickRegistration = 0L;

        public BlockEntityBehaviorThermalFracturingFirepit(BlockEntityFirepit blockEntity) : base(blockEntity)
        {
        }

        public override void Initialize(ICoreAPI api, JsonObject properties)
        {
            base.Initialize(api, properties);

            if (tickRegistration != 0L) {
                Api.World.UnregisterGameTickListener(tickRegistration);
            }
            var ticker = new Ticker(this, Api.World, Pos);
            tickRegistration = Api.World.RegisterGameTickListener(ticker.Tick, 1000);
        }

        public override void OnBlockRemoved()
        {
            base.OnBlockRemoved();
            
            Api.World.UnregisterGameTickListener(tickRegistration);
        }

        private class Ticker
        {
            private readonly BlockEntityBehaviorThermalFracturingFirepit parent;
            private readonly IWorldAccessor world;
            private readonly BlockPos blockPos;

            public Ticker(BlockEntityBehaviorThermalFracturingFirepit parent, IWorldAccessor world, BlockPos blockPos)
            {
                this.parent = parent;
                this.world = world;
                this.blockPos = blockPos.Copy();
            }

            public void Tick(float _)
            {
                DoTick(blockPos.NorthCopy());
                DoTick(blockPos.EastCopy());
                DoTick(blockPos.SouthCopy());
                DoTick(blockPos.WestCopy());
                DoTick(blockPos.UpCopy());
                DoTick(blockPos.DownCopy());
            }

            public void DoTick(BlockPos neighborPos)
            {
                var neighbor = world.BlockAccessor.GetBlock(neighborPos);
                if (!neighbor.HasBehavior<BlockBehaviorThermalFracturingTarget>()) return;
                
                var target = neighbor.GetBehavior<BlockBehaviorThermalFracturingTarget>();
                var temperature = (parent.Blockentity as BlockEntityFirepit).furnaceTemperature;
                target.CheckTransition(world, neighborPos, temperature);
            }
        }
    }
}