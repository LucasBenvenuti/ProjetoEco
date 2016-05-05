using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscolhaController : MonoBehaviour {

    public Transform PainelEscolhas;
    Escolha baseDeDados;

    //string Texto = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais";
    public GameObject PainelTexto;
    public GameObject resultado;

    public Text TextoCena;
    //public Transform posicaoPainelEscolhas;
    //public GameObject[] escolhas;

    CenaController controladorCena;


    bool[] respostasUsadas;

    void Start () {
        controladorCena = FindObjectOfType<CenaController>();

        //teste
        /*baseDeDados = new Escolha();
	    baseDeDados.escolhas = new string[5]{ "parlavra","palavra","parlávra","palava","colhoro" }; 
        baseDeDados.corretas = new bool[5] { false, true, false, false, false };
        baseDeDados.respostas = new string[5] { "erooowwww","aEHOUUU", "nun", "%$¨@& brother", "nunca" };
        */

        StartCoroutine(InicializadorDasVariaveis());

        
    }
    //evita com que as variaveis iniciem sem o banco estar ativo no GameController
    IEnumerator InicializadorDasVariaveis() {
        yield return controladorCena.cenas;

        baseDeDados = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].escolha;
        //logica
        respostasUsadas = new bool[baseDeDados.escolhas.Length];

        //Texto da cena
        TextoCena.text = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].texto;

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
            GameObject button = Instantiate(Resources.Load("Escolha")) as GameObject;
            button.transform.SetParent(PainelEscolhas);
            button.GetComponentInChildren<Text>().text = escolha;
            button.name = escolha;
            int j = i;
            bool respostaCorreta;

            //print(respostasUsadas[j].ToString());
            print(baseDeDados.corretas[j]);
                if (baseDeDados.corretas[j] == true)
                {
                print("Foi");
                respostaCorreta = true;
                    //SceneManager.LoadScene("CenaController");
               
                   //print("to funcionando"); FindObjectOfType<CenaController>().PassaTexto();
                }
                else
                {
                respostaCorreta = false;
                if (respostasUsadas[j] == true)
                    {
                        Color temp = new Color();
                        temp.a = 0f;
                        button.GetComponent<Image>().color = temp;
                        button.GetComponentInChildren<Text>().gameObject.SetActive(false);
                        button.GetComponent<Button>().interactable = false;

                    print("FALSA");
                    }
                }
            
          
                //print(respostasUsadas[j].ToString());
               button.GetComponent<Button>().onClick.AddListener(() => {print(button.name); ResultadoEscolha(baseDeDados.respostas[j], respostaCorreta); respostasUsadas[j] = true;  });
                i++;
            }
            //escolhas[j] = Button;
        
    }//end InstanciaEscolha

    void ResultadoEscolha(string result, bool correta) {
        foreach (Transform child in PainelEscolhas)
        {
            
            Destroy(child.gameObject);
        }

        //escolhas = new GameObject[baseDeDados.escolhas.Length];
        PainelTexto.SetActive(false);
        PainelEscolhas.gameObject.SetActive (false);
        resultado.SetActive(true);
        
        resultado.GetComponentInChildren<Text>().text = result;
        resultado.GetComponent<Button>().onClick.AddListener(() => {
            if (correta) {
                FindObjectOfType<CenaController>().PassaTexto();
            }
            RetornaAsEscolhas();
        });
    }
    void RetornaAsEscolhas() {
        PainelEscolhas.gameObject.SetActive(true);
        PainelTexto.SetActive(true);
        InstanciaEscolhas();
        resultado.SetActive(false);
    }

    void TrocaCena()
    {

    }
    void Update() {
        //print(escolhas.Length.ToString());
    }
}
