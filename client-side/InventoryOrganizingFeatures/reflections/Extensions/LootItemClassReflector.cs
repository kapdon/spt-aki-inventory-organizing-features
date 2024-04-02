using System;

namespace InventoryOrganizingFeatures.Reflections.Extensions
{
    internal static class LootItemClassReflector
    {
        public static Type ReflectedType = typeof(LootItemClass);

        public static Grid[] RGrids(this LootItemClass item)
        {
            var grids = item.GetFieldValue<object[]>("Grids");
            var reflectedGrids = new Grid[grids.Length];
            for(int i = 0; i < grids.Length; i++) {
                reflectedGrids[i] = new Grid(grids[i]);
            }
            return reflectedGrids;
        }
    }
}
