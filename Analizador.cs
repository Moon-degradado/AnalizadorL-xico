using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proyecto_del_analizador_léxico
{
    class Analizador
    {
        ArrayList tokens;
        ArrayList tipos;
        ArrayList Lista_Operaciones;
        static private List<Token> listaTokens;
        public List<Lineas_Ejecutar> lin_ejecutar;
        public List<Graficas_Ejecutar> graf_ejecutar;
        private String retorno;
        public int estado_token;

        //errores tokens
        static private List<ErroresToken> listaErrores;

        public Analizador()
        {
            listaTokens = new List<Token>();
            tokens = new ArrayList();
            tipos = new ArrayList();
            tokens.Add("Resultado");  //0
            tokens.Add("Graficar"); //1
            tokens.Add("Node"); //2

            tipos.Add("Valor");
            tipos.Add("Operador");
            tipos.Add("IZQ");
            tipos.Add("DER");


            Lista_Operaciones = new ArrayList();
            lin_ejecutar = new List<Lineas_Ejecutar>();
            graf_ejecutar = new List<Graficas_Ejecutar>();
            // 
            // Errores Tokens
            // 
            listaErrores = new List<ErroresToken>();

        }

        public void addToken(String lexema, String idToken, int linea, int columna, int indice)
        {
            Token nuevo = new Token(lexema, idToken, linea, columna, indice);
            listaTokens.Add(nuevo);
        }

        public void addError(String lexema, String idToken, int linea, int columna)
        {
            ErroresToken errtok = new ErroresToken(lexema, idToken, linea, columna);
            listaErrores.Add(errtok);
        }

        public void Analizador_cadena(String entrada)
        {
            int estado = 0;
            int columna = 0;
            int fila = 1;
            string lexema = "";
            Char c;
            entrada = entrada + " ";
            for (int i = 0; i < entrada.Length; i++)
            {
                c = entrada[i];
                columna++;
                switch (estado)
                {
                    case 0:
                        //columna++;
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            lexema += c;
                        }
                        else if (Char.IsDigit(c))
                        {
                            estado = 2;
                            lexema += c;
                        }

                        else if (c == '"')
                        {
                            estado = 4;
                            i--;
                            columna--;
                        }
                        else if (c == ',')
                        {
                            estado = 6;
                            i--;
                            columna--;
                        }
                        else if (c == ' ')
                        {
                            estado = 0;
                        }
                        else if (c == '\n')
                        {
                            columna = 0;
                            fila++;
                            estado = 0;
                        }
                        /*nuevos*/
                        else if (c == '{')
                        {
                            lexema += c;
                            addToken(lexema, "llaveIzq", fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '}')
                        {
                            lexema += c;
                            addToken(lexema, "llaveDer", fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '(')
                        {
                            lexema += c;
                            addToken(lexema, "parIzq", fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == ')')
                        {
                            lexema += c;
                            addToken(lexema, "parDer", fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            lexema = "";
                        }

                        else if (c == ';')
                        {
                            lexema += c;
                            addToken(lexema, "Punto y Coma", fila, columna, i - lexema.Length);
                            lexema = "";
                        }

                        else if (c == '<')
                        {
                            lexema += c;
                            addToken(lexema, "Menor", fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '>')
                        {
                            lexema += c;
                            addToken(lexema, "Mayor", fila, columna, i - lexema.Length);
                            lexema = "";
                        }

                        else if (c == '.')
                        {
                            lexema += c;
                            addToken(lexema, "Punto", fila, columna, i - lexema.Length);
                            lexema = "";
                        }

                        /*fin nuevos*/

                        /*operadores mat*/
                        else if (c == '+')
                        {
                            lexema += c;
                            addToken(lexema, "Suma", fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '-')
                        {
                            lexema += c;
                            addToken(lexema, "Menos", fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '*')
                        {
                            lexema += c;
                            addToken(lexema, "Multiplicacion", fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '/')
                        {
                            lexema += c;
                            addToken(lexema, "Division", fila, columna, i);
                            lexema = "";
                        }


                        /*fin operadors mat*/
                        else
                        {
                            estado = -99;
                            i--;
                            columna--;
                        }
                        break;
                    case 1:
                        //if (Char.IsLetter(c))
                        if (Char.IsLetterOrDigit(c) || c == '_')
                        {
                            lexema += c;
                            estado = 1;
                        }
                        else
                        {
                            Boolean encontrado = false;
                            encontrado = Macht_enReser(lexema);
                            if (encontrado)
                            {
                                addToken(lexema, "Reservada", fila, columna, i - lexema.Length);
                            }
                            else
                            {
                                addToken(lexema, "Identificador", fila, columna, i - lexema.Length);
                            }
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;
                    case 2:
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 2;
                        }
                        /*nuevo*/
                        else if (c == '.')
                        {
                            estado = 8;
                            lexema += c;
                        }
                        /*nuevo fin*/
                        else
                        {
                            addToken(lexema, "Digito", fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;
                    case 3:
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 2;
                        }
                        else
                        {
                            estado = -99;
                            i = i - 2;
                            columna = columna - 2;
                            lexema = "";
                        }
                        break;
                    case 4:
                        if (c == '"')
                        {
                            lexema += c;
                            estado = 5;
                        }
                        break;
                    case 5:
                        if (c != '"')
                        {
                            lexema += c;
                            estado = 5;
                        }
                        else
                        {
                            estado = 6;
                            i--;
                            columna--;
                        }
                        break;
                    case 6:
                        if (c == '"')
                        {
                            lexema += c;
                            addToken(lexema, "Cadena", fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            addToken(lexema, "Coma", fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }

                        break;

                    /**inicio nuevo**/
                    case 8:
                        if (Char.IsDigit(c))
                        {
                            estado = 9;
                            lexema += c;
                        }
                        else
                        {
                            addError(lexema, "Se esperaba un digito [" + lexema + "]", fila, columna);
                            estado = 0;
                            lexema = "";
                        }
                        break;
                    case 9:
                        if (Char.IsDigit(c))
                        {
                            estado = 9;
                            lexema += c;
                        }
                        else
                        {
                            addToken(lexema, "Digito", fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }

                        break;
                    /*fin nuevo*/

                    case -99:
                        lexema += c;


                        addError(lexema, "Carácter Desconocido", fila, columna);

                        estado = 0;
                        lexema = "";
                        break;
                }
            }


        }

        public Boolean Macht_enReser(String sente)
        {
            Boolean enco = false;
            for (int i = 0; i < tokens.Count; ++i)
            {
                if (sente.ToString() == tokens[i].ToString())
                {
                    enco = true;
                    estado_token = i;
                    return enco;
                }
                else { enco = false; }

            }
            return enco;
        }
        /*verifica si es tipo*/

        /*reporte de Errores*/
        public void Html_Errores()
        {

            String Contenido_html;
            Contenido_html = "<html>" +
            "<body>" +
            "<h1 align='center'>ERRORES ENCONTRADOS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +
            "<td><strong>No." +
            "</strong></td>" +

            "<td><strong>Error" +
            "</strong></td>" +

            "<td><strong>Descripción" +
            "</strong></td>" +

            "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;

            for (int i = 0; i < listaErrores.Count; i++)
            {
                ErroresToken sen_pos = listaErrores.ElementAt(i);


                tempo_tokens = "";
                tempo_tokens = "<tr>" +
                "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +

                "<td>" + sen_pos.getLexema() +
                "</td>" +

                "<td>" + sen_pos.getIdToken() +
                "</td>" +

                "<td>" + sen_pos.getLinea() +
                "</td>" +

                "<td>" + sen_pos.getColumna() +
                "</td>" +

                "</tr>";
                Cad_tokens = Cad_tokens + tempo_tokens;

            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";
            /*creando archivo html*/
            File.WriteAllText("Reporte de Errores.html", Contenido_html);
            System.Diagnostics.Process.Start("Reporte de Errores.html");


        }

        /*reporte de Tokenss*/
        public void Html_Tokens()
        {
            String Contenido_html;
            Contenido_html = "<html>" +
            "<body>" +
            "<h1 align='center'>LISTADOS DE TOKENS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +
            "<td><strong>No." +
            "</strong></td>" +

            "<td><strong>Lexema" +
            "</strong></td>" +

           "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

             "<td><strong>Token" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;

            for (int i = 0; i < listaTokens.Count; i++)
            {
                Token sen_pos = listaTokens.ElementAt(i);

                tempo_tokens = "";
                tempo_tokens = "<tr>" +
                "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +

                "<td>" + sen_pos.getLexema() +
                "</td>" +

                "<td>" + sen_pos.getLinea() +
                "</td>" +

                "<td>" + sen_pos.getColumna() +
                "</td>" +

                "<td>" + sen_pos.getIdToken() +
                "</td>" +

                "</tr>";
                Cad_tokens = Cad_tokens + tempo_tokens;

            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            /*creando archivo html*/
            File.WriteAllText("Reporte de Tokens.html", Contenido_html);
            System.Diagnostics.Process.Start("Reporte de Tokens.html");


        }
        public void generarLista()
        {
            for (int i = 0; i < listaTokens.Count; i++)
            {
                Token actual = listaTokens.ElementAt(i);
                String mensajeRespuesta = null;
                if (actual.getLexema() != null)
                {
                    mensajeRespuesta = "[Lexema: " + actual.getLexema();
                }
                if (actual.getIdToken() != null)
                {
                    mensajeRespuesta += ", Token: " + actual.getIdToken();
                }
                retorno += mensajeRespuesta + ", Linea: " + actual.getLinea() + "]" + Environment.NewLine;
            }
        }
        public String getRetorno()
        {
            return this.retorno;
        }

        public List<Token> getListaTokens()
        {
            return listaTokens;
        }
    }
}