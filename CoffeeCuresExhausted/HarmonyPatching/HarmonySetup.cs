using HarmonyLib;
using StardewModdingAPI;

namespace CoffeeCuresExhausted.HarmonyPatching
{
	internal class HarmonySetup
	{
		private Harmony _harmony;
		private IMonitor _monitor;

		/// <summary>
		/// Creates Harmony instance
		/// </summary>
		/// <param name="uniqueId">The mod's unique identifier configured in manifest.json</param>
		/// <param name="monitor">A reference to the IMonitor provided by the SMAPI in the mod entry</param>
		public HarmonySetup(string uniqueId, IMonitor monitor)
		{
			_harmony = new Harmony(uniqueId);
			_monitor = monitor;
		}
		
		/// <summary>
		/// Runs all the Harmony patching
		/// </summary>
		/// <returns>True if successful, False if any errors</returns>
		public bool Setup()
		{
			try
			{
				_harmony.Patch(
					original: AccessTools.Method(typeof(StardewValley.Farmer), nameof(StardewValley.Farmer.eatObject)),
					postfix: new HarmonyMethod(typeof(FarmerPatches), nameof(FarmerPatches.eatObject_Postfix))
				);

				_monitor.Log("Patched methods with Harmony successfully.", LogLevel.Debug);
				return true;
			}
			catch
			{
				_monitor.Log("Error patching methods with Harmony.", LogLevel.Error);
				return false;
			}
		}
	}
}
