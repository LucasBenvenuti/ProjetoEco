using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory : MonoBehaviour, IHasChanged {

    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;
    void Start ()
    {
        HasChanged();
	}

    public void HasChanged()
    {
        System.Text.StringBuilder Builder = new System.Text.StringBuilder();
        Builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;

            if (item)
            {
                Builder.Append(item.name);
                Builder.Append(" - ");
            }
        }

        inventoryText.text = Builder.ToString();
    }

}

namespace UnityEngine.EventSystems {

    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
