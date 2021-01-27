Module MdAutomatas
    'decodifica el código y crea la lista de objetos y eventos
    'Public Eventos() As Hecho_simple
    ' Public Conceptos() As Concepto
    Dim Atributos() As noción
    Dim Hechos As List(Of Hecho_simple)
    Dim Objetos As New List(Of Objeto)
    Dim Propiedades As List(Of propiedad)
    Dim Actos As List(Of Acto)
    'Public Objetos() As Objeto
    'Public Actos() As Acto

    Private Class noción
        Property nombre_sistema As String       'nombre único del concepto dentro del sistema
        Property nombres As String              'nombres con que se hace referencia al concepto
        Property esDifuso As Boolean = False    'determina si el concepto es borroso (fuzzy)
        Property cuantificador As String        'determina la extención actual del concepto
        Property EsFinito As Boolean = True     'determina si el concepto es negativo (ej. no-hombre)
        Property Definicion As String           'determina las propiedades esenciales del concepto
        Property EsConjunto As Boolean = False  'determina si el concepto es un conjunto
    End Class
    Private Class Objeto
        Inherits noción
        Property Situación As String
        Property Accidentes As String
        Property EsSingular As Boolean    'determina si el objeto es cuantificable o no
    End Class
    Private Class Hecho_simple
        Property id As Long
        Property Tiempo As String   'presente, pasado o futuro
        Property EsCondicional As Boolean
        Property Sujeto As String
        Property Predicado As Predicado
    End Class

    Private Class Hecho
        Property hechos() As Hecho_simple
        Property Conjuntores As String
    End Class

    Private Class Predicado
        Property Copula As String
        Property Núcleo As String
        Property ObjetoDirecto As String
        Property ObjetoIndirecto As String
        Property Circunstancia As String
    End Class
    Private Class Acto
        Property nombre_sistema As String       'nombre único del concepto dentro del sistema
        Property nombres As String              'nombres con que se hace referencia al concepto
        Property EsTransitiva As Boolean        'determina si la acción es transitiva
    End Class
    Private Class propiedad
        Inherits noción

    End Class

    Sub FromNOVA(s As String)

        'typing: reconocimiento de los elementos de manera individual
        Dim j = Split(s, " ")
        Dim Tip() As String
        ReDim Tip(UBound(j))

        For i = 0 To UBound(j)
            Tip(i) = Elemento(j(i))
        Next

        AUTOM_OBJCOM(s)

    End Sub


    Sub DECO(ByVal s() As String) 'decodificador.
        Dim r() As String
        Dim nuevo_concepto As New noción
        Dim nuevo_objeto As New Objeto
        Dim nuevo_hecho As New Hecho_simple
        Dim nueva_propiedad As New propiedad
        Objetos = New List(Of Objeto)
        Propiedades = New List(Of propiedad)
        Hechos = New List(Of Hecho_simple)
        ' ReDim Conceptos(50)
        ' ReDim Actos(50)
        'ReDim Eventos(50)
        Dim cont As Long = 0
        Dim cont2 As Long = 0
        'Conceptos(0) = nuevo_concepto
        'Eventos(0) = nuevo_hecho

        For i = 0 To UBound(s)
            nuevo_concepto = New Objeto
            nuevo_hecho = New Hecho_simple
            nueva_propiedad = New propiedad

            If Not Trim(s(i)) = "" Then
                'remplazos básicos

                s(i) = Replace(s(i), " es un ", " es&un ")
                s(i) = Replace(s(i), " es una ", " es&una ")
                s(i) = Replace(s(i), " era un ", " era&un ")
                s(i) = Replace(s(i), " era una ", " era&una ")

                s(i) = Replace(s(i), " concepto dif ", " concepto&dif ")
                s(i) = Replace(s(i), " objeto dif ", " objeto&dif ")
                s(i) = Replace(s(i), " acto dif ", " acto&dif ")
                s(i) = Replace(s(i), " propiedad dif ", " propiedad&dif ")
                s(i) = Replace(s(i), " conjunto dif ", " conjunto&dif ")
                s(i) = Replace(s(i), " nombrar en pasado ", " nombrar&en&pasado ")
                s(i) = Replace(s(i), " nombrar en presente ", " nombrar&en&presente ")
                s(i) = Replace(s(i), " nombrar en futuro ", " nombrar&en&futuro ")




                r = Split(s(i), " ")


                'Aumenta la lista de conceptos

                If r(0) = "Ob" Then  'define un concepto
                    If r.Length <> 2 Then
                        Err()
                    Else
                        If Not EsPalabraClave(r(1)) Then
                            nuevo_concepto.nombre_sistema = r(1)
                            Objetos.Add(nuevo_concepto)
                        Else
                            Err()
                        End If

                    End If

                ElseIf r(0) = "Ob&dif" Then
                    If r.Length <> 2 Then
                        Err()
                    Else
                        If Not EsPalabraClave(r(1)) Then
                            nuevo_concepto.nombre_sistema = r(1)
                            nuevo_concepto.esDifuso = True
                            Objetos.Add(nuevo_concepto)
                        Else
                            Err()
                        End If

                    End If

                ElseIf r(0) = "Prop" Then
                    If r.Length <> 2 Then
                        Err()
                    Else
                        If Not EsPalabraClave(r(1)) Then
                            nueva_propiedad.nombre_sistema = r(1)
                            Propiedades.Add(nueva_propiedad)
                        Else
                            Err()
                        End If

                    End If
                ElseIf r(0) = "Prop&dif" Then
                    If r.Length <> 2 Then
                        Err()
                    Else
                        If Not EsPalabraClave(r(1)) Then
                            nuevo_objeto.nombre_sistema = r(1)
                            nuevo_objeto.esDifuso = True
                            Objetos.Add(nuevo_concepto)
                        Else
                            Err()
                        End If

                    End If

                ElseIf r(0) = "dif" Then
                    If r.Length <> 2 Then Err()

                    Dim flag As Boolean = False  'aquí se busca el índice de lista del objeto
                    For j = 0 To Objetos.Count - 1

                        If Objetos(j).nombre_sistema = r(1) Then
                            Objetos(j).esDifuso = True
                            Exit For
                            flag = True
                        End If

                    Next
                    If Not flag Then Err() 'el objeto no fue encontrado

                ElseIf r(0) = "def" Then

                    If Not (r(2) = "es&un" Or r(2) = "es&una") Then Err()
                    For j = 0 To Objetos.Count - 1
                        If Objetos(i).nombre_sistema = r(1) Then
                            Objetos(i).Definicion = r(3)
                            nuevo_hecho.Sujeto = Objetos(i).nombre_sistema
                            nuevo_hecho.Predicado.Núcleo = Objetos(i).nombre_sistema
                            Hechos.Add(nuevo_hecho)
                            Exit For
                        End If
                    Next


                ElseIf EsConcepto(r(0)) And r.Length < 4 Then
                    If r(1) = "es&un" Or r(1) = "es&una" Or r(1) = "es" Then
                        If EsConcepto(r(2)) Then
                            nuevo_hecho.Sujeto = r(0)
                            nuevo_hecho.Predicado = New Predicado
                            nuevo_hecho.Predicado.Núcleo = r(2)
                            Hechos.Add(nuevo_hecho)
                        End If

                    End If

                End If
                'aumenta la lista de hechos

            End If
        Next

    End Sub

    Public Function EsConcepto(ByVal c As String) As Boolean
        'combrueba que la cadena sea el nombre en sistema de un concepto
        Dim res As Boolean = False

        Dim L = ConceposSimples.Find(Function(p) p.nombreID = c)

        If Not L Is Nothing Then 'sí existe el concepto
            If L.categoria = "s" Then res = True
        End If

        Return res
    End Function


    Private Function GetConcepto(ByVal c As String) As noción
        'combrueba que la cadena sea el nombre en sistema de un concepto
        For i = 0 To Objetos.Count
            If Objetos(i).nombre_sistema = c Then
                GetConcepto = Objetos(i)
                Exit Function
            End If
        Next
        GetConcepto = Nothing
    End Function

    Public Function EsObjeto(ByVal c As String) As Integer 'categoría s: devuelve 1 si no es s, -1 si lo es
        'combrueba que la cadena sea el nombre en sistema de un concepto singular (tipo s)
        Dim res As Integer

        Dim L = ConceposSimples.Find(Function(p) p.nombreID = c)

        If Not L Is Nothing Then 'sí existe el concepto
            If L.categoria = "s" Then
                If L.tipo = "s" Then
                    res = -1
                Else
                    res = 1
                End If
            End If
        Else
            res = 0
        End If


        Return res
    End Function

    Public Function EsAccion(ByVal c As String) As Boolean 'categoría tipo h
        Dim res As Boolean = False

        Dim L = ConceposSimples.Find(Function(p) p.nombreID = c)

        If Not L Is Nothing Then 'sí existe el concepto
            If L.categoria = "h" Then res = True
        End If

        Return res
    End Function

    Public Function EsPropiedad(ByVal c As String) As Boolean 'categoría tipo t
        'combrueba que la cadena sea el nombre en sistema de un concepto
        Dim res As Boolean = False

        Dim L = ConceposSimples.Find(Function(p) p.nombreID = c)

        If Not L Is Nothing Then 'sí existe el concepto
            If L.categoria = "t" Then res = True
        End If

        Return res
    End Function

    Public Function EsPalabraClave(ByVal s As String) As Boolean
        Dim pc(9) As String

        pc(0) = "Ob"
        pc(1) = "Sing"
        pc(2) = "acto"
        pc(3) = "es"
        pc(4) = "es&un"
        pc(5) = "es&una"
        pc(6) = "Prop"
        pc(7) = "concepto&dif"
        pc(8) = "objeto&dif"
        pc(9) = "dif"

        For i = 0 To UBound(pc)
            If pc(i) = s Then
                EsPalabraClave = True
                Exit Function
            End If

        Next
        EsPalabraClave = False
    End Function

    Function Elemento(ByVal s As String) As String 'Este es el selector que indica de qué tipo es cada elemento

        If EsObjeto(s) = 1 Then
            Elemento = "OBu" 'OBJETO
        ElseIf EsObjeto(s) = -1 Then
            Elemento = "OBs" 'OBJETO
            'ElseIf EsAccion(s) Then
            '    Elemento = "AC" 'ACCIÓN

        ElseIf EsAccion(s) Then
            'Elemento = "QAC" 'CUANTIFICADOR DE ACCIÓN   mucho, poco, etc.
            Elemento = "AC" 'CUANTIFICADOR DE ACCIÓN   correr, caminar, etc.

        ElseIf EsPropiedad(s) Then
            Elemento = "T" 'PROPIEDAD (ATRIBUTO)  'bueno, rojo, etc.

            'ElseIf EsPropiedad(s) Then
            '    Elemento = "QT" 'CUANTIFICADOR DE PROPIEDAD 'muy, grandemente, totalmente

        ElseIf s = "no-" Then
            Elemento = "NE-" 'NEGADOR II

        ElseIf s = "no" Then
            Elemento = "NE" 'NEGADOR I

        ElseIf s = "es" Then
            Elemento = "COP" 'CÓPULA

        ElseIf s = "todo" Or s = "algún" Or s = "ningún" Or s = "un" Or s = "una" Then
            Elemento = "QUA" 'CUANTIFICADOR  'todo, algún, ningún

        ElseIf s = "y" Or s = "o" Or s = "u" Or s = "e" Then
            Elemento = "CON" 'CONECTOR

        ElseIf s = "que" Then
            Elemento = "QUE" 'concector de predicados

        ElseIf s = "Ob" Then
            Elemento = "INOB"

        ElseIf s = "acto" Then
            Elemento = "INAC"

        ElseIf s = "Prop" Then
            Elemento = "INPR"

        ElseIf s = "var" Then
            Elemento = "INVAR"

        ElseIf s = "def" Then
            Elemento = "DEF"

        ElseIf s = "conjunto" Then
            Elemento = "CONJ"

        ElseIf s = "dif" Then
            Elemento = "DIF"

        Else
            Elemento = "NOID"
        End If

    End Function


#Region "AUTOMATAS DE SINTAXIS"

    Public Function AUTOM_OBJCOM(ByVal s As String, Optional crear_concpto As Boolean = True) As Boolean 'Autómata de reconcoimiento para objetos compuestos
        'DEFINICIÓN DEL AUTÓMATA
        Dim r() = Split(Trim(s), " ")
        Dim q As Long = 0


        For i = 0 To UBound(r)

            q = f_OBJCOM(q, Elemento(r(i)))

        Next

        If q = 4 Or q = 9 Or q = 12 Or q = 18 Then 'estados terminales
            AUTOM_OBJCOM = True
        Else
            AUTOM_OBJCOM = False
        End If

    End Function

    Function f_OBJCOM(ByVal q As Integer, ByVal t As String, Optional Concepto As concepto = Nothing) As Integer
        Dim res As Integer
        res = -1
        If q = 0 Then
            If t = "NE" Then
                res = 1
            ElseIf t = "QUA" Then
                res = 2
            ElseIf t = "OBs" Then 'objeto singular (concepto general tipo s)
                res = 4
            ElseIf t = "PRON" Then 'pronombre (sólo si equivale a singular)
                res = 4
            End If
        ElseIf q = 1 Then '[llevan negador antes]
            If t = "OBs" Then
                res = 4
            ElseIf t = "QUA" Then
                res = 2
            ElseIf t = "PRON" Then
                res = 4
            End If
        ElseIf q = 2 Then '[llevan cuantificador antes]
            If t = "NE" Then
                res = 3
            ElseIf t = "OBu" Then 'objeto universal (concepto general tipo q o c) 
                res = 4
            ElseIf t = "PRON" Then
                res = 4
            End If
        ElseIf q = 3 Then '[llevan negador antes]
            If t = "OBu" Then
                res = 4
            End If
        ElseIf q = 4 Then '+
            If t = "NE" Then
                res = 5
            ElseIf t = "NE-" Then
                res = 8
            ElseIf t = "QT" Then
                res = 6
            ElseIf t = "T" Then
                res = 7 '9?
            ElseIf t = "QUE" Then
                res = 10
            End If
        ElseIf q = 5 Then
            If t = "QT" Then
                res = 6
            End If
        ElseIf q = 6 Then
            If t = "NE-" Then
                res = 8
            ElseIf t = "T" Then
                res = 9
            End If
        ElseIf q = 7 Then
            If t = "T" Then
                res = 9
            ElseIf t = "QT" Then
                res = 6
            ElseIf t = "NE" Then
                res = 5
            ElseIf t = "NE-" Then
                res = 8
            ElseIf t = "QUE" Then
                res = 10
            End If
        ElseIf q = 8 Then
            If t = "T" Then
                res = 9
            End If
        ElseIf q = 9 Then '+
            If t = "CONJ" Then
                res = 7
            ElseIf t = "QUE" Then
                res = 10
            End If
        ElseIf q = 10 Then
            If t = "NE" Then
                res = 11
            ElseIf t = "AC" Then
                res = 12
            ElseIf t = "COP" Then
                res = 19
            End If
        ElseIf q = 11 Then
            If t = "AC" Then
                res = 12
            ElseIf t = "COP" Then
                res = 19
            End If
        ElseIf q = 12 Then '+
            If t = "NE" Then
                res = 17
            ElseIf t = "qAC" Then
                res = 13
            ElseIf t = "NE-" Then
                res = 16
            ElseIf t = "QT" Then
                res = 15
            ElseIf t = "Th" Then
                res = 18
            End If
        ElseIf q = 13 Then
            If t = "NE" Then
                res = 14
            ElseIf t = "qT" Then
                res = 15
            End If
        ElseIf q = 14 Then
            If t = "qT" Then
                res = 15
            End If
        ElseIf q = 15 Then
            If t = "NE-" Then
                res = 16
            ElseIf t = "Th" Then
                res = 18
            End If
        ElseIf q = 16 Then
            If t = "Th" Then
                res = 18
            End If
        ElseIf q = 17 Then
            If t = "qAC" Then
                res = 13
            ElseIf t = "QT" Then
                res = 15
            End If
        ElseIf q = 18 Then '+
            If t = "CONJ" Then
                res = 9
            End If
        ElseIf q = 19 Then
            If t = "T" Then
                res = 9
            ElseIf t = "NE-" Then
                res = 8
            ElseIf t = "QT" Then
                res = 6
            ElseIf t = "NE" Then
                res = 20
            ElseIf t = "QUA" Then
                res = 2
            ElseIf t = "OBs" Then
                res = 4
            ElseIf t = "PRON" Then
                res = 4
            End If
        ElseIf q = 20 Then
            If t = "QUA" Then
                res = 2
            ElseIf t = "OBu" Then
                res = 4
            ElseIf t = "QT" Then
                res = 6
            End If

        End If

        f_OBJCOM = res
    End Function

    Public Function AUTOM_ENUN(ByVal s As String) As Boolean 'Autómata de reconcoimiento para enunciados simples
        'DEFINICIÓN DEL AUTÓMATA
        Dim r() = Split(Trim(s), " ")
        Dim q As Long = 0
        Dim q2 As Long = 0
        Dim qs As Integer = 0
        Dim m As Integer = 0
        Dim t As String = ""

        For i = 0 To UBound(r)
            If qs = 0 Then 'OB
                q2 = f_OBJCOM(q2, Elemento(r(i)))
                If q2 = 4 Or q2 = 9 Or q2 = 12 Or q2 = 18 Then
                    qs = 4
                    t = "OB"
                    m = i
                ElseIf q2 = -1 Then
                    If Not m = 0 Then
                        i = m
                        m = 0
                        t = "OB"
                    Else
                        t = Elemento(r(i))
                    End If
                End If
            ElseIf qs = 1 Then 'con OB y OBU
                q2 = f_OBJCOM(q2, Elemento(r(i)))
                If q2 = 4 Or q2 = 9 Or q2 = 12 Or q2 = 18 Then
                    qs = 4
                    t = "OB"
                    m = i
                ElseIf q2 = -1 Then
                    If Not m = 0 Then
                        i = m
                        m = 0
                        t = "OB"
                    Else
                        t = Elemento(r(i))
                    End If
                End If

            ElseIf qs = 2 Then
            ElseIf qs = 3 Then
            ElseIf qs = 4 Then
                t = Elemento(r(i))
                qs = 5
            ElseIf qs = 5 Then
                If t = "" Then
                    If q = 0 Or q = 5 Or q = 8 Or q = 16 Then   'sólo con OB
                        qs = 0
                    ElseIf q = 2 Then 'con OB y OBU
                        qs = 1
                    ElseIf q = 4 Or q = 14 Then 'con OB y QRT
                        qs = 2
                    ElseIf q = 3 Or q = 7 Or q = 9 Or q = 6 Or q = 10 Or q = 11 Or q = 12 Or q = 15 Then 'con QRT
                        qs = 3
                    Else
                        qs = 4
                    End If

                Else
                    q = f_ENUNCIADO(q, t)
                    t = ""
                End If
            End If
        Next

        If q = 3 Or q = 6 Or q = 7 Or q = 9 Or q = 11 Or q = 14 Or q = 15 Then
            AUTOM_ENUN = True
        Else
            AUTOM_ENUN = False
        End If
    End Function

    Function f_ENUNCIADO(ByVal q As Integer, ByVal t As String) As Integer
        Dim res As Integer
        res = -1
        If q = 0 Then
            If t = "OB" Then
                res = 1
            End If

        ElseIf q = 1 Then
            If t = "NE" Then
                res = 17
            ElseIf t = "COP" Then
                res = 2
            ElseIf t = "ACT" Then
                res = 4
            ElseIf t = "AC" Then
                res = 10
            ElseIf t = "PRONA" Then
                res = 13
            End If
        ElseIf q = 2 Then
            If t = "OB" Then
                res = 3
            ElseIf t = "OBU" Then
                res = 3
            End If
        ElseIf q = 3 Then
            If t = "QTR" Then
                res = 3
            ElseIf t = "CON" Then
                res = 1
            End If
        ElseIf q = 4 Then
            If t = "QTR" Then
                res = 4
            ElseIf t = "A" Then
                res = 5
            ElseIf t = "OB" Then
                res = 7
            End If
        ElseIf q = 5 Then
            If t = "OB" Then
                res = 6
            End If
        ElseIf q = 6 Then
            If t = "QTR" Then
                res = 6
            ElseIf t = "CON" Then
                res = 1
            End If
        ElseIf q = 7 Then
            If t = "QTR" Then
                res = 7
            ElseIf t = "A" Then
                res = 8
            End If
        ElseIf q = 8 Then
            If t = "OB" Then
                res = 9
            End If
        ElseIf q = 9 Then
            If t = "QTR" Then
                res = 9
            ElseIf t = "CON" Then
                res = 1
            End If
        ElseIf q = 10 Then
            If t = "QTR" Then
                res = 11
            End If
        ElseIf q = 11 Then
            If t = "QTR" Then
                res = 11
            ElseIf t = "CON" Then
                res = 12
            End If
        ElseIf q = 12 Then
            If t = "QTR" Then
                res = 11
            ElseIf t = "NE" Then
                res = 17
            ElseIf t = "COP" Then
                res = 2
            ElseIf t = "ACT" Then
                res = 4
            ElseIf t = "AC" Then
                res = 10
            ElseIf t = "PRONA" Then
                res = 13
            End If
        ElseIf q = 13 Then
            If t = "ACT" Then
                res = 14
            End If
        ElseIf q = 14 Then
            If t = "QTR" Then
                res = 14
            ElseIf t = "CON" Then
                res = 1
            ElseIf t = "OB" Then
                res = 15
            ElseIf t = "A" Then
                res = 16
            End If
        ElseIf q = 15 Then
            If t = "QTR" Then
                res = 15
            ElseIf t = "CON" Then
                res = 1
            End If
        ElseIf q = 16 Then
            If t = "OB" Then
                res = 15
            End If
        ElseIf q = 17 Then
            If t = "COP" Then
                res = 2
            ElseIf t = "ACT" Then
                res = 4
            ElseIf t = "AC" Then
                res = 10
            ElseIf t = "PRONA" Then
                res = 13
            End If
        End If
        f_ENUNCIADO = res
    End Function

#End Region


#Region "IA"
    Sub Jarvis()
        Console.WriteLine("Experimental Jarvis-like prototype.")
        Console.WriteLine("Vers. 3.0")
        Console.WriteLine("Salvador D. Escobedo")
        Console.WriteLine("")
        Console.Write("--:")
        Dim R As String
        Dim res As String

        ' Dim i As Long

        Do
            R = Console.ReadLine()
            R = Trim(R)




            '¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡
            ' Console.WriteLine(IAIO("saludo"))
            '¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡



            If R = "hola" Then
                Console.Write("hola")
            End If



            Console.WriteLine("")
            Console.Write("--:")

        Loop Until R = "close"

        Console.WriteLine("Jarvis closed.")
    End Sub

    Class persona
        Property nombre As String
        Property apellido As String
        Property edad As Integer
        Property sexo As String
    End Class

    Function IAIO(index As String, Optional expected As String = "", Optional previous As String = "", Optional emotion As String = "") As String
        Dim r As String = ""
        'definir el tipo de respuesta


        'Experimento no. 1, respuestas básicas.
        ' sí, no, todo bien, no te preocupes, gracias, por favor, de nada.

        'Objetivo principal: interrogar al usuario para saber quién es, sin molestarlo.
        'pasos del programa



        Select Case index

            Case "iniciar conversación"

                'la conversación arranca de acuerdo con las circunstancias
                'caso 1. Se inicia una conversación después de no haber hablando con nadie.
                     'tratar de reconocer al usuario
                     'si es desconocido proceder a la indagación
                     'si es conocido continuar procesos de relación social

                'caso 2. El usuario vueleve de una ausencia (reinicio de conversación).
                     'retomar la conversación de acuerdo con el tiempo

                'caso 3. Se inicia una conversación derivada (otro usuario)
                     'realizar los pasos del caso 1, pero subordinados

            Case "conversar"
                'mantener la conversación, continuar con memoria de conversaciones anteriores
                'caso 1. estado = deseo de indagación. 
                'caso 2. Informar, situaciones que deben alertarse o recordatorios.
                'caso 3. Ejecución de ordenes y peticiones del usuario

            Case "terminar conversación"
                'la conversación termina de acuerdo con las circunstancias.
                'caso 1. Despedida (se da por terminada la conversación, el usuario se ausentará por tiempo indefinido, pero con probabilidades de regresar en el tiempo usual)
                'caso 2. Ausencia forzada. El usuario se ha ausentado sin avisar por un tiempo suficientemente largo
                'caso 3. Despedida definitiva, el usuario se ausentará de manera definitiva
                'caso 4. Ausencia breve, el usuario vuelve enseguida.
        End Select







        'If s Like "F*" Then 'informaciones puras
        '    s = Mid(s, 2)
        '    s = IAIO(s, "", "", "")
        'ElseIf s Like "I*" Then 'instrucciones

        'ElseIf s Like "*[?]" Then 'preguntas

        'ElseIf s Like "O*" Then 'órdenes

        'ElseIf s Like "" Then

        'Else
        '    r = "no entiendo"
        'End If


        Return r
    End Function

    Sub CuestionarNombre(state As String, emotion As String)
        Dim p As New persona
        Select Case state
            Case "saludo"
                'saludar
                Console.WriteLine("Hola")
                If Console.ReadLine() = "hola" Then
                    emotion = ":)"
                    state = "preguntar nombre"
                Else
                    emotion = ":|"

                End If
                Console.WriteLine(emotion)
                IAIO(state)
            Case "preguntar nombre"
                'indagar nombre
                Console.WriteLine("cuál es tu nombre?")
                p.nombre = Console.ReadLine()
                If Not p.nombre = "" Then
                    Console.WriteLine("es un placer " & p.nombre)
                    emotion = ":)"
                    state = "preguntar apellido"
                Else
                    emotion = ":|"

                End If
                Console.WriteLine(emotion)
                IAIO(state)
            Case "preguntar apellido"
                'indagar nombre
                Console.WriteLine("como te apellidas?")
                p.nombre = Console.ReadLine()
                If Not p.nombre = "" Then
                    Console.WriteLine("es un placer " & p.nombre)
                    emotion = ":)"
                    state = "preguntar sexo"
                Else
                    emotion = ":|"

                End If
                Console.WriteLine(emotion)
                IAIO(state)
            Case "preguntar sexo"
                'indagar nombre
                Console.WriteLine("eres hombre o mujer?")
                If Console.ReadLine() = "hombre" Then
                    emotion = ":)"
                    p.sexo = "hombre"
                    state = "preguntar edad"
                ElseIf Console.ReadLine() = "mujer" Then
                    emotion = ":)"
                    p.sexo = "mujer"
                    state = "preguntar edad"
                Else
                    emotion = ":|"

                End If
                Console.WriteLine(emotion)
                IAIO(state)
            Case "preguntar edad"
                'indagar edad
                Console.WriteLine("qué edad tienes?")
                p.edad = Console.ReadLine()
                If p.edad > 1 And p.edad < 150 Then
                    emotion = ":)"
                    state = "despedida"
                Else
                    emotion = ":|"

                End If
                Console.WriteLine(emotion)
                IAIO(state)
            Case "despedida"
                'despedirse
                'saludar
                Console.WriteLine("Adiós")
                If Console.ReadLine() = "Adiós" Then
                    emotion = ":)"
                    state = ""
                Else
                    emotion = ":|"

                End If
                Console.WriteLine(emotion)
                IAIO(state)

        End Select
    End Sub


#End Region

End Module
