using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace IA_buscas {
    class Graph {
        public List<Edge> E;
        public Dictionary<string,Vertex> VD;
        public List<Vertex> V;

        public void createGraph( List<string[]> arestas,List<string[]> vertices) {
            E=new List<Edge>();
            VD= new Dictionary<string , Vertex>();
            V=new List<Vertex>();

            //cria os vertices e para cada vertice sua lista de aresta e no final adiciona todos os vertices na list de vertices do grafo
            for( int i = 0; i<vertices.Count(); i++ ) {
                Vertex v = new Vertex(vertices[i][0]);
                v.Id = i;
                v.Edges=new List<Edge>();
                V.Add(v);
                VD.Add(v.nome,v);
            }

            //cria as arestas do fgrafo
            foreach( string[] aresta in arestas ) {
                
                Edge edge_1 = new Edge( VD[aresta[0]],VD[aresta[1]] , int.Parse(aresta[2]));

                if( !edge_1.InList(E) )
                    E.Add(edge_1);
      
            }


            for( int i = 0; i<E.Count; i++ ) {
                Vertex u = E[i].origem, v = E[i].destino;
                u.Edges.Add(new Edge(u , v , E[i].peso));
                //v.Edges.Add(new Edge(v , u , E[i].peso));
                //v.Edges=v.Edges.OrderBy(a => a.destino.nome).ToList();//ordena as arestas em ordem alfabetica
            }
            foreach (Vertex v in V)
            {
                v.Edges = v.Edges.OrderBy(a => a.destino.nome).ToList();//ordena as arestas em ordem alfabetica
            }
        }

        public List<string> inicia_Busca_expansao(Graph g, string nomeOrigem,string nomeDestino) {
            List<string> caminho = new List<string>();

            //se o vertice de destino for o mesmo vertice de origem
            if (nomeOrigem.Equals(nomeDestino))
            {
                caminho.Add(nomeOrigem);
                return caminho;
            }
            Vertex u = busca_expansao( g, nomeOrigem , nomeDestino);

            caminho.Add(u.nome);
            while( u.pai!=null ) {
                caminho.Add(u.pai.nome);
                u=u.pai;
            }
            caminho.Reverse();//inverte a lista de caminho
            return caminho;
        }

        public List<string> inicia_Busca_asterisco( Graph g , string nomeOrigem , string nomeDestino, Dictionary<string , int> distHeuristica ) {
            List<string> caminho = new List<string>();
            Vertex u = busca_asterisco(g , nomeOrigem , nomeDestino,distHeuristica);

            //se o vertice de destino for o mesmo vertice de origem
            if( nomeOrigem.Equals(nomeDestino) ) {
                caminho.Add(nomeOrigem);
                return caminho;
            }


            caminho.Add(u.nome);
            while( u.pai!=null ) {
                caminho.Add(u.pai.nome);
                u=u.pai;
            }
            caminho.Reverse();//inverte a lista de caminho
            return caminho;
        }


        /// <summary>
        /// metódo classico BFS busca em largura em grafo
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <param name="visitados"></param>
        public Vertex busca_expansao( Graph g , string nomeOrigem , string nomeDestino ) {
            Vertex s = recupera_Vertice(g,nomeOrigem);
            if(s==null) {
                return null;
            }

            //cria uma nova fila
            Queue q = new Queue();

            //seta as informações de todos os vertices cor,nivel defult -1;
            for( int i = 0; i<g.V.Count(); i++ ) {
                g.V[i].cor="b";
                g.V[i].nivel=-1;
                g.V[i].pai=null;
            }
            //marca o vertice inicial
            s.cor="c";
            s.nivel=0;
            s.pai=null;

            //coloca o vertice inicial na fila
            q.Enqueue(s);
            Vertex u = null;
            //explora os vertices dos grafos
            while( q.Count>0 ) {
                u=(Vertex)q.Dequeue();
                for( int i = 0; i<u.Edges.Count(); i++ ) {
                    if( u.Edges[i].destino.cor.Equals("b")) {
                        u.Edges[i].destino.cor="c";
                        u.Edges[i].destino.pai=u;
                        u.Edges[i].destino.nivel=u.Edges[i].destino.pai.nivel+1;
                        if( u.Edges[i].destino.nome.Equals(nomeDestino) ) {
                            u.Edges[i].destino.pai=u;
                            return u.Edges[i].destino;
                        }else {
                            if(!q.Contains(u.Edges[i].destino))
                                q.Enqueue(u.Edges[i].destino);
                        }
                        
                    }
                }
                u.cor="p";
            }
            return null;
        }

        //busca a asterisco busca com informação
        public Vertex busca_asterisco( Graph g , string nomeOrigem , string nomeDestino , Dictionary<string , int> distH ) {

            Vertex s = recupera_Vertice(g , nomeOrigem);
            if( s==null ) {
                return null;
            }

            //cria uma nova fila
            //Queue q = new Queue();
            List<Vertex> fila = new List<Vertex>();

            //seta as informações de todos os vertices cor,nivel defult -1;
            for( int i = 0; i<g.V.Count(); i++ ) {
                g.V[i].cor="b";
                g.V[i].nivel=-1;
                g.V[i].pai=null;
                g.V[i].custo=-1;
            }
            //marca o vertice inicial
            s.cor="c";
            s.nivel=0;
            s.pai=null;
            s.custo=0;

            //coloca o vertice inicial na fila
            fila.Add(s);

            Vertex u = null;

            //explora os vertices dos grafos
            while( fila.Count>0 ) {
                u=fila[0]; // pega o elemento da primeira posição
                fila.RemoveAt(0);//remove o elemento da primeira posição
                if(u.nome.Equals(nomeDestino)) {
                    return u;
                }
                for( int i = 0; i<u.Edges.Count(); i++ ) {
                    if( u.Edges[i].destino.cor.Equals("b") ) {
                        u.Edges[i].destino.cor="c";
                        u.Edges[i].destino.pai=u;
                        u.Edges[i].destino.nivel=u.Edges[i].destino.pai.nivel+1;
                        u.Edges[i].destino.custo=u.Edges[i].destino.pai.custo + u.Edges[i].peso + distH[u.Edges[i].destino.nome+nomeDestino];
                        fila.Add(u.Edges[i].destino);
                        fila = fila.OrderBy(a => a.custo).ToList();
                    }
                    u.cor="p";
                }
            }
            return null;
        }

        public Vertex recupera_Vertice(Graph g ,string nome) {
            foreach(Vertex v in g.V) {
                if( v.nome.Equals(nome) ) {
                    return v;
                }
            }
            return null;
        }
    }
}

