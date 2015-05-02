using Assets.Code.Events.UnitActions;
using Assets.Code.Generic;
using Assets.Code.UI.Interactable;

namespace Assets.Code.Controllers
{
	public class UnitActionHandler : Singleton<UnitActionHandler>
	{
		public EventID LastSelectedUnitAction { get; private set; }
		
		void OnEnable()
		{
			UnitActionMenu.OnButtonClick += SetLastSelectedUnitAction;
		}
		
		void OnDestroy()
		{
			UnitActionMenu.OnButtonClick -= SetLastSelectedUnitAction;
		}
		
		private void SetActionToCapture()
		{
			LastSelectedUnitAction = Unit;
		}
	}
}