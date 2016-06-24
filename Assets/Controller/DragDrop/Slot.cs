using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour, IDropHandler {

    //Ele afirma que o item será sempre o filho (Child -> 0) do pai (slot). Sempre possuirá item se algo estiver dentro do pai
    public GameObject item {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }
    //Ao soltar algum objeto dentro do pai (slot), a acao descrita dentro dele ocorre
    public void OnDrop(PointerEventData eventData)
    {
        //Se o slot for do mesmo tipo do item sendo segurado
        if (gameObject.name == Drag.itemBeingDragged.GetComponent<Drag>().tipo.ToString()) {

            //Se já tiver algum filho centro do pai, ocorrera determinada acao
            if (item)
            {
                if (item.GetComponent<Image>().color == Color.green)
                {
                    Drag.itemBeingDragged.transform.SetParent(Drag.StartParent);
                }
                else
                {

                //Mecanica do Swap
                item.GetComponent<Image>().color = Color.white;

                //Diz que a nova posicao do filho antigo do slot sera a posicao do objeto segurando antes do mesmo estar sendo segurado
                item.transform.position = Drag.itemBeingDragged.GetComponent<Drag>().StartPosition;

                //Diz que o novo pai do filho antigo do slot sera o pai do objeto que esta sendo segurado antes de o mesmo ser segurado
                item.transform.parent = Drag.StartParent;
                    
                //print(Drag.itemBeingDragged.GetComponent<Drag>().StartPosition.ToString());
          
                //Caso queira fazer a mecanica da galinha, somente tirar a linha de cima

                //Diz que o novo do objeto que esta sendo segurado se torna o slot em que ele for jogado
                Drag.itemBeingDragged.transform.SetParent(transform);
                   
                //Diz que a funcao HasChanged ocorre
                ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
                }
            }
            //Se nao possuir item, no caso algum filho, dentro do slot
            else
            {
                //Diz que o novo pai do novo pai do objeto que esta sendo segurado se torna o slot em que ele for jogado
                Drag.itemBeingDragged.transform.SetParent(transform);
                //Diz que a funcao HasChanged ocorre
                ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
            }
        }
        //Se nao for do mesmo tipo
        else
        {
            //O objeto retorna para a posicao antes de ser segurado
            Drag.itemBeingDragged.transform.SetParent(Drag.StartParent);
        }
    }
}
