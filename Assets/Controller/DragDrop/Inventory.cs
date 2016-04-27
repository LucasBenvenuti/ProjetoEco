using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory : MonoBehaviour, IHasChanged {

    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;

    public CenaController controladorCena;

    public Transform OpcoesPainel;
    public Transform RespostasPainel;

    void Start ()
    {
        controladorCena = FindObjectOfType<CenaController>();
        HasChanged();
        StartCoroutine(InstanciaSlots());
	}

    //Thread  para inciar ir para a função abaixo com as cenas referenciadas
    // problema que estava dando é que essa funcao estava indo antes da funcao start dentro do game controller
    public IEnumerator InstanciaSlots()
    {
        BaseDeDados baseDeDados = new BaseDeDados();

        //controladorCena.contTextoAtual = 1;
        // print(controladorCena.contTextoAtual);
        yield return controladorCena.cenas;
        int i = 0;
        foreach (string opcao in controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.opcoes)
        {
            GameObject slot = Instantiate(Resources.Load("SlotOpcao")) as GameObject;

            slot.transform.SetParent(OpcoesPainel);
            slot.GetComponentInChildren<Drag>().name = opcao;
            slot.GetComponentInChildren<Drag>().GetComponent<Image>().sprite = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.imagensOpcoes[i];
            i++;

        }

        foreach (string resposta in controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.resposta)
        {
            GameObject slot = Instantiate(Resources.Load("Slot")) as GameObject;

            slot.transform.SetParent(RespostasPainel);
        }
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
