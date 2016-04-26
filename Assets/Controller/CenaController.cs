using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CenaController : MonoBehaviour {
//classe que controla a mecanica da cena 

    //Array de cenas do jogo
    public Cena[] cenas;


    //Cena em que o player se encontra - cena atual
    public int cenaAtual;
    //numero de textos na cena atual
    public int nTextos;
    // define o texto atual da cena
    public int contTextoAtual;

    //precisa capturar essa informação e verificar \/~

    //verifica o tipo de mecanica e chama a funcao de criar cena por tipo especifico.
    void CriaCena()
    {

        if (cenas[cenaAtual].texto[contTextoAtual].escolha != null)
        {
            CriaCena("choicescene");
        }
        else if (cenas[cenaAtual].texto[contTextoAtual].comparativa != null)
        {
            CriaCena("comparativescene");
        }
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
                break;

            case "comparativescene":
                print("Batata2");
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
    void PassaTexto() {
        //verifica se o texto atual é o ultimo da cena, se não ele passa para o proximo texto
        //tem que arrumar ~ 
        if (nTextos == contTextoAtual)
        {
            TrocarCena();
        }
        else {
            contTextoAtual += 1;
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
        //muda de cena
        try
        {
            cenaAtual += 1;
        }
        catch {
            print("fail");
        }
        //pega a quantidade de textos da cena
        nTextos = cenas[cenaAtual].texto.Length;
        CriaCena();
    }


    IEnumerator Start() {
        yield return cenas;
        //verifica se a base de dados possui alguma cena
        if (!(cenas.Length > 0))
        {
            print("crashed database!");
        }
        else {
            nTextos = cenas[cenaAtual].texto.Length;
        }

        //se possuir cena, cria
        CriaCena();

    }//end Start()
}
