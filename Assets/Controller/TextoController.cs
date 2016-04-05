using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextoController : MonoBehaviour {

    //Lista dos textos
    List<Texto> textos;

    //Textos da cena atual
    Texto[] textosDaCena;

    //Caminho definido pela escolha a seguir
    int[] caminhoASeguir;

    //Verifica os textos pertinentes à determinada cena 
    void VerificarTexto ()
    {
        //transformando a lista em array
        List<Texto> temp = textos.FindAll(delegate (Texto tx) { return tx.cena == GameController.Instance.cenaAtual;});
        textosDaCena = new Texto[temp.Count];
        int i=0;
        foreach (Texto tx in temp) {
            textosDaCena[i] = tx;
            i++;
        }
    }
    //e após os agrupa na cena


    //Verifica as escolhas para pular de uma cena para outra
    void VerificarEscolhas()
    {

    }

    //Desabilita as escolhas que forem selecionadas pelo jogador e que são consideradas erradas
    void DesabilitarEscolha ()
    {

    }
     
}
