using UnityEngine;
using System.Collections;

    //Classe modelo para os textos do jogo
public class Texto {
    //ID para o banco de dados
    public int id;

    //O Texto
    public string texto;
    
    //Controle de cenas para saber qual texto pertence a cada cena
    public int cena;

    //Define a mecanica da cena para o texto
    public string tipoDeCena;

    //Um conjunto de textos que representam escolhas pertinentes
    public string[] escolhas;
    //A ramificação que a escolha trará
    public int[,] caminho;
  

}
