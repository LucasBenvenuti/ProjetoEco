using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscolhaController : MonoBehaviour {

    public Transform PainelEscolhas;
    Escolha baseDeDados;

    public GameObject PainelTexto;
    public GameObject resultado;

    public Text TextoCena;

    CenaController controladorCena;

    //Variavel criada para dizer se a resposta foi utilizada ou nao. Foi criado um array pois possuem varias escolhas dentro da cena
    bool[] respostasUsadas;

    //Define qual o objeto sera referenciado pela variavel controladorCena e inicia uma rotina, no caso, InicializadorDasVariaveis
    void Start ()
    {
        controladorCena = FindObjectOfType<CenaController>();

        StartCoroutine(InicializadorDasVariaveis());
    }
    //evita com que as variaveis iniciem sem o banco estar ativo no GameController
    IEnumerator InicializadorDasVariaveis() {
        //Espera até que as cenas sejam instanciadas
        yield return controladorCena.cenas;

        baseDeDados = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].escolha;
        //a quantidade de opcoes sera o tamanho do array de escolhas dentro do codigo da BaseDeDados
        respostasUsadas = new bool[baseDeDados.escolhas.Length];

        //Texto da cena que se encontra dentro do codigo da BaseDeDados
        TextoCena.text = controladorCena.cenas[controladorCena.cenaAtual].texto[CenaController.contTextoAtual].texto;
        //Inicia a funcao de instancias todas as escolhas
        InstanciaEscolhas();
    }

    //instancia botoes pela quantidade de escolhas da base de dados
    void InstanciaEscolhas()
    {
        //Destroi todos os filhos presentes no PainelEscolhas
        foreach (Transform child in PainelEscolhas)
        {
            Destroy(child.gameObject);
        }
        int i = 0;
        //escolhas = new GameObject[baseDeDados.escolhas.Length];

        //para cada string dentro do array de strings ele executa um loop até acabar as strings dentro do array.
        foreach (string escolha in baseDeDados.escolhas) {
            //Instancia o prefab dentro da pasta Resources cujo nome Escolha
            GameObject button = Instantiate(Resources.Load("Escolha")) as GameObject;
            //Define um pai para o objeto recem instanciado
            button.transform.SetParent(PainelEscolhas);
            //Define um texto que o objeto recem instanciado possuira de acordo com o que esta no BancoDeDados
            button.GetComponentInChildren<Text>().text = escolha;
            //Define um nome para o objeto instanciado de acordo com a escolha presente no codigo do BancoDeDados
            button.name = escolha;
            int j = i;
            //Cria uma variavel booleana para comparar se a resposta dada é correta ou nao
            bool respostaCorreta;

            //print(respostasUsadas[j].ToString());
            //print(baseDeDados.corretas[j]);

            //Se a resposta for correta, a variavel se torna verdadeira
                if (baseDeDados.corretas[j] == true)
                {
                print("Foi");
                respostaCorreta = true;
                    //SceneManager.LoadScene("CenaController");
               
                   //print("to funcionando"); FindObjectOfType<CenaController>().PassaTexto();
                }
                //Se a resposta for incorreta, a variavel continua sendo falsa e ocorre mais uma serie de acontecimentos
                else
                {
                respostaCorreta = false;
                //se a resposta foi usada, o ALPHA da cor do botao é modificado para que seja transparente, bem como é desativado o texto dentro do botao 
                //e retirada a interação do botao com o jogador, afim de que o botao "suma" da cena e que o espaco em branco continue. 
                //Caso ele seja deletado, pelo pai das escolhas possuir um componente de Grid, os botoes se reajustam, o que nesse projeto nao é necessario
                if (respostasUsadas[j] == true)
                    {
                        Color temp = new Color();
                        temp.a = 0f;
                        button.GetComponent<Image>().color = temp;
                        button.GetComponentInChildren<Text>().gameObject.SetActive(false);
                        button.GetComponent<Button>().interactable = false;

                   // print("FALSA");
                    }
                }
            
          
                //print(respostasUsadas[j].ToString());

                //Adiciona funcoes para os botoes instanciados
               button.GetComponent<Button>().onClick.AddListener(() => {print(button.name); ResultadoEscolha(baseDeDados.respostas[j], respostaCorreta); respostasUsadas[j] = true;  });
                i++;
            }
            //escolhas[j] = Button;
        
    }//end InstanciaEscolha

    //Funcao para calcular se a resposta esta certa ou nao
    void ResultadoEscolha(string result, bool correta) {
        //destroi os filhos do PainelEscolhas (no caso ele destroi os botoes de escolha)
        foreach (Transform child in PainelEscolhas)
        {
            
            Destroy(child.gameObject);
        }

        //desativa todas as opcoes e ativa somente o texto em resposta da opcao escolhida, dizendo se a resposta dada esta correta ou nao. 
        //Tudo e pego dentro do codigo da BaseDeDados
        
        //Ele ativa um botao do tamanho da tela que recebera uma funcao assim que for ativo
        PainelTexto.SetActive(false);
        PainelEscolhas.gameObject.SetActive (false);
        resultado.SetActive(true);
        
        resultado.GetComponentInChildren<Text>().text = result;
        //botao recebe a funcao
        resultado.GetComponent<Button>().onClick.AddListener(() => {
            //se a opcao for a correta, ele passa o texto
            if (correta)
            {
                print("Acertou");
                FindObjectOfType<CenaController>().PassaTexto();
            }
            //se a opcao nao for a correta, retorna para as escolhas ate que acerte a opcao
            else
            {
                RetornaAsEscolhas();
            }
        });
    }
    //Retorna para a selecao das escolhas
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
