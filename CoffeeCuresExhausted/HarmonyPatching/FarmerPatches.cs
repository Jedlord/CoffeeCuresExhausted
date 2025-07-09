using StardewModdingAPI;
using StardewValley;

namespace CoffeeCuresExhausted.HarmonyPatching
{
	internal class FarmerPatches
	{
		private static IMonitor? Monitor;

		/// <summary>
		/// Initializes anything these patches require
		/// </summary>
		/// <param name="monitor">A reference to the IMonitor provided by the SMAPI in the mod entry</param>
		internal static void Initialize(IMonitor monitor)
		{
			Monitor = monitor;
		}

		internal static void eatObject_Postfix(Farmer __instance, StardewValley.Object o)
		{
			try
			{
				if (__instance is not null && o.GetFoodOrDrinkBuffs().Count() > 0)
				{
					bool hasSpeedBuffAndDrink = false;
					//string x = "";
					foreach(Buff b in o.GetFoodOrDrinkBuffs())
					{
						//x += $"{b.effects.Speed.Value} | {(b.effects.Speed.Value > 0 ? "true" : "false")} | {b.id} | {b.source}\n";
						if (b.effects.Speed.Value > 0 && b.id == "drink") hasSpeedBuffAndDrink = true;
					}
					//Monitor?.Log(x, LogLevel.Debug);
					if(hasSpeedBuffAndDrink)
					{
						__instance.exhausted.Set(false);
						Monitor?.Log($"Removed exhausted effect from player {__instance.Name}.", LogLevel.Debug);
					}
				}
			}
			catch
			{
				Monitor?.Log($"Error in eatObject Postfix", LogLevel.Error);
			}
		}
	}
}
