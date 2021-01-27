Module Module1
    Public component As New List(Of definición)
    Class config
        Property ejercicios As String
        Property teorias As String
    End Class
    Class DatosEntrada
        Property teoria As String
        Property Teorema As New List(Of theorem)
    End Class
    Class theorem
        Property nombre As String
        Property index As Long
        Property titulo As String
        Property grupo As String
        Property proposición As String
        Property componentes As List(Of definición)
        Property tema As String
        Property demostración As String
        Function getTeorema(estilo As Long) As String
            Dim res As String
            res = "$" & proposición & "$"
            Return res
        End Function
        Function getDemostración(estilo As Long) As String
            Return "$" & demostración & "$"
        End Function
    End Class
    Class axioma
        Property nombre As String
        Property index As Long
        Property titulo As String
        Property proposición As String
        Property componentes As List(Of definición)
        Function getAxioma() As String
            Return "$" & proposición & "$"
        End Function

    End Class
    Class grupoAxioma
        Property titulo As String
        Property nombre As String
        Property componentes As List(Of definición)
        Property axiomas As List(Of axioma)
    End Class
    Class grupoTeorema
        Property titulo As String
        Property nombre As String
        Property componentes As List(Of definición)
        Property teoremas As List(Of theorem)
    End Class

    Class definición
        Property nombre As String  'cuando es singular
        Property nombre_segundo_orden As String 'cuando es universal
        Property clase As String
        Property condición_definitoria As String
        Property definición As String
        Property componentes As New List(Of definición)
        Property primaria As Boolean

        Property titulo As String
        Property tema As String

        Function getDefinición(estilo As Long) As String
            Dim r As String
            If Not condición_definitoria = "" Then
                'Estilo 1.
                If estilo = 1 Then
                    r = "Sea $" & nombre & RE("$ un ", clase) & clase & " tal que $" & condición_definitoria & "$."
                Else
                    'Estilo 2
                    r = "$" & nombre & RE("$ es un ", clase) & clase & " tal que $" & condición_definitoria & "$."
                End If
            Else
                If estilo = 1 Then
                    'Estilo 1.
                    r = "Sea $" & nombre & RE("$ un ", clase) & clase & RE(" definido por la fórmula $", clase) & nombre & "=" & definición & "$."
                    If Not nombre_segundo_orden = "" Then r &= RE(" El ", clase) & clase & " $" & nombre & RE("$ es conocido como \textit{", clase) & nombre_segundo_orden & "}."
                Else
                    'Estilo 2
                    r = "$" & nombre & RE("$ es un ", clase) & clase & RE(" definido por la fórmula $", clase) & nombre & "=" & definición & "$."
                    If Not nombre_segundo_orden = "" Then r &= RE(" El ", clase) & clase & " $" & nombre & RE("$ es conocido como \textit{", clase) & nombre_segundo_orden & "}."

                End If

            End If
            Return r

        End Function
    End Class
    Class teoría
        Property definiciones As New List(Of definición)
        Property Primdefiniciones As New List(Of definición)
        Property axiomas As New List(Of axioma)
        Property GrupoAxiomas As New List(Of grupoAxioma)
        Property teoremas As New List(Of theorem)
        Property GrupoTeoremas As New List(Of grupoTeorema)


        Function getTeoría(text() As String) As teoría
            Dim teor As New teoría
            Dim th As New theorem
            'desglozar la teoría

            Dim i As Long
            For i = 0 To UBound(text)
                If text(i) Like "teorema:*" Then
                    Dim s() As String = Split(text(i), ";")

                    th.titulo = s(0) 'teorema, proposición, ley, lema, etc. 
                    th.nombre = s(1) 'nombre particular del teorema
                    th.grupo = s(2)
                    th.proposición = s(3)
                    th.demostración = s(4)
                    teor.teoremas.Add(th)
                ElseIf text(i) Like "axioma:*" Then
                    Dim a As New axioma
                    Dim s() As String = Split(text(i), ";")
                    'On Error Resume Next

                    If s(0) Like "axioma:*" Then s(0) = Replace(s(0), "axioma:", "")


                    a.titulo = s(0) 'axioma, suposición, premisa, etc
                    a.nombre = s(1) 'nombre particular del axioma
                    a.proposición = s(2)

                    teor.axiomas.Add(a)

                ElseIf text(i) Like "definición:*" Or text(i) Like "Definición:*" Then
                    Dim d As New definición
                    Dim s() As String = Split(text(i), ";")
                    'On Error Resume Next
                    

                    d.nombre = s(0) 'definición 
                    d.nombre_segundo_orden = s(1) 'nombre particular del objeto
                    d.clase = s(2)
                    d.condición_definitoria = s(3)
                    d.definición = s(4)

                    If d.nombre Like "definición:*" Then
                        d.primaria = False
                        d.nombre = Replace(d.nombre, "definición:", "")
                        component.Add(d)
                    Else
                        d.primaria = True
                        d.nombre = Replace(d.nombre, "Definición:", "")
                        d.componentes.AddRange(component)
                        teor.Primdefiniciones.Add(d)
                        component.Clear()
                    End If

                    teor.definiciones.Add(d)


                ElseIf text(i) Like "hipótesis:*" Or text(i) Like "suposición:*" Then
                End If
            Next
            Return teor
        End Function
    End Class

    Class libro
        Property title As String = "Libro de prueba"
        Property encabezado As String
        Property cuerpo As String

        Function getLibro(T As teoría) As String
            Dim res As New libro
            'Encabezado
            res.encabezado = "\documentclass[12pt]{book}" & vbLf & _
    "\usepackage[spanish]{babel}" & vbLf & _
    "\usepackage{amsmath}" & vbLf & _
    "\usepackage[utf8]{inputenc}" & vbLf & _
    "\newtheorem{teo}{Definición}[chapter]" & vbLf & _
     "\newtheorem{de}{Teorema}[chapter]" & vbLf & _
    "\newtheorem{ax}{Axioma}[chapter]" & vbLf & _
    "\title{" & Form1.TextBox2.Text & "}" & vbLf & _
    "\author{" & Form1.TextBox3.Text & "}" & vbLf & _
    "\date{}" & vbLf
            'cuerpo

            res.cuerpo = "\begin{document}"
            res.cuerpo &= vbCrLf & "\maketitle"
            res.cuerpo &= "\chapter{Conceptos básicos}"
            'Dividir los capítulos por temas
            '-----------------------------------------------------------
            'en principio definir los objetos del tema
            Dim j As Long
            For i = 0 To T.Primdefiniciones.Count - 1 '****************************************************************** DEFINICIONES

                'declarar componentes de la definición

                res.cuerpo &= vbCrLf & "\begin{teo}"
                For j = 0 To T.Primdefiniciones(i).componentes.Count - 1
                    res.cuerpo &= vbCrLf & T.Primdefiniciones(i).componentes(j).getDefinición(1)
                Next
                'declarar la definición principal
                res.cuerpo &= vbCrLf & T.Primdefiniciones(i).getDefinición(2)
                res.cuerpo &= vbCrLf & "\end{teo}"
            Next

            'Declarar los axiomas
            For i = 0 To T.axiomas.Count - 1 '******************************************************************    AXIOMAS

                res.cuerpo &= vbCrLf & "\begin{ax}" & T.axiomas(i).getAxioma()
                res.cuerpo &= vbCrLf & "\end{ax}"
            Next

            'exponer, demostrar y explicar los teoremas y los métodos
            For i = 0 To T.teoremas.Count - 1 '****************************************************************** TEOREMAS

                res.cuerpo &= vbCrLf & "\begin{de}" & T.teoremas(i).getTeorema(1)
                res.cuerpo &= vbCrLf & "\end{de}"
                res.cuerpo &= vbCrLf & "\textit{Demostración:} " & T.teoremas(i).getDemostración(1)
            Next
            'proponer ejercicios
            'generar soluciones para los ejercicios
            '-----------------------------------------------------------
            res.cuerpo &= vbCrLf & "\end{document}"

            Return res.encabezado & res.cuerpo
        End Function

    End Class

    'funciones de redacción aleatoria
    Function RE(expresión As String, GeneroComo As String) As String 'genera una randomización de una expresión en un idioma específico
        Dim r As String = expresión
        Dim s() As String
        ReDim s(5)
        Dim randomValue
        Dim gen As String
        'determinar género
        If GeneroComo Like "*a" Or GeneroComo Like "*ón" Or GeneroComo = "clase" Then
            gen = "fem"
        Else
            gen = "masc"
        End If

        Select Case expresión
            Case "$ es conocido como \textit{"
                If gen = "masc" Then
                    s(0) = "$ es conocido como \textit{"
                    s(1) = "$ se llama \textit{"
                    s(2) = "$ se denomina \textit{"
                    s(3) = "$ es llamado \textit{"
                    randomValue = CInt(Math.Floor(4 * Rnd()))
                    r = s(randomValue)
                Else
                    s(0) = "$ es conocida como \textit{"
                    s(1) = "$ se llama \textit{"
                    s(2) = "$ se denomina \textit{"
                    s(3) = "$ es llamada \textit{"
                    randomValue = CInt(Math.Floor(4 * Rnd()))
                    r = s(randomValue)
                End If

            Case " definido por la fórmula $"
                If gen = "masc" Then
                    s(0) = " definido por la fórmula $"
                    s(1) = " definido mediante la fórmula $"
                    s(2) = " dado por $"
                    s(3) = " definido por $"
                    randomValue = CInt(Math.Floor(4 * Rnd()))
                    r = s(randomValue)
                Else
                    s(0) = " definida por la fórmula $"
                    s(1) = " definida mediante la fórmula $"
                    s(2) = " dada por $"
                    s(3) = " definida por $"
                    randomValue = CInt(Math.Floor(4 * Rnd()))
                    r = s(randomValue)
                End If
            Case "$ un "
                If gen = "masc" Then
                    r = "$ un "
                Else
                    r = "$ una "
                End If
            Case "$ es un "
                If gen = "masc" Then
                    r = "$ es un "
                Else
                    r = "$ es una "
                End If
            Case " El "
                If gen = "masc" Then
                    r = " El "
                Else
                    r = " La "
                End If
        End Select
        Return r
    End Function

    Function LaTeX(expresión As String) As String 'convierte a latex un texto matemático
        Dim r = expresión
        r = Replace(r, " -> ", "\rightarrow")
        r = Replace(r, " <-> ", "\leftrightarrow")
        r = Replace(r, "", "\in")

        Return r
    End Function
End Module
