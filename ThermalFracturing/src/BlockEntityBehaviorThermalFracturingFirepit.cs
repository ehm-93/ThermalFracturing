using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Ehm93.VintageStory.ThermalFracturing;

class BlockEntityBehaviorThermalFracturingFirepit : BlockEntityBehavior
{
    public BlockEntityBehaviorThermalFracturingFirepit(BlockEntityFirepit blockEntity) : base(blockEntity)
    {
    }

    public override void Initialize(ICoreAPI api, JsonObject properties)
    {
        base.Initialize(api, properties);
        Blockentity.RegisterGameTickListener(Tick, 1000);
    }

    private void Tick(float _)
    {
        DoTick(Pos.NorthCopy());
        DoTick(Pos.EastCopy());
        DoTick(Pos.SouthCopy());
        DoTick(Pos.WestCopy());
        DoTick(Pos.UpCopy());
        DoTick(Pos.DownCopy());
    }

    private void DoTick(BlockPos neighborPos)
    {
        var neighbor = Api.World.BlockAccessor.GetBlock(neighborPos);
        if (!neighbor.HasBehavior<BlockBehaviorThermalFracturingTarget>()) return;
        
        var target = neighbor.GetBehavior<BlockBehaviorThermalFracturingTarget>();
        var temperature = (Blockentity as BlockEntityFirepit).furnaceTemperature;
        target.CheckTransition(Api.World, neighborPos, temperature);
    }
}
