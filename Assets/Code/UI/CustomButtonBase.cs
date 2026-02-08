using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI
{


    public abstract class CustomButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            // Set Cursor
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            // Play audio
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            // Set Cursor
        }
    }
}