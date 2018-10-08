using System;
using System.Collections.Generic;

namespace IA_buscas {
    class Vertex{

        public int Id { get; set; }
        public string nome { get; set; }
        public List<Edge> Edges { get; set; }
        public int nivel { get; set; }
        public string cor { get; set; }
        public Vertex pai { get; set; }
        public int custo { get; set; }

        public Vertex( string nome ) {
            //Id=id;
            this.nome=nome;
        }

        public Vertex( int id , List<Edge> edges ) {
            this.Id=id;
            this.nome=nome;
            Edges=edges;
        }
        /*
        public int CompareTo( object obj ) {
            Vertex other = (Vertex)obj;

            return Id-other.Id;
        }
        */
    }
}