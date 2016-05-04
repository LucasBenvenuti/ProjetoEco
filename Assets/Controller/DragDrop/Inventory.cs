using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory : MonoBehaviour, IHasChanged {

    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;

    public CenaController controladorCena;

    public Transform ConfirmarPainel;
    public Transform OpcoesPainel;
    public Transform RespostasPainel;

    string[] respostasCorretas;

    string[] respostasPlayer;

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
        GameObject botaoConfirmar = Instantiate(Resources.Load("botaoConfirmar")) as GameObject;
        botaoConfirmar.transform.SetParent(ConfirmarPainel);
        botaoConfirmar.GetComponent<Button>().onClick.AddListener(() => { print(botaoConfirmar.name); CompararRespostas(); });

        yield return controladorCena.cenas;

        respostasCorretas = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.resposta;
        int i = 0;
        foreach (string opcao in controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.opcoes)
        {
            GameObject slot = Instantiate(Resources.Load("SlotOpcao")) as GameObject;

            slot.transform.SetParent(OpcoesPainel);
            slot.GetComponentInChildren<Drag>().name = opcao;
            slot.GetComponentInChildren<Drag>().GetComponent<Image>().sprite = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.imagensOpcoes[i];
            i++;
        
        }

     
        foreach (string resposta in respostasCorretas)
        {
            GameObject slot = Instantiate(Resources.Load("Slot")) as GameObject;
     
            slot.transform.SetParent(RespostasPainel);
           
        }
    }

    void CompararRespostas ()
    {
        respostasPlayer = new string[slots.GetComponentsInChildren<Drag>().Length];

        int f = 0;

        foreach (Drag resposta in slots.GetComponentsInChildren<Drag>())
        {
            respostasPlayer[f] = resposta.name;
            f++;
        }

        if (respostasPlayer.Length == respostasCorretas.Length)
        {
            for (int i = 0; i < respostasCorretas.Length; i++)
            {
                if (respostasPlayer[i] == respostasCorretas[i])
                {
                    print("Correto - " + respostasPlayer[i]);
                }
                else
                {
                    print("Errada - " + respostasPlayer[i]);
                    RevelarErro(i);
                }
            }
        }
        else
        {
            print("Resposta Incompleta!");
        }
    }

    void RevelarErro (int slotErrado)
    {
        slots.GetComponentsInChildren<Drag>()[slotErrado].GetComponent<Image>().color = Color.red;
    }
    
    public void HasChanged()
    {
        foreach (Drag opcoes in FindObjectsOfType<Drag>()) {
            opcoes.GetComponent<Image>().color = Color.white;
        }
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
