using System.Collections.Generic;

namespace IA_buscas {
    class Edge {
        public Vertex origem { get; set; }
        public Vertex destino { get; set; }
        public int peso { get; set; }
        public Edge( Vertex origem , Vertex destino , int peso ) {
            this.origem=origem;
            this.destino=destino;
            this.peso=peso;
        }
        public bool InList( List<Edge> elist ) {
            for( int i = 0; i<elist.Count; i++ ) {
                Edge e = elist[i];

                if( origem == e.origem && destino == e.destino )
                    return true;
            }

            return false;
        }
    }
}