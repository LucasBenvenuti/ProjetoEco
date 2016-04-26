﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EscolhaController : MonoBehaviour {

    public Transform PainelEscolhas;
    Escolha baseDeDados;
    string Texto = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais";
    public GameObject PainelTexto;
    public GameObject resultado;

    public Text TextoCena;
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

        //Texto da cena
        TextoCena.text = Texto;


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
            if (respostasUsadas[j] == true)
            {
                //print("Foi");
                //print(respostasUsadas[j].ToString());
                
                if (baseDeDados.corretas[j] == true)
                {

                    print("CORRETA");
                    button.GetComponent<Button>().onClick.AddListener(() => { print(button.name); ResultadoEscolha(baseDeDados.respostas[j], j); });
                }
                else
                {
                    Color temp = new Color();
                    temp.a = 0f;
                    button.GetComponent<Image>().color = temp;
                    button.GetComponentInChildren<Text>().gameObject.SetActive(false);
                    button.GetComponent<Button>().interactable = false;
                    print("FALSA");
                }
            }
            else {
                //print(respostasUsadas[j].ToString());
                button.GetComponent<Button>().onClick.AddListener(() => {print(button.name); ResultadoEscolha(baseDeDados.respostas[j],j); });
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

    void TrocaCena()
    {

    }
    void Update() {
        //print(escolhas.Length.ToString());
    }
}
