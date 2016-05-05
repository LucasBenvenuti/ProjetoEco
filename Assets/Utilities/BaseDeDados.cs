using UnityEngine;
using System.Collections;

public class BaseDeDados {

    public  Cena[] cenas = new Cena[5];
    public Cena temp = new Cena();
    //Cria uma base para as cenas
    public  void StartBase()
    {
        //Cena temp = new Cena();

        temp.imagensDaCena = new Imagem[2] { new Imagem() , new Imagem() };
        temp.texto = new Texto[2] { new Texto(), new Texto() };

        //cena1
        //take1
        temp.imagensDaCena[0].image = new Sprite();
        temp.imagensDaCena[0].image = Resources.Load("cena1text1") as Sprite;
        temp.texto[0].escolha = new Escolha();
        temp.texto[0].texto = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais";

        temp.texto[0].escolha.escolhas = new string[5] { "parlavra", "palavra", "pudim", "de", "batata" };
        temp.texto[0].escolha.corretas = new bool[5] { false, true, false, false, false };
        temp.texto[0].escolha.respostas = new string[5] { "erooowwww", "aEHOUUU", "nun", "%$¨@& brother", "torre de hanoi" };


        //take2

        temp.imagensDaCena[1].image = new Sprite();
        temp.imagensDaCena[1].image = Resources.Load("cena1text2") as Sprite;
        temp.texto[1].comparativa = new Comparativo();
        temp.texto[1].texto = "Forme um tuberculo";

        temp.texto[1].comparativa.resposta = new string[6] { "b", "a", "t", "a", "t", "a" };
        temp.texto[1].comparativa.tiposRespostas = new int[6] { 0, 0, 0, 0, 0, 0 };
        temp.texto[1].comparativa.opcoes = new string[10] { "x", "a", "y", "á", "b", "t", "t", "a", "h", "a" };
        temp.texto[1].comparativa.tiposOpcoes = new int[10] { 1, 0, 1, 1, 0, 0, 0, 0, 1, 0 };
        temp.texto[1].comparativa.imagensOpcoes = new Sprite[temp.texto[1].comparativa.opcoes.Length];

        Sprite[] words = Resources.LoadAll<Sprite>("words");

        for (int i = 0; i < temp.texto[1].comparativa.imagensOpcoes.Length; i++) {
            foreach (Sprite spr in words)
            {
                if (spr.name == temp.texto[1].comparativa.opcoes[i])
                {
                    temp.texto[1].comparativa.imagensOpcoes[i] = spr;
                    
                }
            }
        }
        cenas[0] = temp;
        /*     cena[0] = temp;
             temp = new Cena();
             cena[1] = temp;
             Debug.Log("test : " + cena[0].texto[0].texto);
             Debug.Log("test : " + cena[1].texto[0].texto);
         */

    }
}
