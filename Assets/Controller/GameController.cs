using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Só funciona se o objtivo no qual estiver possuir CenaController
public class GameController : Singleton<GameController> {


    //Controlador da cena para controlar as mecanicas do cenario
    public CenaController cenaController;
    //Contador para a cena atual

    // cenaController.cenas  

    //Lista de escolhas do jogador <-- log das escolhas que o player fez --!>
    public List<string> escolhasDoJogador;
 	 	   	
    //Também pode ser network essa parte ! ~--> alimentador da cena no inicio da applicacao <-- sera banco de dados, mas usaremos txt no resources para testar por enquanto. 
    //mais tarde iremos gravar essa informação dentro do dispositivo e puxaremos as informacoes de la--!->
    
    void Start ()
    {
        //criando a base a partir da BaseDeDados e iniciando ela 
        BaseDeDados database = new BaseDeDados();
        print("pudim");
        database.StartBase();
        
        //pega a cena do gameobject
        cenaController = GetComponent<CenaController>();
        //alimenta a cenas do cenaController com as inf da database
        if (cenaController.cenas == null)
        {
            cenaController.cenas = database.cenas;
        }
         
    }
}
    