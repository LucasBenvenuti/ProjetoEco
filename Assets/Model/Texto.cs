using UnityEngine;
using System.Collections;

    //Classe modelo para os textos do jogo
public class Texto {

    //ID para o banco de dados
    public int id;

    //O Texto
    public string texto;

    //img do texto -- é possivel que o texto nao seja uma string e sim uma imagem
    public Sprite imagemDoTexto;
    //escolhas e feedbacks do texto -- mecanica de botoes 
    public Escolha escolha;
    //mecanica onde ha uma comparacao de imgs, letras, palavras ou frases
    public Comparativo comparativa;


    //nota: se ha escolhas, necessariamente a var comparativa tera que ser nula , vice versa. Havera situacoes que ambas serao nulas.
}
