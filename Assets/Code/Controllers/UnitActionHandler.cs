using Assets.Code.Events;
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
            UnitActionMenu.OnActionClick += SetActionToCapture;
		}
		
		void OnDestroy()
        {
            UnitActionMenu.OnActionClick -= SetActionToCapture;
		}
		
		private void SetActionToCapture(EventID eventID)
		{
            if (eventID != EventID.Null)
                LastSelectedUnitAction = eventID;
		}
	}
}