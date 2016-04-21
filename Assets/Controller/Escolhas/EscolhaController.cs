using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EscolhaController : MonoBehaviour {

    public Transform PainelEscolhas;
    Escolha baseDeDados;
    string Texto = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais";
    public GameObject PainelTexto;
    public GameObject resultado;
    //public Transform posicaoPainelEscolhas;
    //public GameObject[] escolhas;

    bool[] respostasUsadas;
    void Start () {
        //teste
    baseDeDados = new Escolha();
	baseDeDados.escolhas = new string[5]{ "parlavra","palavra","parlávra","palava","colhoro" }; 
    baseDeDados.corretas = new bool[5] { false, true, false, false, false };
    baseDeDados.respostas = new string[5] { "erooowwww","aEHOUUU", "nun", "%$¨@& brother", "nunca" };

        //logica
        respostasUsadas = new bool[baseDeDados.escolhas.Length];
     


    InstanciaEscolhas();
    }
    //instancia botoes pela quantidade de escolhas da base de dados
    void InstanciaEscolhas()
    {
        foreach (Transform child in PainelEscolhas)
        {
            Destroy(child.gameObject);
        }
        /*GameObject obj = Instantiate(Resources.Load("PainelEscolhas")) as GameObject;
         obj.transform.SetParent(dad.transform);
         obj.transform.position = posicaoPainelEscolhas.transform.position;
         PainelEscolhas = obj;*/
        int i = 0;
        //escolhas = new GameObject[baseDeDados.escolhas.Length];
        //para cada string dentro do array de strings ele executa um loop até acabar as strings dentro do array.
        foreach (string escolha in baseDeDados.escolhas) {

            GameObject Button = Instantiate(Resources.Load("Escolha")) as GameObject;
            Button.transform.SetParent(PainelEscolhas);
            Button.GetComponentInChildren<Text>().text = escolha;
            Button.name = escolha;
            int j = i;
            if (respostasUsadas[j] == true)
            {
                print("Foi");
                Button.GetComponent<Button>().interactable = false;
            }
            else {
                print(respostasUsadas[j].ToString());
                Button.GetComponent<Button>().onClick.AddListener(() => {print(Button.name); ResultadoEscolha(baseDeDados.respostas[j],j); });
            }
            //escolhas[j] = Button;
                i++;
        }
    }//end InstanciaEscolha

    void ResultadoEscolha(string result,int clicado) {
        foreach (Transform child in PainelEscolhas)
        {
            Destroy(child.gameObject);
        }

        //escolhas = new GameObject[baseDeDados.escolhas.Length];
        PainelTexto.SetActive(false);
        PainelEscolhas.gameObject.SetActive (false);
        resultado.SetActive(true);
        respostasUsadas[clicado] = true;
        resultado.GetComponentInChildren<Text>().text = result;
        resultado.GetComponent<Button>().onClick.AddListener(() => { RetornaAsEscolhas(); });
    }
    void RetornaAsEscolhas() {
        PainelEscolhas.gameObject.SetActive(true);
        PainelTexto.SetActive(true);
        InstanciaEscolhas();
        resultado.SetActive(false);
    }
    void Update() {
        //print(escolhas.Length.ToString());
    }
}
