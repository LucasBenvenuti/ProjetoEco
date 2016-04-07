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
        
        //verifica conexao com o banco
        yield return www;

        if (www.error == null) {
            //transformando o json em um array de cena 
            Cena[] cenas = JsonMapper.ToObject<Cena[]>(www.text);
            if (cenas.Length > 0) {

                for (int i = 0; i < cenas.Length; i++)
                {
                    addCena(cenas[i]);

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
        cena.id = scene.id;
        cena.imagensDaCena = scene.imagensDaCena;
        cena.texto = scene.texto;
        downloadCena(cena);
    }

    //cria os diretorios para cada cena
    void downloadCena(Cena scene) {
        
        //-----------Cria diretorio para cena
        string sceneDirectory;

        sceneDirectory = Application.persistentDataPath + "/ProjetoEco/" + scene.id;

            //Verifica se o diretorio da cena já existe
            // Se já existir, o deleta, se não, cria um novo
        if (System.IO.Directory.Exists(sceneDirectory))
        {
            System.IO.Directory.Delete(sceneDirectory, true);
        }
        System.IO.Directory.CreateDirectory(sceneDirectory);

        //-----------Cria o JSon da cena
        string json = JsonConvert.SerializeObject(scene);

        //Escreve um arquivo de texto contendo as informacoes do json
        System.IO.File.WriteAllText(sceneDirectory + "/info.txt", json);

        //Inicia a salvar os dados no arquivo de texto
        // StartCoroutine();

        //-------------Cria diretorio para imagens
        string diretorioDasImgs = sceneDirectory + "/ImagensDaCena/";

        //Verifica se o diretorio das imagens já existe
        // Se já existir, o deleta, se não, cria um novo
        if (System.IO.Directory.Exists(diretorioDasImgs))
        {
            System.IO.Directory.Delete(diretorioDasImgs, true);
        }
        System.IO.Directory.CreateDirectory(diretorioDasImgs);

        foreach (Imagem img in scene.imagensDaCena) {

            StartCoroutine(downloadImage(img.urlImg, img.codImage.ToString(), diretorioDasImgs));
          
        }
        //--Termina de criar o diretorio das imagens

        //-------------Cria diretorio para texto
        string diretorioDosTextos = sceneDirectory + "/TextosDaCena/";

        //Verifica se o diretorio dos textos já existe
        // Se já existir, o deleta, se não, cria um novo
        if (System.IO.Directory.Exists(diretorioDosTextos))
        {
            System.IO.Directory.Delete(diretorioDosTextos, true);
        }
        System.IO.Directory.CreateDirectory(diretorioDosTextos);

        foreach (Texto text in scene.texto)
        {
            StartCoroutine(downloadImage(text.urlImgDoTexto, text.id.ToString(), diretorioDosTextos));

            //-------------Cria diretorio para escolhas
            string diretorioDasEscolhas = sceneDirectory + "/EscolhasDaCena/";

            //Verifica se o diretorio das escolhas já existe
            // Se já existir, o deleta, se não, cria um novo
            if (System.IO.Directory.Exists(diretorioDasEscolhas))
            {
                System.IO.Directory.Delete(diretorioDasEscolhas, true);
            }
            System.IO.Directory.CreateDirectory(diretorioDasEscolhas);

            foreach (Texto escolhas in scene.texto)
            {
                StartCoroutine(downloadImage(escolhas.escolha.urlsImgs.ToString(), escolhas.escolha.escolhas.ToString(), diretorioDasEscolhas));

            }
            //--Termina de criar o diretorio das escolhas

            //-------------Cria diretorio para comparativo
            string diretorioDosComparativos = sceneDirectory + "/ComparativoDaCena/";

            //Verifica se o diretorio dos comparativos já existe
            // Se já existir, o deleta, se não, cria um novo
            if (System.IO.Directory.Exists(diretorioDosComparativos))
            {
                System.IO.Directory.Delete(diretorioDosComparativos, true);
            }
            System.IO.Directory.CreateDirectory(diretorioDosComparativos);

            //-------------Cria diretorio para comparativoOpcoes
            string opcoes = sceneDirectory + diretorioDosComparativos + "OpcoesDaCena/";

            //Verifica se o diretorio dos comparativos já existe
            // Se já existir, o deleta, se não, cria um novo
            if (System.IO.Directory.Exists(opcoes))
            {
                System.IO.Directory.Delete(opcoes, true);
            }
            System.IO.Directory.CreateDirectory(opcoes);

            foreach (Texto comparativosOpcoes in scene.texto)
            {
                StartCoroutine(downloadImage(comparativosOpcoes.comparativa.urlImgOpcoes.ToString(), comparativosOpcoes.comparativa.opcoes.ToString(), opcoes));

            }
            //--Termina de criar o diretorio de opcoes dentro de comparativo

            //-------------Cria diretorio para comparativoRespostas
            string respostas = sceneDirectory + diretorioDosComparativos + "RespostasDaCena/";

            //Verifica se o diretorio das respostas já existe
            // Se já existir, o deleta, se não, cria um novo
            if (System.IO.Directory.Exists(respostas))
            {
                System.IO.Directory.Delete(respostas, true);
            }
            System.IO.Directory.CreateDirectory(respostas);

            foreach (Texto comparativosRespostas in scene.texto)
            {
                StartCoroutine(downloadImage(comparativosRespostas.comparativa.urlImgRespostas.ToString(), comparativosRespostas.comparativa.resposta.ToString(), respostas));

            }
        }
    }//fim do download cena

    IEnumerator downloadImage(string urlimg, string name, string diretory) {
        WWW www = new WWW(urlimg);
        yield return www;

        //Se nao houver erros
        //Salva na pasta do diretorio 
        if (www.error == null) {
            System.IO.File.WriteAllBytes (diretory + name + ".jpg" , www.texture.EncodeToJPG());
        }
    }
}
