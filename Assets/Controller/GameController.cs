using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : Singleton<GameController> {


    //Controlador da cena para controlar as mecanicas do cenario
    CenaController cenaController;
    //Contador para a cena atual
    

    //Lista de escolhas do jogador <-- log das escolhas que o player fez --!>
    public List<string> escolhasDoJogador;

    //Também pode ser network essa parte ! ~--> alimentador da cena no inicio da applicacao <-- sera banco de dados, mas usaremos txt no resources para testar por enquanto. 
    //mais tarde iremos gravar essa informação dentro do dispositivo e puxaremos as informacoes de la--!->
    

}
