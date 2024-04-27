using EFT.InventoryLogic;
using ContainerFilter = GClass2521;
using StashGridClass = StashGrid;

namespace InventoryOrganizingFeatures
{
    internal static class Extensions
    {
        public static bool CanAccept(this StashGridClass grid, Item item)
        {
            // find the class using [CheckItemExcludedFilter, CheckItemFilter, CanAccept]
            return ContainerFilter.CanAccept(grid, item);
        }
    }
}
