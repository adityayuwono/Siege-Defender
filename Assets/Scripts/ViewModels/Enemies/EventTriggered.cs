using System;
using Scripts.Helpers;
using Scripts.Models.Actions;
using Scripts.ViewModels.Weapons;

namespace Scripts.ViewModels.Enemies
{
	public class EventTriggered : Triggered
	{
		private readonly EventTriggeredModel _model;

		private Object _parentObject;

		private bool _isInvoked;

		public EventTriggered(EventTriggeredModel model, Base parent) : base(model, parent)
		{
			_model = model;

			if (_model.Event == Event.None)
			{
				throw new EngineException(this, "EventTrigger doesn't have an event specified");
			}
		}

		protected override void OnActivate()
		{
			_parentObject = GetParent<Object>();
			if (_parentObject == null)
			{
				throw new EngineException(this, "Failed to find Parent Object");
			}

			switch (_model.Event)
			{
				case Event.Interrupt:
				{
					var boss = _parentObject as Boss;
					if (boss != null)
					{
						boss.OnInterrupt += InvokeEvent;
					}
				}
					break;
				case Event.Click:
				{
					var buttonGUI = _parentObject as ButtonGUI;
					if (buttonGUI != null)
					{
						buttonGUI.OnClick += InvokeEvent;
					}
				}
					break;
				case Event.Break:
				{
					var limb = _parentObject as Limb;
					if (limb != null)
					{
						limb.OnBreak += InvokeEvent;
					}
				}
					break;
				case Event.Spawn:
				{
					var enemy = _parentObject as Enemy;
					if (enemy != null)
					{
						enemy.Spawn += InvokeEvent;
					}
				}
					break;
				case Event.Attack:
				{
					var enemy = _parentObject as Enemy;
					if (enemy != null)
					{
						enemy.Attack += InvokeEvent;
					}
				}
					break;
				case Event.Walk:
				{
					var enemy = _parentObject as Enemy;
					if (enemy != null)
					{
						enemy.Walk += InvokeEvent;
					}
				}
					break;
				case Event.Death:
				{
					var livingObject = _parentObject as LivingObject;
					if (livingObject != null)
					{
						livingObject.Death += InvokeEvent;
					}
				}
					break;
				case Event.DeathEnd:
				{
					var livingObject = _parentObject as LivingObject;
					if (livingObject != null)
					{
						livingObject.DeathEnd += InvokeEvent;
					}
				}
					break;
				case Event.Hit:
				{
					var projectile = _parentObject as Projectile;
					if (projectile != null)
					{
						projectile.Hit += InvokeEvent;
					}
					var livingObject = _parentObject as LivingObject;
					if (livingObject != null)
					{
						livingObject.Hit += InvokeEvent;
					}
				}
					break;
				case Event.GameOver:
				{
					var player = _parentObject as Player;
					if (player != null)
					{
						player.OnGameOver += InvokeEvent;
					}
				}
					break;
				case Event.None:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void InvokeEvent()
		{
			if (!_isInvoked)
			{
				_isInvoked = true;
				base.OnActivate();
			}
		}

		protected override void OnDeactivate()
		{
			switch (_model.Event)
			{
				case Event.Interrupt:
				{
					var boss = _parentObject as Boss;
					if (boss != null)
					{
						boss.OnInterrupt -= InvokeEvent;
					}
				}
					break;
				case Event.Click:
				{
					var buttonGUI = _parentObject as ButtonGUI;
					if (buttonGUI != null)
					{
						buttonGUI.OnClick -= InvokeEvent;
					}
				}
					break;
				case Event.Break:
				{
					var limb = _parentObject as Limb;
					if (limb != null)
					{
						limb.OnBreak -= InvokeEvent;
					}
				}
					break;
				case Event.Spawn:
				{
					var enemy = _parentObject as Enemy;
					if (enemy != null)
					{
						enemy.Spawn -= InvokeEvent;
					}
				}
					break;
				case Event.Attack:
				{
					var enemy = _parentObject as Enemy;
					if (enemy != null)
					{
						enemy.Attack -= InvokeEvent;
					}
				}
					break;
				case Event.Walk:
				{
					var enemy = _parentObject as Enemy;
					if (enemy != null)
					{
						enemy.Walk -= InvokeEvent;
					}
				}
					break;
				case Event.Death:
				{
					var livingObject = _parentObject as LivingObject;
					if (livingObject != null)
					{
						livingObject.Death -= InvokeEvent;
					}
				}
					break;
				case Event.DeathEnd:
				{
					var livingObject = _parentObject as LivingObject;
					if (livingObject != null)
					{
						livingObject.DeathEnd -= InvokeEvent;
					}
				}
					break;
				case Event.Hit:
				{
					var projectile = _parentObject as Projectile;
					if (projectile != null)
					{
						projectile.Hit -= InvokeEvent;
					}
					var livingObject = _parentObject as LivingObject;
					if (livingObject != null)
					{
						livingObject.Hit -= InvokeEvent;
					}
				}
					break;
				case Event.GameOver:
				{
					var player = _parentObject as Player;
					if (player != null)
					{
						player.OnGameOver -= InvokeEvent;
					}
				}
					break;
				case Event.None:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (_isInvoked)
			{
				_isInvoked = false;
				base.OnDeactivate();
			}
		}
	}
}