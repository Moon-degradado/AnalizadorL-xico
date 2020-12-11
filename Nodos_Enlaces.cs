using System;
namespace Proyecto_del_analizador_léxico
{
    public class Nodos_Enlaces
    {
        String nod1_or;
        String punto;
        String direccion;
        String di1;
        String di2;
        String nod2_des;

        

        public Nodos_Enlaces(String nod1_or, String punto, String direccion, String di1, String di2, String nod2_des)
        {

            this.nod1_or = nod1_or;
            this.punto = punto;
            this.direccion = direccion;
            this.di1 = di1;
            this.di2 = di2;
            this.nod2_des = nod2_des;

        }

        public String getNod1_or()
        {
            return this.nod1_or;
        }
        public String getPunto()
        {
            return this.punto;
        }
        public String getDireccion()
        {
            return this.direccion;
        }

        public String getDi1()
        {
            return this.di1;
        }

        public String getDi2()
        {
            return this.di2;
        }
        public String getNod_des()
        {
            return this.nod2_des;
        }
    }
}