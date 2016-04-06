using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using LitJson;
public class Network : MonoBehaviour {
    //url do json
    public string url = "";
    //Verifica se existe diretorio no dispositivo , se nao cria um para o projeto
    private void createDirectory()
    {
        string gameDirectory = Application.persistentDataPath + "/ProjetoEco";
        print(gameDirectory);
        if (!System.IO.Directory.Exists(gameDirectory))
        {
            System.IO.Directory.CreateDirectory(gameDirectory);
        }
    }

    IEnumerator Start() {
        createDirectory();
        WWW www = new WWW(url);
        //verifica coneccao com o banco
        yield return www;
        if (www.error == null) {
            //transformando o json em um array de cena 
            Cena[] cenas = JsonMapper.ToObject<Cena[]>(www.text);
            if (cenas.Length > 0) {

                
                for (int i = 0; i < cenas.Length; i++)
                {
                   // (cenas[i]);

                }


            }
        }
        else
        {
            print("Erro na coneccao : " + www.error);
        }
    }//end start
    //cria uma instancia de cena e chama a funcao de download para baixar a cena 
    void addCena(Cena scene) {
        Cena cena = new Cena();
        cena.imagensDaCena = scene.imagensDaCena;
        cena.texto = scene.texto;
        downloadCena(cena);
    }

    //cria os diretorios para cada cena
    void downloadCena(Cena scene) {



    }
}
