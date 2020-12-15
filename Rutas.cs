using System;

namespace Proyecto_del_analizador_léxico
{
    class Rutas
    {
        private string path;
        private String Nombre;

        public Rutas(string path, String Nombre)
        {
            this.path = path;
            this.Nombre = Nombre;
        }
        public string getPath()
        {
            return this.path;
        }
        public String getNombre()
        {
            return this.Nombre;
        }
      
    }
}