//  Project : 2D Roguelike Actors
// Contacts : @Alexeee#8796 - https://discord.gg/zAJn9SX

using Pixeye.Actors;
using UnityEngine;

namespace Roguelike
{
	sealed class ProcessorEnemy : Processor, ITick
	{
		Group<ComponentObject, ComponentEnemy, ComponentTurnEnd> source;
		Group<ComponentObject, ComponentPlayer> groupPlayers;

		Vector2[] direction = new[] {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

		public void Tick(float delta)
		{
			foreach (ent entity in source)
			{
				var cObject = entity.ComponentObject();
				var cEnemy = entity.ComponentEnemy();

				var dir    = direction.Random();
				var target = dir + new Vector2(cObject.position.x, cObject.position.y);

				var hasSolidColliderInPoint = Phys.HasSolidColliderInPoint(target, 1 << 10, out ent withEntity);

				entity.Remove<ComponentTurnEnd>();

				if (!hasSolidColliderInPoint && !withEntity.exist)
				{
					Game.MoveTo(entity, target);
				}
				else if (withEntity.Has<ComponentPlayer>())
				{
					Game.Draw.SetAnimation(withEntity, Anim.Hit, Anim.Once);
					Game.ChangeHealth(withEntity, entity.DataEnemy().damage);
				}
			}
		}

		public override void HandleEcsEvents()
		{
			foreach (ent entity in source.removed)
			{
				if (source.length == 0)
					groupPlayers[0].Get<ComponentTurnEnd>();
				break;
			}
		}
	}
}