
Imports System.Math

Module ModFuncionesMatem

    '###############################################################################
    '###############################################################################
    '###                                                                         ###
    '###                                                                         ###
    '###                    MÓDULO DE FUNCIONES MATEMÁTICAS                      ###
    '###                                                                         ###
    '###                                                                         ###
    '### FUNCIONES NUMÉRICAS, ALGEBRÁICAS, GEOMÉTRICAS, ESTADÍSTICAS, ECT.       ###
    '### MAR. 2008                                                               ###
    '###############################################################################
    '###############################################################################
    '²³¹º


    Public funcAuxiliar(3) As String

    Const PI = 3.14159265358979
    Const PIENTRE2 = 1.5707963267949
    Const PIENTRE4 = 0.785398163397448
    Const PIENTRE180 = 0.0174532925199433
    Const C80ENTREPI = 57.2957795130823
    Const EULER = 2.71828182845905
    Const GAMMA = 2

#Region "FUNCIONES NUMÉRICAS"


    '############################################################################

    '1. FUNCIONES NUMÉRICAS

    '############################################################################

    ' devuelve un array con los primeros N números primos

    Function ArrayPrimos(numeroDePrimos As Long) As Long()
        Dim encontrado As Long, n As Long, i As Long
        If numeroDePrimos <= 0 Then Err.Raise(1002, , "Argumento no válido")
        ' de entrada, conocemos el tamaño del resultado
        Dim resultado() As Long
        ReDim resultado(0 To numeroDePrimos)
        ' "2" es el primer número primo
        resultado(1) = 2 : encontrado = 1
        n = 1
        Do While encontrado < numeroDePrimos
            ' todos los demás números primos son impares por lo que
            ' podemos ignorar todos los números pares
            n = n + 2
            ' verifiquemos si N es un número primo
            For i = 1 To encontrado
                If (n Mod resultado(i)) = 0 Then Exit For
            Next
            If i > encontrado Then
                ' no número primo < N es un divisor de N, por lo tanto, N es primo
                encontrado = encontrado + 1
                resultado(encontrado) = n
            End If
        Loop
        ArrayPrimos = resultado
    End Function

    'Devuelve el n-ésimo número primo
    Function NPrimo(numeroDePrimos As Long) As Long
        Dim encontrado As Long, n As Long, i As Long
        If numeroDePrimos <= 0 Then Err.Raise(1002, , "Argumento no válido")
        ' de entrada, conocemos el tamaño del resultado
        Dim resultado() As Long
        ReDim resultado(0 To numeroDePrimos)
        ' "2" es el primer número primo
        resultado(1) = 2 : encontrado = 1
        n = 1
        Do While encontrado < numeroDePrimos
            ' todos los demás números primos son impares por lo que
            ' podemos ignorar todos los números pares
            n = n + 2
            ' verifiquemos si N es un número primo
            For i = 1 To encontrado
                If (n Mod resultado(i)) = 0 Then Exit For
            Next
            If i > encontrado Then
                ' no número primo < N es un divisor de N, por lo tanto, N es primo
                encontrado = encontrado + 1
                resultado(encontrado) = n
            End If
        Loop
        NPrimo = resultado(encontrado)
    End Function

    ' devuelve un array de longs que incluye todos los números naturales
    ' en el rango PRIMERO a ULTIMO.
    Function InicArray(primero As Long, Ultimo As Long) As Long()
        Dim resultado() As Long
        ' ReDim resultado(primero To Ultimo)

        Dim i As Long
        For i = primero To Ultimo : resultado(i) = i : Next

        InicArray = resultado

    End Function

    ' función que suma todos los valores que se le pasen
    ' o los concatena en caso de que sean cadenas

    Function Sumar(ParamArray args()) As Double
        Dim i As Integer, resultado As Double
        For i = LBound(args) To UBound(args)
            resultado = resultado + args(i)
        Next
        Sumar = resultado
    End Function

    ' función que devuelve el valor mayor de entre los pasados como argumento

    Function Max(primero As Double, ParamArray args() As Object) As Double
        Dim i As Integer, resultado As Double
        resultado = primero
        For i = LBound(args) To UBound(args)
            If args(i) > resultado Then resultado = args(i)
        Next
        Max = resultado
    End Function

    ' función que proporciona el valor más bajo de entre los pasados como argumentos

    Function Min(primero As Double, ParamArray args() As Object) As Double
        Dim i As Integer, resultado As Double
        resultado = primero
        For i = LBound(args) To UBound(args)
            If args(i) < resultado Then resultado = args(i)
        Next
        Min = resultado
    End Function

    'devuelve el inverso de la suma de los inversos
    Function RsumR(x() As Double) As Double
        Dim res As Double
        Dim i As Long

        For i = LBound(x) To UBound(x)
            res = res + 1 / x(i)
        Next i

        RsumR = 1 / res
    End Function



    '----------------------------------------------------------------------------
    'ANÁLISIS COMBINATORIO
    '----------------------------------------------------------------------------

    'función que debuelve el factorial de un número entero
    ' si n es mayor que 170 ocurre un desbordamiento y la función debuelve -1
    ' nota: n debe ser entero
    Function Fac(n As Double) As Double
        Dim i As Long
        Dim res As Double
        n = Fix(n)

        If n < 171 Then
            res = 1
            If n >= 1 Then

                For i = 1 To n
                    res = res * i
                Next i

            Else
                res = 1
            End If
        Else
            res = -1
        End If

        Fac = res
    End Function

    'función coeficiente binomial
    ' nota: se debe tener n >= k.
    Function Comb(n As Double, k As Double) As Double
        Comb = Fac(n) \ (Fac(k) * Fac(n - k))
    End Function

    'función permutaciones
    Function Permut(n As Double, k As Double) As Double
        Permut = Fac(n) \ Fac(n - k)
    End Function

    '----------------------------------------------------------------------------
    'FUNCIONES TRIGONOMÉTRICAS
    '----------------------------------------------------------------------------

    'hiperbólicas

    'seno hiperbólico
    Function senh(x As Double) As Double
        senh = (Exp(x) - 1 / Exp(x)) * 0.5
    End Function

    'coseno hiperbólico
    Function cosh(x As Double) As Double
        cosh = (Exp(x) + 1 / Exp(x)) * 0.5
    End Function

    'tangente hiperbólica
    Function tanh(x As Double) As Double
        tanh = senh(x) / cosh(x)
    End Function

    'cotangente hiperbólica
    Function coth(x As Double) As Double
        coth = 1 / tanh(x)
    End Function

    'secante hiperbólica
    Function sech(x As Double) As Double
        'sech = 1 / tanh(x)
    End Function

    'cosecante hiperbólica
    Function csch(x As Double) As Double
        'csch = 1 / tanh(x)
    End Function

    'inversas hiperbólicas

    'arg. seno hiperbólico
    Function argsenh(x As Double) As Double
        argsenh = Log(x + Sqrt(x * x + 1))
    End Function

    'arg. coseno hiperbólico
    Function argcosh(x As Double) As Double
        argcosh = Log(x + Sqrt(x * x - 1))
    End Function

    'arg. tangente hiperbólica
    Function argtanh(x As Double) As Double
        argtanh = (Log(1 + x) - Log(1 - x)) / 2
    End Function

    'arg. cotangente hiperbólica
    Function argcoth(x As Double) As Double
        argcoth = (Log(x + 1) - Log(x - 1)) / 2
    End Function

    'arg. secante hiperbólica
    Function argsech(x As Double) As Double
        'argsech = 1 / tanh(x)
    End Function

    'arg. cosecante hiperbólica
    Function argcsch(x As Double) As Double
        'argcsch = 1 / tanh(x)
    End Function

    'trig. elípticas

    'inversas
    Function arcSin(x As Double) As Double  'arco seno en radianes
        arcSin = Atan(x / Sqrt(-x * x + 1))
    End Function

    Function arccos(x As Double) As Double
        arccos = Atan(-x / Sqrt(-x * x + 1)) + 2 * Atan(1)
    End Function

    Function arcsec(x As Double) As Double
        arcsec = Atan(x / Sqrt(x * x - 1)) + (Sign(x) - 1) * (2 * Atan(1))
    End Function

    Function arccsc(x As Double) As Double
        arccsc = Atan(x / Sqrt(x * x - 1)) + (Sign(x) - 1) * (2 * Atan(1))
    End Function

    Function arccot(x As Double) As Double
        arccot = Atan(x) + 2 * Atan(1)
    End Function

    'conversión de medidas angulares.

    'Convierte radianes a grados
    Function Deg(x As Double) As Double
        Deg = x * PI
    End Function

    'Convierte grados a radianes
    Function Rad(x As Double) As Double
        Rad = x * PI
    End Function

    'logarítmos
    Function ln(x As Double) As Double
        ln = Log(Abs(x))
    End Function

    Function expe(x As Double) As Double
        If x < 0 Then
            expe = 1 / Exp(x)
        Else
            expe = Exp(x)
        End If
    End Function

#End Region

#Region "FUNCIONES DE PROCESAMIENTO ALGEBRÁICO"


    '############################################################################

    '2. FUNCIONES DE PROCESAMIENTO ALGEBRÁICO

    '############################################################################

    'adapta una cadena para ser procesada
    Sub Adapt(ByRef s As String)

        s = Trim(s)
        s = Replace$(s, " y ", " & ")
        s = Replace$(s, " implica ", " -> ")

        s = Replace$(s, " si y sólo si ", " <-> ")
        s = Replace$(s, " si y solo si ", " <-> ")

        'pertenencia de conjuntos
        s = Replace$(s, " es elemento de ", " en ")
        s = Replace$(s, " está en ", " en ")
        s = Replace$(s, " pertenece a ", " en ")

        'descriptor
        s = Replace$(s, " de ", "|")
        s = Replace$(s, " tal que ", "|")
        s = Replace$(s, " tales que ", "|")
        s = Replace$(s, " se tiene que ", "|")
        s = Replace$(s, " se tiene ", "|")
        s = Replace$(s, " se cumple ", "|")

        'cuantificador universal
        s = Replace$(s, " para todo ", "@")
        s = Replace$(s, " & todo ", "@")
        s = Replace$(s, " para toda ", "@")
        s = Replace$(s, " & toda ", "@")
        s = Replace$(s, " para cualquier ", "@")
        s = Replace$(s, " para cualesquier ", "@")

        If s Like "si * entonces *" Then
            'separar el antecdente del consecuente
            s = Trim(Mid$(s, 4))
            s = Replace(s, " entonces ", " -> ")
        End If



        'Do
        '    s = Replace$(s, " ", "") 'quitar espacios
        'Loop Until s = Replace$(s, " ", "")

        s = Replace$(s, ")(", ")*(")

        'r = Replace$(r, "-", "+-") 'signo menos
        'r = Replace$(r, "*+-", "*-") 'signo menos
        'r = Replace$(r, "/+-", "/-") 'signo menos
        'r = Replace$(r, "++", "+")

        'r = Replace$(r, "/", "*/") 'signo entre

    End Sub

    'adapta una cadena para ser mostrada
    Function Mostrar(s As String)
        Dim r As String
        r = Replace$(s, " ", "") 'quitar espacios

        r = Replace$(r, "+", " + ") 'signo más
        r = Replace$(r, "*", " * ")

        r = Replace$(r, "/", " / ") 'signo entre
        Mostrar = r
    End Function

    'produce un arreglo que contiene los operandos de una cadena.
    Function PSplit(ByVal s As String, ByRef separador As String) As String()
        Dim n, k As Long
        Dim s1 As String

        If Not Len(separador) = 1 Then
            s = Replace(s, separador, "#")

            k = 0
            'sustituir todos los operadores que no se hallen entre paréntesis por "$"
            For n = 1 To Len(s)

                s1 = Mid$(s, n, 1)
                If s1 = "(" Then k = k + 1
                If s1 = ")" Then k = k - 1
                If s1 = "#" And k = 0 Then Mid$(s, n, 1) = "$"

            Next n

            s = Replace(s, "#", separador)
        Else
            k = 0
            'sustituir todos los operadores que no se hallen entre paréntesis por "$"
            For n = 1 To Len(s)

                s1 = Mid$(s, n, 1)
                If s1 = "(" Then k = k + 1
                If s1 = ")" Then k = k - 1
                If s1 = separador And k = 0 Then Mid$(s, n, 1) = "$"

            Next n
        End If

        PSplit = Split(s, "$")
    End Function

    'debuelve true si hay operandos no anidados
    Function OperNoAnid(s As String, operador As String) As Boolean
        Dim n, k As Long
        Dim s1 As String
        Dim resp As Boolean
        resp = False
        k = 0

        If Not Len(operador) = 1 Then
            s = Replace(s, operador, "$")
            'sustituir todos los operadores por "$"
            For n = 1 To Len(s)

                s1 = Mid$(s, n, 1)
                If s1 = "(" Then k = k + 1
                If s1 = ")" Then k = k - 1
                If s1 = "$" And k = 0 Then
                    resp = True
                    Exit For
                End If
            Next n
        Else

            For n = 1 To Len(s)

                s1 = Mid$(s, n, 1)
                If s1 = "(" Then k = k + 1
                If s1 = ")" Then k = k - 1
                If s1 = operador And k = 0 Then
                    resp = True
                    Exit For
                End If
            Next n

        End If

        Return resp
    End Function


    'revisa el orden de parénteis de una fórmula
    Function Parentesis(s As String) As Boolean
        Dim s1 As String
        Dim res As Boolean
        Dim n, k As Long
        k = 0

        If s = "" Then
            Parentesis = False
            Exit Function
        End If

        For n = 1 To Len(s)
            s1 = Mid(s, n, 1)
            If s1 = "(" Then k = k + 1
            If s1 = ")" Then k = k - 1
            If k < 0 Then
                Parentesis = False
                Exit Function
            End If
        Next n

        If k = 0 Then
            res = True
        Else
            res = False
        End If

        Parentesis = res
    End Function

    'omite paréntesis inecesarios
    Function POmitir(ByVal s As String) As String
        Dim res As String
        res = s
        'omite paréntesis exremos
        Do While s Like "(*)"
            s = Mid$(s, 2, Len(s) - 2)
            If Parentesis(s) Then res = s
        Loop

        POmitir = res
    End Function



    'Analiza una cadena sin operandos y la interpreta como una fórmula.
    Function GF(s As String, x As Double) As Double

        Dim r As Double
        Dim l As Long
        l = Len(s)


        'función identidad
        If s = "x" Then
            GF = x
            Exit Function
        ElseIf s = "e" Then
            GF = EULER
            Exit Function
        End If




        '--------------------------------------------------
        'funciones con exponente

        If s Like "*²" Then

            r = SS(Mid$(s, 1, l - 1), x) * SS(Mid$(s, 1, l - 1), x)
            '--------------------------------------------------
        ElseIf s Like "*³" Then

            r = SS(Mid$(s, 1, l - 1), x) * SS(Mid$(s, 1, l - 1), x) * SS(Mid$(s, 1, l - 1), x)
            '--------------------------------------------------
        ElseIf s Like "*½" Then

            r = Math.Sqrt(SS(Mid$(s, 1, l - 1), x))
            '--------------------------------------------------
        ElseIf s Like "-*" Then 'signo menos

            r = -SS(Mid$(s, 2), x)

        ElseIf s Like "(*)" Then 'signo menos

            r = SS(Mid$(s, 2, l - 2), x)

            'funciones trigonométricas

        ElseIf s Like "sen(*)" Then

            r = Sin(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "cos(*)" Then

            r = Cos(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "tan(*)" Then

            r = Tan(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "cot(*)" Then

            r = 1 / Tan(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "csc(*)" Then

            r = 1 / Sin(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "sec(*)" Then

            r = 1 / Cos(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
            'trigonométricas inversas

        ElseIf s Like "arctan(*)" Then

            r = Math.Atan(SS(Mid$(s, 8, l - 8), x))
            '--------------------------------------------------
        ElseIf s Like "arcsen(*)" Then

            r = arcSin(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "arccos(*)" Then

            r = (PI / 2) - arcSin(SS(Mid$(s, 8, l - 8), x))
            '--------------------------------------------------
        ElseIf s Like "arcsec(*)" Then

            r = arcsec(SS(Mid$(s, 8, l - 8), x))
            '--------------------------------------------------
        ElseIf s Like "arccsc(*)" Then

            r = arccsc(SS(Mid$(s, 8, l - 8), x))
            '--------------------------------------------------
        ElseIf s Like "arccot(*)" Then

            r = arccot(SS(Mid$(s, 8, l - 8), x))
            '--------------------------------------------------
            'trigonométricas hiperbólicas

        ElseIf s Like "senh(*)" Then

            r = senh(SS(Mid$(s, 6, l - 6), x))
            '--------------------------------------------------
        ElseIf s Like "cosh(*)" Then

            r = cosh(SS(Mid$(s, 6, l - 6), x))
            '--------------------------------------------------
        ElseIf s Like "tanh(*)" Then

            r = tanh(SS(Mid$(s, 6, l - 6), x))
            '--------------------------------------------------
        ElseIf s Like "coth(*)" Then

            r = coth(SS(Mid$(s, 6, l - 6), x))
            '--------------------------------------------------
        ElseIf s Like "sech(*)" Then

            r = sech(SS(Mid$(s, 6, l - 6), x))
            '--------------------------------------------------
        ElseIf s Like "csch(*)" Then

            r = csch(SS(Mid$(s, 6, l - 6), x))
            '--------------------------------------------------
            'hiperbólicas inversas
        ElseIf s Like "argsenh(*)" Then

            r = argsenh(SS(Mid$(s, 9, l - 9), x))
            '--------------------------------------------------
        ElseIf s Like "argcosh(*)" Then

            r = argcosh(SS(Mid$(s, 9, l - 9), x))
            '--------------------------------------------------

            'funciones logarítmicas y exponenciales

        ElseIf s Like "log(*)" Then

            r = ln((SS(Mid$(s, 5, l - 5), x)))
            '--------------------------------------------------
        ElseIf s Like "exp(*)" Then

            r = Exp(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "log10(*)" Then

            r = Log(SS(Mid$(s, 7, l - 7), x)) / 2.30258509299405
            '--------------------------------------------------
        ElseIf s Like "log2(*)" Then

            r = Log(SS(Mid$(s, 5, l - 5), x)) / 0.693147180559945
            '--------------------------------------------------
        ElseIf s Like "*!" Then

            r = Fac(SS(Mid$(s, 1, l - 1), x))
            '--------------------------------------------------
        ElseIf s Like "abs(*)" Then

            r = Abs(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "sgn(*)" Then

            r = Sign(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "sqrt(*)" Then

            r = Sqrt(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "fix(*)" Then

            r = Fix(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "int(*)" Then

            r = Int(SS(Mid$(s, 5, l - 5), x))

            '--------------------------------------------------
        ElseIf s Like "p(*,*)" Then

            'r = Per(x)

            '--------------------------------------------------
        ElseIf s Like "c(*,*)" Then

            'r = Comb(x, k)
            '--------------------------------------------------
            'funciones de conversión
        ElseIf s Like "deg(*)" Then

            r = Deg(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------
        ElseIf s Like "rad(*)" Then

            r = Rad(SS(Mid$(s, 5, l - 5), x))
            '--------------------------------------------------

        ElseIf s Like "d(*)" Then

            r = DerivadaNum(Mid$(s, 3, l - 3), x, 0.000001)
            '--------------------------------------------------

            'funciones auxiliares
        ElseIf s Like "g(*)" Then

            r = SS(funcAuxiliar(0), SS(Mid$(s, 3, l - 3), x))
            '--------------------------------------------------
        ElseIf s Like "h(*)" Then

            r = SS(funcAuxiliar(1), SS(Mid$(s, 3, l - 3), x))
        Else
            r = Val(s)
        End If
        '--------------------------------------------------



        GF = r

    End Function

    Function SS(ByVal s As String, x As Double) As Double
        Dim res As Double
        Dim i As Long
        Dim A() As String

        's = POmitir(s)

        If s = "x" Then
            SS = x
            Exit Function
        End If

        If OperNoAnid(s, "+") Then

            res = 0
            A = PSplit(s, "+")


            For i = LBound(A) To UBound(A)
                res = res + SS(A(i), x)
            Next i

        ElseIf OperNoAnid(s, "*") Then

            res = 1
            A = PSplit(s, "*")


            For i = LBound(A) To UBound(A)

                If A(i) Like "/*" Then
                    res = res / SS(Mid$(A(i), 2), x)
                Else
                    res = res * SS(A(i), x)
                End If
            Next i




        ElseIf OperNoAnid(s, "^") Then
            Dim r1 As Double

            A = PSplit(s, "^")

            r1 = SS(A(1), x)
            'If Sgn(r1) = 1 Then
            res = SS(A(0), x) ^ r1
            'Else
            'res = 1 / (SS(A(0), x) ^ SS(A(1), x))
            'End If

            '   If UBound(A) = 1 Then
            'Pot = expo ^ SS(A(1), x)
            '  Else

            '     For n = UBound(A) To LBound(A) + 1

            '    expo = SS(A(n), x) ^ expo

            '   Next n

        Else
            res = GF(s, x)

        End If

        SS = res

    End Function


    'Devuelve el resulado de la operación n-aria indicada
    Function Operar(s As String, s1 As String, x As Double) As Double
        Dim A() As String
        Dim res, r1 As Double
        Dim i As Long

        '-------------------------------------------------------------------
        If s1 = "+" Then        'suma de términos

            A = PSplit(s, "+")


            For i = LBound(A) To UBound(A)
                res = res + SS(A(i), x)
            Next i

            '-------------------------------------------------------------------
        ElseIf s1 = "*" Then    'multiplicación de factores
            res = 1
            A = PSplit(s, "*")


            For i = LBound(A) To UBound(A)

                If A(i) Like "/*" Then
                    res = res / SS(Mid$(A(i), 2), x)
                Else
                    res = res * SS(A(i), x)
                End If
            Next i

            '-------------------------------------------------------------------
        ElseIf s1 = "^" Then        'eleva a la r-potencia
            A = PSplit(s, "^")

            r1 = SS(A(1), x)
            'If Sgn(r1) = 1 Then
            res = SS(A(0), x) ^ SS(A(1), x)
            'Else
            'res = 1 / (SS(A(0), x) ^ SS(A(1), x))
            'End If

            '   If UBound(A) = 1 Then
            'Pot = expo ^ SS(A(1), x)
            '  Else

            '     For n = UBound(A) To LBound(A) + 1

            '    expo = SS(A(n), x) ^ expo

            '   Next n
            '-------------------------------------------------------------------

        End If
        Operar = res

    End Function


    'series y sucesiones

    'calcula la suma de una serie
    Function Sumatoria(LimInf As Long, LimSup As Long,
 Expresion As String) As Double

        Dim n, k, i As Long
        Dim res As Double
        res = 0

        For i = LimInf To LimSup
            res = res + SS(Expresion, CDbl(i))
        Next i

        Sumatoria = res
    End Function

    'calcula un producto-serie
    Function Producto(LimInf As Long, LimSup As Long,
Skip As Long, Expresion As String, x As Double) As Double

        Dim n, k, i As Long
        Dim res As Double
        res = 1

        For i = LimInf To LimSup
            res = res * SS(Expresion, CDbl(i))
        Next i
        Producto = res

    End Function

    'calculo numérico
    Function DerivadaNum(formula As String, x As Double, dx As Double) As Double
        Dim dx2 As Double
        dx2 = dx / 2
        DerivadaNum = (SS(formula, x + dx2) - SS(formula, x - dx2)) / dx

    End Function

#End Region

#Region "FUNCIONES VECTORIALES"

    '##############################################################

    'FUNCIONES VECTORIALES

    '##############################################################

    'Devuelve el producto interno de dos vectores
    Function ProdIn(x() As Double, y() As Double) As Double
        Dim d As Integer
        Dim r As Double
        Dim i As Long
        If UBound(x) = UBound(y) Then
            d = UBound(x)
            r = 0

            For i = 1 To d
                r = r + x(i) * y(i)
            Next i

            ProdIn = r
        Else
            MsgBox("Los vectores no tienen la misma dimensión")
        End If
    End Function


    'Devuelve el covector de x y y.
    Function Covector(x() As Double, y() As Double) As Double()
        'Dim d As Integer
        'Dim i As Long
        'd = 3
        'Dim resultado(1 To 3)

        'For i = 1 To d
        '    resultado(i) = x(i) * y(i)
        'Next i

        'Covector = resultado
    End Function

    'Devuelve un array numérico a partir de una cadena
    'El separador es ","
    Function ArrNum(cad As String) As Double()
        'Dim A() As String
        'Dim i As Long

        'A = Split(cad, ",")
        'Dim Res() As Double
        'ReDim Res(LBound(A) To UBound(A))

        'For i = LBound(A) To UBound(A)
        '    Res(i) = A(i)
        'Next i
        'ArrNum = Res
    End Function

    'Multiplica las componentes de un vector por un número.
    Function ProdPorEscalar(x() As Double, y As Double) As Double()
        'Dim i As Long
        'Dim r() As Double
        'ReDim r(LBound(x) To UBound(x))

        'For i = LBound(x) To UBound(x)
        '    r(i) = x(i) * y
        'Next i
        'ProdPorEscalar = r
    End Function

    ' Función polimórfica que suma los valores contenidos en cualquier array
    Function SumaArray(Arr As Object) As Object
        Dim i As Long, resultado As Object
        For i = LBound(Arr) To UBound(Arr)
            resultado = resultado + Arr(i)
        Next
        SumaArray = resultado
    End Function

#End Region

#Region "FUNCIOINES QUE MANEJAN ARRAYS Y MATRICES"


    '#######################################################################

    'FUNCIONES QUE MANEJAN ARRAYS Y MATRICES

    '#######################################################################

    'Proporciona un array con valores de tipo Double
    Function ArrDbl(x()) As Double()
        Dim i As Long
        Dim resul() As Double
        ' ReDim resul(LBound(x) To UBound(x))

        For i = LBound(x) To UBound(x)
            resul(i) = CDbl(x(i))
        Next i
        ArrDbl = resul
    End Function


    'Proporciona un array con valores de tipo String
    Function ArrStr(x()) As String()
        Dim i As Long
        Dim resul() As Double
        'ReDim resul(LBound(x) To UBound(x)) 
        For i = LBound(x) To UBound(x)
            resul(i) = CStr(x(i))
        Next i
        'ArrStr = resul
    End Function

    ' Esta rutina obtiene el número de dimensiones de un array
    ' pasado como argumento, ó 0 si no es un array.
    Function NumeroDeDim(Arr As Object) As Integer
        Dim dims As Integer, postizo As Long
        On Error Resume Next
        Do
            postizo = UBound(Arr, dims + 1)
            '     If Err() Then Exit Do
            dims = dims + 1
        Loop
        NumeroDeDim = dims
    End Function
    ' función suma polimórfica que también trabaja con matrices bidimensionales

    Function SumaArray2(Arr As Object) As Object
        Dim i As Long, j As Long, resultado As Object
        ' En primer lugar, verificamos si podemos trabajar con este array.
        Select Case NumeroDeDim(Arr) 'decía NumeroDeDims(Arr)
            Case 1  ' array unidimensional
                For i = LBound(Arr) To UBound(Arr)
                    resultado = resultado + Arr(i)
                Next
            Case 2  ' matriz bidimensional
                For i = LBound(Arr) To UBound(Arr)
                    For j = LBound(Arr, 2) To UBound(Arr, 2)
                        resultado = resultado + Arr(i, j)
                    Next
                Next
            Case Else  'No es un array o tiene demasiadas dimensiones
                Err.Raise(1001, , "No es un array o tiene más de dos dimensiones")
        End Select
        SumaArray2 = resultado
    End Function

#End Region

#Region "PROCESAMIENTO DE CADENAS DE CARACTERES"


    '#############################################################################

    'FUNCIONES QUE PROCESAN CADENAS DE CARACTERES

    '#############################################################################

    ' rutina optimizada que cuenta los espacios contenidos en una cadena
    ' NOTA: esta función no funciona con alfabetos no latinos.

    Function ContarEspacios(Texto As String) As Long
        Dim B() As Byte, i As Long, resultado As Long
        'B = Texto
        For i = 0 To UBound(B) Step 2
            ' Considerar sólo elementos impares
            ' Se ahora tiempo y código utilizando el nombre
            ' de la función como una variable local
            If B(i) = 32 Then resultado = resultado + 1
        Next
        ContarEspacios = resultado
    End Function

    ' rutina optimizada que cuenta el número de repeticiones de un caracter dado
    ' contenido en una cadena.
    ' NOTA: esta función no funciona con alfabetos no latinos.

    Function ContarCaracter(Texto As String, caracter As Integer) As Long
        Dim B() As Byte, i As Long, resultado As Long
        '    B() = Texto
        For i = 0 To UBound(B)
            ' Considerar sólo elementos impares
            ' Se ahora tiempo y código utilizando el nombre
            ' de la función como una variable local
            If B(i) = caracter Then resultado = resultado + 1
        Next
        ContarCaracter = resultado
    End Function

    'intercambia en una cadena dos subcadenas
    Function Intercambiar(ByVal s As String, cadena1 As String, cadena2 As String) As String
        s = Replace$(s, cadena1, "$")
        s = Replace$(s, cadena2, cadena1)
        s = Replace$(s, "$", cadena2)
        Intercambiar = s
    End Function


    ' rutina que proporciona la frecuencia relativa de un caracter en una cadena
    ' Si la cadena es vacía devuelve -1.
    Function FrecCarac(Texto As String, caract As Integer) As Double
        If Texto > "" Then
            FrecCarac = ContarCaracter(Texto, caract) / Len(Texto)
        Else
            FrecCarac = -1
        End If

    End Function

#End Region

#Region "TEORÍA DE CONJUNTOS Y LÓGICA FORMAL"

    '##############################################################################

    'TEORÍA DE CONJUNTOS (COLECCIONES) Y LÓGICA FORMAL

    '##############################################################################

    ' proporciona True si existe realmente el elemento de la colección

    Function ExisteElem(col As Collection, Clave As String) As Boolean
        Dim prueba As Object
        On Error Resume Next
        prueba = col.Item(Clave)
        '  ExisteElem = (Err() = False)
    End Function

    ' Eliminar todos los elementos en una colección

    Sub EliminarTodosElementos(col As Collection)
        Do While col.Count
            col.Remove(1)
        Loop
    End Sub


    'SISTEMAS NUMÉRICOS

    'devuelve un array con unos y ceros correspondientes al binario de n
    Function BinarArr(ByVal n As Long, k As Long) As Long()
        Dim A() As Long
        ReDim A(k)

        Dim i As Long
        i = 0

        Do While i <= k
            A(i) = n Mod 2
            n = n \ 2
            i = i + 1
            'If i > 1000 Then Exit Do
        Loop

        BinarArr = A
    End Function

#End Region

#Region "PROCESAMIENTO DE EXPRESIONES"
    Function Elementos(s As String) As String() 'devuelve un arreglo con todas los elementos (variables, constantes, funciones, relatores, etc) de una expresión
        'NOTA: es importante mantener actualzada esta función en razón de los operadores de FF y GG
        'eliminar operadores
        s = Replace$(s, "@", " ")
        s = Replace$(s, "<->", " ")
        s = Replace$(s, "->", " ")
        s = Replace$(s, " en ", " ")
        s = Replace$(s, " or ", " ")
        s = Replace$(s, "&", " ")
        s = Replace$(s, "+", " ")
        s = Replace$(s, "-", " ")
        s = Replace$(s, "*", " ")
        s = Replace$(s, "/", " ")
        s = Replace$(s, "=", " ")
        s = Replace$(s, "<", " ")
        s = Replace$(s, ">", " ")
        s = Replace$(s, ">", " ")
        s = Replace$(s, "no(", " ")
        s = Replace$(s, "(", " ")
        s = Replace$(s, ")", " ")

        s = Replace$(s, "  ", " ")

        Return Split(s, " ")
    End Function

    'Transforma una expresión NOVA math en una forma canónica
    Function FF(s As String, Optional ToLatex As Boolean = False, Optional Categoria_Esperada As String = "") As String
        Dim Res As String
        Dim i As Long
        Dim A() As String

        s = POmitir(s)

        If OperNoAnid(s, "@") Then 'cuantificador universal '************************************

            Res = ""
            A = PSplit(s, "@")

            A(0) = FF(Trim(A(0)), ToLatex, "P")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            If ToLatex Then
                Res = "(\forall " & A(1) & " " & A(0) & ")"
            Else
                Dim x() As String
                ReDim x(UBound(A) - 1)

                For i = 1 To UBound(A)
                    x(i - 1) = A(i) & " en U"
                Next

                Res = "(" & Join(x, " & ") & "->" & A(0) & ")"
            End If

        ElseIf OperNoAnid(s, ",") Then '************************************

            Res = ""
            A = PSplit(s, ",")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "")
            Next i

            If ToLatex Then
                Res = "(" & Join(A, ", ") & ")"
            Else
                Res = "(" & Join(A, ",") & ")"
            End If

        ElseIf OperNoAnid(s, "->") Then '************************************

            Res = ""
            A = PSplit(s, "->")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "P")
            Next i

            If ToLatex Then
                Res = "(" & Join(A, "\rightarrow ") & ")"
            Else
                Res = "(" & Join(A, " -> ") & ")"
            End If

        ElseIf OperNoAnid(s, " or ") Then 'or (disyunción) '************************************

            Res = ""
            A = PSplit(s, " or ")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "P")
            Next i

            If ToLatex Then
                Res = "(" & Join(A, "\vee ") & ")"
            Else
                Res = "(" & Join(A, " or ") & ")"
            End If

        ElseIf OperNoAnid(s, "&") Then '************************************

            Res = ""
            A = PSplit(s, "&")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "P")
            Next i

            If ToLatex Then
                Res = "(" & Join(A, "\wedge ") & ")"
            Else
                Res = "(" & Join(A, "&") & ")"
            End If

        ElseIf OperNoAnid(s, " en ") Then '************************************

            Res = ""
            A = PSplit(s, " en ")

            A(0) = FF(Trim(A(0)), ToLatex, "s")

            For i = LBound(A) + 1 To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "sC")   'sC categoría s de conjunto
            Next i

            If ToLatex Then
                Res = "(" & Join(A, "\in ") & ")"
            Else
                Res = "(" & Join(A, " en ") & ")"
            End If

        ElseIf OperNoAnid(s, "=") Then '************************************

            Res = ""
            A = PSplit(s, "=")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            Res = "(" & Join(A, "=") & ")"

        ElseIf OperNoAnid(s, "<") Then '************************************

            Res = ""
            A = PSplit(s, "<")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            Res = "(" & Join(A, "<") & ")"

        ElseIf OperNoAnid(s, ">") Then '************************************

            Res = ""
            A = PSplit(s, ">")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            Res = "(" & Join(A, ">") & ")"

        ElseIf OperNoAnid(s, "+") Then '************************************

            Res = ""
            A = PSplit(s, "+")

            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            Res = "(" & Join(A, "+") & ")"

        ElseIf OperNoAnid(s, "-") Then '************************************

            Res = ""
            A = PSplit(s, "-")


            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i


            Res = "(" & Join(A, "-") & ")"


        ElseIf OperNoAnid(s, "*") Then '************************************

            Res = ""
            A = PSplit(s, "*")


            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            If ToLatex Then
                Res = "(" & Join(A, " ") & ")"
            Else
                Res = "(" & Join(A, "*") & ")"
            End If


        ElseIf OperNoAnid(s, "/") Then '************************************

            Res = ""
            A = PSplit(s, "/")


            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i

            If ToLatex Then
                Res = "\frac{" & A(0) & "}{" & A(1) & "}"
            Else
                Res = "(" & Join(A, "/") & ")"
            End If



        ElseIf OperNoAnid(s, "^") Then '************************************

            Res = ""
            A = PSplit(s, "^")


            For i = LBound(A) To UBound(A)
                A(i) = FF(Trim(A(i)), ToLatex, "s")
            Next i


            If ToLatex Then
                Res = A(0) & "^{" & A(1) & "}"
            Else
                Res = "(" & Join(A, "^") & ")"
            End If

        Else
            Res = GG(s, ToLatex, Categoria_Esperada)

        End If

        Return Res
    End Function

    'tranforma una forma canónica en expresión latex
    Function GG(s As String, Optional toLatex As Boolean = False, Optional Categoria_Esperada As String = "") As String
        Dim r As String
        ' s = Trim(s)

        'buscar variable
        'buscar constante
        'buscar fuctor
        'buscar relator monádico
        'buscar conjuntor monádico
        If s Like "no(*)" Then
            r = Mid(s, Len("no("), Len(s) - 1)
            If toLatex Then
                r = "\neg(" & POmitir(FF(r, toLatex, "P")) & ")"
            Else
                r = "no(" & POmitir(FF(r, toLatex, "P")) & ")"
            End If

            Return r
            Exit Function
        End If


        If s Like "*(*)" Then 'es functor o relator
            Dim j() = Split(s, "(")

            If toLatex Then
                r = j(0)
                Return r
                Exit Function
            End If

            Dim f = conceptosMat.Find(Function(p) p.nombreID = j(0))

            If Not f Is Nothing Then
                'corresponde con la categoría esperada?
                If f.categoria = Categoria_Esperada Then 'sí
                    r = Mid(s, Len(j(0) & "("), Len(s) - 1)
                    r = j(0) & "(" & POmitir(FF(r, toLatex, "s")) & ")"

                Else 'no
                    Console.Write("La función " & j(0) & " no es de la categoría correcta. ")
                    If Categoria_Esperada = "s" Then
                        Console.WriteLine("Se esperaba un término")
                    ElseIf Categoria_Esperada = "P" Then
                        Console.WriteLine("Se esperaba una sentencia")
                    ElseIf Categoria_Esperada = "sC" Then
                        Console.WriteLine("Se esperaba un conjunto")
                    End If

                    r = "?"
                End If
            Else
                'si no contiene espacios agregar como función
                If j(0) = Replace(j(0), " ", "") Then

                    If Categoria_Esperada = "P" Then 'se espera una sentencia
                        Dim c As New concepto.simple
                        c.nombreID = j(0)
                        c.categoria = "P"
                        c.tipo = "c"
                        conceptosMat.Add(c)
                        Console.WriteLine("Se ha agregado la función sentencial " & j(0))
                        r = Mid(s, Len(j(0) & "("), Len(s) - 1)
                        r = j(0) & "(" & POmitir(FF(r, toLatex, "s")) & ")"

                    ElseIf Categoria_Esperada = "s" Then 'se espera un término
                        Dim c As New concepto.simple
                        c.nombreID = j(0)
                        c.categoria = "s"
                        c.tipo = "c"
                        conceptosMat.Add(c)
                        Console.WriteLine("Se ha agregado la función " & j(0))
                        r = Mid(s, Len(j(0) & "("), Len(s) - 1)
                        r = j(0) & "(" & POmitir(FF(r, toLatex, "s")) & ")"

                    ElseIf Categoria_Esperada = "sC" Then 'se espera un conjunto
                        Dim c As New concepto.simple
                        c.nombreID = j(0)
                        c.categoria = "sC"
                        c.tipo = "c"
                        conceptosMat.Add(c)
                        Console.WriteLine("Se ha agregado la función de conjunto " & j(0))
                        r = Mid(s, Len(j(0) & "("), Len(s) - 1)
                        r = j(0) & "(" & POmitir(FF(r, toLatex, "s")) & ")"
                    Else
                        Console.WriteLine("No se ha entendido la expresión " & j(0))
                        r = "?"
                    End If

                Else '(si contiene espacios tal vez sea una sentencias simple)
                    Console.WriteLine("No se ha entendido la expresión " & s)
                    r = "?"
                End If

            End If
            Return r
            Exit Function
        End If

        If toLatex Then
            Return s
            Exit Function
        End If

        'buscar concepto
        Dim t = conceptosMat.Find(Function(p) p.nombreID = s)

        If Not t Is Nothing Then
            'corresponde con la categoría esperada?
            If t.categoria = Categoria_Esperada Then 'sí
                r = s
            Else 'no
                Console.Write("El símbolo " & s & " no es de la categoría correcta. ")
                If Categoria_Esperada = "s" Then
                    Console.WriteLine("Se esperaba un término")
                ElseIf Categoria_Esperada = "P" Then
                    Console.WriteLine("Se esperaba una sentencia")
                ElseIf Categoria_Esperada = "sC" Then
                    Console.WriteLine("Se esperaba un conjunto")
                End If

                r = "?"
            End If

        Else 'nada fue encontrado
            'si no contiene espacios agregar como vairable
            If s = Replace(s, " ", "") Then

                If Categoria_Esperada = "P" Then 'se espera una sentencia
                    Dim c As New concepto.simple
                    c.nombreID = s
                    c.categoria = "P"
                    c.tipo = "c"
                    conceptosMat.Add(c)
                    Console.WriteLine("Se ha agregado la variable sentencial " & s)
                    r = s
                ElseIf Categoria_Esperada = "s" Then 'se espera un término
                    Dim c As New concepto.simple
                    c.nombreID = s
                    c.categoria = "s"
                    c.tipo = "c"
                    conceptosMat.Add(c)
                    Console.WriteLine("Se ha agregado la variable " & s)
                    r = s
                ElseIf Categoria_Esperada = "sC" Then 'se espera un conjunto
                    Dim c As New concepto.simple
                    c.nombreID = s
                    c.categoria = "sC"
                    c.tipo = "c"
                    conceptosMat.Add(c)
                    Console.WriteLine("Se ha agregado el conjunto " & s)
                    r = s
                Else
                    Console.WriteLine("No se ha entendido la expresión " & s)
                    r = "?"
                End If

            Else '(si contiene espacios tal vez sea una sentencias simple)
                Console.WriteLine("No se ha entendido la expresión " & s)
                r = "?"
            End If


        End If



        Return r
    End Function

#End Region

End Module


