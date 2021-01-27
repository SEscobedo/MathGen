Imports System.Text
Imports System.IO

Module Module1

    Dim modo As String = "" 'modo de operación: normal = "",  matemática = "M", historia = "H".

    Sub Main()


        Dim s As String
        Dim flag As Boolean = True
        Console.Title = "NOVA"
        'Console.OutputEncoding = System.Text.Encoding.UTF8
        'Console.BackgroundColor = ConsoleColor.Blue
        Console.WriteLine("NOVA experimental lenguaje proyect. 26/06/2016. By Salvador D. Escobedo")
        Console.WriteLine("Version 1.0")
        Fill() 'cargar conceptos logicos fundamentales
        Console.WriteLine("Conceptos fundamentales cargados")
        CargarConceptos()
        Console.WriteLine("")

        Console.Write(modo & ">> ")
        Dim R As String

        Do
            R = Console.ReadLine()
            R = Trim(R)

            If R = "clear" Then 'limpiar la pantalla
                Console.Clear()


            ElseIf R = "concepto" Or R = "c" Then 'crear concepto

                CrearConcepto()

            ElseIf R = "lista conceptos" Or R = "lista c" Then 'listar conceptos registrados

                If modo = "" Then
                    If ConceposSimples.Count = 0 Then
                        Console.WriteLine("No hay conceptos simples registrados")
                    Else
                        Console.WriteLine("")
                        For i = 0 To ConceposSimples.Count - 1
                            Console.WriteLine(Str(i) & " " & ConceposSimples(i).nombreID)
                        Next
                        Console.WriteLine("")
                    End If
                ElseIf modo = "M" Then 'modo matemático
                    If conceptosMat.Count = 0 Then
                        Console.WriteLine("No hay conceptos simples registrados")
                    Else
                        Console.WriteLine("")
                        For i = 0 To conceptosMat.Count - 1
                            Console.WriteLine(Str(i) & " " & conceptosMat(i).nombreID)
                        Next
                        Console.WriteLine("")
                    End If
                End If


            ElseIf R = "borrar concepto" Then

                BorrarConcepto()


            ElseIf R = "cargar conceptos" Then

                CargarConceptos()

            ElseIf R Like "analizar concepto *" Then
                R = Mid(R, Len("analizar concepto "))
                AnalizarConcepto(R)

            ElseIf R = "cargar diccionarios" Then

                CargarDiccionario("portuguese.dat")
                CargarDiccionario("english.dat")

            ElseIf R = "guardar conceptos" Then

                Dim FileNumber As Integer = FreeFile()
                FileOpen(FileNumber, "conceptos.dat", OpenMode.Output)
                For Each Item As concepto.simple In ConceposSimples
                    PrintLine(FileNumber, Item.CodeStr)
                Next
                FileClose(FileNumber)
                Console.WriteLine("conceptos guardados en conceptos.dat")




            ElseIf R = "cargar sentencias" Or R = "cargar s" Then
                Dim filename As String = ""

                If modo = "" Then
                    filename = "sentencias.dat"
                ElseIf modo = "M" Then
                    filename = "sentenciasMat.dat"
                End If

                Try
                    ' Open the file using a stream reader.
                    Using sr As New StreamReader(filename, Encoding.UTF8)
                        Dim line As String
                        ' Read the stream to a string and write the string to the console.
                        line = sr.ReadToEnd()
                        Dim j() As String = Split(line, vbCrLf)

                        If modo = "" Then
                            For i = 0 To UBound(j) - 1
                                Dim c As New sentencia
                                c.FromCodeStr(j(i))
                                Sentencias.Add(c)
                            Next
                        ElseIf modo = "M" Then
                            For i = 0 To UBound(j) - 1
                                Dim c As New sentenciaComp
                                c.FromCodeStr(j(i))
                                SentenciasComp.Add(c)
                            Next
                        End If



                    End Using
                    Console.WriteLine("sentencias cargadas")

                Catch e As Exception
                    Console.WriteLine("The file could not be read:")
                    Console.WriteLine(e.Message)
                End Try



            ElseIf R = "prop" Or R = "p" Then
                Console.Write("proposición (cu, S, cópula, P): ")
                s = Console.ReadLine()
                CrearSentencia(s)



            ElseIf R = "borrar sentencia" Then

                BorrarSentencia()

            ElseIf R = "borrar lista sentencias" Then

                Sentencias.Clear()

            ElseIf R = "forma canónica" Then

                If Sentencias.Count = 0 Then
                    Console.WriteLine("No hay sentencias registradas")
                Else
                    Console.WriteLine(Canonic(Sentencias.Last).Spanish())
                End If


            ElseIf R = "lista sentencias" Or R = "lista s" Then

                If modo = "" Then
                    If Sentencias.Count = 0 Then
                        Console.WriteLine("No hay sentencias registradas")
                    Else
                        Console.WriteLine("")
                        For i = 0 To Sentencias.Count - 1
                            Console.WriteLine(Str(i) & " " & Sentencias(i).Spanish)
                        Next
                        Console.WriteLine("")
                    End If
                ElseIf modo = "M" Then
                    If SentenciasComp.Count = 0 Then
                        Console.WriteLine("No hay sentencias registradas")
                    Else
                        Console.WriteLine("")
                        For i = 0 To SentenciasComp.Count - 1
                            Console.WriteLine(Str(i) & " " & SentenciasComp(i).Spanish)
                        Next
                        Console.WriteLine("")
                    End If
                End If

            ElseIf R = "lista sentencias código" Then

                If Sentencias.Count = 0 Then
                    Console.WriteLine("No hay sentencias registradas")
                Else
                    Console.WriteLine("")
                    For i = 0 To Sentencias.Count - 1
                        Console.WriteLine(Str(i) & " " & Sentencias(i).CodeStr)
                    Next
                    Console.WriteLine("")
                End If

            ElseIf R = "lista sentencias inglés" Then

                If Sentencias.Count = 0 Then
                    Console.WriteLine("No hay sentencias registradas")
                Else
                    Console.WriteLine("")
                    For i = 0 To Sentencias.Count - 1
                        Console.WriteLine(Str(i) & " " & Sentencias(i).English)
                    Next
                    Console.WriteLine("")
                End If

            ElseIf R = "lista sentencias portugués" Then

                If Sentencias.Count = 0 Then
                    Console.WriteLine("No hay sentencias registradas")
                Else
                    Console.WriteLine("")
                    For i = 0 To Sentencias.Count - 1
                        Console.WriteLine(Str(i) & " " & Sentencias(i).Portuguese)
                    Next
                    Console.WriteLine("")
                End If

            ElseIf R = "lista sentencias latex" Then
                If modo = "M" Then
                    If SentenciasComp.Count = 0 Then
                        Console.WriteLine("No hay sentencias registradas")
                    Else
                        Console.WriteLine("")
                        For i = 0 To SentenciasComp.Count - 1
                            Console.WriteLine(Str(i) & " " & SentenciasComp(i).Spanish(True))
                        Next
                        Console.WriteLine("")
                    End If
                End If
            ElseIf R = "guardar sentencias" Then


                Try

                    Dim filename As String = ""
                    If modo = "" Then
                        filename = "sentencias.dat"
                    ElseIf modo = "M" Then
                        filename = "sentenciasMat.dat"
                    End If

                    Dim FileNumber As Integer = FreeFile()
                    FileOpen(FileNumber, filename, OpenMode.Output)

                    If modo = "" Then
                        For Each Item As sentencia In Sentencias
                            PrintLine(FileNumber, Item.CodeStr)
                        Next
                    ElseIf modo = "M" Then
                        For Each Item As sentenciaComp In SentenciasComp
                            PrintLine(FileNumber, Item.CodeStr)
                        Next
                    End If

                    For Each Item As sentencia In Sentencias
                        PrintLine(FileNumber, Item.CodeStr)
                    Next

                    FileClose(FileNumber)
                    Console.WriteLine("sentencias guardadas en " & filename)
                Catch ex As Exception
                    Console.WriteLine(Err.Description)
                End Try


            ElseIf R = "borrar sentencias" Or R = "clear s" Then
                Sentencias.Clear()
                Console.WriteLine("Se ha borrado la lista de sentencias")



#Region "TEORÍAS AXIOMATICAS"
                '=================================================================================================
                'CAPTURA DE TEORÍAS AXIOMÁTICAS
                '=================================================================================================
            ElseIf R = "crear teoría" Then 'GENERAR TEORÍA AUTOMÁTICAMENTE ***************************************
                    CrearTeoría()

                ElseIf R = "axioma" Or R = "axiom" Then 'AXIOMAS *****************************************************
                    Dim p As New sentenciaComp
                    Console.Write("enunciado: ")
                    s = Console.ReadLine()
                    p.fromNOVA(s)
                    '  TeoríaEnCurso.axiomas.Add(p)
                    Console.WriteLine("axioma guardado")

                ElseIf R = "lista axiomas" Then
                    If TeoríaEnCurso.axiomas.Count = 0 Then
                        Console.WriteLine("No hay axiomas registrados")
                    Else
                        Console.WriteLine("")
                        For i = 0 To TeoríaEnCurso.axiomas.Count - 1
                            Console.WriteLine(i & " " & TeoríaEnCurso.axiomas(i).sentencia.Spanish)
                        Next
                        Console.WriteLine("")
                    End If

                ElseIf R = "teorema" Or R = "theorem" Then 'TEOREMAS *****************************************************
                    Dim p As New sentenciaComp
                    Console.Write("enunciado: ")
                    s = Console.ReadLine()
                    p.fromNOVA(s)
                    '  TeoríaEnCurso.teoremas.Add(p)
                    Console.WriteLine("teorema guardado")

                ElseIf R = "lista teoremas" Then
                    If TeoríaEnCurso.teoremas.Count = 0 Then
                        Console.WriteLine("No hay teoremas registrados")
                    Else
                        Console.WriteLine("")
                        For i = 0 To TeoríaEnCurso.teoremas.Count - 1
                            Console.WriteLine(i & " " & TeoríaEnCurso.teoremas(i).sentencia.Spanish)
                        Next
                        Console.WriteLine("")
                    End If

                ElseIf R = "def" Or R = "definición" Then 'DEFINICIONES *****************************************************
                    Dim p As New sentenciaComp
                    Console.Write("enunciado: ")
                    s = Console.ReadLine()
                    p.fromNOVA(s)
                    '       TeoríaEnCurso.definiciones.Add(p)
                    Console.WriteLine("definición guardada")

                ElseIf R = "lista definiciones" Then
                    If TeoríaEnCurso.definiciones.Count = 0 Then
                        Console.WriteLine("No hay definiciones registradas")
                    Else
                        Console.WriteLine("")
                        For i = 0 To TeoríaEnCurso.definiciones.Count - 1
                            Console.WriteLine(i & " " & TeoríaEnCurso.definiciones(i).sentencia.Spanish)
                        Next
                        Console.WriteLine("")
                    End If

                ElseIf R = "prueba" Or R = "proof" Then 'PRUEBAS *****************************************************

                    'Ir capturando sentencia por sentencia
                    Dim K As Long = 1
                    Dim p As premisa
                    Dim a As New argumentación

                    Do
                        Console.Write("(" & K & "): ")
                        s = Console.ReadLine()
                        If s = "end" Or s = "fin" Then
                            Exit Do
                        Else
                            p = New premisa
                            '    p.fromNOVA(s)
                            a.premisas.Add(p)
                            K += 1
                        End If
                    Loop

                    TeoríaEnCurso.demostraciones.Add(a)
                    '=================================================================================================
#End Region

#Region "CREACIÓN DE LIBROS"

                ElseIf R = "crear libro" Then
                    Dim L As New libro
                    Console.Write("título: ")
                    L.título = Console.ReadLine()
                    Console.Write("Autor: ")
                    L.autor = Console.ReadLine()

                    Try
                        ' Open the file using a stream writer.
                        Using sw As StreamWriter = New StreamWriter(L.título & ".tex", True, System.Text.Encoding.UTF8)
                        s = L.Spanish(True, modo)
                        Dim j() = Split(s, vbCrLf)
                            For Each Item As String In j
                                sw.Write(Item)
                            Next
                            sw.Close()
                        End Using
                        Console.WriteLine("Libro guardado en " & L.título & ".tex")

                    Catch e As Exception
                        Console.WriteLine("The file could not be read:")
                        Console.WriteLine(e.Message)
                    End Try

#End Region

#Region "MANIPULACIÓN LÓGICA DE SENTENCIAS"

                ElseIf R Like "convertir: *" Then
                    s = Trim(Mid$(R, Len("convertir: ")))

                    Console.WriteLine("---> " & Conversio(GetSent(s, False)).Spanish)
                    Console.WriteLine("")

                ElseIf R = "luego" Or R = "por tanto" Then 'obtiene una deducción a partir de las dos ultimas premisas


                    If Sentencias.Count > 1 Then
                        Dim L As New List(Of sentencia)
                        L.Add(Sentencias(Sentencias.Count - 2))
                        L.Add(Sentencias(Sentencias.Count - 1))

                        Dim D = Deduce(L)

                        L = New List(Of sentencia)

                        Console.WriteLine("")
                        For i = 0 To D.Count - 1
                            Console.WriteLine(i & " " & D(i).Spanish)
                        Next
                        Console.WriteLine("")

                        '    Try
                        '        Dim p1 As sentencia = Sentencias(Sentencias.Count - 1)
                        '        Dim p2 As sentencia = Sentencias(Sentencias.Count - 2)

                        '        s = Razonamiento(p1, p2).Spanish
                        '        Console.WriteLine("")
                        '        Console.WriteLine("*********************************************")
                        '        Console.WriteLine("  " & s & vbLf & "(" & Reporte & ")")


                        '        p2 = Sentencias(Sentencias.Count - 1)
                        '        p1 = Sentencias(Sentencias.Count - 2)
                        '        s = Razonamiento(p1, p2).Spanish
                        '        Console.WriteLine("")
                        '        Console.WriteLine("  " & s & vbLf & "(" & Reporte & ")")
                        '        Console.WriteLine("*********************************************")
                        '        Console.WriteLine("")

                        '    Catch ex As Exception
                        '        Console.WriteLine(Err.Description)
                        '    End Try
                    Else
                        Console.WriteLine("Nada se sigue")
                    End If



                ElseIf R = "deduce" Then
                    If modo = "" Then
                        Console.Write("Índice de la premisa mayor: ")
                        Dim num1 As Long = Console.ReadLine()
                        Console.Write("Índice de la premisa menor: ")
                        Dim num2 As Long = Console.ReadLine()

                        Dim p1 As sentencia = Sentencias(num1)
                        Dim p2 As sentencia = Sentencias(num2)

                        s = Razonamiento(p1, p2).Spanish
                        Console.WriteLine("")
                        Console.WriteLine("*********************************************")
                        Console.WriteLine(p1.Spanish & vbLf & p2.Spanish & vbLf & "Luego " & s & vbLf & " ->" & Reporte)
                        Reporte = ""
                        Console.WriteLine("*********************************************")
                        Console.WriteLine("")

                    ElseIf modo = "M" Then
                        Console.Write("Índice de la premisa mayor: ")
                        Dim num1 As Long = Console.ReadLine()
                        Console.Write("Índice de la premisa menor: ")
                        Dim num2 As Long = Console.ReadLine()
                        Try
                            Dim p1 As sentenciaComp = SentenciasComp(num1)
                            Dim p2 As sentenciaComp = SentenciasComp(num2)

                            s = RazonamientoH(p1, p2).Spanish
                            Console.WriteLine("")
                            Console.WriteLine("*********************************************")
                            If s = "" Then s = "nada se sigue"

                            Console.WriteLine(p1.Spanish & vbLf & p2.Spanish & vbLf & "Luego " & s & vbLf & " ->" & Reporte)

                            Reporte = ""
                            Console.WriteLine("*********************************************")
                            Console.WriteLine("")

                        Catch ex As Exception
                            Console.WriteLine(Err.Description)
                        End Try

                    End If



                ElseIf R = "expandir sentencias" Or R = "expandir s" Then

                    '1. Deducir sentencias
                    DED = ExpandirLista(Sentencias)
                    Dim M As New List(Of sentencia)

                    'Escribir los datos en pantalla
                    If DED.Count = 0 Then
                        Console.WriteLine("La lista es nula")
                    Else
                        Console.WriteLine("")
                        For i = 0 To DED.Count - 1
                            Console.WriteLine(Str(i) & " " & DED(i).Spanish)
                        Next
                        Console.WriteLine("")
                        Sentencias = DED
                    End If

                ElseIf R = "contraer sentencias" Or R = "contraer s" Then

                    '1. Generar axiomas
                    DED = ContraerLista(Sentencias)
                    Dim M As New List(Of sentencia)

                    'Escribir los datos en pantalla
                    If DED.Count = 0 Then
                        Console.WriteLine("La lista es nula")
                    Else
                        Console.WriteLine("")
                        For i = 0 To DED.Count - 1
                            Console.WriteLine(Str(i) & " " & DED(i).Spanish)
                        Next
                        Console.WriteLine("")
                        Sentencias = DED
                    End If

                ElseIf R = "analizar sentencias" Or R = "analizar s" Then
                    If modo = "" Then
                        Try
                            '1. Deducir sentencias
                            DED = Deduce(Sentencias, True)
                            Dim M As New List(Of sentencia)
                            M.AddRange(Sentencias)
                            M.AddRange(DED)

                            '2. Revisar consistencia
                            If InconsistentList(M) Then
                                Console.WriteLine("")
                                Console.WriteLine("La lista de sentencias es inconsistente")
                            End If

                        Catch ex As Exception
                            Console.WriteLine(Err.Description)
                        End Try

                        'Escribir los datos en pantalla
                        If DED.Count = 0 Then
                            Console.WriteLine("Nada se sigue de la lista")
                        Else
                            Console.WriteLine("")
                            For i = 0 To DED.Count - 1
                                Console.WriteLine(Str(i) & " " & DED(i).Spanish)
                            Next
                            Console.WriteLine("")
                        End If
                    ElseIf modo = "M" Then
                        Try
                            '1. Deducir sentencias
                            DEDComp = DeduceH(SentenciasComp)
                            Dim M As New List(Of sentenciaComp)
                            M.AddRange(SentenciasComp)
                            M.AddRange(DEDComp)

                            '2. Revisar consistencia
                            'If InconsistentList(M) Then
                            '    Console.WriteLine("")
                            '    Console.WriteLine("La lista de sentencias es inconsistente")
                            'End If

                        Catch ex As Exception
                            Console.WriteLine(Err.Description)
                        End Try

                        'Escribir los datos en pantalla
                        If DEDComp.Count = 0 Then
                            Console.WriteLine("Nada se sigue de la lista")
                        Else
                            Console.WriteLine("")
                            For i = 0 To DEDComp.Count - 1
                                Console.WriteLine(Str(i) & " " & DEDComp(i).Spanish)
                            Next
                            Console.WriteLine("")
                        End If
                    End If


                ElseIf R Like "analizar sentencias #" Then
                    Dim k As Integer = Mid(R, Len(R) - 1)

                    Dim L As New List(Of sentencia)

                    Try

                        '1. Deducir sentencias
                        L = Analisis(Sentencias, k)
                        Dim M As New List(Of sentencia)
                        M.AddRange(Sentencias)
                        M.AddRange(L)

                        '2. Revisar consistencia
                        If InconsistentList(M) Then
                            Console.WriteLine("")
                            Console.WriteLine("La lista de sentencias es inconsistente")
                        End If

                    Catch ex As Exception
                        Console.WriteLine(Err.Description)
                    End Try

                    If L.Count = 0 Then
                        Console.WriteLine("Nada se sigue de la lista")
                    Else
                        Console.WriteLine("")
                        For i = 0 To L.Count - 1
                            Console.WriteLine(Str(i) & " " & L(i).Spanish)
                        Next
                        Console.WriteLine("")
                    End If

                ElseIf R = "agregar deducciones" Then
                    Dim cont As Long = 0
                    For Each item As sentencia In DED
                        If Not EnLista(item, Sentencias) Then
                            Sentencias.Add(item)
                            cont += 1
                        End If
                    Next

                    Console.WriteLine("Las deducciones se han agredado a la lista de sentencias sin repeticiones")
                    Console.WriteLine("Nuevas sentencias agregadas: " & cont)
                    Console.WriteLine("")

                ElseIf R = "revisar consistencia" Then
                    Dim L As New List(Of sentencia)
                    Try
                        L = Inconsistencias(Sentencias)

                        If Not L.Count = 0 Then 'la lista es inconsistente
                            Console.WriteLine("")
                            Console.WriteLine("La lista de sentencias es inconsistente")
                            Console.WriteLine("")
                            For i = 0 To L.Count - 1
                                Console.WriteLine(i & " " & L(i).Spanish)
                            Next
                            Console.WriteLine("")
                        Else
                            Console.WriteLine("")
                            Console.WriteLine("No se encontraron inconsistencias")
                        End If

                    Catch ex As Exception
                        Console.WriteLine(Err.Description)
                    End Try


                    Console.WriteLine("")

#End Region

                ElseIf R Like "*[?]" Then
                    'generar proposición
                    s = Trim(Mid$(R, 1, Len(R) - 1))

                    Try
                        Dim p As sentencia = GetSent(s, False)
                        p = Canonic(p)

                        If SeDeduceSeparado(p, Sentencias) Or SeDeduce(p, Sentencias) Then
                            Console.WriteLine("Así es, " & p.Spanish)
                        ElseIf SeDeduce(Neg(p), Sentencias) Then
                            Console.WriteLine("Falso")
                        Else
                            Console.WriteLine("No lo sé")
                        End If

                    Catch ex As Exception
                        Console.WriteLine(Err.Description)
                    End Try

                    Console.WriteLine("")
                ElseIf R = "lista compuestas" Then
                    ListaSentenciasComp()

                ElseIf R Like ":*" Then
                    CrearSentenciaComp(R)

#Region "ANÁLISIS DE LENGUAJE"

                    '***********************************************************************
                    'FUNCIONES DE ANÁLISIS DE LENGUAJE
                    '--> suponen el módulo MdGramáticaEspañola
                ElseIf R = "modo normal" Or R = "modo n" Then
                    modo = ""
                    Console.WriteLine("Se ha establecido el modo de lenguaje a normal")
                    Console.WriteLine("")

                ElseIf R = "modo matemática" Or R = "modo m" Then
                    modo = "M"
                    Console.WriteLine("Se ha establecido el modo de lenguaje a matemático")
                    Console.WriteLine("")

                ElseIf R = "conjugar" Or R Like "conjugar *" Then 'pregunta un verbo y lo conjuga


                    If Not R = "conjugar" Then
                        Console.Write("Verbo a conjugar: ")
                        R = Trim(Mid(R, Len("conjugar ")))
                        s = Console.ReadLine()
                        Console.WriteLine(Verbo(s, R))
                    Else
                        Console.Write("Verbo a conjugar: ")
                        s = Console.ReadLine()
                        For i = 1 To 42
                            Console.WriteLine(Verbo(s, i))
                        Next
                    End If


                ElseIf R = "singular" Then 'convierte una palabra en plural a su original
                    Console.Write("sustantivo plural: ")
                    s = Console.ReadLine()
                    Console.WriteLine(Singular(s))

                ElseIf R = "plural" Then 'da el plural de una palabra
                    Console.Write("sustantivo plural: ")
                    s = Console.ReadLine()
                    Console.WriteLine(PluralPor(s))

                ElseIf R = "revisar plural" Then 'da el plural de una palabra

                    For i = 0 To ConceposSimples.Count - 1
                        If Not ConceposSimples(i).categoria = "h" Then
                            Dim w As Boolean = ConceposSimples(i).nombreID = Singular(Plural(ConceposSimples(i).nombreID))

                            If Not w Then
                                s = i & " " & ConceposSimples(i).nombreID & vbTab & vbTab & Plural(ConceposSimples(i).nombreID) & vbTab & vbTab & Singular(Plural(ConceposSimples(i).nombreID))
                                Console.WriteLine(s)
                            End If

                        End If
                    Next

                    'REVISION DE AUTÓMATAS********************
                ElseIf R = "autómata" Then 'da el plural de una palabra
                    Console.Write("texto: ")
                    s = Console.ReadLine()
                    Console.WriteLine(AUTOM_OBJCOM(s))
                    Console.Write("")

                ElseIf R = "es objeto" Then 'dice un concepto es tipo "S" o singular
                    Console.Write("texto: ")
                    s = Console.ReadLine()
                    Console.WriteLine(EsObjeto(s))
                    Console.Write("")
                ElseIf R = "es acción" Then 'dice un concepto es tipo "S" o singular
                    Console.Write("texto: ")
                    s = Console.ReadLine()
                    Console.WriteLine(EsAccion(s))
                    Console.Write("")
                ElseIf R = "Psplit" Then 'revisa el orden de los paréntesis
                    Console.Write("texto: ")
                    s = Console.ReadLine()
                    Console.WriteLine(POmitir(FF(s)))
                    Console.Write("")
#End Region

                ElseIf R = "" Then '*******************************************************

                Else
                    If modo = "" Then
                    CrearSentencia(R)
                ElseIf modo = "M" Then
                    CrearSentenciaComp(R)
                End If

                Console.WriteLine("")
            End If
            Console.Write(modo & ">> ")

        Loop Until R = "close"

        Console.WriteLine("Fin del programa")
    End Sub

    'Construye una sentencia y la incluye en la lista (opcional)
    Function GetProp(st As String, Optional Incluir_en_lista As Boolean = True) As sentencia
        'se buscan por separación de espacios en el orden siguiente CU, S, Copula, P
        'prever negaciones; siempre con un no antes de la palabra
        st = " " & st
        st = Replace$(st, " no ", " no,")
        st = Trim(st)

        Dim j() As String = Split(st, " ")
        Dim flag As Boolean = True
        Dim Negación(UBound(j)) As Boolean
        Dim contraction As Long = 0

        'determinar negaciones
        If j(0) = "ningún" Then j(0) = "no,x@"

        For i = 0 To UBound(j)
            If j(i) Like "no,*" Then
                Negación(i) = False
                j(i) = Replace$(j(i), "no,", "")
            Else
                Negación(i) = True
            End If
        Next

        'buscar el cuantificador 
        Dim s As New sentencia
        Dim lista As New List(Of concepto.simple)
        If j(0) = "todo" Then j(0) = "@"
        If j(0) = "algún" Then j(0) = "x@"


        lista = ConceposSimples.FindAll(Function(p) p.nombreID = j(0))
        If Not lista.Count = 0 Then
            'concepto encontrado. Es cuantificador?
            If lista(0).tipo = "c" Then
                'cuantificador encontrado
                If Not Negación(0) Then
                    s.CU = Neg(G(lista(0)))
                Else
                    s.CU = G(lista(0))
                End If
            Else 'concepto encontrado pero no es cuantificador
                s.CU = todo 'las sentencias sin cuantificador son de sujeto singular, asúmase como universal
                contraction += 1
            End If


        Else
            Console.WriteLine("No ha sido encontrado el cuantificador " & j(0))
            s = ErrCon
            flag = False
        End If

        lista = ConceposSimples.FindAll(Function(p) p.nombreID = j(1 - contraction))
        If Not lista.Count = 0 Then
            'sujeto encontrado
            If Not Negación(1 - contraction) Then
                s.S = Neg(G(lista(0)))
            Else
                s.S = G(lista(0))
            End If

        Else
            Console.WriteLine("No ha sido encontrado el sujeto " & j(1 - contraction))
            s = ErrCon
            flag = False
        End If

        lista = ConceposSimples.FindAll(Function(p) p.nombreID = j(2 - contraction))
        If Not lista.Count = 0 Then
            'concepto encontrado asumir como cópula?
            'si hay un cuarto elemento asumir como cópula

            'cópula encontrada
            If Not Negación(2 - contraction) Then
                s.Copula = Neg(G(lista(0)))
            Else
                s.Copula = G(lista(0))
            End If

        Else
            Console.WriteLine("No ha sido encontrada la copula " & j(2 - contraction))
            s = ErrCon
            flag = False
        End If

        lista = ConceposSimples.FindAll(Function(p) p.nombreID = j(3 - contraction))
        If Not lista.Count = 0 Then
            'predicado encontrado
            If Not Negación(3 - contraction) Then
                s.P = Neg(G(lista(0)))
            Else
                s.P = G(lista(0))
            End If



        Else
            Console.WriteLine("No ha sido encontrado el predicado " & j(3 - contraction))
            s = ErrCon
            flag = False
        End If

        s.tipo = s.getTipo()
        If flag And Incluir_en_lista Then Sentencias.Add(s)

        Return s
    End Function

    Function GetSent(st As String, Optional Incluir_en_lista As Boolean = True) As sentencia 'From NOVA para sentencias simples
        Dim r As New sentencia
        Dim secuencia As New List(Of concepto.simple)
        Dim lista As New List(Of concepto.simple)

        'se buscan por separación de espacios en el orden siguiente CU, S, Copula, P
        'prever negaciones; siempre con un no antes de la palabra
        st = " " & st
        st = Replace$(st, " son ", " es ") 'la cópula no distingue el número
        st = Replace$(st, " no ", " no,")
        st = Replace$(st, " y ", " ")
        st = Trim(st)

        Dim j() As String = Split(st, " ")
        Dim flag As Boolean = True
        Dim Negación(UBound(j)) As Boolean

        'retirar negadores de la fórmula
        If j(0) = "ningún" Then j(0) = "no,x@"
        If j(0) = "ningun" Then j(0) = "no,x@"
        If j(0) = "ninguna" Then j(0) = "no,x@"

        For i = 0 To UBound(j)
            If j(i) Like "no,*" Then
                Negación(i) = False
                j(i) = Replace$(j(i), "no,", "")
            Else
                Negación(i) = True
            End If
        Next

        If j(0) = "todo" Then j(0) = "@"
        If j(0) = "toda" Then j(0) = "@"
        If j(0) = "algún" Then j(0) = "x@"
        If j(0) = "algun" Then j(0) = "x@"
        If j(0) = "alguna" Then j(0) = "x@"

        For i = 0 To UBound(j)
            Dim v As Long = i
            lista = ConceposSimples.FindAll(Function(p) p.nombreID = j(v))
            If Not lista.Count = 0 Then 'concepto encontrado
                secuencia.Add(lista(0))
            Else
                'el concepto no ha sido encontrado
                'buscar variaciones linguísticas: plural
                lista = ConceposSimples.FindAll(Function(p) Plural(p.nombreID) = j(v))
                If Not lista.Count = 0 Then 'concepto encontrado
                    secuencia.Add(lista(0))
                Else
                    Console.WriteLine("No ha sido encontrado el concepto " & j(i))
                    Return ErrCon
                    Exit Function
                End If
            End If
        Next

        'recuperar negaciones**************************
        For i = 0 To secuencia.Count - 1
            If Not Negación(i) Then
                secuencia(i) = Neg(secuencia(i))
            End If

        Next '******************************************


        Dim k As Integer = 0

        'cuantificador
        Do
            If secuencia(k).tipo = "c" Then
                Union(r.CU, secuencia(k))
                k += 1
            Else
                Exit Do
            End If
        Loop

        If r.CU.conceptos.Count = 0 Then r.CU = todo 'si no hay cuantificador, agregar un universal

        'sujeto

        Do
            If secuencia(k).categoria = "s" Then
                Union(r.S, secuencia(k))
                k += 1
            Else
                Exit Do
            End If
        Loop

        Do
            If secuencia(k).categoria = "t" Then
                Union(r.S, secuencia(k))
                k += 1
            Else
                Exit Do
            End If
        Loop

        'cópula
        If secuencia(k).nombreID = "es" Then
            r.Copula = G(secuencia(k))
            k += 1
        Else
            r.Copula = vac 'para el caso de predicado sin cópula
        End If

        'predicado
        For i = k To secuencia.Count - 1
            Union(r.P, secuencia(k))
            k += 1
        Next

        r.tipo = r.getTipo()
        r.getConcordancia()

        If Incluir_en_lista Then
            'revisar que la sentencia no esté ya en la lista
            If Not EnLista(Canonic(r)) Then Sentencias.Add(Canonic(r))
        End If

        Return r
    End Function

    Sub CrearConcepto()
        Dim s As String
        Dim flag As Boolean = True

        Console.WriteLine("Crear nuevo concepto simple")
        Dim c As New concepto.simple
        Console.Write("Nombre del concepto: ")
        c.nombreID = Console.ReadLine()

        If ConceposSimples.FindAll(Function(p) p.nombreID = c.nombreID).Count = 0 Then
            'Console.Write("finito: (true/false) ")
            's = Console.ReadLine()
            'If s = "" Then
            c.finito = True
            'Else
            '    c.finito = CBool(s)
            'End If

            Do
                Console.Write("categoría: (s, t, h) ")
                s = Console.ReadLine()
                If s = "ayuda" Or s = "help" Then
                    Console.WriteLine("categoría s = conceptos que significan objetos; casa, arbol, hombre")
                    Console.WriteLine("categoría t = conceptos que significan propiedades; blanco, bueno, racional")
                    Console.WriteLine("categoría h = conceptos que significan actos; correr, leer, amar")
                    Console.WriteLine("Introduzca ahora su respuesta")
                    s = Console.ReadLine()
                End If

                If s = "s" Or s = "t" Or s = "h" Then
                    c.categoria = s
                Else
                    Console.WriteLine("categoría no reconocida")
                    flag = False
                End If
            Loop Until flag Or s = "exit"

            Do
                Console.Write("tipo: (q, c, s) ")
                s = Console.ReadLine()
                If s = "ayuda" Or s = "help" Then
                    Console.WriteLine("tipo q = conceptos universales cualitativos: hombre, animal, casa")
                    Console.WriteLine("tipo c = conceptos universales cuantitativos: todos, ningún, algún")
                    Console.WriteLine("tipo s = conceptos singulares: Pedro, Juan, el Cometa Halley")
                    Console.WriteLine("Introduzca ahora su respuesta")
                    s = Console.ReadLine()
                End If

                If s = "q" Or s = "c" Or s = "s" Then
                    c.tipo = s
                Else
                    Console.WriteLine("categoría no reconocida")
                    flag = False
                End If
            Loop Until flag Or s = "exit"

            'si el concepto no está ya registrado, agregar
            If modo = "" Then
                ConceposSimples.Add(c)
            ElseIf modo = "M" Then
                conceptosMat.Add(c)
            End If


            Console.WriteLine("")
            Console.WriteLine("*********************************************")
            Console.WriteLine("NUEVO CONCEPTO CREADO")
            Console.WriteLine("Nombre: " & c.nombreID)
            Console.WriteLine("finito: " & c.finito)
            Console.WriteLine("categoria: " & c.categoria)
            Console.WriteLine("tipo: " & c.tipo)
            Console.WriteLine("*********************************************")
            Console.WriteLine("")

        Else
            Console.WriteLine("El concepto ya existe. Operación cancelada")
        End If

    End Sub

    Sub BorrarConcepto()
        Dim s As String

        Console.Write("Indice del concepto para borrar: ")
        s = Console.ReadLine()

        If s = "exit" Or s = "" Then
            Console.WriteLine("Operación cancelada")
        Else
            Dim num As Long = CLng(s)

            Console.WriteLine("")
            Console.WriteLine("*********************************************")
            Console.WriteLine("CONCEPTO POR BORRAR")
            Console.WriteLine("Nombre: " & ConceposSimples(num).nombreID)
            Console.WriteLine("finito: " & ConceposSimples(num).finito)
            Console.WriteLine("categoria: " & ConceposSimples(num).categoria)
            Console.WriteLine("tipo: " & ConceposSimples(num).tipo)
            Console.WriteLine("*********************************************")
            Console.Write("Desea borrar este elemento? (sí/no) ")
            s = Console.ReadLine()

            If s = "sí" Or s = "si" Or s = "y" Or s = "s" Then
                Try
                    ConceposSimples.Remove(ConceposSimples(num))
                    Console.WriteLine("Concepto eliminado")
                Catch ex As Exception
                    Console.WriteLine(Err.Description)
                End Try

            ElseIf s = "no" Or s = "n" Then
                Console.WriteLine("Operación cancelada")
            Else
                Console.WriteLine("Respuesta no comprendida. Operación cancelada")
            End If


        End If
    End Sub

    Sub CargarConceptos()


        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader("conceptos.dat", Encoding.UTF8)
                Dim line As String
                ' Read the stream to a string and write the string to the console.
                line = sr.ReadToEnd()
                Dim j() As String = Split(line, vbCrLf)
                For i = 0 To UBound(j) - 1
                    Dim c As New concepto.simple
                    c.FromCodeStr(j(i))
                    ConceposSimples.Add(c)
                Next
            End Using

        Catch e As Exception
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(e.Message)
        End Try

        Console.WriteLine("conceptos.dat cargados")

    End Sub

    Sub CargarDiccionario(Optional lenguaje As String = "spanish.dat")


        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader(lenguaje, Encoding.UTF8)
                Dim line As String
                ' Read the stream to a string and write the string to the console.
                line = sr.ReadToEnd()
                Dim j() As String = Split(line, vbCrLf)
                For i = 0 To UBound(j) - 1
                    Dim s = Split(j(i), ",")
                    If lenguaje = "spanish.dat" Then
                        SpanishDc.Add(s(0), s(1))
                    ElseIf lenguaje = "english.dat" Then
                        EnglishDc.Add(s(0), s(1))
                    ElseIf lenguaje = "portuguese.dat" Then
                        PortugueseDc.Add(s(0), s(1))
                    End If
                Next
            End Using
            Console.WriteLine("diccionario " & lenguaje & " cargado")
        Catch e As Exception
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(e.Message)
        End Try



    End Sub

    Sub CrearSentencia(s As String)


        'Separar por puntos.
        Dim j() = Split(s, ".")
            For i = 0 To UBound(j)
                'si la sentencia no está en la lista, incluirla
                Dim u As sentencia = GetSent(j(i), False)
                s = u.Spanish
                u = Canonic(u)

                If Inconsistent(u) Then 'si está en contradicción con las otras sentencias
                    Console.WriteLine("La sentencia está en contradicción con las sentencias de la lista")

                ElseIf SelfContradic(u) Then 'si la sentencia es contradictoria en sí misma
                    Console.WriteLine("La sentencia es contradictoria")

                ElseIf Tautologic(u) Then 'si la sentencia es una tautología
                    Console.WriteLine("La sentencia es tautológica")

                ElseIf u.Equivale(ErrCon) Then 'si la expresión no está bien codificada o faltan conceptos para entenderla
                    s = "no se ha entendio la expresión"

                ElseIf EnLista(u) Then 'si ya se había registrado la teoría
                    Console.WriteLine("La sentencia ya se había añadido")
                Else
                    Sentencias.Add(u)
                End If

                Console.WriteLine(i & " ...> " & s)


            Next
            Console.WriteLine("")
        '  Console.WriteLine("...> " & GetSent(s, False).English)
        '  Console.WriteLine("...> " & GetSent(s, False).Portuguese)


    End Sub

    Sub CrearSentenciaComp(s As String)
        Dim r As New sentenciaComp
        'revisar coherencia de paréntesis
        If Parentesis(s) Then

            r.fromNOVA(s)
            s = r.Spanish

            If EnLista(r, SentenciasComp) Then
                Console.WriteLine("La sentencia ya se había añadido")
            ElseIf s Like "*[?]*" Then
                Console.WriteLine("Hay elementos desconocidos en la expresión")
            Else
                SentenciasComp.Add(r)
            End If

        Else
            Console.WriteLine("Existe un error de paréntesis en la fórmula")
            Console.WriteLine("")
        End If

        Console.WriteLine(" ...> " & s)
        Console.WriteLine("")

    End Sub

    Sub BorrarSentencia()
        Dim s As String

        Console.Write("Indice de la sentencia para borrar: ")
        s = Console.ReadLine()

        If s = "exit" Or s = "" Then
            Console.WriteLine("Operación cancelada")
        Else
            Dim num As Long = CLng(s)

            Console.WriteLine("")
            Console.WriteLine("*********************************************")
            Console.WriteLine("SENTENCIA POR BORRAR")
            Console.WriteLine("tipo: " & Sentencias(num).Spanish)
            Console.WriteLine("*********************************************")
            Console.Write("Desea borrar este elemento? (sí/no) ")
            s = Console.ReadLine()

            If s = "sí" Or s = "si" Or s = "y" Or s = "s" Then
                Try
                    Sentencias.Remove(Sentencias(num))
                    Console.WriteLine("Sentencia eliminada")
                Catch ex As Exception
                    Console.WriteLine(Err.Description)
                End Try

            ElseIf s = "no" Or s = "n" Then
                Console.WriteLine("Operación cancelada")
            Else
                Console.WriteLine("Respuesta no comprendida. Operación cancelada")
            End If

        End If

    End Sub

    Sub ListaSentenciasComp()
        If SentenciasComp.Count = 0 Then
            Console.WriteLine("No hay sentencias registradas")
        Else
            Console.WriteLine("")
            For i = 0 To SentenciasComp.Count - 1
                Console.WriteLine(i & " " & SentenciasComp(i).Spanish)
            Next
            Console.WriteLine("")
        End If
    End Sub

    Sub AnalizarConcepto(concepto As String, Optional Tipo_de_búsqueda As String = "sujeto")
        'identificar el concepto
        Dim c As concepto.simple = ConceposSimples.Find(Function(p) p.nombreID = Trim(concepto))
        Dim L As New List(Of sentencia)

        If Not c Is Nothing Then
            If Tipo_de_búsqueda = "sujeto" Then 'buscar todas las sentencias de la lista que contengan al concepto c como sujeto
                L = Sentencias.FindAll(Function(p) p.S.Equivale(G(c)))
            ElseIf Tipo_de_búsqueda = "predicado" Then
                L = Sentencias.FindAll(Function(p) p.P.Equivale(G(c)))
            End If

            If L.Count = 0 Then
                Console.WriteLine("No hay información sobre el concepto " & c.Spanish)
                Console.WriteLine("")
            Else
                Console.WriteLine("")
                Console.WriteLine("*********************************************")
                Console.WriteLine("Concepto analizado: " & c.nombreID)

                For Each item As sentencia In L
                    Console.WriteLine("      " & item.Spanish)
                Next

                Console.WriteLine("*********************************************")
                Console.WriteLine("")
            End If


        Else
            Console.WriteLine("Concepto no encontrado")
            Console.WriteLine("")
        End If


    End Sub

    Sub CrearTeoría()
        If modo = "" Then
            TeoríaEnCurso = GenerarTeoríaCompleta(Sentencias)
            Console.WriteLine("Teoría generada")
            Console.WriteLine("")
        ElseIf modo = "M" Then
            TeoríaHEnCurso = GenerarTeoríaHCompleta(SentenciasComp)
            EstructuraEnCurso.GetGrupos(TeoríaHEnCurso)
            Console.WriteLine("Teoría generada")
            Console.WriteLine("")

        End If

    End Sub
End Module
