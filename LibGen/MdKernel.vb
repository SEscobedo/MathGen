Module MdKernel
    Public Libro As New book

#Region "ESTRUCTURA DE DATOS"
    Class element
        Property label As String 'Identificador del elemento
        Property ElemType As String 'tipo de elemento

        Class axiom   'Define un axioma; propocición que afirma algo, sin prueba.
            Inherits element
            Property ClasicName As String  'Nombre clásico ej. Axioma de elección.

        End Class

        Class definition
            Inherits element

            Property type As String

            Property nombre As String  'cuando es singular
            Property nombre_segundo_orden As String 'cuando es universal
            Property clase As String 'genero al que pertenece el objeto a definir
            Property condición_definitoria As String 'relación que define el objeto
            Property definición As String 'formula que define el objeto
            Property componentes As New List(Of element.definition)
            Property rango As String
            Property primaria As Boolean

            Property titulo As String
            Property tema As String

            Function genDef(estilo As Long) As String
                Dim r As String = ""

                If estilo = 3 Then
                    r = "Entonces "
                    estilo = 2
                End If

                If Not condición_definitoria = "" Then
                    'Estilo 1.
                    If estilo = 1 Then
                        r &= "Sea $" & nombre & RE("$ un ", clase) & clase & " tal que $" & condición_definitoria & "$."
                    Else
                        'Estilo 2
                        r &= "$" & nombre & RE("$ es un ", clase) & clase & " tal que $" & condición_definitoria & "$."
                    End If
                Else
                    If estilo = 1 Then
                        'Estilo 1.
                        If Not definición = "" Then r &= "Sea $" & nombre & RE("$ un ", clase) & clase & RE(" definido por la fórmula $", clase) & nombre & "=" & definición & "$."
                        If Not nombre_segundo_orden = "" Then r &= RE(" El ", clase) & clase & " $" & nombre & RE("$ es conocido como \textit{", clase) & nombre_segundo_orden & "}."
                    Else
                        'Estilo 2
                        If Not definición = "" Then r &= "$" & nombre & RE("$ es un ", clase) & clase & RE(" definido por la fórmula $", clase) & nombre & "=" & definición & "$."
                        If Not nombre_segundo_orden = "" Then r &= RE(" El ", clase) & clase & " $" & nombre & RE("$ es conocido como \textit{", clase) & nombre_segundo_orden & "}."

                    End If

                End If
                Return r

            End Function
        End Class

        Class theorem   'Define un teorema: propocición que afirma algo, con demostración a partir de los axiomas
            Inherits element
            Property ClasicName As String  'Nombre clásico del teorema ej: "Primer teorema fundamental del cálculo".
            Property author As String  'Autor del teorema'
            Property proposition As String
            Property prueba As String  'Demostración del teorema'
            Property componets As List(Of theorem)  'lista de subteoremas



        End Class

        Class proof
            Inherits element
            Property teorema As String

        End Class

        Class hypothesis
            Inherits element

        End Class

        Class method 'secuencia de pasos para resolver un problema u obtener un resultado
            Inherits element
            Property intructions As List(Of String)
        End Class

        Class example 'Ejemplificación usando un caso particular.
            Inherits element
        End Class

        Class exercise 'Ejercicio propuesto al lector
            Inherits element
        End Class

    End Class
    Class theory
        Property label As String
        Property elements As New List(Of element)   'Esta lista contiene todos los elementos
        'elementos
        Property theorems As New List(Of element.theorem)   'teoremas
        Property proofs As New List(Of element.proof)        'pruebas
        Property hypothesis As New List(Of element.hypothesis)  'suposiciones
        Property definitions As New List(Of element.definition)  'definiciones
        Property axioms As New List(Of element.axiom)             'axiomas
        Property methods As New List(Of element.method)            'métodos
        Property examples As New List(Of element.example)          'ejemplos
        Property exercises As New List(Of element.exercise)         'ejercicios

    End Class

    Class book 'Libro
        Property label As String
        Property theory As New theory
        Property título As String
        Property encabezado As String
        Property cuerpo As String
    End Class

#End Region

#Region "ALGORITMOS DE COMPILACIÓN"

    'Partimos de un código para generar la estructura de datos
    Sub Libro_de_prueba(L As book)  'Estructura para pruebas unitarias

        Dim def As New element.definition
        def.ElemType = "DF"
        def.label = "Def1"
        def.nombre = "k"
        def.type = "variable"
        def.clase = "número real"
        def.definición = " \frac{1}{4\pi \epsilon_0}"
        def.primaria = False
        L.theory.elements.Add(def)

        Dim def1 As New element.definition
        def1.ElemType = "DF"
        def1.label = "Def1"
        def1.nombre = "n"
        def1.type = "variable"
        def1.clase = "número entero"
        def1.condición_definitoria = "n < k + 1"
        def1.primaria = False

        Dim def3 As New element.definition
        def3.ElemType = "DF"
        def3.label = "Def1"
        def3.nombre = "\epsilon_0"
        def3.type = "constante"
        def3.clase = "número real"
        def3.nombre_segundo_orden = "permitividad eléctrica"
        'def3.condición_definitoria = "n < k + 1"
        def3.primaria = False

        Dim def2 As New element.definition
        def2.ElemType = "DF"
        def2.label = "Def1"
        def2.nombre = "x"
        def2.type = "variable"
        def2.clase = "número real"
        def2.condición_definitoria = "f(x)=0"
        def2.primaria = True
        def2.titulo = "raiz"
        def2.componentes.Add(def)
        def2.componentes.Add(def1)
        'def2.componentes.Add(def3)
        L.theory.elements.Add(def2)
        L.theory.elements.Add(def3)


        Dim ax As New element.axiom
        ax.ElemType = "AX"
        ax.label = "axiom1"
        ax.ClasicName = "Axioma de elección"
        L.theory.elements.Add(ax)

        Dim pr As New element.proof
        pr.ElemType = "PR"
        pr.label = "prueba1"
        pr.teorema = "Theorem1"
        L.theory.elements.Add(pr)

        Dim hy As New element.hypothesis
        hy.ElemType = "HP"
        hy.label = "hyp1"
        L.theory.elements.Add(hy)

        Dim mth As New element.method
        mth.ElemType = "MT"
        mth.label = "metodo1"
        L.theory.elements.Add(mth)

        Dim ex As New element.example
        ex.ElemType = "EX"
        ex.label = "ejemplo1"
        L.theory.elements.Add(ex)

    End Sub





#End Region

#Region "ALGORITMOS DE GENERACION DE TEXTO"
    'Partimos de un objeto libro para generar el texto
    Function GenBook(L As book) As String
        'Imprimir encabezado
        L.encabezado = "\documentclass[12pt]{book}" & vbLf &
    "\usepackage[spanish]{babel}" & vbLf &
    "\usepackage{amsmath}" & vbLf &
    "\usepackage[utf8]{inputenc}" & vbLf &
    "\newtheorem{definition}{Definición}[chapter]" & vbLf &
     "\newtheorem{theorem}{Teorema}[chapter]" & vbLf &
    "\newtheorem{axiom}{Axioma}[chapter]" & vbLf &
    "\newtheorem{proof}{Demostración}" & vbLf &
    "\title{" & Form1.TextBox2.Text & "}" & vbLf &
    "\author{" & Form1.TextBox3.Text & "}" & vbLf &
    "\date{}" & vbLf

        '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        L.cuerpo = "\begin{document}"
        L.cuerpo &= vbCrLf & "\maketitle" & vbCrLf

        'REORDENAR LOS ELEMENTOS DE ACUERDO AL PLAN DE REDACCIÓN TRAZADO.
        'Plan de redacción: 
        'A) modo ciego: colocar en el orden que se encuentran en el código
        'B) modo discriminante:
        '   1° Definiciones
        '   2° Axiomas
        '   3° Teoremas, hipótesis, métodos
        '   4° Ejemplos
        '   5° Ejercisios.
        'C) modo inteligente: Los elementos se ordenan de acuerdo a sus intercontexiones y de acuerdo al progreso lógico de la teoría.

        'PLAN DE REDACCIÓN A
        For i = 0 To L.theory.elements.Count - 1 'hacer para cada elemento
            If L.theory.elements(i).ElemType = "AX" Then 'axioma
                L.cuerpo &= genAxiom(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "DF" Then 'definición
                L.cuerpo &= genDefinition(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "TH" Then 'teorema
                L.cuerpo &= genTheorem(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "PR" Then 'prueba
                L.cuerpo &= genProof(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "HP" Then 'hipóteis
                L.cuerpo &= genHypothesis(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "MT" Then 'método
                L.cuerpo &= genMethod(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "EX" Then 'ejemplo
                L.cuerpo &= genExample(L.theory.elements(i), 1)
            ElseIf L.theory.elements(i).ElemType = "EJ" Then 'ejercicio
                L.cuerpo &= genExersise(L.theory.elements(i), 1)
            End If
        Next

        'PLAN DE REDACCIÓN B
        'PLAN DE REDACCIÓN C

        L.cuerpo &= vbCrLf & "\end{document}"
        '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        Return L.encabezado & L.cuerpo
    End Function

    '$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    'REDACCIÓN POR PARTES
    '$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

    Function genDefinition(D As element.definition, estilo As Integer) As String 'Redacción de la definición

        Dim r As String = ""

        If D.primaria Then
            r &= "\begin{definition}"
        End If

        'declarar componentes de la definición
        Dim i As Long
        For i = 0 To D.componentes.Count - 1
            r &= vbCrLf & D.componentes(i).genDef(1)
        Next
        'declarar la definición principal
        If i > 0 Then
            r &= vbCrLf & D.genDef(3)
        Else
            r &= vbCrLf & D.genDef(2)
        End If

        If D.primaria Then
            r &= vbCrLf & "\label{" & D.label & "}"
            r &= vbCrLf & "\end{definition}"
        End If

        Return r & vbCrLf


    End Function


    Function genAxiom(A As element.axiom, estilo As Integer) As String
        Return "prueba correcta axioma" & vbCrLf
    End Function

    Function genTheorem(T As element.theorem, estilo As Integer) As String 'Redacta el texto del teorema
        Dim res As String = ""

        If estilo = 1 Then 'Importancia normal, teorema planteado por separado, numerado y etiquetado para referencia
            'La demostración puede ir antes o después.
            res &= vbCrLf & "\begin{theorem}"
            'Redacción del encabezado del teorema
            '-- asumtions
            '-- propositions
            'CASO 1. UNICA PROPOSICIÓN
            If T.componets.Count = 0 Then

                'CASO 2. DOS PROPOSICIONES
            ElseIf T.componets.Count = 1 Then



                'CASO 3. MÁS DE DOS PROPOSICIONES
            Else

            End If



            '-- aclaraciones y comentarios
            res &= vbCrLf & "\label{" & T.label & "}" 'Etiqueta el teorema con el identificador del objeto
            res &= vbCrLf & "\end{theorem}"

            'Redactar la pureba (si la hay)
            res &= vbCrLf & "\begin{proof}"
            res &= vbCrLf & "\end{proof}"

        ElseIf estilo = 2 Then 'Importancia normal, teorema planteado por separado, estilo formal con simbología estricta, etiquetado para referencia.
            res &= vbCrLf & "\begin{theorem}"
            'Redacción del encabezado del teorema
            '-- asumtions
            '-- propositions
            res &= vbCrLf & "\end{theorem}"
        Else 'Importancia regular, teorema no numrado ni resaltado, redactado como texto común.
            '-- asumtions
            '-- conclusions
        End If

        Return res
    End Function

    Function genProof(P As element.proof, estilo As Integer) As String
        Return "prueba correcta prueba" & vbCrLf
    End Function

    Function genHypothesis(T As element.hypothesis, estilo As Integer) As String
        Return "prueba correcta hipotesis" & vbCrLf
    End Function

    Function genMethod(M As element.method, estilo As Integer) As String
        Return "prueba correcta método" & vbCrLf
    End Function

    Function genExample(E As element.example, estilo As Integer) As String
        Return "prueba correcta ejemplo" & vbCrLf
    End Function

    Function genExersise(E As element.exercise, estilo As Integer) As String
        Return "prueba correcta ejeercicio" & vbCrLf
    End Function

#End Region

End Module
