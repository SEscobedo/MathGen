Module MdExperimental
    '   Dim CN As New concepto.general
    Public ConceposSimples As New List(Of concepto.simple)
    Public Sentencias As New List(Of sentencia)
    Public TeoríaEnCurso As New teoría
    Public TeoríaHEnCurso As New teoríaH
    Public EstructuraEnCurso As New EstructuraFormal
    Public DED As New List(Of sentencia)

    Public SentenciasComp As New List(Of sentenciaComp)
    Public DEDComp As New List(Of sentenciaComp)
    Public conceptosMat As New List(Of concepto.simple)

    Friend Reporte As String
    Public SpanishDc As New Dictionary(Of String, String)
    Public EnglishDc As New Dictionary(Of String, String)
    Public PortugueseDc As New Dictionary(Of String, String)

    Class concepto

        Class simple
            Property finito As Boolean = True 'concepto finito o infinito
            Property categoria As String = "" 'categoria s, t, h
            Property tipo As String = "" 'tipo cuantitativo, cualitativo o singular: c,q,s
            Property nombreID As String = ""

            Function Equivale(c As concepto.simple, Optional strict As Boolean = False) As Boolean 'determina si otro concepto simple es equivalente
                Dim r As Boolean = True

                If Not nombreID = c.nombreID Then r = False
                If Not tipo = c.tipo Then r = False

                If strict Then
                    If Not finito = c.finito Then r = False
                    If Not categoria = c.categoria Then r = False
                End If

                Return r
            End Function

            Sub FromCodeStr(s As String) 'Genera un concepto simple a partir de una cadena codificada
                If s Like "N*" Then
                    finito = False
                    s = Mid$(s, 2)
                Else
                    finito = True
                End If

                categoria = Mid$(s, 1, 1)
                tipo = Mid$(s, 2, 1)
                nombreID = Mid$(s, 3)
            End Sub

            Function CodeStr() As String  'Genera una cadena para codificar el concepto
                Dim r As String = ""
                If Not finito Then r &= "N"
                r &= categoria
                r &= tipo
                r &= nombreID
                Return r
            End Function

            Function Spanish(Optional omitir As Boolean = False, Optional concor As concordancia = Nothing) As String

                'en caso de omitir la expresión ******************************
                Dim r As String = ""
                If omitir Then
                    Return ""
                    Exit Function
                End If '******************************************************



                'Introducción del negador y diccionario *******************************************
                If tipo = "c" Then 'cuantitativo  ejemplo: no todo, diez, nada

                    If Not finito Then r &= "no "
                    r &= Dcs(nombreID, concor, tipo, categoria)

                ElseIf tipo = "q" Then 'cualitativo ejemplo: no-hombre, no blanco
                    If categoria = "s" Then
                        If Not finito Then r &= "no-"
                    Else
                        If Not finito Then r &= "no "
                    End If
                    r &= Dcs(nombreID, concor, tipo, categoria)

                Else 'singular ejemplos no-Pedro
                    If Not finito Then r &= "no-"
                    r &= Dcs(nombreID, concor, tipo, categoria)
                End If '*****************************************************************************



                Return r
            End Function

            Function English(Optional omitir As Boolean = False, Optional concor As concordancia = Nothing) As String
                Dim r As String = ""

                If omitir Then
                    Return ""
                    Exit Function
                End If

                If tipo = "c" Then 'cuantitativo  ejemplo: no todo, diez, nada

                    If Not finito Then r &= "not "
                    r &= Dce(nombreID)

                ElseIf tipo = "q" Then 'cualitativo
                    If categoria = "s" Then
                        If Not finito Then r &= "non-"
                    Else
                        If Not finito Then r &= "non-"
                    End If
                    r &= Dce(nombreID)
                Else 'singular
                    If Not finito Then r &= "non-"
                    r &= Dce(nombreID)
                End If

                Return r
            End Function

            Function Portuguese(Optional omitir As Boolean = False, Optional concor As concordancia = Nothing) As String

                If omitir Then
                    Return ""
                    Exit Function
                End If

                Dim r As String = ""
                If tipo = "c" Then 'cuantitativo  ejemplo: no todo, diez, nada

                    If Not finito Then r &= "nem "
                    r &= Dcp(nombreID)

                ElseIf tipo = "q" Then 'cualitativo  
                    If categoria = "s" Then
                        If Not finito Then r &= "não "
                    Else
                        If Not finito Then r &= "não-"
                    End If
                    r &= Dcp(nombreID)
                Else 'singular
                    If Not finito Then r &= "não-"
                    r &= Dcp(nombreID)
                End If

                Return r
            End Function

        End Class

        Class compuesto
            Property A As concepto.simple
            Property B As concepto.simple
            Property tipo As String  'conjunción simple, o complementación.

            Function Spanish() As String
                Dim r As String = ""
                If A.categoria = B.categoria Then
                    r = A.Spanish & " y " & B.Spanish
                ElseIf A.categoria = "t" And A.tipo = "c" Then
                    r = A.Spanish & " " & B.Spanish
                ElseIf B.categoria = "t" And B.tipo = "c" Then
                    r = B.Spanish & " " & A.Spanish
                ElseIf A.tipo = "q" And A.categoria = "t" Then
                    r = B.Spanish & " " & A.Spanish
                ElseIf B.tipo = "q" And B.categoria = "t" Then
                    r = A.Spanish & " " & B.Spanish
                Else
                    r = "err"
                End If

                Return r
            End Function

            Function English() As String
                Dim r As String = ""
                If A.categoria = B.categoria Then
                    r = A.English & " and " & B.English
                ElseIf A.categoria = "t" And A.tipo = "c" Then
                    r = A.English & " " & B.English
                ElseIf B.categoria = "t" And B.tipo = "c" Then
                    r = B.English & " " & A.English
                ElseIf A.tipo = "q" And A.categoria = "t" Then
                    r = B.English & " " & A.English
                ElseIf B.tipo = "q" And B.categoria = "t" Then
                    r = A.English & " " & B.English
                Else
                    r = "err"
                End If

                Return r
            End Function

            Function Portuguese() As String
                Dim r As String = ""
                If A.categoria = B.categoria Then
                    r = A.Portuguese & " e " & B.Portuguese
                ElseIf A.categoria = "t" And A.tipo = "c" Then
                    r = A.Portuguese & " " & B.Portuguese
                ElseIf B.categoria = "t" And B.tipo = "c" Then
                    r = B.Portuguese & " " & A.Portuguese
                ElseIf A.tipo = "q" And A.categoria = "t" Then
                    r = B.Portuguese & " " & A.Portuguese
                ElseIf B.tipo = "q" And B.categoria = "t" Then
                    r = A.Portuguese & " " & B.Portuguese
                Else
                    r = "err"
                End If

                Return r
            End Function

        End Class

        Class general
            Property conceptos As New List(Of concepto.simple)
            Property tipo As String = ""
            Property categoria As String = ""
            Property extension As String = "" 'universal o particular
            Property positivo As Boolean = True 'positivo o negativo
            Property composicion As String = ""

            Function copy() As concepto.general
                Dim x As New concepto.general
                x.conceptos.AddRange(conceptos)
                x.tipo = tipo
                x.categoria = categoria
                x.extension = extension
                x.positivo = positivo
                x.composicion = composicion
                Return x
            End Function

            Function CodeStr() As String
                Dim r As String = ""

                If conceptos.Count > 0 Then

                    'cuantificadores
                    r &= conceptos(0).CodeStr
                    If conceptos.Count > 1 Then
                        For i = 1 To conceptos.Count - 1

                            r &= "," & conceptos(i).CodeStr

                        Next
                    End If
                End If

                Return r
            End Function

            Sub FromCodeStr(s As String)
                Dim r() As String = Split(s, ",")
                conceptos.Clear()

                For i = 0 To UBound(r)
                    Dim c As New concepto.simple
                    c.FromCodeStr(r(i))
                    conceptos.Add(c)
                Next

                categoria = Getcategoria()
                extension = GetExtension()
                positivo = GetPositivo()
                tipo = GetTipo()


            End Sub

            'determina si dos conceptos son iguales o equivalentes
            Function Equivale(ByVal X As concepto.general, Optional strict As Boolean = False) As Boolean
                Dim flag As Boolean = True
                Dim ORDEN_importa As Boolean = True
                'la lista de conceptos debe ser la misma, en el mismo orden (strict) o en orden aleatorio

                If Not X.conceptos.Count = conceptos.Count Then 'si el número de conceptos no es igual, no puede ser el mismo concepto general
                    flag = False

                    'deben coincidir las propiedades de concepto general
                    '--------modo estricto---------------------------
                ElseIf Not X.extension = extension And strict Then
                    flag = False
                ElseIf Not X.categoria = categoria And strict Then
                    flag = False
                ElseIf Not X.positivo = positivo And strict Then
                    flag = False
                    '----------------------------------------------

                ElseIf Not X.tipo = tipo Then
                    flag = False

                ElseIf Not X.composicion = composicion Then
                    flag = False

                Else

                    'los conceptos simples son equivalentes en el mismo orden?
                    'si X es una conjunción, entonces el orden no importa
                    If composicion = "conj" Then ORDEN_importa = False

                    If ORDEN_importa Then

                        For i = 0 To conceptos.Count - 1
                                If Not X.conceptos(i).Equivale(conceptos(i), strict) Then
                                    flag = False
                                    Exit For
                                End If
                            Next

                        Else

                            Dim ListaX As List(Of concepto.simple) = X.conceptos.OrderBy(Function(p) p.nombreID).ToList()
                            Dim ListaY As List(Of concepto.simple) = conceptos.OrderBy(Function(p) p.nombreID).ToList()

                            For i = 0 To conceptos.Count - 1
                                If Not ListaX(i).Equivale(ListaY(i), strict) Then
                                    flag = False
                                    Exit For
                                End If
                            Next
                        End If

                    End If


                    Return flag

            End Function

            Function Getcategoria() As String
                Dim r As String = ""

                If r = "" Then
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r = "h"
                            Return r
                            Exit Function
                        End If

                    Next
                End If

                For i = 0 To conceptos.Count - 1
                    If conceptos(i).categoria = "s" Then
                        r = "s"
                        Return r
                        Exit Function
                    End If
                Next

                If r = "" Then
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "t" Then
                            r = "t"
                            Return r
                            Exit Function
                        End If
                    Next
                End If
                Return r
            End Function

            Function GetExtension() As String
                Dim r As String = ""
                For i = 0 To conceptos.Count - 1
                    If conceptos(i).tipo = "c" Then
                        If conceptos(i).nombreID = "@" Then
                            r = "u"
                            Exit For
                        ElseIf conceptos(i).nombreID = "x@" Then
                            r = "p"
                            Exit For
                        Else
                            r = "desc" 'desconocido (tiene cuantificador pero es desconocido)
                            Exit For
                        End If
                    End If
                    r = "indef" 'extensión indefinida (sin cuantificadores)
                Next
                Return r
            End Function

            Function GetPositivo() As Boolean
                Dim r As Boolean = True

                For i = 0 To conceptos.Count - 1
                    If conceptos(i).categoria = "h" Then
                        r = conceptos(i).finito
                        Return r
                        Exit Function

                    End If
                Next

                For i = 0 To conceptos.Count - 1
                    If conceptos(i).categoria = "s" Then
                        r = conceptos(i).finito
                        Return r
                        Exit Function

                    End If
                Next

                For i = 0 To conceptos.Count - 1
                    If conceptos(i).categoria = "t" Then
                        r = conceptos(i).finito
                        Return r
                        Exit Function
                    End If
                Next

                Return r
            End Function

            Function GetTipo() As String
                Dim r As String = ""
                'determinar tipo
                Dim distintos As Boolean = True 'suponemos que son distintas las categorías
                Dim cat As String

                If conceptos.Count > 1 Then
                    distintos = False 'asumimos ahora que son iguales

                    cat = conceptos(0).categoria

                    For i = 0 To conceptos.Count - 1
                        If Not conceptos(i).categoria = cat Then
                            distintos = True 'verifcamos que no son iguales
                            Exit For
                        End If
                    Next
                End If

                If Not distintos Then
                    r = cat
                    composicion = "conj"
                Else
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r = conceptos(i).tipo
                            Return r
                            Exit Function

                        End If
                    Next


                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            r = conceptos(i).tipo
                            Return r
                            Exit Function
                        End If
                    Next


                    If r = "" Then r = conceptos(0).tipo
                End If

                Return r
            End Function

            Function Spanish(Optional omitir As Boolean = False, Optional concor As concordancia = Nothing) As String

                If omitir Then
                    Return ""
                    Exit Function
                End If

                Dim r As String = ""

                If composicion = "conj" Then

                    'CASO 1: simple conjunción
                    r = conceptos(0).Spanish(omitir, concor)

                    If conceptos.Count > 2 Then
                        For i = 1 To conceptos.Count - 2
                            r &= ", " & conceptos(i).Spanish(omitir, concor)
                        Next
                    End If
                    r &= " y " & conceptos(conceptos.Count - 1).Spanish(omitir, concor)
                Else

                    'CASO 2:

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r &= " " & conceptos(i).Spanish(omitir, concor)
                        End If
                    Next

                    'cuantificadores primero (por orden de generalidad)
                    Dim c As String = ""

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "c" And conceptos(i).categoria = "t" Then
                            c &= " " & conceptos(i).Spanish(omitir, concor)
                        End If
                    Next

                    'cualificadores después (por orden de generalidad)

                    Dim flag As Boolean = True

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            If Not conceptos(i).tipo = "s" Then r &= c 'si el primer concepto del sujeto no es singular, agregar los cuantificadores
                            r &= " " & conceptos(i).Spanish(omitir, concor)
                            flag = False
                        End If
                    Next

                    If flag Then r &= c

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "q" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Spanish(omitir, concor)
                        End If
                    Next



                End If

                Return Trim(r)
            End Function

            Function English(Optional omitir As Boolean = False, Optional concor As concordancia = Nothing) As String

                If omitir Then
                    Return ""
                    Exit Function
                End If

                Dim r As String = ""
                If composicion = "conj" Then
                    'CASO 1: simple conjunción
                    r = conceptos(0).English(omitir, concor)

                    If conceptos.Count > 2 Then
                        For i = 1 To conceptos.Count - 2
                            r &= ", " & conceptos(i).English(omitir, concor)
                        Next
                    End If
                    r &= " and " & conceptos(conceptos.Count - 1).English(omitir, concor)
                Else
                    'CASO 2: hay un tipo s y el resto son t

                    'cuantificadores primero (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "c" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).English(omitir, concor)
                        End If
                    Next

                    'cualificadores después (por orden de generalidad)

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r &= " " & conceptos(i).English(omitir, concor)
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            r &= " " & conceptos(i).English(omitir, concor)
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "q" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).English(omitir, concor)
                        End If
                    Next

                End If

                Return Trim(r)
            End Function

            Function Portuguese(Optional omitir As Boolean = False, Optional concor As concordancia = Nothing) As String

                If omitir Then
                    Return ""
                    Exit Function
                End If

                Dim r As String = ""
                If composicion = "conj" Then
                    'CASO 1: simple conjunción
                    r = conceptos(0).Portuguese(omitir, concor)

                    If conceptos.Count > 2 Then
                        For i = 1 To conceptos.Count - 2
                            r &= ", " & conceptos(i).Portuguese(omitir, concor)
                        Next
                    End If
                    r &= " e " & conceptos(conceptos.Count - 1).Portuguese(omitir, concor)
                Else
                    'CASO 2: hay un tipo s y el resto son t

                    'cuantificadores primero (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "c" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Portuguese(omitir, concor)
                        End If
                    Next

                    'cualificadores después (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r &= " " & conceptos(i).Portuguese(omitir, concor)
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            r &= " " & conceptos(i).Portuguese(omitir, concor)
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "q" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Portuguese(omitir, concor)
                        End If
                    Next

                End If

                Return Trim(r)
            End Function

        End Class


    End Class

    Class sentencia
        Property CU As New concepto.general 'cuantificador
        Property S As New concepto.general 'sujeto
        Property Copula As New concepto.general 'cópula
        Property P As New concepto.general 'predicado
        Property tipo As String 'tipo de sentencia: A E I O
        Property concordancia As New concordancia 'datos gramaticales del sujeto
        Property ID As String 'identificador

        Function copy() As sentencia
            Dim X As New sentencia
            X.CU = CU.copy
            X.S = S.copy
            X.Copula = Copula.copy
            X.P = P.copy
            X.tipo = tipo
            X.concordancia = concordancia
            X.ID = ID
            Return X
        End Function

        Function CodeStr() As String
            Dim r As String
            r = CU.CodeStr & ";" & S.CodeStr & ";" & Copula.CodeStr & ";" & P.CodeStr
            Return r
        End Function

        Sub FromCodeStr(st As String)
            Dim r() As String = Split(st, ";")

            If UBound(r) = 3 Then
                Dim c As New concepto.general
                c.FromCodeStr(r(0))
                CU = c
                c = New concepto.general
                c.FromCodeStr(r(1))
                S = c
                c = New concepto.general
                c.FromCodeStr(r(2))
                Copula = c
                c = New concepto.general
                c.FromCodeStr(r(3))
                P = c

                tipo = getTipo()
                getConcordancia()
            End If

        End Sub

        Function getTipo() As String
            Dim r As String = ""

            If CU.extension = "u" And CU.positivo = True Then 'Ej. Todo hombre
                If (Copula.positivo And P.positivo) = True Then
                    'universal positivo
                    r = "A"
                Else
                    'universal negativo
                    r = "E"
                End If
            ElseIf CU.extension = "u" And CU.positivo = False Then 'Ej No todo hombre
                If (Copula.positivo And P.positivo) = True Then
                    'particular negativo
                    r = "O"
                Else
                    'particular positivo
                    r = "I"
                End If
            ElseIf CU.extension = "p" And CU.positivo = False Then 'Ej ningún hombre
                If (Copula.positivo And P.positivo) = True Then
                    'universal negativo
                    r = "E"
                Else
                    'universal positivo
                    r = "A"
                End If

            ElseIf CU.extension = "p" And CU.positivo = True Then 'Ej algún hombre
                If (Copula.positivo And P.positivo) = True Then
                    'particular positivo
                    r = "I"
                Else
                    'particular negativo
                    r = "O"
                End If
            End If

            Return r
        End Function

        Sub getConcordancia()
            'caso
            concordancia.caso = "n"

            'número
            If S.composicion = "conj" Then
                concordancia.número = "p"
            Else
                concordancia.número = "s"
            End If

            'género
            concordancia.género = "m" 'por default masculino (crear función que obtenga el género)

            'persona
            concordancia.persona = "3" 'por default tercer persona. (crear función que obtenga la persona)

        End Sub

        Function Equivale(X As sentencia) As Boolean
            Dim r As Boolean = True
            If Not CU.Equivale(X.CU, True) Then r = False
            If Not S.Equivale(X.S, True) Then r = False
            If Not Copula.Equivale(X.Copula, True) Then r = False
            If Not P.Equivale(X.P, True) Then r = False

            Return r
        End Function

        Function Spanish() As String
            Dim r As String = ""
            Dim c As String = ""

            Dim sujeto As String = ""
            Dim predicado As String = ""

            If Not CU.positivo And S.tipo = "s" Then c = "no "

            If Not S.categoria = "h" Then
                sujeto = CU.Spanish(S.tipo = "s", concordancia) & " " & S.Spanish() '& " " & c & Copula.Spanish(, concordancia) & " " & P.Spanish(, concordancia)

            Else 'el cuantificador debe ir en modo genitivo = todos los que... alguno de los que...
                Dim con, con2 As New concordancia
                con.caso = "g"
                con2.número = "s" 'esto va a depender de cómo se escriba el cuantificador o la cópula 

                sujeto = CU.Spanish(, con) & " " & S.Spanish(, con2) '& " " & c & Copula.Spanish(, concordancia) & " " & P.Spanish(, concordancia)

            End If

            If P.categoria = "h" And Copula.Equivale(est, True) Then
                If Copula.positivo Then
                    predicado = c & Copula.Spanish(True) & " " & P.Spanish(, concordancia)
                Else
                    predicado = c & " no " & P.Spanish(, concordancia)
                End If
            Else
                predicado = c & Copula.Spanish(, concordancia) & " " & P.Spanish(, concordancia)
            End If


            predicado = Replace(predicado, " no no ", " ") 'anular doble negación

            r = sujeto & " " & predicado

            Return Cs(r)
        End Function

        Function English() As String
            Dim r As String = ""
            r = CU.English(S.tipo = "s") & " " & S.English & " " & Copula.English & " " & P.English
            Return Ce(r)
        End Function

        Function Portuguese() As String
            Dim r As String = ""
            r = CU.Portuguese(S.tipo = "s") & " " & S.Portuguese & " " & Copula.Portuguese & " " & P.Portuguese
            Return Cp(r)
        End Function

    End Class

    Class sentenciaComp
        Property antecedente As String = ""
        Property consecuente As String = ""
        Property NombreID As String
        Function Equivale(s As sentenciaComp) As Boolean
            Dim res As Boolean = False
            If antecedente = s.antecedente And consecuente = s.consecuente Then
                res = True
            End If
            Return res
        End Function

        Sub fromNOVA(s As String)

            Adapt(s)
            s = POmitir(FF(Trim(s),, "P"))

            If OperNoAnid(s, "->") Then
                Dim j() As String = PSplit(s, "->")
                antecedente = j(0)
                consecuente = j(1)
            Else
                antecedente = s
            End If

        End Sub

        Sub fromCodeStr(s As String)
            Dim j = PSplit(s, "->")

            If j.Count = 2 Then
                antecedente = Trim(POmitir(Trim(j(0))))
                consecuente = Trim(POmitir(Trim(j(1))))
            Else
                antecedente = Trim(POmitir(Trim(s)))
            End If

        End Sub

        Function CodeStr() As String
            Dim r As String
            If consecuente = "" Then
                r = consecuente
            Else
                r = "(" & Trim(POmitir(Trim(antecedente))) & ") -> (" & Trim(POmitir(Trim(consecuente))) & ")"
            End If

            Return r
        End Function

        Function Spanish(Optional latex As Boolean = False) As String
            Dim r As String
            If latex Then
                If consecuente = "" Then
                    r = POmitir(FF(antecedente, True))
                Else
                    r = POmitir(FF(antecedente & " -> " & consecuente, True))
                End If
            Else
                If consecuente = "" Then
                    r = antecedente
                Else
                    r = "si " & antecedente & " entonces " & consecuente
                End If
            End If

            Return r
        End Function

        Function English() As String
            Dim r As String
            If consecuente = "" Then
                r = antecedente
            Else
                r = "if " & antecedente & " then " & consecuente
            End If
            Return r
        End Function

        Function Portuguese() As String
            Dim r As String
            If consecuente = "" Then
                r = antecedente
            Else
                r = "se " & antecedente & " então " & consecuente
            End If
            Return r
        End Function

    End Class

    Class premisa
        Property sentencia As sentencia
        Property mayor As sentencia
        Property menor As sentencia
        Property paso As String 'determina el ultimo paso en la ducción de la sentencia. Por ejemplo una regla lógica
        Property mark As String 'determina el tipo de sentencia en la secuencia de razonamiento. Por ejemplo, hipóteis, axima, etc.

        'Sub fromNOVA(s As String)
        '    sentencia.fromNOVA(s)
        'End Sub
    End Class

    Class premisaH
        Property sentencia As sentenciaComp
        Property mayor As sentenciaComp
        Property menor As sentenciaComp
        Property paso As String 'determina el ultimo paso en la ducción de la sentencia. Por ejemplo una regla lógica
        Property mark As String 'determina el tipo de sentencia en la secuencia de razonamiento. Por ejemplo, hipóteis, axima, etc.

        'Sub fromNOVA(s As String)
        '    sentencia.fromNOVA(s)
        'End Sub
    End Class

    Class argumentación
        Property premisas As New List(Of premisa)   'la ultima premisa de esta lista es la conclusión
        Property metodo As String 'determina el método de la demostración, si tiene un nombre. (ad absurdum, inducción matemática, etc.)

        Sub AddRange(X As argumentación)
            premisas.AddRange(X.premisas)
        End Sub

        Sub AddRange(S As List(Of sentencia))

            For Each item As sentencia In S
                Dim p As New premisa
                p.sentencia = item.copy
                premisas.Add(p)
            Next
        End Sub

        Function Spanish(Optional estilo As String = "explicito") As String 'Redacta en español la demostración
            Dim r As String = ""
            If estilo = "explicito" Then
                For i = 0 To premisas.Count
                    r &= premisas(i).mark & ": " & premisas(i).sentencia.Spanish & vbTab & premisas(i).paso & vbCrLf
                Next
            ElseIf estilo = "lineal" Then
                '˫
            End If
            Return r
        End Function
    End Class

    Class argumentaciónH
        Property premisas As New List(Of premisaH)   'la ultima premisa de esta lista es la conclusión
        Property metodo As String 'determina el método de la demostración, si tiene un nombre. (ad absurdum, inducción matemática, etc.)

        Sub AddRange(X As argumentaciónH)
            premisas.AddRange(X.premisas)
        End Sub

        Sub AddRange(S As List(Of sentenciaComp))

            For Each item As sentenciaComp In S
                Dim p As New premisaH
                p.sentencia = item
                premisas.Add(p)
            Next
        End Sub

        Function Spanish(Optional estilo As String = "explicito") As String 'Redacta en español la demostración
            Dim r As String = ""
            If estilo = "explicito" Then
                For i = 0 To premisas.Count
                    r &= premisas(i).mark & ": " & premisas(i).sentencia.Spanish & vbTab & premisas(i).paso & vbCrLf
                Next
            ElseIf estilo = "lineal" Then
                '˫
            End If
            Return r
        End Function
    End Class

    Class teoría
        Property NombreID As String
        Property conceptos As New List(Of concepto.simple)
        Property axiomas As New List(Of premisa)
        Property definiciones As New List(Of premisa)
        Property teoremas As New List(Of premisa)
        Property demostraciones As New List(Of argumentación)

        Sub GenerarConceptos()
            Dim cs As New List(Of concepto.simple)

            For Each item As premisa In axiomas
                'sacar los conceptos sujeto y predicado de cada premisa
                cs.AddRange(item.sentencia.S.conceptos)
                cs.AddRange(item.sentencia.P.conceptos)
            Next

            'quitar repeticiones
            For Each item As concepto.simple In cs
                Dim nc = conceptos.Find(Function(p) p.nombreID = item.nombreID)
                If nc Is Nothing Then
                    conceptos.Add(item)
                End If
            Next


        End Sub

        Function Spanish(Optional latex As Boolean = False)

            Dim s
            'introducción de conceptos
            s &= " Introducimos sin definición los conceptos siguientes: \textit{" & vbLf

            For i = 0 To conceptos.Count - 1
                If Not i = 0 Then s &= ", "
                s &= conceptos(i).Spanish
            Next
            s &= "}" & vbLf & "\\"
            s &= vbLf & "\\"
            s &= "Mediante estos conceptos formamos los axiomas que siguen. \\" & vbLf

            ' axiomas
            s &= "\textbf{Axiomas:}" & vbLf
            s &= "\begin{enumerate}"
            For i = 0 To axiomas.Count - 1
                s &= vbLf & "\item " & axiomas(i).sentencia.Spanish
            Next
            s &= vbLf & "\end{enumerate}"


            If Not definiciones.Count = 0 Then
                'definiciones
                For i = 0 To definiciones.Count - 1
                    s &= vbLf & "\end{enumerate}"
                    s &= vbLf & "\begin{definition}"
                    s &= vbLf & definiciones(i).sentencia.Spanish
                    s &= vbLf & "\label{def: " & i & "}"
                    s &= vbLf & "\end{defintion}"
                Next
            End If

            'teoremas
            s &= "De la lista anterior se deducen los próximos teoremas. \\" & vbLf

            For i = 0 To teoremas.Count - 1

                If Not (teoremas(i).mayor Is Nothing Or teoremas(i).menor Is Nothing) Then
                    s &= vbLf & "\begin{theorem}"
                    s &= vbLf & teoremas(i).sentencia.Spanish
                    s &= vbLf & "\label{th: " & i & "}"
                    s &= vbLf & "\end{theorem}"

                    'demostración                
                    s &= vbCrLf & "\begin{proof}\\"
                    s &= vbCrLf & teoremas(i).mayor.Spanish & vbTab & " (Premisa mayor) \\"
                    s &= vbCrLf & teoremas(i).menor.Spanish & vbTab & " (Premisa menor) \\"
                    s &= vbCrLf & "Luego, " & teoremas(i).sentencia.Spanish & vbTab & teoremas(i).paso & " \\"
                    s &= vbCrLf & " \end{proof}"
                End If
            Next


            Return s
        End Function


        Function English()
            Dim r
            Return r

        End Function

        Function Portuguese()
            Dim r
            Return r
        End Function

    End Class

    Class teoríaH
        Property NombreID As String
        Property conceptos As New List(Of concepto.simple)
        Property axiomas As New List(Of premisaH)
        Property definiciones As New List(Of premisaH)
        Property teoremas As New List(Of premisaH)
        Property demostraciones As New List(Of argumentaciónH)

        Sub GenerarConceptos()
            'Dim cs As New List(Of concepto.simple)

            'For Each item As premisaH In axiomas
            '    'sacar los conceptos sujeto y predicado de cada premisa
            '    cs.AddRange(item.sentencia)
            '    cs.AddRange(item.sentencia.P.conceptos)
            'Next

            ''quitar repeticiones
            'For Each item As concepto.simple In cs
            '    Dim nc = conceptos.Find(Function(p) p.nombreID = item.nombreID)
            '    If nc Is Nothing Then
            '        conceptos.Add(item)
            '    End If
            'Next


        End Sub

        Function Spanish(Optional latex As Boolean = False)

            Dim s
            'introducción de conceptos
            s &= " Introducimos sin definición los conceptos siguientes: \textit{" & vbLf

            For i = 0 To conceptos.Count - 1
                If Not i = 0 Then s &= ", "
                s &= conceptos(i).Spanish
            Next
            s &= "}" & vbLf & "\\"
            s &= vbLf & "\\"
            s &= "Mediante estos conceptos formamos los axiomas que siguen. \\" & vbLf

            ' axiomas
            s &= "\textbf{Axiomas:}" & vbLf
            s &= "\begin{enumerate}"
            For i = 0 To axiomas.Count - 1
                s &= vbLf & "\item $" & axiomas(i).sentencia.Spanish(True) & "$"
            Next
            s &= vbLf & "\end{enumerate}"


            If Not definiciones.Count = 0 Then
                'definiciones
                For i = 0 To definiciones.Count - 1
                    s &= vbLf & "\end{enumerate}"
                    s &= vbLf & "\begin{definition}"
                    s &= vbLf & definiciones(i).sentencia.Spanish(True)
                    s &= vbLf & "\label{def: " & i & "}"
                    s &= vbLf & "\end{defintion}"
                Next
            End If


            If Not teoremas.Count = 0 Then
                'teoremas
                s &= "De la lista anterior se deducen los próximos teoremas. \\" & vbLf

                For i = 0 To teoremas.Count - 1

                    If Not (teoremas(i).mayor Is Nothing Or teoremas(i).menor Is Nothing) Then
                        s &= vbLf & "\begin{theorem}"
                        s &= vbLf & teoremas(i).sentencia.Spanish(True)
                        s &= vbLf & "\label{th: " & i & "}"
                        s &= vbLf & "\end{theorem}"

                        'demostración                
                        s &= vbCrLf & "\begin{proof}\\"
                        s &= vbCrLf & teoremas(i).mayor.Spanish(True) & vbTab & " (Premisa mayor) \\"
                        s &= vbCrLf & teoremas(i).menor.Spanish(True) & vbTab & " (Premisa menor) \\"
                        s &= vbCrLf & "Luego, " & teoremas(i).sentencia.Spanish(True) & vbTab & teoremas(i).paso & " \\"
                        s &= vbCrLf & " \end{proof}"
                    End If
                Next

            End If
            Return s
        End Function

    End Class

    Class EstructuraFormal
        Property grupos As New List(Of grupo)

        Sub GetGrupos(T As teoríaH) 'genera los grupos a partir de una teoría
            'provisionalmente hacer un grupo con los axiomas de la teoría
            Dim g As New grupo

            If T.axiomas.Count > 0 Then
                g.Premisas.AddRange(T.axiomas)
                g.tipo = "axioma"
                grupos.Add(g)
            End If

            If T.definiciones.Count > 0 Then
                g = New grupo
                g.Premisas.AddRange(T.definiciones)
                g.tipo = "definición"
                grupos.Add(g)
            End If

            If T.teoremas.Count > 0 Then
                g = New grupo
                g.Premisas.AddRange(T.teoremas)
                g.tipo = "teorema"
                grupos.Add(g)
            End If

            g.GrupoAjuste()
        End Sub

        Function Spanish(Optional toLatex As Boolean = False, Optional estilo As String = "standar")

            Dim s
            ''introducción de conceptos
            's &= " Introducimos sin definición los conceptos siguientes: \textit{" & vbLf

            'For i = 0 To conceptos.Count - 1
            '    If Not i = 0 Then s &= ", "
            '    s &= conceptos(i).Spanish
            'Next
            's &= "}" & vbLf & "\\"
            's &= vbLf & "\\"
            's &= "Mediante estos conceptos formamos los axiomas que siguen. \\" & vbLf

            'REDACTAR GRUPO POR GRUPO **************************************************
            For i = 0 To grupos.Count - 1

                Select Case grupos(i).tipo
                    Case "axioma"

                        If estilo = "standar" Then

                            s &= vbLf & "\begin{axiom}"

                            If grupos(i).condiciones.Count > 1 Or grupos(i).condiciones(0).antecedente Like "*;*" Then
                                s &= vbLf & "Sean "
                            Else
                                s &= vbLf & "Sea "
                            End If

                            For Each item As sentenciaComp In grupos(i).condiciones
                                s &= "$" & Replace(item.Spanish(True), ";", ",") & "$"
                            Next
                            s &= ". "

                            s &= vbLf & "Se cumple entonces lo siguente: "
                            s &= vbLf & "\begin{enumerate}"
                            For j = 0 To grupos(i).Premisas.Count - 1

                                s &= vbLf & "\item $" & grupos(i).Premisas(j).sentencia.Spanish(True) & "$"

                            Next
                            s &= vbLf & "\end{enumerate}"

                            s &= vbLf & "\label{ax: " & i & "}"

                            s &= vbLf & "\end{axiom}"

                        Else





                            If grupos(i).condiciones.Count > 1 Or grupos(i).condiciones(0).antecedente Like "*;*" Then
                                s &= vbLf & "Sean "
                            Else
                                s &= vbLf & "Sea "
                            End If

                            For Each item As sentenciaComp In grupos(i).condiciones
                                s &= "$" & Replace(item.Spanish(True), ";", ",") & "$"
                            Next
                            s &= ". "

                            s &= vbLf & "Los siguientes axiomas son válidos: "

                            For j = 0 To grupos(i).Premisas.Count - 1
                                s &= vbLf & "\begin{axiom}"
                                s &= vbLf & "$" & grupos(i).Premisas(j).sentencia.Spanish(True) & "$"
                                s &= vbLf & "\end{axiom}"
                            Next


                            s &= vbLf & "\label{ax: " & i & "}"




                        End If


                    Case "definición"
                        s &= vbLf & "\begin{definition}"

                        s &= vbLf & "\begin{enumerate}"

                        For j = 0 To grupos(i).Premisas.Count - 1

                            s &= vbLf & "\item $" & grupos(i).Premisas(j).sentencia.Spanish(True) & "$"

                        Next
                        s &= vbLf & "\end{enumerate}"

                        s &= vbLf & "\label{def: " & i & "}"

                        s &= vbLf & "\end{definition}"
                    Case "teorema"

                        'teoremas
                        s &= "De la lista anterior se deducen los próximos teoremas. \\" & vbLf

                        'For i = 0 To teoremas.Count - 1

                        '    If Not (teoremas(i).mayor Is Nothing Or teoremas(i).menor Is Nothing) Then
                        '        s &= vbLf & "\begin{theorem}"
                        '        s &= vbLf & teoremas(i).sentencia.Spanish(True)
                        '        s &= vbLf & "\label{th: " & i & "}"
                        '        s &= vbLf & "\end{theorem}"

                        '        'demostración                
                        '        s &= vbCrLf & "\begin{proof}\\"
                        '        s &= vbCrLf & teoremas(i).mayor.Spanish(True) & vbTab & " (Premisa mayor) \\"
                        '        s &= vbCrLf & teoremas(i).menor.Spanish(True) & vbTab & " (Premisa menor) \\"
                        '        s &= vbCrLf & "Luego, " & teoremas(i).sentencia.Spanish(True) & vbTab & teoremas(i).paso & " \\"
                        '        s &= vbCrLf & " \end{proof}"
                        '    End If
                        'Next

                End Select

            Next
            '***************************************************************************



            Return s

        End Function

        Function NOVAScript()  'genera el código script de la estructura
            Return ""
        End Function
        Function FromNOVAScript()  'obtiene el código de la estructura de un script

            Return ""
        End Function
    End Class

    Class grupo
        Property Premisas As New List(Of premisaH)
        Property tipo As String  'axioma, definición, teorema, etc.
        Property prueba As New argumentación
        Property condiciones As New List(Of sentenciaComp)
        Property título As String

        Sub GrupoAjuste() 'obtiene las condiciones del grupo y las remueve de los antecendentes

            condiciones.Clear()


            'caso 1. sólo hay un elemento  **********************************************************
            If Premisas.Count = 1 Then

                'la premisa contiene declaraciones de inclusión?
                Dim c = PSplit(Premisas(0).sentencia.antecedente, "&")
                Dim l As New List(Of String)

                For Each item As String In c
                    If OperNoAnid(item, " en ") Then
                        Dim t As New sentenciaComp
                        t.fromCodeStr(POmitir(Trim(item)))
                        condiciones.Add(t)
                    Else
                        l.Add(item) 'no es una condición del encabezado
                    End If
                Next
                Premisas(0).sentencia.antecedente = Join(l.ToArray, "&")

                'caso 2 hay varios elementos **********************************************************
            ElseIf Premisas.Count > 1 Then
                Dim l As New List(Of String)
                'ir de premisa en premisa

                For i = 0 To Premisas.Count - 1
                    'separar los condicionantes.
                    'si una condicion está en todas las otras premisas, sacar e incluir en el encabezado de condicones

                    'la premisa contiene declaraciones de inclusión?
                    Dim c = PSplit(Premisas(i).sentencia.antecedente, "&")


                    For Each item As String In c
                        If OperNoAnid(item, " en ") Then
                            Dim t As New sentenciaComp
                            t.fromCodeStr(item)
                            If Not EnLista(t, condiciones) Then condiciones.Add(t)
                        Else
                            l.Add(item) 'no es una condición del encabezado
                        End If
                    Next
                    Premisas(i).sentencia.antecedente = Join(l.ToArray, "&")
                    If Premisas(i).sentencia.antecedente = "" Then
                        Premisas(i).sentencia.antecedente = Premisas(i).sentencia.consecuente
                        Premisas(i).sentencia.consecuente = ""
                    End If
                Next
            End If

            'contraer los conjuntos en común a una enumeración a en B, b en B --> a,b en B

            'tomar un valor de conjunto y juxtaponer todos los que tengan ese valor


            For i = 0 To condiciones.Count - 1
                Dim u = PSplit(condiciones(i).antecedente, " en ")

                For j = i + 1 To condiciones.Count - 1
                    If Not condiciones(j).antecedente = "" Then
                        Dim v = PSplit(condiciones(j).antecedente, " en ")
                        If u(1) = v(1) Then
                            condiciones(i).antecedente = v(0) & ";" & POmitir(Trim(condiciones(i).antecedente)) 'agregar el antencedente al i
                            condiciones(j).antecedente = ""
                        End If
                    End If
                Next
            Next


            Dim k As Long = 0

            Do While condiciones.Count > k
                If condiciones(k).antecedente = "" Then
                    condiciones.RemoveAt(k)
                Else
                    k += 1
                End If
            Loop


        End Sub

    End Class

    Class libro
        Property label As String
        Property título As String
        Property autor As String
        Property encabezado As String
        Property cuerpo

        Class part
            Property text
            Property chapters As List(Of chapter)

        End Class
        Class chapter
            Property title As String
            Property text
            Property sections As List(Of section)
        End Class
        Class section
            Property title As String
            Property text
            Property subsections As List(Of subsecton)
        End Class
        Class subsecton
            Property title As String
            Property text
            Property subsubsections As List(Of subsubsection)
        End Class
        Class subsubsection
            Property title As String
            Property text
        End Class
        Class paragraph
            Property title
            Property text
        End Class


        Function Spanish(Optional latex As Boolean = False, Optional modo As String = "")

            If latex Then '*********************

                'Imprimir encabezado
                encabezado = "\documentclass[12pt]{book}" & vbLf &
            "\usepackage[spanish]{babel}" & vbLf &
            "\usepackage{amsmath}" & vbLf &
            "\usepackage[utf8]{inputenc}" & vbLf &
             "\newtheorem{theorem}{Teorema}[chapter]" & vbLf &
            "\newtheorem{definition}{Definición}[chapter]" & vbLf &
            "\newtheorem{axiom}{Axioma}[chapter]" & vbLf &
            "\newtheorem{proof}{Demostración}" & vbLf &
            "\title{" & título & "}" & vbLf &
            "\author{" & autor & "}" & vbLf &
            "\date{}" & vbLf

                '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                cuerpo = "\begin{document}"
                cuerpo &= vbLf & "\maketitle" & vbLf

                If modo = "" Then
                    cuerpo &= TeoríaEnCurso.Spanish(True)
                ElseIf modo = "M" Then
                    cuerpo &= EstructuraEnCurso.Spanish(True, "expand")
                End If


                cuerpo &= vbLf & "\\" & "\small{Este libro fue generado automáticamene por NOVA.} \\" & vbLf
                cuerpo &= "\small{Salvador D. Escobedo, Julio 2016}." & vbLf

                cuerpo &= vbCrLf & "\end{document}"
                '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            Else '*******************************

            End If

            Return encabezado & cuerpo
        End Function

    End Class


    Public Class concordancia
        Property género As String = "" 'masculino = m, femenino = f, neutro = n
        Property número As String = "" 'singular = s, plural = p
        Property persona As String = "3" 'primera persona = 1, segunda persona = 2, tercer persona = 3
        Property caso As String = "n" 'genitivo = g, acusativo = a, ablativo = b, vocativo = v, dativo = d, nomitativo = o
        Property tiempo As String = "p" 'presente = p, pasado = r, futuro = f
    End Class

#Region "TRANSFORMACION DE CONCEPTOS"

    'Hace un concepto simple a objeto concepto general
    Function G(ParamArray c() As concepto.simple) As concepto.general
        Dim r As New concepto.general

        For i = 0 To UBound(c)
            Union(r, c(i))
        Next

        Return r
    End Function

    Function G(NombreID As String, tipo As String, categoria As String, Optional finito As Boolean = True) As concepto.simple
        Dim r As New concepto.simple

        r.nombreID = NombreID
        r.finito = finito
        r.tipo = tipo
        r.categoria = categoria
        Return r
    End Function

    'Esta función niega un concepto
    Function Neg(s As concepto.simple) As concepto.simple
        Dim r As New concepto.simple
        r.nombreID = s.nombreID
        r.categoria = s.categoria
        r.tipo = s.tipo
        r.finito = Not s.finito

        Return r
    End Function

    Function Neg(ByVal c As concepto.general) As concepto.general
        'invertir el estado del primer concepto
        Dim r As New concepto.general

        r.positivo = Not c.positivo
        r.tipo = c.tipo
        r.extension = c.extension

        For i = 0 To c.conceptos.Count - 1
            Dim r2 As New concepto.simple
            r2.categoria = c.conceptos(i).categoria
            r2.finito = c.conceptos(i).finito
            r2.nombreID = c.conceptos(i).nombreID
            r2.tipo = c.conceptos(i).tipo
            r.conceptos.Add(r2)
        Next


        r.categoria = c.categoria

        Dim flag As Boolean = False
        For i = 0 To c.conceptos.Count - 1
            If c.conceptos(i).tipo = "c" Then
                r.conceptos(i).finito = Not c.conceptos(i).finito
                flag = True
                Exit For
            End If
        Next

        If Not flag Then
            r.conceptos(0).finito = Not c.conceptos(0).finito
        End If

        Return r
    End Function

    Function Neg(s As sentenciaComp) As sentenciaComp
        Dim r As New sentenciaComp
        If s.consecuente = "" Then
            r.antecedente = "no(" & s.antecedente & ")"
            r.consecuente = ""
        End If

        r.NombreID = r.NombreID

        Return r
    End Function

    'debuelve la inversión simple de una sentencia
    Function Conversio(s As sentencia) As sentencia

        Dim X As New sentencia
        X = s.copy

        If Not (s.Copula.Equivale(est) Or s.Copula.Equivale(vac)) Then
            Return X
            Exit Function
        End If


        If X.tipo = "E" Or X.tipo = "I" Then 'Simpliciter
            Dim y As concepto.general
            y = X.S
            X.S = X.P
            X.P = y
        ElseIf X.tipo = "A" Then 'per accidens
            Dim y As concepto.general
            y = X.S
            X.S = X.P
            X.P = y
            X.CU = Particularizar(X.CU)
            X.tipo = X.getTipo
        End If

        Return X
    End Function

    Function Particularizar(C As concepto.general) As concepto.general 'devuelve el particular de un cuantificador universal
        Dim r As New concepto.general
        r.positivo = C.positivo
        r.tipo = C.tipo
        r.extension = "p"
        r.categoria = C.categoria

        'hacer copia del concepto para no modificar instancia
        For i = 0 To C.conceptos.Count - 1
            Dim r2 As New concepto.simple
            r2.categoria = C.conceptos(i).categoria
            r2.finito = C.conceptos(i).finito
            r2.nombreID = C.conceptos(i).nombreID
            r2.tipo = C.conceptos(i).tipo
            r.conceptos.Add(r2)
        Next

        'particularizar el generalizador
        For i = 0 To r.conceptos.Count - 1
            If r.Equivale(todo) Or r.Equivale(algun) Then
                r.conceptos(i).nombreID = "x@"
                Exit For
            End If
        Next

        Return r
    End Function

    'Devuelve la subordinada de una sentencia
    Function Particularizar(s As sentencia) As sentencia
        Dim X As New sentencia
        X = s.copy
        X.CU = Particularizar(X.CU)
        Return X
    End Function

    Function Generalizar(C As concepto.general) As concepto.general 'devuelve el universal de un concepto particular
        Dim r As New concepto.general
        r.positivo = C.positivo
        r.tipo = C.tipo
        r.extension = "u"
        r.categoria = C.categoria

        'hacer copia del concepto para no modificar instancia
        For i = 0 To C.conceptos.Count - 1
            Dim r2 As New concepto.simple
            r2.categoria = C.conceptos(i).categoria
            r2.finito = C.conceptos(i).finito
            r2.nombreID = C.conceptos(i).nombreID
            r2.tipo = C.conceptos(i).tipo
            r.conceptos.Add(r2)
        Next

        'generalizar el particularizador
        For i = 0 To r.conceptos.Count - 1
            If r.Equivale(algun) Or r.Equivale(todo) Then
                r.conceptos(i).nombreID = "@"
                Exit For
            End If
        Next

        Return r
    End Function

#End Region

#Region "TRANSFORMACIÓN DE ENUNCIADOS"
    'coloca una sentencia en su forma estándar
    Function Canonic(S As sentencia) As sentencia
        'la forma canónica para sentencias es: cuantificador positvio, sujeto, cópula, predicado positivo.
        'si el predicado no es positivo: todo hombre es no racional --> todo hombre no es racional
        'si el cuantificador es negativo: no todo hombre es racional --> algún hombre no es racional
        '                                 no algún hombre es racional --> todo hombre no es racional



        If Not S.Copula.Equivale(vac, True) Then 'si hay cópula
            If Not S.P.positivo Then
                S.P = Neg(S.P)
                S.Copula = Neg(S.Copula)
            End If
            If Not S.CU.positivo Then
                S.CU = Neg(S.CU)
                If S.CU.extension = "u" Then S.CU = Particularizar(S.CU)
                If S.CU.extension = "p" Then S.CU = Generalizar(S.CU)
                S.Copula = Neg(S.Copula)
            End If
        Else 'si no hay cópula

            If Not S.CU.positivo Then
                S.CU = Neg(S.CU)
                If S.CU.extension = "u" Then S.CU = Particularizar(S.CU)
                If S.CU.extension = "p" Then S.CU = Generalizar(S.CU)
                S.P = Neg(S.P)
            End If

        End If

        'si el sujeto es de tipo singular, entonces el cuantificador debe ser general
        If S.S.tipo = "s" Then S.CU = Generalizar(S.CU)
        Return S
    End Function

    'Devuelve la subalterna de un enunciado
    Function SubAlt(S As sentencia) As sentencia
        Return S
    End Function

    'devuelve la contradictoria de un enunciado
    Function Neg(s As sentencia) As sentencia
        Dim X As New sentencia
        X.S = s.S
        X.CU = s.CU
        X.concordancia = s.concordancia

        If s.Copula.Equivale(vac) Then 'caso sin cópula
            X.P = Neg(s.P)
            X.Copula = vac
        Else 'caso con cópula
            X.P = s.P
            X.Copula = Neg(s.Copula)
        End If

        X.tipo = X.getTipo

        Return X
    End Function

    Function Neg(ByVal s As String) As String
        s = "no(" & s & ")"
        Return s
    End Function

    'determina si dos sentencias son contradictorias
    Function Contradic(a As sentencia, b As sentencia) As Boolean
        Dim contrad As Boolean = False


        If a.Copula.Equivale(vac, True) Then 'cuando no tiene cópula
            If a.CU.Equivale(b.CU) And a.S.Equivale(b.S, True) And a.Copula.Equivale(b.Copula) Then
                If a.P.Equivale(Neg(b.P), True) Then
                    contrad = True
                End If
            End If
        Else 'cuando tiene cópula

            If a.CU.Equivale(b.CU) And a.S.Equivale(b.S, True) And a.P.Equivale(b.P, True) Then
                If a.Copula.Equivale(Neg(b.Copula), True) Then
                    contrad = True
                End If
            End If

        End If

        Return contrad
    End Function

    'devuelve la contraria o subcontraria de un enunciado
    Function Cont(s As sentencia) As sentencia

        Return s
    End Function

#End Region

#Region "OPERACIONES DE AVANCE"


    'Esta función une conceptos, eligiendo atomáticamente el tipo de unión de acuerdo con la categoría de los conceptos que se desea unir.
    Sub Union(CC As concepto.general, c As concepto.simple)
        Dim cs As New concepto.simple
        cs = c

        CC.conceptos.Add(cs)
        CC.categoria = CC.Getcategoria
        CC.extension = CC.GetExtension
        CC.positivo = CC.GetPositivo
        CC.tipo = CC.GetTipo

        'si tiene cuantificadores y la categoría es t transformar a s
        If Not CC.extension Is Nothing Then
            If Not CC.extension = "indef" And cs.categoria = "t" And Not cs.tipo = "c" Then cs.categoria = "s"  'Ejemplo, todo  MORTAL(t) ---> todo MORTAL(s)
        End If




    End Sub

    Function Juicio(CU As concepto.general, S As concepto.general, Copula As concepto.general, P As concepto.general) As sentencia
        Dim A As New sentencia

        A.CU = CU
        A.Copula = Copula
        A.S = S
        A.P = P

        A.tipo = A.getTipo
        A.getConcordancia()
        Return A
    End Function

    Function Razonamiento(P1 As sentencia, P2 As sentencia) As sentencia
        Reporte = ""
        'Reducir las sentencias a una forma estándar

        'Determinar la figura y modo de razonamiento y sacar la conclusión
        Dim C As New sentencia


        If P1.S.Equivale(P2.P) Then 'PRIMERA FIGURA

            If P1.tipo = "A" And P2.tipo = "A" Then 'Barbara
                C.P = P1.P
                C.S = P2.S 'A
                C.Copula = P1.Copula
                C.CU = todo
                Reporte &= "FigI:Barbara"
            ElseIf P1.tipo = "A" And P2.tipo = "I" Then 'Darii
                C.P = P1.P
                C.S = P2.S 'I
                C.Copula = P1.Copula
                C.CU = algun
                Reporte &= "FigI:Darii"
            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Celarent
                C.P = P1.P
                C.S = P2.S 'A->E
                C.Copula = P1.Copula
                C.CU = todo
                Reporte &= "FigI:Celarent"
            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Ferio
                C.P = P1.P
                C.S = P2.S 'O
                C.Copula = P1.Copula
                C.CU = algun
                Reporte &= "FigI:Ferio"
            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigI:invalid"
            End If

        ElseIf P1.P.Equivale(P2.P) Then 'SEGUNDA FIGURA

            'recordar: la cópula debe ser una relación de equivalencia (analizar los casos con conceptos no singulares)
            If P1.P.tipo = "s" And P1.tipo = "A" And P2.S.tipo = "s" And (P1.Copula.positivo Or P2.Copula.positivo) Then 'terna *******************
                C.P = P1.S
                C.S = P2.S

                'si una de ellas es negativa, la conclusión será negativa
                If Not P1.Copula.positivo And P2.Copula.positivo Then 'si la primera es negativa, la conclusión será negativa
                    C.Copula = P1.Copula

                ElseIf P1.Copula.positivo And Not P2.Copula.positivo Then 'si la segunda es negativa, la conclusión será negativa
                    C.Copula = P2.Copula

                Else 'si ambas son positvias, la conclusión será positiva
                    C.Copula = P1.Copula
                End If

                C.CU = P2.CU
                Reporte &= "FigII:terna de equivalencia" '*****************************************************

            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Cesare
                C.P = P1.S
                C.S = P2.S 'E
                C.Copula = Neg(est)
                C.CU = todo
                Reporte &= "FigII:Cesare"

            ElseIf P1.tipo = "A" And P2.tipo = "E" Then 'Camestres
                C.P = P1.S
                C.S = P2.S 'E
                C.Copula = Neg(est)
                C.CU = todo
                Reporte &= "FigII:Camestres"

            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Festino
                C.P = P1.S
                C.S = P2.S 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigII:Festino"

            ElseIf P1.tipo = "A" And P2.tipo = "O" Then 'Baroco
                C.P = P1.S
                C.S = P2.S 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigII:Baroco"
            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigII:invalid"
            End If

        ElseIf P1.S.Equivale(P2.S) Then 'TERCERA FIGURA

            If P1.tipo = "A" And P2.tipo = "A" Then 'Darapti
                C.P = P1.P
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "FigIII:Darapti"
            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Felapton
                C.P = P1.P
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigIII:Felapton"

            ElseIf P1.tipo = "I" And P2.tipo = "A" Then 'Disamis
                C.P = P1.P
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "FigIII:Disamis"

            ElseIf P1.tipo = "A" And P2.tipo = "I" Then 'Datisi
                C.P = P1.P
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "FigIII:Datisi"

            ElseIf P1.tipo = "O" And P2.tipo = "A" Then 'Bocardo
                C.P = P1.P
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigIII:Bocardo"

            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Ferison
                C.P = P1.P
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigIII:Ferison"

            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigIII:invalid"
            End If

        ElseIf P1.P.Equivale(P2.S) Then 'CUARTA FIGURA

            If P1.tipo = "A" And P2.tipo = "A" Then 'Baralipton / Bramantip
                C.P = P1.S
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "FigIV:Bramantip"

            ElseIf P1.tipo = "A" And P2.tipo = "E" Then 'Celantes / Camenes
                C.P = P1.S
                C.S = P2.P 'E
                C.Copula = Neg(est)
                C.CU = todo
                Reporte &= "FigIV:Camenes"

            ElseIf P1.tipo = "I" And P2.tipo = "A" Then 'Dabitis / Dimaris
                C.P = P1.S
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "FigIV:Dimaris"

            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Fapesmo / Fesapo
                C.P = P1.S
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigIV:Fesapo"

            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Frisesomorum / Fresison
                C.P = P1.S
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "FigIV:Fresison"

            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigIV:invalid"
            End If

        Else
            'no hay conclusión
        End If

        C.tipo = C.getTipo
        C.getConcordancia()

        Return C
    End Function

    Sub AddCop(c As concepto.general)
        Dim copula As New concepto.simple
        copula.categoria = "h"
        copula.finito = True
        copula.nombreID = "es"
        copula.tipo = "q"
        c.conceptos.Add(copula)
        c.categoria = "h"
    End Sub


#End Region


#Region "ANÁLISIS DE ENUNCIADOS"

    'Genera todas las conclusiones silogísticas a partir de una lista de premisas
    Function Argumentar(A As argumentación) As argumentación
        Dim H As New List(Of sentencia)
        Dim Hp As New argumentación
        Dim B As New List(Of sentencia)
        Dim r As New sentencia
        Dim c As Long
        Dim n_max = 1000 'númer maximo de razonamientos

        For Each item As premisa In A.premisas
            B.Add(item.sentencia)
        Next

        For i = 0 To A.premisas.Count - 1
            For j = 0 To A.premisas.Count - 1
                If Not i = j Then

                    'razonar de combinación en combinación
                    Reporte = ""
                    r = Razonamiento(A.premisas(i).sentencia, A.premisas(j).sentencia)
                    If Not Reporte Like "*invalid" And Not Reporte = "" Then
                        'si la conclusión no se encuentra ya en la lista, añadir:
                        r = Canonic(r)
                        If Not Tautologic(r) And Not EnLista(r, B) And Not EnLista(r, H) Then 'sólo incluir casos no repetidos   'quitar tautologías?
                            H.Add(r)

                            Dim p As New premisa
                            p.sentencia = r
                            p.mayor = A.premisas(i).sentencia
                            p.menor = A.premisas(j).sentencia
                            p.paso = Reporte
                            p.mark = "prem"

                            Hp.premisas.Add(p)
                            Console.WriteLine("")
                            Console.WriteLine("*********************************************")
                            Console.WriteLine(A.premisas(i).sentencia.Spanish & vbLf & A.premisas(j).sentencia.Spanish & vbLf & "Luego " & r.Spanish & vbLf & " ->" & Reporte)

                            'nota: esta función no revisa consistencia, pues eso debe revisarse antes de generar la argumentación

                            Console.WriteLine("*********************************************")
                            Console.WriteLine("")



                        End If
                    End If
                    c += 1
                End If
                If c > n_max Then Exit For
            Next
        Next

        Return Hp
    End Function

    'devuelve todas las deducciones posibles de una lista de sentencias a un nivel de deducción categórica
    Function Deduce(S As List(Of sentencia), Optional exhaustivo As Boolean = False) As List(Of sentencia) 'devuelve en forma canónica
        Dim H As New List(Of sentencia)
        Dim r As New sentencia
        Dim c As Long
        Dim n_max = 1000 'númer maximo de razonamientos

        For i = 0 To S.Count - 1
            For j = 0 To S.Count - 1
                If Not i = j Then

                    'razonar de combinación en combinación
                    Reporte = ""
                    r = Razonamiento(S(i), S(j))
                    If Not Reporte Like "*invalid" And Not Reporte = "" Then
                        'si la conclusión no se encuentra ya en la lista, añadir:
                        r = Canonic(r)
                        If Not Tautologic(r) And Not EnLista(r, S) And Not EnLista(r, H) Then 'sólo incluir casos no repetidos   'quitar tautologías?
                            H.Add(r)
                            If exhaustivo Then
                                Console.WriteLine("")
                                Console.WriteLine("*********************************************")
                                Console.WriteLine(S(i).Spanish & vbLf & S(j).Spanish & vbLf & "Luego " & r.Spanish & vbLf & " ->" & Reporte)
                                'Revisar consistencia
                                Dim T As New List(Of sentencia)
                                T.AddRange(S)
                                T.AddRange(H)
                                If Inconsistent(r, T) Then Console.WriteLine("INCONSISTENTE!")
                                If SelfContradic(r) Then Console.WriteLine("CONTRADICCIÓN!")
                                Console.WriteLine("*********************************************")
                                Console.WriteLine("")
                            End If
                        End If
                    End If
                    c += 1
                End If
                If c > n_max Then Exit For
            Next
        Next

        Return H
    End Function

    Function RazonamientoH(P1 As sentenciaComp, P2 As sentenciaComp) As sentenciaComp
        'modus ponendo ponens
        'si A entonces B
        'A
        'luego B
        Reporte = ""
        Dim r As New sentenciaComp
        If P2.antecedente = P1.antecedente And P1.consecuente = "" And Not P2.consecuente = "" Then
            r.antecedente = P2.consecuente
            Reporte = "FigI:ponendo ponens"
        ElseIf P1.antecedente = P2.antecedente And P2.consecuente = "" And Not P1.consecuente = "" Then
            r.antecedente = P1.consecuente
            Reporte = "FigI:ponendo ponens"
        ElseIf Neg(P1.consecuente) = P2.antecedente And P2.consecuente = "" And Not P1.consecuente = "" Then
            r.antecedente = Neg(P1.antecedente)
            Reporte = "FigI:tollendo tollens"

        Else
                Reporte = "Hipotetic:invalid"
        End If

        Return r
    End Function

    'devuelve todas las deducciones posibles de una lista de sentencias hipotéticas
    Function DeduceH(S As List(Of sentenciaComp)) As List(Of sentenciaComp)

        Dim H As New List(Of sentenciaComp)
        Dim r As New sentenciaComp
        Dim c As Long
        Dim n_max = 1000 'númer maximo de razonamientos

        For i = 0 To S.Count - 1
            For j = 0 To S.Count - 1
                If Not i = j Then

                    'razonar de combinación en combinación
                    Reporte = ""
                    r = RazonamientoH(S(i), S(j))
                    If Not Reporte Like "*invalid" And Not Reporte = "" Then
                        H.Add(r)
                    End If
                    c += 1
                End If
                If c > n_max Then Exit For
            Next
        Next

        Return H
    End Function

    'devuelve todas las deducciones posibles de una lista de sentencias a varios niveles de deducción
    Function Analisis(S As List(Of sentencia), n As Long) As List(Of sentencia)
        Dim X As New List(Of sentencia)
        X.AddRange(S)

        If n < 1 Then n = 1 'el mínimo es 1

        For i = 1 To n
            X.AddRange(Deduce(X))
        Next

        Return X
    End Function

    Function GenerarTeoríaHCompleta(S As List(Of sentenciaComp), Optional max_itear As Long = 1000, Optional max_num_sent As Long = 10000) As teoríaH
        Dim X As New argumentaciónH
        Dim T As New teoríaH
        '  Dim counter As Long = 0

        X.AddRange(S)

        For i = 0 To S.Count - 1
            Dim p As New premisaH
            p.sentencia = S(i)
            T.axiomas.Add(p)
        Next


        'counter = X.premisas.Count
        'For i = 1 To max_itear
        '    X.AddRange(Argumentar(X))
        '    If counter = X.premisas.Count Then
        '        Exit For
        '    Else
        '        counter = X.premisas.Count
        '    End If
        'Next


        'T.teoremas = X.premisas
        'T.GenerarConceptos()
        Return T
    End Function

    Function GenerarTeoríaCompleta(S As List(Of sentencia), Optional max_itear As Long = 1000, Optional max_num_sent As Long = 10000) As teoría
        Dim X As New argumentación
        Dim T As New teoría
        Dim counter As Long = 0

        X.AddRange(S)

        For i = 0 To S.Count - 1
            Dim p As New premisa
            p.sentencia = S(i)
            T.axiomas.Add(p)
        Next


        counter = X.premisas.Count
        For i = 1 To max_itear
            X.AddRange(Argumentar(X))
            If counter = X.premisas.Count Then
                Exit For
            Else
                counter = X.premisas.Count
            End If
        Next


        T.teoremas = X.premisas
        T.GenerarConceptos()
        Return T
    End Function
    'Genera la teoría completa a partir de una lista de proposiciones
    Function ExpandirLista(S As List(Of sentencia), Optional max_itear As Long = 1000, Optional max_num_sent As Long = 10000) As List(Of sentencia)
        Dim X As New List(Of sentencia)
        Dim counter As Long = 0

        X.AddRange(S)
        counter = X.Count
        For i = 1 To max_itear
            X.AddRange(Deduce(X))
            If counter = X.Count Then
                Exit For
            Else
                counter = X.Count
            End If
        Next

        Return X
    End Function

    'Genera la lista de axiomas de los cuales se deduce una lista dada 
    Function ContraerLista(S As List(Of sentencia))

        Dim X As New List(Of sentencia)
        Dim Y As List(Of sentencia)
        X.AddRange(S)
        Dim s1 As sentencia
        Dim i As Long = 0
        Dim n As Long = 0
        Dim m As Long = 0

        Do While i < X.Count
            m += 1
            Y = New List(Of sentencia)
            Y.AddRange(X)
            s1 = New sentencia
            s1 = X(i)
            Y.Remove(s1)
            'vamos a ver si esta sentencia se sigue de alunas otras dos de la lista
            If SeDeduceSeparado(s1, Y, True) Or SeDeduce(s1, Y) Then
                X.Remove(s1)
            Else
                i += 1
            End If

            If n = 9 Then
                n = 0
                Console.WriteLine("Sentencias procesadas: " & m & " de " & S.Count)
            Else
                n += 1
            End If

        Loop

        Console.WriteLine("Sentencias procesadas: " & S.Count & " de " & S.Count)

        Return X
    End Function
    'Revisa la consistencia de una lista de propoisiciones
    Function InconsistentList(X As List(Of sentencia)) As Boolean
        Dim r As Boolean = False

        For i = 0 To X.Count - 1
            For j = i To X.Count - 1
                If Inconsistent(X(i), X) Then
                    r = True
                End If
            Next
        Next
        Return r
    End Function

    'Verifica rápidamente la sentencia p, suponiendo verdaderas las sentencias de una lista
    Function EnLista(r As sentencia, Optional S As List(Of sentencia) = Nothing) As Boolean
        Dim repetido As Boolean = False
        If S Is Nothing Then S = Sentencias

        For Each item As sentencia In S
            If item.Equivale(r) Then
                repetido = True
            End If
        Next

        Return repetido
    End Function

    Function EnLista(r As sentenciaComp, Optional S As List(Of sentenciaComp) = Nothing) As Boolean
        Dim repetido As Boolean = False
        If S Is Nothing Then S = SentenciasComp

        For Each item As sentenciaComp In S
            If item.Equivale(r) Then
                repetido = True
            End If
        Next

        Return repetido
    End Function

    'determina si una sentencia se deduce de una lista de sentencias (inmediatamente: si se sigue de alguna sentencia de la lista por conversión)
    Function SeDeduce(r As sentencia, S As List(Of sentencia)) As Boolean
        Dim res As Boolean = False

        If Tautologic(r) Then
            res = True

        ElseIf SeDeduceSeparado(r, Deduce(S), True) Then 'se sigue por un silogismo
            res = True
        ElseIf EnLista(r, ExpandirLista(S)) Then 'fuerza bruta
            res = True
        End If

        Return res
    End Function

    Function SeDeduceSeparado(a As sentencia, S As List(Of sentencia), Optional porRepetición As Boolean = True) As Boolean 'indica si a se deduce de b
        Dim res As Boolean = False
        For i = 0 To S.Count - 1
            If SeDeduceSeparado(a, S(i), True) Then  'se deduce inmediatamente con repetición
                Return True
                Exit Function
            End If
        Next
        Return res
    End Function


    Function SeDeduceSeparado(a As sentencia, b As sentencia, Optional porRepetición As Boolean = True) As Boolean 'indica si a se deduce de b
        Dim res As Boolean
        If porRepetición And a.Equivale(b) Then 'por repetición
            res = True
        ElseIf (b.tipo = "A" Or b.tipo = "E") And a.Equivale(Particularizar(b)) Then 'por subalternación
            res = True
        ElseIf a.Equivale(Conversio(b)) Then 'por conversión
            res = True
        End If

        Return res
    End Function

    'Determina si la nueva sentencia es inconsistente respecto a las otras sentencias de una la lista
    Function Inconsistent(r As sentencia, Optional S As List(Of sentencia) = Nothing) As Boolean
        If S Is Nothing Then S = Sentencias
        Dim flag As Boolean = False

        For Each item As sentencia In S
            If Contradic(r, item) Then
                flag = True
                Exit For
            End If
        Next

        Return flag
    End Function

    'encuentra las inconsistencias y contradiciones y devuelve una lista de ellas
    Function Inconsistencias(S As List(Of sentencia)) As List(Of sentencia)

        Dim H As New List(Of sentencia)

        For i = 0 To S.Count - 1
            If SelfContradic(S(i)) Then
                H.Add(S(i))
                Exit For
            End If
            For j = i + 1 To S.Count - 1
                If Contradic(S(j), S(i)) Then
                    H.Add(S(i))
                    H.Add(S(j))
                    Exit For
                End If
            Next
        Next

        Return H
    End Function

    'determina si una sentencia es contradictoria 'la sentencia se asume en forma canónica
    Function SelfContradic(S As sentencia) As Boolean
        Dim r As Boolean = False
        'asumimos equivalencia dévil por si hay diferencias de categoría
        If S.Copula.Equivale(est) Then
            'caso 1: hay cópula es
            If S.S.Equivale(S.P) And Not S.Copula.positivo Then r = True
        ElseIf S.Copula.Equivale(vac) Then
            'caso 2: no hay cópula
            If S.S.Equivale(S.P) And Not S.P.positivo Then r = True
        End If

        Return r
    End Function

    'determina si una sentencia es tautológica 'la sentencia se asume en forma canónica
    Function Tautologic(S As sentencia) As Boolean
        Dim r As Boolean = False
        'asumimos equivalencia dévil por si hay diferencias de categoría
        If S.Copula.Equivale(est) Then
            'caso 1: hay cópula es
            If S.S.Equivale(S.P) And S.Copula.positivo Then r = True
        ElseIf S.Copula.Equivale(vac) Then
            'caso 2: no hay cópula
            If S.S.Equivale(S.P) And S.P.positivo Then r = True
        End If

        Return r
    End Function

#End Region

#Region "INTRODUCCIÓN DEL MODELO"

    '************************************************************************
    Dim todoS As New concepto.simple
    Public todo As New concepto.general
    Dim algunS As New concepto.simple
    Dim algun As New concepto.general
    Public ErrCon As New sentencia
    Dim estS As New concepto.simple
    Public est As New concepto.general
    Public vac As New concepto.general

    '***************************************************************************

    Sub Fill() 'Carga variables conceptos fundamentales para las funciones de razonamiento

        'sentencia de error
        ErrCon = Juicio(G(G("nada", "c", "s")), G(G("se", "q", "s")), G(G("esta", "q", "h")), G(G("demostrando", "c", "h")))

        'cuantificadores básicos
        estS.categoria = "h"
        estS.finito = True
        estS.nombreID = "es"
        estS.tipo = "q"

        est = G(estS)

        vac = G(G("|", "q", "h")) 'copula vacía

        todoS.categoria = "t"
        todoS.finito = True
        todoS.nombreID = "@"
        todoS.tipo = "c"

        todo = G(todoS)


        algunS.categoria = "t"
        algunS.finito = True
        algunS.nombreID = "x@"
        algunS.tipo = "c"

        algun = G(algunS)

    End Sub


#End Region



#Region "LENGUAJE"

    Function Dcs(s As String, concordancia As concordancia, tipo As String, categoria As String) As String 'Diccionario Código-Español

        'remplazos fundamentales: (no dependen del diccionario)
        If concordancia Is Nothing Then concordancia = New concordancia

        'CONCEPTOS FUNDAMENTALES]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]
        'caso nominativo (cuando el sujeto es un conjunto)

        If s = "@" And concordancia.caso = "n" Then
            s = "todo"
        ElseIf s = "x@" And concordancia.caso = "n" Then
            s = "algún"
        ElseIf s = "|" Then
            s = ""

            'caso genitivo (cuando el sujeto es una acción tipo h)

        ElseIf s = "@" And concordancia.caso = "g" Then
            s = "todo aquél que"
        ElseIf s = "x@" And concordancia.caso = "g" Then
            s = "alguno que"
        ElseIf s = "|" Then
            s = ""

            'verbo ser (todo en 3era persona)
        ElseIf s = "es" And concordancia.tiempo = "p" And concordancia.número = "s" Then
            s = "es"
        ElseIf s = "es" And concordancia.tiempo = "p" And concordancia.número = "p" Then
            s = "son"
        ElseIf s = "es" And concordancia.tiempo = "r" And concordancia.número = "s" Then
            s = "era"
        ElseIf s = "es" And concordancia.tiempo = "r" And concordancia.número = "p" Then
            s = "eran"
            ']]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]


        Else 'OTROS CONCEPTOS EN DICCIONARIO ]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]

            'RECUPERAR DEL DICCIONARIO:
            If SpanishDc.ContainsKey(s) Then s = SpanishDc(s)

            'si la cadena no fue reconocida, conservar el NombreID del concepto.

            'AJUSTE DE LA CONCORDANCIA*******************************************************

            If categoria = "h" Then 'si es un verbo

                'conjugar de acuerdo con las exigencias de concordancia
                Dim f As String
                f = concordancia.tiempo & concordancia.persona & concordancia.número
                s = Verbo(s, f)

            Else 'categoría s o t

                'NÚMERO
                If concordancia.número = "p" Then

                    'generar plural
                    s = Plural(s)

                End If
                '********************************************************************************

            End If




        End If

        Return s
    End Function

    Function Cs(s As String) As String 'contracciones español
        Dim r As String = " " & s & " "

        r = Replace$(r, " no algún ", " ningún ")
        r = Replace$(r, " no alguno ", " ninguno ")
        r = Replace$(r, "  ", " ")

        Return Trim(r)
    End Function

    Function Dce(s As String, Optional concordancia As String = "") As String 'Diccionario Código-Inglés

        If s = "@" Then
            s = "every"
        ElseIf s = "x@" Then
            s = "some"
        ElseIf s = "es" Then
            s = "is"
        ElseIf s = "|" Then
            s = ""
        Else
            If EnglishDc.ContainsKey(s) Then s = EnglishDc(s)
        End If
        Return s
    End Function

    Function Ce(s As String) As String 'contracciones inglés
        Dim r As String = " " & s & " "

        r = Replace$(r, " not some ", " no ")
        r = Replace$(r, " non-is ", " is not ")
        r = Replace$(r, "  ", " ")

        Return Trim(r)
    End Function

    Function Dcp(s As String, Optional concordancia As String = "") As String 'Diccionario Código-Portugés
        If s = "@" Then
            s = "todo"
        ElseIf s = "x@" Then
            s = "algum"
        ElseIf s = "es" Then
            s = "é"
        ElseIf s = "|" Then
            s = ""
        Else
            If PortugueseDc.ContainsKey(s) Then s = PortugueseDc(s)
        End If
        Return s
    End Function

    Function Cp(s As String) As String 'contracciones porgués
        Dim r As String = " " & s & " "

        r = Replace$(r, " nem algum ", " nenhum ")
        r = Replace$(r, " não-é ", " não é ")
        r = Replace$(r, "  ", " ")

        Return Trim(r)
    End Function

#End Region

    'AMDG
End Module
