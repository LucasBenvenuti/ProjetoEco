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
    
        //coloca as  variaveis/inf na cena   
        // a partir disso ~> cenas[cenaAtual].

    }


    /* parece estranho


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

}
