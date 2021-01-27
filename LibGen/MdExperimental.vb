Module MdExperimental
    Dim CN As New concepto.general
    Friend Reporte As String

    Class concepto
        Class simple
            Property finito As Boolean = True 'concepto finito o infinito
            Property categoria As String 'categoria s, t, h
            Property tipo As String 'tipo cuantitativo, cualitativo o singular: c,q,s
            Property nombreID As String

            Function CodeStr() As String
                Dim r As String = ""
                If Not finito Then r &= "n"
                r &= categoria
                r &= tipo
                r &= nombreID
                Return r
            End Function

            Function Spanish() As String
                Dim r As String = ""
                If tipo = "c" Then 'cuantitativo  ejemplo: no todo, diez, nada

                    If Not finito Then r &= "no "
                    r &= nombreID

                ElseIf tipo = "q" Then 'cualitativo
                    If categoria = "s" Then
                        If Not finito Then r &= "no-"
                    Else
                        If Not finito Then r &= "no "
                    End If
                    r &= nombreID
                Else 'singular
                    If Not finito Then r &= "no-"
                    r &= nombreID
                End If

                Return r
            End Function
            Function English() As String
                Dim r As String = ""
                If tipo = "c" Then 'cuantitativo  ejemplo: no todo, diez, nada

                    If Not finito Then r &= "not "
                    r &= nombreID

                ElseIf tipo = "q" Then 'cualitativo
                    If categoria = "s" Then
                        If Not finito Then r &= "non-"
                    Else
                        If Not finito Then r &= "non-"
                    End If
                    r &= nombreID
                Else 'singular
                    If Not finito Then r &= "non-"
                    r &= nombreID
                End If

                Return r
            End Function
            Function Portuguese() As String
                Dim r As String = ""
                If tipo = "c" Then 'cuantitativo  ejemplo: no todo, diez, nada

                    If Not finito Then r &= "nem "
                    r &= nombreID

                ElseIf tipo = "q" Then 'cualitativo  
                    If categoria = "s" Then
                        If Not finito Then r &= "não "
                    Else
                        If Not finito Then r &= "não-"
                    End If
                    r &= nombreID
                Else 'singular
                    If Not finito Then r &= "não-"
                    r &= nombreID
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
            Property tipo As String
            Property categoria As String
            Property extension As String 'universal o particular
            Property positivo As Boolean 'positivo o negativo

            'determina si dos conceptos son iguales o equivalentes
            Function Equivale(X As concepto.general) As Boolean
                Dim flag As Boolean = True

                If Not X.conceptos.Count = conceptos.Count Then
                    flag = False
                Else

                    For i = 0 To conceptos.Count - 1
                        If Not X.conceptos(i).nombreID = conceptos(i).nombreID Then
                            flag = False
                            Exit For
                        End If
                    Next
                End If


                Return flag

            End Function

            Function Getcategoria() As String
                Dim r As String = ""
                If r = "" Then
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then r = "h"
                    Next
                End If

                For i = 0 To conceptos.Count - 1
                    If conceptos(i).categoria = "s" Then r = "s"
                Next

                If r = "" Then
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "t" Then r = "t"
                    Next
                End If
                Return r
            End Function

            Function GetExtension() As String
                Dim r As String = ""
                For i = 0 To conceptos.Count - 1
                    If conceptos(i).tipo = "c" Then
                        If conceptos(i).nombreID = "todo" Then
                            r = "u"
                            Exit For
                        ElseIf conceptos(i).nombreID = "algún" Then
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
                    If conceptos(i).tipo = "h" Then
                        r = conceptos(i).finito
                        Exit For
                    ElseIf conceptos(i).tipo = "s" Then
                        r = conceptos(i).finito
                        Exit For
                    ElseIf conceptos(i).tipo = "t" Then
                        r = conceptos(i).finito
                        Exit For
                    End If
                Next

                Return r
            End Function

            Function Spanish() As String
                Dim r As String = ""

                If tipo = "conj" Then
                    'CASO 1: simple conjunción
                    r = conceptos(0).Spanish
                    If conceptos.Count > 2 Then
                        For i = 1 To conceptos.Count - 2
                            r &= ", " & conceptos(i).Spanish
                        Next
                    End If
                    r &= " y " & conceptos(conceptos.Count - 1).Spanish
                Else
                    'CASO 2: hay un tipo s y el resto son t

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r &= " " & conceptos(i).Spanish
                        End If
                    Next

                    'cuantificadores primero (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "c" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Spanish
                        End If
                    Next

                    'cualificadores después (por orden de generalidad)


                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            r &= " " & conceptos(i).Spanish
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "q" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Spanish
                        End If
                    Next



                End If

                Return Trim(r)
            End Function

            Function English() As String
                Dim r As String = ""
                If tipo = "conj" Then
                    'CASO 1: simple conjunción
                    r = conceptos(0).Spanish

                    If conceptos.Count > 2 Then
                        For i = 1 To conceptos.Count - 2
                            r &= ", " & conceptos(i).English
                        Next
                    End If
                    r &= " and " & conceptos(conceptos.Count - 1).English
                Else
                    'CASO 2: hay un tipo s y el resto son t

                    'cuantificadores primero (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "c" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).English
                        End If
                    Next

                    'cualificadores después (por orden de generalidad)

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r &= " " & conceptos(i).English
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            r &= " " & conceptos(i).English
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "q" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).English
                        End If
                    Next

                End If

                Return Trim(r)
            End Function

            Function Portuguese() As String
                Dim r As String = ""
                If tipo = "conj" Then
                    'CASO 1: simple conjunción
                    r = conceptos(0).Spanish

                    If conceptos.Count > 2 Then
                        For i = 1 To conceptos.Count - 2
                            r &= ", " & conceptos(i).Portuguese
                        Next
                    End If
                    r &= " e " & conceptos(conceptos.Count - 1).Portuguese
                Else
                    'CASO 2: hay un tipo s y el resto son t

                    'cuantificadores primero (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "c" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Portuguese
                        End If
                    Next

                    'cualificadores después (por orden de generalidad)
                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "h" Then
                            r &= " " & conceptos(i).Portuguese
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).categoria = "s" Then
                            r &= " " & conceptos(i).Portuguese
                        End If
                    Next

                    For i = 0 To conceptos.Count - 1
                        If conceptos(i).tipo = "q" And conceptos(i).categoria = "t" Then
                            r &= " " & conceptos(i).Portuguese
                        End If
                    Next

                End If

                Return Trim(r)
            End Function



        End Class


    End Class

    Class sentencia
        Property CU As concepto.general 'cuantificador
        Property S As concepto.general 'sujeto
        Property Copula As concepto.general 'cópula
        Property P As concepto.general 'predicado

        Property tipo As String

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

        Function Spanish() As String
            Dim r As String = ""
            r = CU.Spanish & " " & S.Spanish & " " & Copula.Spanish & " " & P.Spanish
            Return r
        End Function

        Function English() As String
            Dim r As String = ""
            r = CU.English & " " & S.English & " " & Copula.English & " " & P.English
            Return r
        End Function

        Function Portuguese() As String
            Dim r As String = ""
            r = CU.Portuguese & " " & S.Portuguese & " " & Copula.Portuguese & " " & P.Portuguese
            Return r
        End Function

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


#End Region

#Region "OPERACIONES DE AVANCE"


    'Esta función une conceptos, eligiendo atomáticamente el tipo de unión de acuerdo con la categoría de los conceptos que se desea unir.
    Sub Union(CC As concepto.general, c As concepto.simple)
        Dim cs As New concepto.simple
        cs = c
        'si tiene cuantificadores y la categoría es t transformar a s
        If Not CC.extension Is Nothing Then
            If Not CC.extension = "indef" And cs.categoria = "t" Then cs.categoria = "s"  'Ejemplo, todo  MORTAL(t) ---> todo MORTAL(s)
        End If


        CC.conceptos.Add(cs)
        CC.categoria = CC.Getcategoria
        CC.extension = CC.GetExtension
        CC.positivo = CC.GetPositivo



        'determinar tipo
        If CC.conceptos.Count > 1 Then
            Dim flag As Boolean = True
            Dim cat As String
            cat = CC.conceptos(0).categoria

            For i = 0 To CC.conceptos.Count - 1
                If Not CC.conceptos(i).categoria = cat Then
                    flag = False
                    Exit For
                End If
            Next

            If flag Then CC.tipo = "conj"

        End If

    End Sub

    Function Juicio(CU As concepto.general, S As concepto.general, Copula As concepto.general, P As concepto.general) As sentencia
        Dim A As New sentencia

        A.CU = CU
        A.Copula = Copula
        A.S = S
        A.P = P

        'If S.categoria = "s" Then
        '    A.S = S 'A es el sujeto
        'Else  'si el sujeto no es de categoría s
        '    A.S = S 'A es el sujeto


        'End If

        ''si P es tipo distinto de h agregar cópula
        'If P.categoria = "h" Then
        '    A.P = P
        'Else
        '    'AddCop(P)
        '    A.P = P
        'End If

        A.tipo = A.getTipo

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
                C.Copula = est
                C.CU = todo
                Reporte &= "Barbara"
            ElseIf P1.tipo = "A" And P2.tipo = "I" Then 'Darii
                C.P = P1.P
                C.S = P2.S 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "Darii"
            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Celarent
                C.P = P1.P
                C.S = P2.S 'A->E
                C.Copula = est
                C.CU = Neg(algun)
                Reporte &= "Celarent"
            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Ferio
                C.P = P1.P
                C.S = P2.S 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Ferio"
            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigI:invalid"
            End If

        ElseIf P1.P.Equivale(P2.P) Then 'SEGUNDA FIGURA

            If P1.tipo = "E" And P2.tipo = "A" Then 'Cesare
                C.P = P1.S
                C.S = P2.S 'E
                C.Copula = est
                C.CU = Neg(algun)
                Reporte &= "Cesare"
            ElseIf P1.tipo = "A" And P2.tipo = "E" Then 'Camestres
                C.P = P1.S
                C.S = P2.S 'E
                C.Copula = est
                C.CU = Neg(algun)
                Reporte &= "Camestres"
            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Festino
                C.P = P1.S
                C.S = P2.S 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Festino"

            ElseIf P1.tipo = "A" And P2.tipo = "O" Then 'Baroco
                C.P = P1.S
                C.S = P2.S 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Baroco"
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
                Reporte &= "Darapti"
            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Felapton
                C.P = P1.P
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Felapton"

            ElseIf P1.tipo = "I" And P2.tipo = "A" Then 'Disamis
                C.P = P1.P
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "Disamis"

            ElseIf P1.tipo = "A" And P2.tipo = "I" Then 'Datisi
                C.P = P1.P
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "Datisi"

            ElseIf P1.tipo = "O" And P2.tipo = "A" Then 'Bocardo
                C.P = P1.P
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Bocardo"

            ElseIf P1.tipo = "E" And P2.tipo = "I" Then 'Ferison
                C.P = P1.P
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Ferison"

            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigIII:invalid"
            End If

        ElseIf P1.P.Equivale(P2.S) Then 'CUARTA FIGURA

            If P1.tipo = "A" And P2.tipo = "A" Then 'Baralipton
                C.P = P1.S
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "Baralipton"

            ElseIf P1.tipo = "E" And P2.tipo = "A" Then 'Celantes
                C.P = P1.S
                C.S = P2.P 'E
                C.Copula = est
                C.CU = Neg(algun)
                Reporte &= "Celantes"

            ElseIf P1.tipo = "A" And P2.tipo = "I" Then 'Dabitis
                C.P = P1.S
                C.S = P2.P 'I
                C.Copula = est
                C.CU = algun
                Reporte &= "Dabitis"

            ElseIf P1.tipo = "A" And P2.tipo = "E" Then 'Fapesmo
                C.P = P1.S
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Fapesmo"

            ElseIf P1.tipo = "I" And P2.tipo = "E" Then 'Frisesomorum
                C.P = P1.S
                C.S = P2.P 'O
                C.Copula = Neg(est)
                C.CU = algun
                Reporte &= "Frisesomorum"

            Else
                'modo inválido; no se sigue nada
                C = ErrCon
                Reporte &= "FigIV:invalid"
            End If

        Else
            'no hay conclusión
        End If

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

#Region "INTRODUCCIÓN DEL MODELO"

    Dim c1 As New concepto.simple
    Dim c2 As New concepto.simple
    Dim c3 As New concepto.simple
    Dim c4 As New concepto.simple
    Dim c5 As New concepto.general
    Dim c6 As New concepto.simple
    Dim todoS As New concepto.simple
    Dim todo As New concepto.general
    Dim algunS As New concepto.simple
    Dim algun As New concepto.general
    Dim SN As New sentencia
    Dim SN2 As New sentencia
    Dim ErrCon As New sentencia
    Dim CN2 As New concepto.general
    Dim Concl As New sentencia
    Dim estS As New concepto.simple
    Dim est As New concepto.general

    Sub Fill()

        'sentencia de error

        ErrCon = Juicio(G(G("nada", "c", "s")), G(G("se", "q", "s")), G(G("esta", "q", "h")), G(G("demostrando", "c", "h")))

        estS.categoria = "h"
        estS.finito = True
        estS.nombreID = "es"
        estS.tipo = "q"

        est = G(estS)


        todoS.categoria = "t"
        todoS.finito = True
        todoS.nombreID = "todo"
        todoS.tipo = "c"

        todo = G(todoS)


        algunS.categoria = "t"
        algunS.finito = True
        algunS.nombreID = "algún"
        algunS.tipo = "c"

        algun = G(algunS)

        c1.categoria = "s"
        c1.finito = True
        c1.nombreID = "piedra"
        c1.tipo = "q"

        c2.categoria = "t"
        c2.finito = True
        c2.nombreID = "todo"
        c2.tipo = "c"

        c3.categoria = "t"
        c3.finito = True
        c3.nombreID = "vivo"
        c3.tipo = "q"


        c4.categoria = "s"
        c4.finito = True
        c4.nombreID = "ser"
        c4.tipo = "q"

        c5 = G(G("hombre", "q", "s"))
        c6 = G("corporal", "q", "t")

        ' Union(CN, c4) 'ser
        Union(CN, c3) 'vivo

        ' Union(CN, c6) 'corporeo
        'Union(CN, c3)
        CN2 = G(c3, c1)
        'Union(CN2, c2) 'todo
        ' Union(CN2, c4)
        Union(CN2, c1) 'animal

        Dim CN3 As New concepto.general
        Union(CN3, c1) 'animal
        Dim CX1 As New concepto.general
        Union(CX1, c3) 'mortal

        SN = Juicio(todo, c5, Neg(est), G(c3)) 'todo mineral no es ser vivo
        SN2 = Juicio(algun, G(c1), est, G(c3)) 'todo hombre es ser vivo


        Concl = Razonamiento(SN, SN2)

    End Sub
#End Region

    Function Experiment() As String


        Fill()
        Return SN.Spanish & vbCrLf & SN2.Spanish & vbCrLf & "Luego " & Concl.Spanish & vbLf & "-> " & Reporte

    End Function

#Region "IO"
    Class response
        Property verbal As String = ""
        Property factual As String = ""
    End Class

    Function IO(input As String) As response
        Dim r As New response

        'comandos
        If input = "clear" Then

            r.factual = "clear1"
        ElseIf input = "close" Then
            r.factual = "close"
        ElseIf input = "syl" Then
            Fill()
            r.verbal = SN.Spanish & vbCrLf & SN2.Spanish & vbCrLf & "Luego " & Concl.Spanish
        ElseIf input = "syl report" Then
            Fill()
            r.verbal = SN.Spanish & vbCrLf & SN2.Spanish & vbCrLf & "Luego " & Concl.Spanish
            r.verbal &= vbLf & ">> " & Reporte


        Else
            r.verbal = "?"
        End If

        Return r
    End Function


#End Region


End Module
