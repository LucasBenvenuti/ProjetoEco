using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory : MonoBehaviour, IHasChanged {

    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;

    public CenaController controladorCena;

    public Transform Painel;
    public Transform RespostasPainel;

    //posicao Random das pecas de opcoes
    public float RandomPositionX_Min;
    public float RandomPositionX_Max;
    public float RandomPositionY_Min;
    public float RandomPositionY_Max;

    public Drag BugNoHasChanged;

    string[] respostasCorretas;

    string[] respostasPlayer;

    private IEnumerator esperarSoltar;

    //Inicia dizendo qual objeto sera referenciado pela variavel controladorCena, qual funcao sera referenciada pela variavel esperarSoltar e inicia uma rotina (a de instanciar os slots de respsota e de opcoes)
    void Start ()
    {
        controladorCena = FindObjectOfType<CenaController>();
        esperarSoltar = EsperaSoltar();
        StartCoroutine(InstanciaSlots());
	}

    //Thread  para inciar ir para a funcao abaixo com as cenas referenciadas
    // problema que estava dando e que essa funcao estava indo antes da funcao start dentro do game controller
    public IEnumerator InstanciaSlots()
    {
        //Espera retornar os valores da cena para as intancias... Casi nao for colocado esse yield a cena bugara e nao instancia nada pois antes de receber o valor das cenas tudo sera nulo
        yield return controladorCena.cenas;

        //nomeia uma variavel para reduzir o tamanho da linha
        respostasCorretas = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.resposta;
        int i = 0;

        //Instancia os slots das opcoes com base em quantas opcoes existem dentro do codigo da BaseDeDados
        foreach (string opcao in controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.opcoes)
        {
            //nomeia uma variavel para que os slots sejam instanciados randomicamente dentro de um determinado limite
            Vector3 RandomPosition = new Vector3 (Random.Range(RandomPositionX_Min, RandomPositionX_Max),Random.Range(RandomPositionY_Min, RandomPositionY_Max), 0);
            //instancia os slots que estao nomeados como Opcao dentro da pasta Resources
            GameObject slot = Instantiate(Resources.Load("Opcao")) as GameObject;
            //nomeia esses slots
            slot.name = "100";
            //define um Pai para esses slotss
            slot.transform.SetParent(Painel);
            //define uma posicao para os slots instanciados
            slot.transform.position = RandomPosition;
            //nomeia o filho do slot com base no nome dado dentro do array de strings opcoes dentro do codigo da BaseDeDados
            slot.GetComponentInChildren<Drag>().name = opcao;
            //determina o tipo que o slot sera baseado no que foi determinado dentro do array de inteiros dentro do codigo da BaseDeDados
            slot.GetComponentInChildren<Drag>().tipo = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.tiposOpcoes[i];
            //determina qual imagem cada filho do slot possuira. Foi atribuida as sprites dentro do array de sprites dentro do codigo da BaseDeDados conforme o nome de cada filho do slot. Ex: Se o seu nome for "a", a sprite nomeada como "a" sera destinada a esse filho
            slot.GetComponentInChildren<Drag>().GetComponent<Image>().sprite = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.imagensOpcoes[i];
            i++;
        
        }

        i = 0;
        //Instancia os slots das respostas baseadas em quantas letras a palavra correta possuira
        foreach (string resposta in respostasCorretas)
        {
            //instancia os slots que estao nomeados como Slot dentro da pasta Resources
            GameObject slot = Instantiate(Resources.Load("Slot")) as GameObject;
            //nomeia os slots
            slot.name = "";
            //nomeia qual o tipo de objeto podera interagir com cada slot. Esse tipo esta dentro do codigo da BaseDeDados
            slot.name += controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].comparativa.tiposRespostas[i];

            //define um Pai para cada slot instanciado
            slot.transform.SetParent(RespostasPainel);
            i++;
        }
    }

    //Compara as respostas do player com as respostas corretas do sistema
    public void CompararRespostas ()
    {
        print(PegarRespostasDoPlayer().ToString());
        //Se as a variavel for verdadeira, executa funcoes abaixo
        if (PegarRespostasDoPlayer()) {

            //Pega todas as respostas do player para que sejam comparadas às corretas
            int i = 0;
            foreach (string resposta in respostasPlayer)
            {
                //Se a resposta do player for correta, o objeto ficara verde e nao podera mais ser retirado
                if (resposta == respostasCorretas[i])
                {
                    slots.GetComponentsInChildren<Drag>()[i].GetComponent<Image>().color = Color.green;
                    slots.GetComponentsInChildren<Drag>()[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                //Se a respsota do player for incorreta, o objeto ficara vermelho e podera ser retirado ou trocado com outros
                else
                {
                    slots.GetComponentsInChildren<Drag>()[i].GetComponent<Image>().color = Color.red;
                }
                i++;
            }
        }
    }

    //Retorna um valor positivo ou negativo caso todos os slots de resposta estejam sendo utilizados (algo dentro deles)
    bool PegarRespostasDoPlayer()
    {
        int f = 0;
        //As respostas que o player devera colocar devera ser o numero de respostas corretas
        respostasPlayer = new string[respostasCorretas.Length];
        //a variavel se inicia falsa pois quando vazio o slot, nao ha nenhuma resposta dada
        bool resposta = false;
   
        foreach (Transform slot in slots)
        {
            //Cria uma variavel para dizer quando o slot esta sendo ocupado ou nao
            GameObject item = slot.GetComponent<Slot>().item;
            //Se o slot possuir algum objeto, a resposta se torna verdadeira e o nome do item que estiver nela tera seu nome salvo na variavel respostasPlayer (array que serve para pegar todas as respostas em string e comparar com as respostas corretas tambem em string)
            if (item)
            {
                resposta = true;
                respostasPlayer[f] = item.name;
                
            }
            //Se o slot nao possuir nenhum objeto, a resposta sera false e o looping devera acabar por aqui, pois se tiver pelo menos UMA resposta sem nenhum objeto dentro, nao podera dizer quais respostas estao corretas de quais estao incorretas
            else
            {
                resposta = false;
                break;
            }
            f++;
        }
        //Retorna uma resposta (true ou false) baseada no resultado do looping 
        return resposta;

    }
    //Ao ocorrer alguma mudanca, faz determinada funcao
    public void HasChanged()
    {
        StartCoroutine(EsperaSoltar());
        
        /*foreach (Drag opcoes in FindObjectsOfType<Drag>()) {
            opcoes.GetComponent<Image>().color = Color.white;
        }*/
        //CompararRespostas();

        //System.Text.StringBuilder Builder = new System.Text.StringBuilder();
        //Builder.Append(" - ");
             
        //foreach (Transform slotTransform in slots)
        //{
          //  GameObject item = slotTransform.GetComponent<Slot>().item;


            /*if (item)
            {
                //Builder.Append(item.name);
                //Builder.Append(" - ");
            }*/

        //}

        //print(Builder.ToString());
        //inventoryText.text = Builder.ToString();
    }
    //Funcao que espera soltar o objeto para que ocorra a comparacao das respostas do player com as respostas corretas
    public IEnumerator EsperaSoltar()
    {
        yield return BugNoHasChanged;

        print("AEHOOUUU");

        CompararRespostas();

        BugNoHasChanged = null;

        StopCoroutine(EsperaSoltar());
    }

}
//Cria funcao para quando sofrer alguma mudanca no ambiente
namespace UnityEngine.EventSystems {

    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
