Module Module4
    '**********************
    'NADO 1.1
    '**********************

    'entrega el siguente nivel de un arbol
    Function ArborSub(ByVal arbor As String) As String
        Dim d As Integer
        'Formato: "Juan(hombre(racional,animal),tapatío(mexicano)))"
        'identificar el primer paréntesis
        d = InStr(arbor, "(")
        If d Then
            arbor = Mid$(arbor, d + 1, Len(arbor) - d - 1)
        Else
            arbor = ""
        End If

        ArborSub = arbor
    End Function

    Function ArborHead(ByVal arbor As String) As String
        Dim d As Integer
        'Formato: "Juan(hombre(racional,animal),tapatío(mexicano)))"
        'identificar el primer paréntesis
        d = InStr(arbor, "(")
        If d Then
            arbor = Mid$(arbor, 1, d - 1)
        End If

        ArborHead = Mid$(arbor, 2)
    End Function

    'agrega una cadena al primer nivel del arbor
    Function ArborAppend(ByVal arbor As String, ByVal ParteNova As String) As String
        Dim d As String, e As String

        d = ArborSub(arbor)
        e = ArborHead(arbor)

        If d = "" Then
            arbor = e & "(" & ParteNova & ")"
        Else
            arbor = e & "(" & d & "," & ParteNova & ")"
        End If

        ArborAppend = arbor
    End Function

    'busca un elemento y lo aumenta con parte nova
    'Nota: si el árbol consiste en un sólo concepto head, usar ArborAppend.
    Function ArborAuge(ByVal arbor As String, ByVal ParteNova As String, ByVal Head As String) As String


        'buscar la parte y replazarla

        If Head = ArborHead(arbor) Then
            arbor = "," & Head & "(" & ParteNova & ")"
        Else
            arbor = Replace$(arbor, "," & Head & "(", "," & Head & "(" & ParteNova & ",")
            arbor = Replace$(arbor, "," & Head & ",", "," & Head & "(" & ParteNova & "),")
            arbor = Replace$(arbor, "," & Head & ")", "," & Head & "(" & ParteNova & "))")
            arbor = Replace$(arbor, "(" & Head & ")", "(" & Head & "(" & ParteNova & "))")
            arbor = Replace$(arbor, "(" & Head & ",", "(" & Head & "(" & ParteNova & "),")
            arbor = Replace$(arbor, "(" & Head & "(", "(" & Head & "(" & ParteNova & ",")
        End If

        ArborAuge = arbor
    End Function

    Sub MEMOAppend(ByVal ParteNova As Object, ByRef Memoria As Object)

        If Not Memoria = "" Then
            Memoria = Memoria & ParteNova & "%"
        Else
            Memoria = Memoria & "%" & ParteNova & "%"
        End If

    End Sub
    Function LastPart(ByVal cadena As String, ByVal separador As String) As String
        On Error GoTo Line1
        Dim A = Split(cadena, separador)
        LastPart = A(UBound(A) - 1)
        Exit Function
Line1:
        LastPart = "Error"
    End Function
End Module
