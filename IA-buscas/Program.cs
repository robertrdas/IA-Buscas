using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace IA_buscas {
    class Program {
        static void Main( string[] args ) {
            List<string[]> arestas = lerArestas();
            List<string[]> vertices = lerVertices();
            Dictionary<string , int> distHeuristica = geraDistHeuristica();
            List<string> caminho;

            Graph g = new Graph();

            g.createGraph(arestas,vertices);

            /*busca em expansão - busca cega*/
            //caminho=g.inicia_Busca_expansao(g , "sao luis" , "joao pessoa");
            //caminho=g.inicia_Busca_expansao(g , "fortaleza" , "salvador");
            //caminho=g.inicia_Busca_expansao(g , "salvador" , "fortaleza");
            //caminho=g.inicia_Busca_expansao(g , "joao pessoa", "sao luis");

            /*busca asterisco - busca com informação*/
            //caminho =g.inicia_Busca_asterisco(g,"sao luis","joao pessoa",distHeuristica);
            //caminho=g.inicia_Busca_asterisco(g , "fortaleza" , "salvador",distHeuristica);
            //caminho=g.inicia_Busca_asterisco(g , "salvador" , "fortaleza",distHeuristica);
            caminho=g.inicia_Busca_asterisco(g , "joao pessoa", "sao luis",distHeuristica);

            Console.WriteLine();
            foreach ( string nome in caminho) {
               
              System.Diagnostics.Debug.WriteLine(nome);
                Console.Write(nome+" -> ");
            }
            Console.Read();
            int x = 0;
        }

        public static List<string[]> lerArestas() {
            List<string[]> vertice = new List<string[]>();

            string path = @"D:\Desktop\IA-buscas\\arestas.txt";

            StreamReader rd = new StreamReader(path, Encoding.UTF8);

            String linha = null;
            //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
            string[] linhaseparada = null;
            //realizo o while para ler o conteudo da linha 
            //linha=rd.ReadLine();
            while( ( linha=rd.ReadLine() )!=null ) {
                //com o split adiciono a string 'quebrada' dentro do array 
                linhaseparada=linha.Split(';');
                vertice.Add(linhaseparada);
            }
            return vertice;
        }

        public static List<string[]> lerVertices() {
            List<string[]> vertice = new List<string[]>();

            string path = @"D:\Desktop\IA-buscas\\vertices.txt";

            StreamReader rd = new StreamReader(path , Encoding.UTF8);

            String linha = null;
            //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
            string[] linhaseparada = null;
            //realizo o while para ler o conteudo da linha 
            //linha=rd.ReadLine();
            while( ( linha=rd.ReadLine() )!=null ) {
                //com o split adiciono a string 'quebrada' dentro do array 
                linhaseparada=linha.Split(';');
                vertice.Add(linhaseparada);
            }
            return vertice;
        }

        public static Dictionary<string , int> geraDistHeuristica() {
            Dictionary<string , int> distHeuristica = new Dictionary<string , int>();

            string path = @"D:\Desktop\IA-buscas\\distheuristica.txt";

            StreamReader rd = new StreamReader(path , Encoding.UTF8);

            String linha = null;
            //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
            string[] linhaseparada = null;
            //realizo o while para ler o conteudo da linha 
            //linha=rd.ReadLine();
            while( ( linha=rd.ReadLine() )!=null ) {
                //com o split adiciono a string 'quebrada' dentro do array 
                linhaseparada=linha.Split(';');
                distHeuristica.Add(linhaseparada[0]+linhaseparada[1],int.Parse(linhaseparada[2].ToString()));
            }
            return distHeuristica;
        }
    }
}
