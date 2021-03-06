﻿//  Project : 2D Roguelike Actors
// Contacts : @Alexeee#8796 - https://discord.gg/zAJn9SX

using Pixeye.Actors;

namespace Roguelike
{
	public class ComponentTurnEnd
	{
	}

	#region HELPERS

	static partial class Component
	{
		public const string TurnEnd = "Roguelike.ComponentTurnEnd";

		public static ref ComponentTurnEnd ComponentTurnEnd(in this ent entity) =>
			ref Storage<ComponentTurnEnd>.components[entity.id];
	}

	sealed class StorageComponentTurnEnd : Storage<ComponentTurnEnd>
	{
		public override ComponentTurnEnd Create() => new ComponentTurnEnd();

		// Use for cleaning components that were removed at the current frame.
		public override void Dispose(indexes disposed)
		{
			foreach (var id in disposed)
			{
				ref var component = ref components[id];
			}
		}
	}

	#endregion
}