using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CenaController : Singleton<CenaController> {
//classe que controla a mecanica da cena 

    //Array de cenas do jogo
    public Cena[] cenas;


    //Cena em que o player se encontra - cena atual
    public int cenaAtual;
    //numero de textos na cena atual
    public int nTextos;
    // define o texto atual da cena
    [SerializeField]
    public static int contTextoAtual;

    //precisa capturar essa informação e verificar \/~

    IEnumerator Start() {
        yield return cenas;
    //    print("eo: " + contTextoAtual);
        //verifica se a base de dados possui alguma cena
        if (!(cenas.Length > 0))
        {
            print("crashed database!");
        }
        else {
            nTextos = cenas[cenaAtual].texto.Length;
        }

        //se possuir cena, cria
        //CriaCena();
    }//end Start()

    //verifica o tipo de mecanica e chama a funcao de criar cena por tipo especifico.


    //Cria as cenas de acordo com acena em que o jogador se encontra. Existem 3 tipos de cenas, as cenas normais (apenas com textos e imagens), cenas de Drag and Drop
    //e cenas de escolhas.
    void CriaCena()
    {
        //Printa a cena atual
        print("cena atual: " + cenaAtual + "texto atual: " + contTextoAtual);
        //Cria cenas de escolhas
        if (cenas[cenaAtual].texto[contTextoAtual].escolha != null)
        {
            CriaCena("choicescene");
        }
        //Cria cenas de Drag and Drop
        else if (cenas[cenaAtual].texto[contTextoAtual].comparativa != null)
        {
            CriaCena("comparativescene");
        }
        //Cria cenas normais
        else {
            CriaCena("normalscene");
        }

    }
    //instancia a cena dependendo de qual se encontra e o tipo de mecanica
    // cria cena a partir de um tipo especifico
    void CriaCena(string tipoDaCena) {
       
        switch (tipoDaCena) {
            case "choicescene":
                print("Batata");

                SceneManager.LoadScene("Escolhas");
                break;

            case "comparativescene":
                print("Batata2");
                SceneManager.LoadScene("DragDrop");
                break;

            case "normalscene":
                print("Batata3");
                break;

            default:
                print("Error");
                break;

        }
       

        //coloca as  variaveis/inf na cena   
        // a partir disso ~> cenas[cenaAtual].

    }

    // passa de um texto para outro
    public void PassaTexto() {
        //verifica se o texto atual é o ultimo da cena, se não ele passa para o proximo texto
        //tem que arrumar ~ 
        if (nTextos == contTextoAtual)
        {

            TrocarCena();

        }
        else {
            contTextoAtual += 1;
            print("pao"+ contTextoAtual);
            CriaCena();
        }
    }

   /* parece estranho
    //Verifica as escolhas para dar o feedback visual e escrito da escolha, dependendo da escolha
    void VerificarEscolhas()
    {

    }



    //Isso é view ---  Desabilita as escolhas que forem selecionadas pelo jogador e que são consideradas erradas 
    void DesabilitarEscolha ()
    {

    }
    */
    //Troca de cena ,chama a funcao de criar nova cena
    void TrocarCena()
    {
        //pega a quantidade de textos da cena
        cenaAtual += 1;
        nTextos = cenas[cenaAtual].texto.Length;
        print("TrocarCena");
        CriaCena();
    }


}
