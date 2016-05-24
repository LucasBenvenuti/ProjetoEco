using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int tipo;

    public static GameObject itemBeingDragged;
    public Vector3 StartPosition;
    public static Transform StartParent;
    public Transform TemporaryParent;
    Inventory inventario;

    //Diz quem seria o temporary parent e pega determinado objeto para se tornar a variavel inventario
    void Start()
    {        
        TemporaryParent = transform.parent.parent;
        inventario = FindObjectOfType<Inventory>();

    }
    //Ao ser clicado antes de arrastar
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
        //Diz que a variavel itemBeingDragged recebe o objeto que esta sendo segurado pelo mouse
        itemBeingDragged = gameObject;

        //Diz a posicao inicial e o pai inicial do objeto que sera segurado para eventuais Swaps e retornos do objeto caso seja colocado em local inadequado
        StartPosition = transform.position;
        StartParent = transform.parent;

        //Desabilita a interacao do eventuais eventos enquanto esta sendo segurado
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    //Quando o objeto esta sendo segurado
    public void OnDrag(PointerEventData eventData)
    {
        //Muda sua cor para Branco (padrão para neutro, no caso, nem correto nem falso)
        gameObject.GetComponent<Image>().color = Color.white;
        print("Drag");

        //O pai do objeto se torna o TemporaryParent
        transform.SetParent(TemporaryParent);
        //A posicao do objeto sera a posicao X e Y do mouse enquanto estiver sendo arrastado
        transform.position = Input.mousePosition;
        
    }
    //Ao soltar o objeto
    public void OnEndDrag(PointerEventData eventData)
    {
        
        print("OnEndDrag");
        //O item que esta sendo segurado se torna nulo, ou seja, nao ha mais nada sendo segurado, POIS FOI SOLTO ;D
        itemBeingDragged = null;
        //As interacoes do objeto sao reativas
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        //Recoloca o objeto na antiga posição e parent antes de ser arrastado caso as condicoes sejam atendidas
        //if (transform.parent != StartParent && transform.parent == TemporaryParent.transform)
        if (transform.parent == TemporaryParent.transform)
        {
            transform.parent = StartParent;
            //transform.position = StartPosition;

        }
        //variavel para dizer se o objeto foi solto ou nao para uma outra funcao presente no codigo do INVENTORY
        inventario.BugNoHasChanged = this;
        
    }
}
