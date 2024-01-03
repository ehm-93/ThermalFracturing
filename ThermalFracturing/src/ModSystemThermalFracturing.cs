using Vintagestory.API.Common;

namespace Ehm93.VintageStory.ThermalFracturing;

public class ThermalFracturingModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);

        Register(api);

        api.Logger.Debug("Thermal Fracturing is registered!");
    }

    private void Register(ICoreAPI api) {
        api.RegisterBlockEntityBehaviorClass("ThermalFracturingFirepit", typeof(BlockEntityBehaviorThermalFracturingFirepit));
        
        api.RegisterBlockBehaviorClass("ThermalFracturingTarget", typeof(BlockBehaviorThermalFracturingTarget));
    }
}
