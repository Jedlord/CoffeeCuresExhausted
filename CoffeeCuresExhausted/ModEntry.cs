using CoffeeCuresExhausted.HarmonyPatching;
using StardewModdingAPI;
using StardewValley;

namespace CoffeeCuresExhausted
{
	internal class ModEntry : Mod
	{
		public override void Entry(IModHelper helper)
		{
			Monitor.Log("Mod entry loaded", LogLevel.Debug);
			FarmerPatches.Initialize(Monitor);
			HarmonySetup harmony = new(this.ModManifest.UniqueID, Monitor);
			Monitor.Log($"Harmony setup result: {harmony.Setup()}", LogLevel.Debug);

			helper.ConsoleCommands.Add("coffeesetup", $"Sets you up to debug test the {this.ModManifest.Name} mod.", (name, args) =>
			{
				Game1.player.addItemToInventory(new StardewValley.Object("395", 2));
				Game1.player.addItemToInventory(new StardewValley.Object("253", 2));
				Game1.player.addItemToInventory(new StardewValley.Object("614", 2));
				Game1.player.addItemToInventory(new StardewValley.Object("167", 2));
				Game1.player.Stamina = 0f;
				Game1.chatBox.addInfoMessage("Added 2x Coffee, Triple Shot Espresso, Green Tea, and Joja Cola each and set Stamina to 0");
				Monitor.Log("Added 2x Coffee, Triple Shot Espresso, Green Tea, and Joja Cola each and set Stamina to 0", LogLevel.Debug);
			});
		}
	}
}
