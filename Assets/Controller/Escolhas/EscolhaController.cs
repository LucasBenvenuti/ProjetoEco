using UnityEngine;
using System.Collections;

public class EscolhaController : MonoBehaviour {

    Escolha baseDeDados;

	// Use this for initialization
	void Start () {
	baseDeDados.escolhas = new string[5]{ "parlavra","palavra","parlávra","palava","colhoro" };
    baseDeDados.corretas = new bool[5] { false, true, false, false, false };
    baseDeDados.respostas = new string[5] { "erooowwww","aEHOUUU", "nun", "%$¨@& brother", "nunca" };
    }
	
}
